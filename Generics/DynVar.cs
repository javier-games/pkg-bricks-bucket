using UnityEngine;

namespace Framework.Generics {

    /// <summary>
    /// DynVar.
    ///
    /// Dynamic variable that stores different types of data.
    /// By Javier García.
    /// </summary>
    [System.Serializable]
    public class DynVar {



        #region Class Members

        private int m_HashCode;// = -1;        //  Hashcode of this instance.
        protected bool m_ReadOnly;          //  Flag to know if this is read only.

        [SerializeField]
        protected string m_String;          //  String variable.

        [SerializeField]
        protected Vector4 m_Vector;         //  Vector4 variable.

        [SerializeField]
        protected Object m_Object;          //  UnityEngine.Object variable.

        [SerializeField]
        protected AnimationCurve m_Curve    //  Curve variable
        = new AnimationCurve ();

        [SerializeField]
        protected DataType m_Type;         //  Stores the type of variable.

        #endregion



        #region Accesors

        /// <summary> Gets or sets the type. </summary>
        public DataType Type {
            get { return m_Type; }
            set {
                if (value != m_Type) {
                    m_Vector = Vector4.zero;
                    m_String = string.Empty;
                    m_Object = null;
                    if (m_Curve != null)
                        for (int i = 0; i < m_Curve.keys.Length; i++)
                            m_Curve.RemoveKey (i);
                }
                m_Type = value;
            }
        }

        /// <summary> Gets or sets a boolean value. </summary>
        public bool Boolean {
            get { return (m_Vector.x > 0 || m_Vector.x < 0); }
            set { m_Vector.x = value ? 1 : 0; }
        }

        /// <summary> Gets or sets the integer. </summary>
        public int Integer {
            get { return (int)m_Vector.x; }
            set { m_Vector.x = value; }
        }

        /// <summary> Gets or sets the float. </summary>
        public float Float {
            get { return m_Vector.x; }
            set { m_Vector.x = value; }
        }

        /// <summary> Gets or sets the double. </summary>
        public double Double {
            get { return (double)m_Vector.x; }
            set { m_Vector.x = (float)value; }
        }

        /// <summary> Gets or sets the vector2. </summary>
        public Vector2 Vector2 {
            get { return (Vector2)m_Vector; }
            set { m_Vector = (Vector4)value; }
        }

        /// <summary> Gets or sets the vector3. </summary>
        public Vector3 Vector3 {
            get { return (Vector3)m_Vector; }
            set { m_Vector = (Vector4)value; }
        }

        /// <summary> Gets or sets the vector4. </summary>
        public Vector4 Vector4 {
            get { return m_Vector; }
            set { m_Vector = value; }
        }

        /// <summary> Gets or sets the color. </summary>
        public Color Color {
            get { return m_Vector; }
            set { m_Vector = value; }
        }

        /// <summary> Gets or sets the quaternion. </summary>
        public Quaternion Quaternion {
            get {
                return new Quaternion (
              m_Vector.x,
              m_Vector.y,
              m_Vector.z,
              m_Vector.w
          );
            }
            set {
                m_Vector = new Vector4 (value.x, value.y, value.z, value.w);
            }
        }

        /// <summary> Gets or sets the string. </summary>
        public string String {
            get { return m_String; }
            set { m_String = value; }
        }

        /// <summary> Gets or sets the asset. </summary>
        public Object Asset {
            get { return m_Object; }
            set { m_Object = value; }
        }

        /// <summary> Gets or sets the curve. </summary>
        public AnimationCurve Curve {
            get { return m_Curve; }
            set { m_Curve = value; }
        }

        /// <summary> Returns true if this instance is write protected. </summary>
        public bool ReadOnly {
            get { return m_ReadOnly; }
        }

        /// <summary> Determines whether this instance is nil or void. </summary>
        public bool IsNil {
            get { return this.Type == DataType.Null; }
        }

        /// <summary> Gets the hash code. </summary>
        public int HashCode {
            get { return m_HashCode; }
            private set { m_HashCode = value; }
        }

        #endregion



        #region Class Implementation

        public DynVar (bool m_ReadOnly = false) {
            m_Type = DataType.Null;
            m_Vector = Vector4.zero;
            m_String = string.Empty;
            m_Object = null;
            m_Curve = new AnimationCurve ();
            m_HashCode = -1;
            this.m_ReadOnly = m_ReadOnly;
            for (int i = 0; i < m_Curve.keys.Length; i++)
                m_Curve.RemoveKey (i);
        }

        public DynVar (
            Vector4 m_Vector,
            string m_String,
            AnimationCurve m_Curve,
            Object m_Object,
            int m_HashCode,
            DataType m_Type,
            bool m_ReadOnly
        ) {
            this.m_Vector = m_Vector;
            this.m_String = m_String;
            this.m_Curve = m_Curve;
            this.m_Object = m_Object;
            this.m_HashCode = m_HashCode;
            this.m_Type = m_Type;
            this.m_ReadOnly = m_ReadOnly;
        }

        /// <summary> Clones this instance, overriding the "readonly". </summary>
        public DynVar Clone (bool readOnly) {
            DynVar v = new DynVar {
                m_Vector = this.m_Vector,
                m_String = this.m_String,
                m_Curve = this.m_Curve,
                m_Object = this.m_Object,
                m_HashCode = this.m_HashCode,
                m_Type = this.m_Type,
                m_ReadOnly = readOnly
            };
            return v;
        }

        /// <summary> Clones this instance. </summary>
        public DynVar Clone () {
            return Clone (this.ReadOnly);
        }

        /// <summary> Returns this value as readonly </summary>
        public DynVar AsReadOnly () {
            if (ReadOnly)
                return this;
            else {
                return Clone (true);
            }
        }

        /// <summary> Clones this instance, returning a writable copy. </summary>
        public DynVar AsWritable () {
            return Clone (false);
        }

        /// <summary> Performs an assignment with the specified one. </summary>
        public void Assign (DynVar value) {
            if (this.ReadOnly)
                throw new System.Exception ("Assigning on r-value");

            this.m_Vector = value.m_Vector;
            this.m_String = value.m_String;
            this.m_Object = value.m_Object;
            this.m_Curve = value.m_Curve;
            this.m_Type = value.Type;
            this.m_HashCode = -1;
        }

        /// <summary> Set with the specified value. </summary>
        public void Set (object value) {

            if (this.ReadOnly)
                throw new System.Exception ("Writing on r-value");

            if (value == null) {
                Type = DataType.Null;
                return;
            }

            System.Type desiredType = value.GetType ();

            if (desiredType == typeof (bool)) {
                Type = DataType.Boolean;
                Boolean = (bool)System.Convert.ChangeType (
                    value,
                    typeof (bool)
                );
            } else if (desiredType == typeof (int)) {
                Type = DataType.Integer;
                Integer = (int)System.Convert.ChangeType (
                    value,
                    typeof (int)
                );
            } else if (desiredType == typeof (float)) {
                Type = DataType.Float;
                Float = (float)System.Convert.ChangeType (
                    value,
                    typeof (float)
                );
            } else if (desiredType == typeof (double)) {
                Type = DataType.Double;
                Double = (double)System.Convert.ChangeType (
                    value,
                    typeof (double)
                );
            } else if (desiredType == typeof (Vector2)) {
                Type = DataType.Vector2;
                Vector2 = (Vector2)System.Convert.ChangeType (
                    value,
                    typeof (Vector2)
                );
            } else if (desiredType == typeof (Vector3)) {
                Type = DataType.Vector3;
                Vector3 = (Vector3)System.Convert.ChangeType (
                    value,
                    typeof (Vector3)
                );
            } else if (desiredType == typeof (Vector4)) {
                Type = DataType.Vector4;
                Vector4 = (Vector4)System.Convert.ChangeType (
                    value,
                    typeof (Vector4)
                );
            } else if (desiredType == typeof (Quaternion)) {
                Type = DataType.Quaternion;
                Quaternion = (Quaternion)System.Convert.ChangeType (
                    value,
                    typeof (Quaternion)
                );
            } else if (desiredType == typeof (Color)) {
                Type = DataType.Color;
                Color = (Color)System.Convert.ChangeType (
                    value,
                    typeof (Color)
                );
            } else if (desiredType == typeof (string)) {
                Type = DataType.String;
                String = (string)System.Convert.ChangeType (
                    value,
                    typeof (string)
                );
            } else if (desiredType == typeof (AnimationCurve)) {
                Type = DataType.Curve;
                Curve = (AnimationCurve)System.Convert.ChangeType (
                    value,
                    typeof (AnimationCurve)
                );
            } else {
                try {
                    Type = DataType.Asset;
                    Asset = value as Object;
                }
                catch (System.Exception e) {
                    Debug.LogWarning (e);
                }
            }
        }

        /// <summary> Returns this DynValue to an object of a T type. </summary>
        public T Get<T> () {
            return (T)Get (typeof (T));
        }

        /// <summary> Returns this DynValue to an object of desired type. </summary>
        public object Get (System.Type desiredType) {

            if (this.ReadOnly)
                throw new System.Exception ("Writing on r-value");

            if (desiredType == typeof (bool))
                return Boolean;

            else if (desiredType == typeof (int))
                return Integer;

            else if (desiredType == typeof (float))
                return Float;

            else if (desiredType == typeof (double))
                return Double;

            else if (desiredType == typeof (Vector2))
                return Vector2;

            else if (desiredType == typeof (Vector3))
                return Vector3;

            else if (desiredType == typeof (Vector4))
                return Vector4;

            else if (desiredType == typeof (Quaternion))
                return Quaternion;

            else if (desiredType == typeof (Color))
                return Color;

            else if (desiredType == typeof (string))
                return String;

            else if (desiredType == typeof (AnimationCurve))
                return Curve;

            else
                return Asset;
        }

        #endregion



        #region Superclass Overrides

        /// <summary> Determines if Obj is equal to this instance. </summary>
        public override bool Equals (object obj) {
            DynVar other = obj as DynVar;

            if (other == null)
                return false;

            if (other.Type == this.Type && this.Type == DataType.Null)
                return true;

            if (other.Type != this.Type)
                return false;


            switch (Type) {
                case DataType.Null:
                    return true;

                case DataType.Boolean:
                case DataType.Double:
                case DataType.Integer:
                case DataType.Float:
                case DataType.Vector2:
                case DataType.Vector3:
                case DataType.Vector4:
                case DataType.Quaternion:
                case DataType.Color:
                    return Vector4 == other.Vector4;

                case DataType.String:
                    return String == other.String;

                case DataType.Curve:
                    return Curve == other.Curve;

                case DataType.Asset:
                    return Asset == other.Asset;

                default:
                    return object.ReferenceEquals (this, other);
            }
        }

        /// <summary> Returns a String that represents this instance. </summary>
        public override string ToString () {
            switch (Type) {

                case DataType.Null:
                    return "Null";

                case DataType.Boolean:
                    return Boolean.ToString ().ToLower ();

                case DataType.Double:
                case DataType.Integer:
                case DataType.Float:
                    return Float.ToString (
                        System.Globalization.CultureInfo.InvariantCulture
                    );

                case DataType.Vector2:
                    return Vector2.ToString ();

                case DataType.Vector3:
                    return Vector3.ToString ();

                case DataType.Vector4:
                    return Vector4.ToString ();

                case DataType.Quaternion:
                    return Quaternion.ToString ();

                case DataType.Color:
                    return Color.ToString ();

                case DataType.String:
                    return String;

                case DataType.Asset:
                    return Asset.ToString ();

                case DataType.Curve:
                    return Curve.ToString ();

                default:
                    return "(???)";
            }
        }

        /// <summary> Returns a hash code for this instance. </summary>
        public override int GetHashCode () {
            if (HashCode != -1)
                return HashCode;

            int baseValue = ((int)(Type)) << 27;

            switch (Type) {

                case DataType.Null:
                    HashCode = 0;
                    break;

                case DataType.Boolean:
                    HashCode = Boolean ? 1 : 2;
                    break;

                case DataType.Double:
                case DataType.Integer:
                case DataType.Float:
                    HashCode = baseValue ^ Double.GetHashCode ();
                    break;

                case DataType.String:
                    HashCode = baseValue ^ String.GetHashCode ();
                    break;


                case DataType.Vector2:
                    HashCode = baseValue ^ Vector2.GetHashCode ();
                    break;

                case DataType.Vector3:
                    HashCode = baseValue ^ Vector3.GetHashCode ();
                    break;

                case DataType.Vector4:
                    HashCode = baseValue ^ Vector4.GetHashCode ();
                    break;

                case DataType.Quaternion:
                    HashCode = baseValue ^ Quaternion.GetHashCode ();
                    break;

                case DataType.Color:
                    HashCode = baseValue ^ Color.GetHashCode ();
                    break;


                case DataType.Asset:
                    HashCode = baseValue ^ Asset.GetHashCode ();
                    break;

                case DataType.Curve:
                    HashCode = baseValue ^ Curve.GetHashCode ();
                    break;

                default:
                    HashCode = 999;
                    break;
            }

            return HashCode;
        }

        #endregion



        #region Static Methods

        /// <summary> Creates a new Null DynVar. </summary>
        public static DynVar NewNull () {
            return new DynVar ();
        }

        /// <summary> Creates a new Boolean DynVar. </summary>
        public static DynVar NewBoolean (bool v) {
            return new DynVar () { Type = DataType.Boolean, Boolean = v };
        }

        /// <summary> Creates a new Integer DynVar. </summary>
        public static DynVar NewInteger (int v) {
            return new DynVar () { Type = DataType.Integer, Integer = v };
        }

        /// <summary> Creates a new Float DynVar. </summary>
        public static DynVar NewFloat (float v) {
            return new DynVar () { Type = DataType.Float, Float = v };
        }

        /// <summary> Creates a new Double DynVar. </summary>
        public static DynVar NewDouble (double v) {
            return new DynVar () { Type = DataType.Double, Double = v };
        }

        /// <summary> Creates a new Vector2 DynVar. </summary>
        public static DynVar NewVector2 (Vector2 v) {
            return new DynVar () { Type = DataType.Vector2, Vector2 = v };
        }

        /// <summary> Creates a new Vector3 DynVar. </summary>
        public static DynVar NewVector3 (Vector3 v) {
            return new DynVar () { Type = DataType.Vector3, Vector3 = v };
        }

        /// <summary> Creates a new Vector4 DynVar. </summary>
        public static DynVar NewVector4 (Vector4 v) {
            return new DynVar () { Type = DataType.Vector4, Vector4 = v };
        }

        /// <summary> Creates a new Quaternion DynVar. </summary>
        public static DynVar NewQuaternion (Quaternion v) {
            return new DynVar () { Type = DataType.Quaternion, Quaternion = v };
        }

        /// <summary> Creates a new Color DynVar. </summary>
        public static DynVar NewColor (Color v) {
            return new DynVar () { Type = DataType.Color, Color = v };
        }

        /// <summary> Creates a new String DynVar. </summary>
        public static DynVar NewString (string format, params object [] args) {
            return new DynVar () {
                Type = DataType.String,
                String = string.Format (format, args)
            };
        }

        /// <summary> Creates a new AnimationCurve DynVar. </summary>
        public static DynVar NewCurve (AnimationCurve v) {
            return new DynVar () { Type = DataType.Curve, Curve = v };
        }

        #endregion



    }


    /// <summary>
    /// Enumeration of possible data types.
    /// </summary>
    public enum DataType {
        Null,
        Boolean,
        Integer,
        Float,
        Double,
        Vector2,
        Vector3,
        Vector4,
        Quaternion,
        Color,
        Curve,
        String,
        Asset
    }
}