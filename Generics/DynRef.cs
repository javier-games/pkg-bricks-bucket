using UnityEngine;

namespace Framework.Generics {

    /// <summary>
    /// DynRef.
    /// 
    /// Dynamic reference to component properties.
    /// By Javier García.
    /// </summary>
    [System.Serializable]
    public class DynRef
    {
        
        #region Fields

        private static RegisteredTypes
            _registeredTypes = new RegisteredTypes();

        [SerializeField]
        private Object component;    //  Component Reference.

        [SerializeField]
        private string property;     //  Name of the _property.

        [SerializeField]
        private DynVar value;        //  Value of the _property.

        #endregion



        #region Accessors

        /// <summary> Gets the component reference. </summary>
        public Object Component {
            get => component;
            protected set => component = value;
        }

        /// <summary> Gets the _property name. </summary>
        public string Property {
            get => property;
            protected set => property = value;
        }

        /// <summary> Gets the dynamic variable. </summary>
        public DynVar DynVar {
            get => value;
            protected set => this.value = value;
        }

        public virtual RegisteredTypes Types =>
            _registeredTypes ?? (_registeredTypes = new RegisteredTypes());

        #endregion



        #region Class Implementation

        /// <summary> Initializes a new instance of the DynRef class. </summary>
        public DynRef () {
            Component = null;
            Property = string.Empty;
            DynVar = new DynVar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        public void SetReference (Object reference)
        {
            if (Component == reference) return;
            Component = reference;
            Property = string.Empty;
            DynVar.Type = DataType.NULL;
        }

        /// <summary> Sets the property. </summary>
        public void SetProperty (string propertyName) {
            Property = propertyName;
            UpdatedDynVar ();
        }

        /// <summary> Updates the dyn variable. </summary>
        public void UpdatedDynVar () {
            if (GetValue () != null)
                DynVar.Set (GetValue ());
        }

        /// <summary> Gets the value. </summary>
        public object GetValue () {
            if (Component == null || string.IsNullOrEmpty (Property)) {
                return null;
            }

            try { return Types.GetValue (Component, Property); }
            catch (System.Exception) {
                return null;
            }
        }

        /// <summary> Sets the value. </summary>
        public void SetValue (object propertyValue) {

            if (Component == null || string.IsNullOrEmpty (Property))
                return;

            try
            {
                Types.SetValue (Component, Property, propertyValue);
            }
            catch (System.Exception)
            {
                // ignored
            }
        }

        #endregion

    }
}