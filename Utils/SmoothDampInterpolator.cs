using UnityEngine;

namespace Framework.Utils {

    /// <summary>
    /// Smooth Base.
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Base for smooth damp interpolators.
    /// </para>
    /// </summary>
    public abstract class Smooth<T> {

        protected T _target;        //  Taget for the interpolation.
        protected T _current;       //  Current value of transition.
        protected T _velocity;      //  Current velocity in interpolation.

        protected float _smooth;    //  Smoothness.

        /// <summary> Gets the velocity. </summary>
        public T velocity {
            get { return _velocity; }
        }

        /// <summary> Gets or sets the target. </summary>
        public T target { 
            get { return _target; }
            set { _target = value; }
        }

        /// <summary> Gets or sets the current value. </summary>
        public T current {
            get { return _current; }
            set { _current = value; }
        }

        /// <summary> Gets the incoming value. </summary>
        public abstract T incoming{ get; }

        /// <summary> Initializes a new instance of Smooth class. </summary>
        protected Smooth (float smooth) {
            _smooth = smooth;
        }

        /// <summary> Updates the position and target. </summary>
        public T Update(T current, T target){
            this.target = target;
            return Update (current);
        }

        /// <summary> Updates the position. </summary>
        public T Update (T current) {
            this.current = current;
            return this.incoming;
        }

    }

    /// <summary>
    /// Smooth Float.
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Smooth damp interpolator for floats.
    /// </para>
    /// </summary>
    public class SmoothFloat : Smooth<float> {

        /// <summary> Gets or sets the current value. </summary>
        public override float incoming {
            get {
                _current = Mathf.SmoothDamp (
                    current:            _current,
                    target:             _target,
                    currentVelocity:    ref _velocity,
                    smoothTime:         _smooth,
                    maxSpeed:           float.MaxValue,
                    deltaTime:          Time.deltaTime
                );
                return _current;
            }
        }

        /// <summary> Initializes a new instance of this class. </summary>
        public SmoothFloat(float smooth): base(smooth){}
    }

    /// <summary>
    /// Smooth Angle.
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Smooth damp interpolator for floats angles.
    /// </para>
    /// </summary>
    public class SmoothAngle : Smooth<float> {

        /// <summary> Gets or sets the current value. </summary>
        public override float incoming {
            get {
                _current = Mathf.SmoothDampAngle (
                    current:            _current,
                    target:             _target,
                    currentVelocity:    ref _velocity,
                    smoothTime:         _smooth,
                    maxSpeed:           float.MaxValue,
                    deltaTime:          Time.deltaTime
                );
                return _current;
            }
        }

        /// <summary> Initializes a new instance of this class. </summary>
        public SmoothAngle (float smooth) : base (smooth) { }
    }

    /// <summary>
    /// Vector2 Smooth
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Smooth damp interpolator for Vector2
    /// </para>
    /// </summary>
    public class SmoothVector2 : Smooth<Vector2> {

        /// <summary> Gets or sets the current value. </summary>
        public override Vector2 incoming {
            get {
                _current = Vector2.SmoothDamp (
                    current:            _current,
                    target:             _target,
                    currentVelocity:    ref _velocity,
                    smoothTime:         _smooth,
                    maxSpeed:           float.MaxValue,
                    deltaTime:          Time.deltaTime
                );
                return _current;
            }
        }

        /// <summary> Initializes a new instance of this class. </summary>
        public SmoothVector2 (float smooth) : base (smooth) { }
    }

    /// <summary>
    /// Vector3 Smooth
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Smooth damp interpolator for Vector3.
    /// </para>
    /// </summary>
    public class SmoothVector3 : Smooth<Vector3> {

        /// <summary> Gets or sets the current value. </summary>
        public override Vector3 incoming {
            get {
                _current = Vector3.SmoothDamp (
                    current:            _current,
                    target:             _target,
                    currentVelocity:    ref _velocity,
                    smoothTime:         _smooth,
                    maxSpeed:           float.MaxValue,
                    deltaTime:          Time.deltaTime
                );
                return _current;
            }
        }

        /// <summary> Initializes a new instance of this class. </summary>
        public SmoothVector3 (float smooth) : base (smooth) { }
    }

    /// <summary>
    /// Smooth Euler
    /// By Javier García, 2018.
    /// 
    /// <para>
    /// Smooth damp interpolator for Euler Angles.
    /// </para>
    /// </summary>
    public class SmoothEuler : Smooth<Vector3> {

        /// <summary> Gets or sets the current value. </summary>
        public override Vector3 incoming {
            get {
                _current = new Vector3 (
                    Mathf.SmoothDampAngle (
                        current:            _current.x,
                        target:             _target.x,
                        currentVelocity:    ref _velocity.x,
                        smoothTime:         _smooth,
                        maxSpeed:           float.MaxValue,
                        deltaTime:          Time.deltaTime
                    ),
                    Mathf.SmoothDampAngle (
                        current:            _current.y,
                        target:             _target.y,
                        currentVelocity:    ref _velocity.y,
                        smoothTime:         _smooth,
                        maxSpeed:           float.MaxValue,
                        deltaTime:          Time.deltaTime
                    ),
                    Mathf.SmoothDampAngle (
                        current:            _current.z,
                        target:             _target.z,
                        currentVelocity:    ref _velocity.z,
                        smoothTime:         _smooth,
                        maxSpeed:           float.MaxValue,
                        deltaTime:          Time.deltaTime
                    )
                );
                return _current;
            }
        }

        /// <summary> Initializes a new instance of this class. </summary>
        public SmoothEuler(float smooth) : base (smooth) { }
    }
}