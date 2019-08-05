using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BricksBucket.Collections
{
    /// <summary>
    /// 
    /// Array Drawer Base.
    /// 
    /// <para>
    /// Base of an array drawer.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the UnityExtensions.ArrayDrawer by @garettbass.
    /// https://github.com/garettbass/UnityExtensions.ArrayDrawer
    /// </para>
    /// 
    /// </summary>
    public abstract class ArrayDrawerBase : DecoratorDrawer
    {

        #region Class Members

        //  Type of Property Handler.
        private static readonly Type
        _propertyHandler = typeof (DecoratorDrawer).Assembly.GetType (
            name: "UnityEditor.PropertyHandler"
        );

        //  Type of Script Attribute Utility.
        private static readonly Type
        _scriptAttributeUtility = typeof (DecoratorDrawer).Assembly.GetType (
            name: "UnityEditor.ScriptAttributeUtility"
        );

        //  Type of Porperty Handler Cache.
        private static readonly Type
        _propertyHandlerCache = typeof (DecoratorDrawer).Assembly.GetType (
            name: "UnityEditor.PropertyHandlerCache"
        );

        //  Field Info of property drawer of type Property Handler.
        private static readonly FieldInfo
        _propertyHandlerPropertyDrawer = _propertyHandler.GetField (
            name: "m_PropertyDrawer",
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance
        );

        //  Field iIfo of decorator drawer of type Property Handler.
        private static readonly FieldInfo
        _propertyHandlerDecoratorDrawers = _propertyHandler.GetField (
            name: "m_DecoratorDrawers",
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance
        );

        //  Property Info of Property Handler Cache.
        private static readonly PropertyInfo
        _propertyHandlerCacheInfo = _scriptAttributeUtility.GetProperty (
            name: "propertyHandlerCache",
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Static
        );

        //  Field Info of Property Handlers of type Property Handler Cache.
        private static readonly FieldInfo
        _propertyHandlerCacheHandlers = _propertyHandlerCache.GetField (
            name: "m_PropertyHandlers",
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance
        );

        //  Bool to determinate wether this has an injector array drawer.
        private bool _didInjectArrayDrawer;

        #endregion



        #region Decorator Drawer Overrides

        /// <summary> Defines wether the InsperctorGUI can be cached. </summary>
        /// <returns> Wether the insperctor GUI can be cached. </returns>
        public sealed override bool CanCacheInspectorGUI ()
        {
            //  Inject an array drawer if it has not one yet.
            if (!_didInjectArrayDrawer)
                InjectArrayDrawer ();
            return false;
        }

        /// <summary> Determinates how tall the decorator is. </summary>
        /// <returns> Height in pixels. </returns>
        public sealed override float GetHeight ()
        {
            //  Inject an array drawer if it has not one yet.
            if (!_didInjectArrayDrawer)
                InjectArrayDrawer ();
            return 0;
        }

        /// <summary> Draws on GUI. </summary>
        /// <param name="position"></param>
        public sealed override void OnGUI (Rect position) { }

        #endregion



        #region Class Implementation

        /// <summary> Injects an array drawer. </summary>
        private void InjectArrayDrawer ()
        {
            _didInjectArrayDrawer = true;

            var propertyHandler = GetPropertyHandler ();

            var propertyDrawer = GetPropertyDrawer (propertyHandler);

            if (propertyDrawer == null)
            {
                propertyDrawer = new ArrayDrawerAdapter ((ArrayDrawer) this);
                SetPropertyDrawer (propertyHandler, propertyDrawer);
            }
        }

        /// <summary> Returns the property handler. </summary>
        /// <returns> Property handler. </returns>
        internal object GetPropertyHandler ()
        {

            //  Getting the property Handlers.
            var propertyHandlerCache =
                _propertyHandlerCacheInfo.GetValue (null, null);

            var propertyHandlersDictionary =
                (IDictionary) _propertyHandlerCacheHandlers.GetValue (
                    obj: propertyHandlerCache
                );

            var propertyHandlers = propertyHandlersDictionary.Values;

            //  Return the list of decorators or null.
            foreach (var propertyHandler in propertyHandlers)
            {
                var decoratorDrawers =
                    (List<DecoratorDrawer>)
                    _propertyHandlerDecoratorDrawers.GetValue (
                        obj: propertyHandler
                    );

                if (decoratorDrawers == null)
                    continue;

                var index = decoratorDrawers.IndexOf (this);
                if (index < 0)
                    continue;

                return propertyHandler;
            }

            return null;
        }

        /// <summary> Returns the property drawer class of an object. </summary>
        /// <param name="handler"></param>
        /// <returns> The property Drawer. </returns>
        internal static PropertyDrawer GetPropertyDrawer (object handler)
        {
            return (PropertyDrawer) _propertyHandlerPropertyDrawer.GetValue (
                obj: handler
            );
        }

        /// <summary> Sets the property drawer of a handler. </summary>
        /// <param name="handler"></param>
        /// <param name="propertyDrawer"></param>
        internal static void
        SetPropertyDrawer (object handler, PropertyDrawer propertyDrawer)
        {
            _propertyHandlerPropertyDrawer.SetValue (handler, propertyDrawer);
        }

        #endregion
    }
}