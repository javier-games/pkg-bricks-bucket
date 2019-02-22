using UnityEngine;

namespace Framework.Generics {

    /// <summary>
    /// DynRef.
    /// 
    /// Dynamic reference to component properties.
    /// By Javier García.
    /// </summary>
    [System.Serializable]
    public class DynRef {



        #region Class Members

        [SerializeField]
        private Object _component;    //  Component Reference.

        [SerializeField]
        private string _property;     //  Name of the property.

        [SerializeField]
        private DynVar _value;        //  Value of the property.

        #endregion



        #region Accessors

        /// <summary> Gets the component reference. </summary>
        public Object Component {
            get { return _component; }
        }

        /// <summary> Gets the property name. </summary>
        public string Property {
            get { return _property; }
        }

        /// <summary> Gets the dynamic variable. </summary>
        public DynVar DynVar {
            get { return _value; }
        }

        #endregion



        #region Class Implementation

        /// <summary> Initializes a new instance of the DynRef class. </summary>
        public DynRef () {
            _component = null;
            _property = string.Empty;
            _value = DynVar.NewNull ();
        }

        /// <summary> Sets the reference. </summary>
        public void SetReference (Object component) {
            if (_component != component) {
                _component = component;
                _property = string.Empty;
                _value.Type = DataType.Null;
            }
        }

        /// <summary> Sets the property. </summary>
        public void SetProperty (string property) {
            _property = property;
            UpdatedDynVar ();
        }

        /// <summary> Updates the dyn variable. </summary>
        public void UpdatedDynVar () {
            if (GetValue () != null)
                _value.Set (GetValue ());
        }

        /// <summary> Gets the value. </summary>
        public object GetValue () {
            if (Component == null || string.IsNullOrEmpty (Property)) {
            #if !UNITY_EDITOR
            Debug.LogWarning (
                "Trying to access to: " +
                "\n\tComponent[" + Component + "]" +
                "\n\tProperty["+ Property + "]"
            );
            #endif
                return null;
            }

            try { return RegisteredTypes.GetValue (Component, Property); }
            catch (System.Exception e) {
                Debug.LogWarning (e);
                return null;
            }
        }

        /// <summary> Sets the value. </summary>
        public void SetValue (object value) {

            if (Component == null || string.IsNullOrEmpty (Property))
                return;

            try { RegisteredTypes.SetValue (Component, Property, value); }
            catch (System.Exception e) {
                Debug.Log (e);
            }
        }

        #endregion

    }
}