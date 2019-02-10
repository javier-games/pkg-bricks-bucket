//  Based on the following projects:
//  The Grain Baker Shader on PostProcessing repo from Unity-Technologies.
//  https://github.com/Unity-Technologies/PostProcessing
//  The WhiteNoise gist of josephbk117.
//  https://gist.github.com/josephbk117/884900c161705738d1b2fc4be38c5910
//  The AnalogGlitch on KinoGlitch repo from Keijiro.
//  https://github.com/keijiro/KinoGlitch


Shader "Hidden/JGFramework/Camera/AnalogGlitch"{
    Properties{
        _MainTex ("Albedo", 2D) = "white"  {}
        
        [Header(Grain Noise)]
        _Strength("Strength", Range(-1,1)) = 0
        _Scale("Scale", float) = 100
        _Phase("Phase", float) = 0
        _NoiseParameters("Noise", Vector) = (0,0,0,0)
        
        [Header(Positional Glitch)]
        _Jump("Vertical Jump", Float) = 0
        _Shake("Horizontal Shake", Float) = 0
        _Drift("Color Drift", Float) = 0
        
        [Header(Jitter)]
        _Displacement ("Displacement", Range(0,1)) = 0
        _Threshold ("Threshold", Range(0,1)) = 0
        
        [Header(Jitter Functions)]
        _Sinc("Sinc Function", Range(0,1)) = 0
        _Sigmoid("Sigmoid Function", Range(0,1)) = 0
        
        _Amplitude("Amplitude", Float) = 0
        _Frequency("Frequency", Float) = 0
        _Offset("Offset", Vector) = (0,0,0,0)
    }
    
    
    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _MainTex;
    float4 _MainTex_ST;
    
    float _Strength;            //  Strength of noise.
    float _Scale;               //  Scale of noise.
    float4 _NoiseParameters;    //  Parameters for noise.
    
    float _Jump;                //  Horizontal jump.
    float _JumpTime;            //  Jump timer.
    float _Shake;               //  Amount of shake.
    float _Drift;               //  Amount of drift.
    float _DriftTime;           //  Drift timer.
    
    float _Displacement;        //  Jitter Displacement.
    float _Threshold;           //  Jitter Threshold.
    
    float _Sinc;                //  Sinc enable value.
    float _Sigmoid;             //  Sigmoid enable value.
    float _Amplitude;           //  Amplitud of functions.
    float _Frequency;           //  Frequency of functions.
    float4 _Offset;             //  Offset of functions.
    
    struct appdata {            
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        float2 uv : TEXCOORD0;
    };
    
    struct v2f {
        float4 pos : SV_POSITION;
        half2 uv   : TEXCOORD0; 
        
        float3 normal : NORMAL;
        float3 noise_uv : TEXCOORD1;
    };
    
    
    //  Canonical 2D random function.
    float random(float x, float y) {
        float2 p = float2(x, y + x);
        return frac(sin(dot(p.xy,float2(12.9898, 78.233))) * 43758.5453);
    }
    
    //  Variation of canonical function.
    float Noise(float2 n, float x) {
        n += x;
        return frac(sin(dot(n.xy, _NoiseParameters.xy)) * _NoiseParameters.z);
    }
    
    //  Step one for noise.
    float Step1(float2 uv, float n) {
        float b = 2.0, c = -12.0;
        return (1.0 / (4.0 + b * 4.0 + abs(c))) * (
            Noise(uv + float2(-1.0, -1.0), n) +
            Noise(uv + float2( 0.0, -1.0), n) * b +
            Noise(uv + float2( 1.0, -1.0), n) +
            Noise(uv + float2(-1.0,  0.0), n) * b +
            Noise(uv + float2( 0.0,  0.0), n) * c +
            Noise(uv + float2( 1.0,  0.0), n) * b +
            Noise(uv + float2(-1.0,  1.0), n) +
            Noise(uv + float2( 0.0,  1.0), n) * b +
            Noise(uv + float2( 1.0,  1.0), n)
        );
    }
    
    //  Step two for noise.
    float Step2(float2 uv, float n) {
        float b = 2.0, c = 4.0;
        return (1.0 / (4.0 + b * 4.0 + abs(c))) * (
            Step1(uv + float2(-1.0, -1.0), n) +
            Step1(uv + float2( 0.0, -1.0), n) * b +
            Step1(uv + float2( 1.0, -1.0), n) +
            Step1(uv + float2(-1.0,  0.0), n) * b +
            Step1(uv + float2( 0.0,  0.0), n) * c +
            Step1(uv + float2( 1.0,  0.0), n) * b +
            Step1(uv + float2(-1.0,  1.0), n) +
            Step1(uv + float2( 0.0,  1.0), n) * b +
            Step1(uv + float2( 1.0,  1.0), n)
        );
    }
    
    //  Step three for noise.
    float3 Step3(float2 uv) {
        float a = Step2(uv, 0.07 * frac(0));
        float b = Step2(uv, 0.11 * frac(0));
        float c = Step2(uv, 0.13 * frac(0));
        return float3(a, b, c);
    }
    
    //  Cardinal sine function.
    float sinc(float v) {
        float pi = 3.1416;
        float denominator = _Frequency * pi * v + _Offset.y;
        float numerator = sin(denominator);
        return _Offset.x + _Amplitude * numerator/denominator;
    }
    
    // Logistic function sigmoid curve.
    float sigmoid(float v) {
        return _Offset.x + _Amplitude/(1 + exp(-_Frequency * (v - _Offset.y)));
    }
    
    //  Pragma Vertex
    v2f vert (appdata v) {
        v2f o;
        
        o.normal = v.normal;
        o.pos = UnityObjectToClipPos(v.vertex);
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        o.noise_uv = v.vertex.xyz/v.vertex.w;

        return o;
    }
    
    //  Pragma Fragment
    half4 frag(v2f i) : SV_Target {
        float u = i.uv.x;
        float v = i.uv.y;
        float2 uv = i.uv;
        
        //  Jitter
        float jitter = random(v, _Time.x) * 2 - 1;
        jitter *= step(_Threshold, abs(jitter)) * _Displacement;
        jitter = _Sinc * sinc(v) * jitter + (1 - _Sinc) * jitter;
        jitter = _Sigmoid * sigmoid(v) * jitter + (1 - _Sigmoid) * jitter;
        
        //  Vertical Jump
        float jump = lerp(v, frac(v + _JumpTime), _Jump.x);
        
        //  Horizontal Shake
        float shake = (random(_Time.x, 2) - 0.5) * _Shake;
        
        //  Color Drift
        float drift = sin(jump + _DriftTime) * _Drift;
        
        //  Sources
        float src = u + jitter + shake;
        half4 src1 = tex2D(_MainTex, frac(float2(src, jump)));
        half4 src2 = tex2D(_MainTex, frac(float2(src + drift, jump)));
        
        //  Grain
        float2 uvScaled = floor(uv * _Scale);
        float rand = random(uvScaled.x * _Time.x, uvScaled.y * _Time.x);
        fixed3 grain = Step3(uv.xy * _Time.xy * rand);
        
        return half4(src1.r, src2.g, src1.b, 1) + float4(grain, 1) * _Strength;
    }

    ENDCG
    SubShader {
        Pass {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            ENDCG
        }
    }
}