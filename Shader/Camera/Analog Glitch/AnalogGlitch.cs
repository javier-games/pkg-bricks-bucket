/* 
 * Based on the AnalogGlitch shader on KinoGlitch repo from Keijiro.
 * https://github.com/keijiro/KinoGlitch
*/

using UnityEngine;

namespace Framework.CameraShader {

    /// <summary>
    /// Analog Glitch
    /// By Javier García, 2019.
    /// 
    /// <para>
    /// Controller for analog glitch camera shader.
    /// </para>
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent (typeof (Camera))]
    public class AnalogGlitch: MonoBehaviour {


        #region Class Members

        [SerializeField]
        Shader _shader;                     //  Shader.
        Material _material;                 //  Material.

        [Header ("Grain Noise")]
        [SerializeField, Range (-1f, 1f)]
        private float _strength;            //  Strength of noise.
        [SerializeField]
        private float _scale;               //  Scale of noise.
        [SerializeField]
        private Vector3 _parameters;        //  Parameters for noise.

        [Header ("Positional Glitch")]
        [SerializeField]
        private float _verticalJump;        //  Horizontal jump.
        [SerializeField]
        private float _horizontalShake;     //  Amount of shake.
        [SerializeField]
        private float _colorDrift;          //  Amount of drift.
        private float _jumpTime;

        [Header ("Jitter")]
        [SerializeField, Range (0f, 1f)]
        private float _displacement;        //  Jitter Displacement.
        [SerializeField, Range (0f, 1f)]
        private float _threshold;           //  Jitter Threshold.
        [SerializeField]
        private JitterFunctions _function;  //  Type of function.
        [SerializeField]
        private float _amplitude;           //  Amplitud of functions.
        [SerializeField]
        private float _frequency;           //  Frequency of functions.
        [SerializeField]
        private Vector2 _offset;            //  Offset of functions.

        #endregion


        #region Accesors

        /// <summary> Gets the shader. </summary>
        private Shader Shader {
            get {
                if (_shader == null)
                    _shader = Shader.Find ("Hidden/JGFramework/Camera/AnalogGlitch");
                return _shader;
            }
        }

        /// <summary> Gets or sets the strength. </summary>
        public float Strength {
            get { return _strength; }
            set { _strength = value; }
        }

        /// <summary> Gets or sets the noise parameters. </summary>
        public Vector3 NoiseParameters {
            get { return _parameters; }
            set { _parameters = value; }
        }

        /// <summary> Gets or sets the scale. </summary>
        public float Scale {
            get { return _scale; }
            set { _scale = value; }
        }

        /// <summary> Gets or sets the vertical jump. </summary>
        public float VerticalJump {
            get { return _verticalJump; }
            set { _verticalJump = value; }
        }

        /// <summary> Gets or sets the horizontal scale. </summary>
        public float HorizontalScale {
            get { return _horizontalShake; }
            set { _horizontalShake = value; }
        }

        /// <summary> Gets or sets the color drift. </summary>
        public float ColorDrift {
            get { return _colorDrift; }
            set { _colorDrift = value; }
        }

        /// <summary> Gets or sets the displacement. </summary>
        public float Displacement {
            get { return _displacement; }
            set { _displacement = value; }
        }

        /// <summary> Gets or sets the threshold. </summary>
        public float Threshold {
            get { return _threshold; }
            set { _threshold = value; }
        }

        /// <summary> Gets or sets the function. </summary>
        public JitterFunctions Function {
            get { return _function; }
            set { _function = value; }
        }

        /// <summary> Gets or sets the amplitud. </summary>
        public float Amplitud {
            get { return _amplitude; }
            set { _amplitude = value; }
        }

        /// <summary> Gets or sets the frequency. </summary>
        public float Frequency {
            get { return _frequency; }
            set { _frequency = value; }
        }

        /// <summary> Gets or sets the offset. </summary>
        public Vector2 Offset {
            get { return _offset; }
            set { _offset = value; }
        }

        #endregion


        #region MonoBehaviour Overrides

        //   Called on render image.
        void OnRenderImage (RenderTexture source, RenderTexture destination) {

            if (_material == null)
                _material = new Material (Shader) {
                    hideFlags = HideFlags.DontSave
                };


            //  Changing the grain properties.
            _material.SetFloat ("_Strength", _strength);
            _material.SetFloat ("_Scale", _scale);
            _material.SetVector ("_NoiseParameters", _parameters);

            //  Changing the vertical jump properties.
            _jumpTime += Time.deltaTime * _verticalJump;
            _material.SetFloat ("_Jump", _verticalJump);
            _material.SetFloat ("_JumpTime", _jumpTime);

            //  Changing the horizontal shake properties.
            _material.SetFloat ("_Shake", _horizontalShake);

            //  Changing the color drifter properties.
            _material.SetFloat ("_Drift", _colorDrift);
            _material.SetFloat ("_DriftTime", Time.time);

            //  Changing the Jitter properties.
            _material.SetFloat ("_Displacement", _displacement);
            _material.SetFloat ("_Threshold", _threshold);

            switch (_function) {
                case JitterFunctions.SINC:
                _material.SetFloat ("_Sinc", 1f);
                _material.SetFloat ("_Sigmoid", 0f);
                break;
                case JitterFunctions.SIGMOID:
                _material.SetFloat ("_Sigmoid", 1f);
                _material.SetFloat ("_Sinc", 0f);
                break;
                default:
                _material.SetFloat ("_Sinc", 0f);
                _material.SetFloat ("_Sigmoid", 0f);
                break;
            }

            _material.SetFloat ("_Amplitude", _amplitude);
            _material.SetFloat ("_Frequency", _frequency);
            _material.SetVector ("_Offset", _offset);

            Graphics.Blit (source, destination, _material);
        }

        #endregion


        #region Sub Classes

        /// <summary>  functions. </summary>
        public enum JitterFunctions {
            NONE,
            SINC,
            SIGMOID
        }

        #endregion
    }
}
