Shader "Hidden/JGFramework/Camera/AnalogGlitch"
{
    Properties
    {
        _MainTex ("Albedo", 2D) = "white"  {}
        
        [Space]
        [Header(Grain Noise)]
        _Strength("Strength", Range(-1,1)) = 0
        
        [Space]
        [Header(Positional Glitch)]
        _Jump("Vertical Jump", Float) = 0
        _Shake("Horizontal Shake", Float) = 0
        _Drift("Color Drift", Float) = 0
        
        [Space]
        [Header(Jitter)]
        _Displacement ("Displacement", Range(0,1)) = 0
        _Threshold ("Threshold", Range(0,1)) = 0
        
        [Space]
        [Header(Jitter Functions)]
        _Sinc("Sinc Function", Range(0,1)) = 0
        _Sigmoid("Sigmoid Function", Range(0,1)) = 0
        
        [Space]
        _Amplitude("Amplitude", Float) = 0
        _Frequency("Frequency", Float) = 0
        _Offset("Offset", Vector) = (0,0,0,0)
    }
    
    
    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _MainTex;
    float4 _MainTex_ST;
    
    float _Strength;
    
    float _Jump;
    float _JumpTime;
    float _Shake;
    float _Drift;
    float _DriftTime;
    
    float _Displacement;
    float _Threshold;
    
    float _Sinc;
    float _Sigmoid;
    float _Amplitude;
    float _Frequency;
    float4 _Offset;
    
    struct appdata
    {            
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        float2 uv : TEXCOORD0;
    };
    
    struct v2f
    {
        float4 pos : SV_POSITION;
        half2 uv   : TEXCOORD0; 
        
        float3 normal : NORMAL;
        float3 noise_uv : TEXCOORD1;
    };
    
    
    //  Canonical 2D random function.
    float random(float2 p)
    {
        return frac(sin(dot(p.xy,float2(12.9898, 78.233))) * 43758.5453);
    }
    
    //  Cardinal sine function.
    float sinc(float v)
    {
        float pi = 3.1416;
        float denominator = _Frequency * pi * v + _Offset.y;
        float numerator = sin(denominator);
        return _Offset.x + _Amplitude * numerator/denominator;
    }
    
    // Logistic function sigmoid curve.
    float sigmoid(float v)
    {
        return _Offset.x + _Amplitude/(1 + exp(-_Frequency * (v - _Offset.y)));
    }
    
    v2f vert (appdata v)
    {
        v2f o;
        
        o.normal = v.normal;
        o.pos = UnityObjectToClipPos(v.vertex);
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        o.noise_uv = v.vertex.xyz/v.vertex.w;

        return o;
    }

    half4 frag(v2f i) : SV_Target
    {
        float u = i.uv.x;
        float v = i.uv.y;
        float2 uv = i.uv;
        
        //  Jitter
        float jitter = random(float2(v, _Time.x)) * 2 - 1;
        jitter *= step(_Threshold, abs(jitter)) * _Displacement;
        jitter = _Sinc * sinc(v) * jitter + (1 - _Sinc) * jitter;
        jitter = _Sigmoid * sigmoid(v) * jitter + (1 - _Sigmoid) * jitter;
        
        //  Vertical Jump
        float jump = lerp(v, frac(v + _JumpTime), _Jump.x);
        
        //  Horizontal Shake
        float shake = (random(float2(_Time.x, 2)) - 0.5) * _Shake;
        
        //  Color Drift
        float drift = sin(jump + _DriftTime) * _Drift;
        
        //  Sources
        float src = u + jitter + shake;
        half4 src1 = tex2D(_MainTex, frac(float2(src, jump)));
        half4 src2 = tex2D(_MainTex, frac(float2(src + drift, jump)));
        
        //  Grain
        float rand = random(float2(u * _Time.x, v * _Time.y));
        fixed4 grain = fixed4(rand,rand,rand,1.0);
        grain = lerp(half4(src1.r, src2.g, src1.b, 1), grain, _Strength);
        return grain;
    }

    ENDCG
    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            ENDCG
        }
    }
}