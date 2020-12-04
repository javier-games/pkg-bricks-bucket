using UnityEngine;

namespace Framework.Generics {

    /// <summary>
    /// AbstractReference.
    /// 
    /// Dynamic reference to component properties.
    /// By Javier García.
    /// </summary>
    [System.Serializable]
    public abstract class AbstractReference<THardwired> : IReference
    where THardwired : AbstractHardwired, new()
    {
        
        #region Fields

        private static THardwired _hardwired = new THardwired();

        [SerializeField]
        private Object component;    //  Component Reference.

        [SerializeField]
        private string property;     //  Name of the _property.

        [SerializeField]
        private DynVariable value;        //  Value of the _property.

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

        public virtual IVariable Variable => value;

        public virtual IHardwiredRegistry Hardwired =>
            _hardwired ?? (_hardwired = new THardwired());


        #endregion



        #region Class Implementation

        /// <summary>
        /// Initializes a new instance of the AbstractReference class.
        /// </summary>
        protected AbstractReference () {
            Component = null;
            Property = string.Empty;
            value = new DynVariable();
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
            value.Type = DataType.NULL;
        }

        /// <summary> Sets the property. </summary>
        public void SetProperty (string propertyName) {
            Property = propertyName;
            UpdatedDynVar ();
        }

        /// <summary> Updates the dyn variable. </summary>
        public void UpdatedDynVar () {
            if (GetValue () != null)
                value.Set (GetValue ());
        }

        /// <summary> Gets the value. </summary>
        public object GetValue () {
            if (Component == null || string.IsNullOrEmpty (Property)) {
                return null;
            }

            try { return _hardwired.GetValue (Component, Property); }
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
                _hardwired.SetValue (Component, Property, propertyValue);
            }
            catch (System.Exception)
            {
                // ignored
            }
        }

        #endregion

    }
}