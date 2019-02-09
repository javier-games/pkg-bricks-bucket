using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof (Camera))]
public class AnalogGlitch : MonoBehaviour {

    #region Class Members

    Shader _shader;
    Material _material;

    [Header ("Grain Noise")]
    [SerializeField, Range (-1f, 1f)]
    private float _strength;

    [Header ("Positional Glitch")]
    [SerializeField]
    private float _verticalJump;
    [SerializeField]
    private float _horizontalShake;
    [SerializeField]
    private float _colorDrift;
    private float _jumpTime;

    [Header ("Jitter")]
    [SerializeField, Range (0f, 1f)]
    private float _displacement;
    [SerializeField, Range (0f, 1f)]
    private float _threshold;
    [SerializeField]
    private JitterFunctions _function;
    [SerializeField]
    private float _amplitude;
    [SerializeField]
    private float _frequency;
    [SerializeField]
    private Vector2 _offset;

    #endregion


    private Shader shader {
        get {
            if (_shader == null)
                _shader = Shader.Find ("Hidden/JGFramework/Camera/AnalogGlitch");
            return _shader;
        }
    }


    #region MonoBehaviour Overrides

    void OnRenderImage (RenderTexture source, RenderTexture destination) {

        if (_material == null)
            _material = new Material (shader) {
                hideFlags = HideFlags.DontSave
            };


        //  Changing the grain properties.
        _material.SetFloat ("_Strength", _strength);

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


    public enum JitterFunctions {
        NONE,
        SINC,
        SIGMOID
    }

}
