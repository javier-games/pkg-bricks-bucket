using System;
using System.Globalization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework.Generics {

    /// <summary>
    /// DynVar.
    ///
    /// Dynamic variable that stores different types of data.
    /// By Javier García.
    /// </summary>
    [Serializable]
    public class DynVar {



        #region Class Members

        private int _hashCode = -1;         //  Hashcode of this instance.

        [SerializeField]
        protected string stringValue;           //  String variable.

        [SerializeField]
        protected Vector4 vector;          //  Vector4 variable.

        [SerializeField]
        protected Object asset;           //  UnityEngine.Object variable.

        [SerializeField]
        protected AnimationCurve curve     //  Curve variable
        = new AnimationCurve ();

        [SerializeField]
        protected DataType type;           //  Stores the type of variable.

        #endregion



        #region Accesors

        /// <summary> Gets or sets the type. </summary>
        public DataType Type {
            get => type;
            set {
                if (value != type) {
                    vector = Vector4.zero;
                    stringValue = string.Empty;
                    asset = null;
                    if (curve != null)
                        for (int i = 0; i < curve.keys.Length; i++)
                            curve.RemoveKey (i);
                }
                type = value;
            }
        }

        /// <summary> Gets or sets a boolean value. </summary>
        public bool Boolean {
            get => (vector.x > 0 || vector.x < 0);
            set => vector.x = value ? 1 : 0;
        }

        /// <summary> Gets or sets the integer. </summary>
        public int Integer {
            get => (int)vector.x;
            set => vector.x = value;
        }

        /// <summary> Gets or sets the float. </summary>
        public float Float {
            get => vector.x;
            set => vector.x = value;
        }

        /// <summary> Gets or sets the double. </summary>
        public double Double {
            get => vector.x;
            set => vector.x = (float)value;
        }

        /// <summary> Gets or sets the vector2. </summary>
        public Vector2 Vector2 {
            get => vector;
            set => vector = value;
        }

        /// <summary> Gets or sets the vector3. </summary>
        public Vector3 Vector3 {
            get => vector;
            set => vector = value;
        }

        /// <summary> Gets or sets the vector4. </summary>
        public Vector4 Vector4 {
            get => vector;
            set => vector = value;
        }

        /// <summary> Gets or sets the color. </summary>
        public Color Color {
            get => vector;
            set => vector = value;
        }

        /// <summary> Gets or sets the quaternion. </summary>
        public Quaternion Quaternion {
            get => new Quaternion (
                vector.x,
                vector.y,
                vector.z,
                vector.w
            );
            set => vector = new Vector4 (value.x, value.y, value.z, value.w);
        }

        /// <summary> Gets or sets the string. </summary>
        public string String {
            get => stringValue;
            set => stringValue = value;
        }

        /// <summary> Gets or sets the asset. </summary>
        public Object Asset {
            get => asset;
            set => asset = value;
        }

        /// <summary> Gets or sets the curve. </summary>
        public AnimationCurve Curve {
            get => curve;
            set => curve = value;
        }

        /// <summary> Returns true if this instance is write protected. </summary>
        public bool ReadOnly { get; private set; }

        /// <summary> Determines whether this instance is nil or void. </summary>
        public bool IsNil => Type == DataType.NULL;

        /// <summary> Gets the hash code. </summary>
        public int HashCode {
            get => _hashCode;
            private set => _hashCode = value;
        }

        #endregion



        #region Class Implementation

        public DynVar (bool readOnly = false) {
            Type = DataType.NULL;
            Vector4 = Vector4.zero;
            String = string.Empty;
            Asset = null;
            Curve = new AnimationCurve ();
            HashCode = -1;
            ReadOnly = readOnly;
            for (var i = 0; i < curve.keys.Length; i++)
                curve.RemoveKey (i);
        }

        public DynVar (
            Vector4 vector,
            string @string,
            AnimationCurve curve,
            Object @object,
            int hashCode,
            DataType type,
            bool readOnly
        ) {
            Vector4 = vector;
            String = @string;
            Curve = curve;
            Asset = @object;
            HashCode = hashCode;
            Type = type;
            ReadOnly = readOnly;
        }

        /// <summary>
        /// Clones this instance, overriding the "readonly".
        /// </summary>
        public DynVar Clone (bool readOnly) {
            var v = new DynVar {
                Vector4 = vector,
                String = stringValue,
                Curve = curve,
                Asset = asset,
                HashCode = HashCode,
                Type = type,
                ReadOnly = readOnly
            };
            return v;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public DynVar Clone () => Clone (ReadOnly);

        /// <summary>
        /// Returns this value as readonly
        /// </summary>
        public DynVar AsReadOnly () => ReadOnly ? this : Clone (true);

        /// <summary>
        /// Clones this instance, returning a writable copy.
        /// </summary>
        public DynVar AsWritable () => Clone (false);

        /// <summary>
        /// Performs an assignment with the specified one.
        /// </summary>
        public void Assign (DynVar value) {
            if (ReadOnly)
                throw new Exception ("Assigning on r-value");

            Vector4 = value.vector;
            String = value.stringValue;
            Asset = value.asset;
            Curve = value.curve;
            Type = value.Type;
            HashCode = -1;
        }

        /// <summary> Set with the specified value. </summary>
        public void Set (object value) {

            if (ReadOnly)
                throw new Exception ("Writing on r-value");

            if (value == null) {
                Type = DataType.NULL;
                return;
            }

            var desiredType = value.GetType ();

            if (desiredType == typeof (bool)) {
                Type = DataType.BOOLEAN;
                Boolean = (bool)Convert.ChangeType (
                    value,
                    typeof (bool)
                );
            } else if (desiredType == typeof (int)) {
                Type = DataType.INTEGER;
                Integer = (int)Convert.ChangeType (
                    value,
                    typeof (int)
                );
            } else if (desiredType == typeof (float)) {
                Type = DataType.FLOAT;
                Float = (float)Convert.ChangeType (
                    value,
                    typeof (float)
                );
            } else if (desiredType == typeof (double)) {
                Type = DataType.DOUBLE;
                Double = (double)Convert.ChangeType (
                    value,
                    typeof (double)
                );
            } else if (desiredType == typeof (Vector2)) {
                Type = DataType.VECTOR2;
                Vector2 = (Vector2)Convert.ChangeType (
                    value,
                    typeof (Vector2)
                );
            } else if (desiredType == typeof (Vector3)) {
                Type = DataType.VECTOR3;
                Vector3 = (Vector3)Convert.ChangeType (
                    value,
                    typeof (Vector3)
                );
            } else if (desiredType == typeof (Vector4)) {
                Type = DataType.VECTOR4;
                Vector4 = (Vector4)Convert.ChangeType (
                    value,
                    typeof (Vector4)
                );
            } else if (desiredType == typeof (Quaternion)) {
                Type = DataType.QUATERNION;
                Quaternion = (Quaternion)Convert.ChangeType (
                    value,
                    typeof (Quaternion)
                );
            } else if (desiredType == typeof (Color)) {
                Type = DataType.COLOR;
                Color = (Color)Convert.ChangeType (
                    value,
                    typeof (Color)
                );
            } else if (desiredType == typeof (string)) {
                Type = DataType.STRING;
                String = (string)Convert.ChangeType (
                    value,
                    typeof (string)
                );
            } else if (desiredType == typeof (AnimationCurve)) {
                Type = DataType.CURVE;
                Curve = (AnimationCurve)Convert.ChangeType (
                    value,
                    typeof (AnimationCurve)
                );
            } else {
                try {
                    Type = DataType.ASSET;
                    Asset = value as Object;
                }
                catch (Exception e) {
                    Debug.LogWarning (e);
                }
            }
        }

        /// <summary> Returns this DynValue to an object of a T type. </summary>
        public T Get<T> () {
            return (T)Get (typeof (T));
        }

        /// <summary> Returns this DynValue to an object of desired type. </summary>
        public object Get (Type desiredType) {

            if (ReadOnly)
                throw new Exception ("Writing on r-value");

            if (desiredType == typeof (bool))
                return Boolean;

            if (desiredType == typeof (int))
                return Integer;

            if (desiredType == typeof (float))
                return Float;

            if (desiredType == typeof (double))
                return Double;

            if (desiredType == typeof (Vector2))
                return Vector2;

            if (desiredType == typeof (Vector3))
                return Vector3;

            if (desiredType == typeof (Vector4))
                return Vector4;

            if (desiredType == typeof (Quaternion))
                return Quaternion;

            if (desiredType == typeof (Color))
                return Color;

            if (desiredType == typeof (string))
                return String;

            if (desiredType == typeof (AnimationCurve))
                return Curve;

            return Asset;
        }

        #endregion



        #region Superclass Overrides

        /// <summary> Determines if Obj is equal to this instance. </summary>
        public override bool Equals (object obj) {
            if (!(obj is DynVar other))
                return false;

            if (other.Type == Type && Type == DataType.NULL)
                return true;

            if (other.Type != Type)
                return false;

            switch (Type) {
                case DataType.NULL:
                    return true;

                case DataType.BOOLEAN:
                case DataType.DOUBLE:
                case DataType.INTEGER:
                case DataType.FLOAT:
                case DataType.VECTOR2:
                case DataType.VECTOR3:
                case DataType.VECTOR4:
                case DataType.QUATERNION:
                case DataType.COLOR:
                    return Vector4 == other.Vector4;

                case DataType.STRING:
                    return String == other.String;

                case DataType.CURVE:
                    return Curve == other.Curve;

                case DataType.ASSET:
                    return Asset == other.Asset;

                default:
                    return ReferenceEquals (this, other);
            }
        }

        /// <summary> Returns a String that represents this instance. </summary>
        public override string ToString () {
            switch (Type) {

                case DataType.NULL:
                    return "Null";

                case DataType.BOOLEAN:
                    return Boolean.ToString ().ToLower ();

                case DataType.DOUBLE:
                case DataType.INTEGER:
                case DataType.FLOAT:
                    return Float.ToString (
                        CultureInfo.InvariantCulture
                    );

                case DataType.VECTOR2:
                    return Vector2.ToString ();

                case DataType.VECTOR3:
                    return Vector3.ToString ();

                case DataType.VECTOR4:
                    return Vector4.ToString ();

                case DataType.QUATERNION:
                    return Quaternion.ToString ();

                case DataType.COLOR:
                    return Color.ToString ();

                case DataType.STRING:
                    return String;

                case DataType.ASSET:
                    return Asset.ToString ();

                case DataType.CURVE:
                    return Curve.ToString ();

                default:
                    return string.Empty;
            }
        }

        /// <summary> Returns a hash code for this instance. </summary>
        public override int GetHashCode () {
            if (HashCode != -1)
                return HashCode;

            var baseValue = ((int)(Type)) << 27;

            switch (Type) {

                case DataType.NULL:
                    HashCode = 0;
                    break;

                case DataType.BOOLEAN:
                    HashCode = Boolean ? 1 : 2;
                    break;

                case DataType.DOUBLE:
                case DataType.INTEGER:
                case DataType.FLOAT:
                    HashCode = baseValue ^ Double.GetHashCode ();
                    break;

                case DataType.STRING:
                    HashCode = baseValue ^ String.GetHashCode ();
                    break;

                case DataType.VECTOR2:
                    HashCode = baseValue ^ Vector2.GetHashCode ();
                    break;

                case DataType.VECTOR3:
                    HashCode = baseValue ^ Vector3.GetHashCode ();
                    break;

                case DataType.VECTOR4:
                    HashCode = baseValue ^ Vector4.GetHashCode ();
                    break;

                case DataType.QUATERNION:
                    HashCode = baseValue ^ Quaternion.GetHashCode ();
                    break;

                case DataType.COLOR:
                    HashCode = baseValue ^ Color.GetHashCode ();
                    break;

                case DataType.ASSET:
                    HashCode = baseValue ^ Asset.GetHashCode ();
                    break;

                case DataType.CURVE:
                    HashCode = baseValue ^ Curve.GetHashCode ();
                    break;

                default:
                    HashCode = 999;
                    break;
            }

            return HashCode;
        }

        #endregion
        
    }


    /// <summary>
    /// Enumeration of possible data types.
    /// </summary>
    public enum DataType {
        NULL,
        BOOLEAN,
        INTEGER,
        FLOAT,
        DOUBLE,
        VECTOR2,
        VECTOR3,
        VECTOR4,
        QUATERNION,
        COLOR,
        CURVE,
        STRING,
        ASSET
    }
}