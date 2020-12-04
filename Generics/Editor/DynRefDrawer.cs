using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Framework.Generics {

    /// <summary>
    /// Dyn reference drawer.
    /// 
    /// Drawer for the DynVar Class.
    /// By Javier García.
    /// </summary>
    [CustomPropertyDrawer(typeof(DynRef))]
    public class DynRefDrawer : PropertyDrawer
    {



        #region Class Members

        //  Height of a standard singleline.
        private readonly float _fieldSize = EditorGUIUtility.singleLineHeight;

        //  Array of possible rects.
        private readonly Rect [] _rects = new Rect [5];

        //  References of a dynamic variable.
        private readonly DynRef _dynRef = new DynRef ();

        //  List that the stores the properties and components.
        private readonly List<string> _dropdown = new List<string> ();


        private PropertyInfo [] _propertiesInfo;      //  Reference to proertiesInfo.
        private Component [] _components;             //  Reference to components.
        private int _rectIndex;                      //  Curren count of rects.
        private int _count;                          //  Count of fields.

        private const float Padding = 4;            //  Size of padding.
        private const string Text = "Value";        //  Text to show in value.

        #endregion



        #region Superclass Overrides

        //  Returns the height of the property.
        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            return (_fieldSize * _count) + (Padding * (_count + 1));
        }

        //  Called to draw in gui.
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

            //  Declaration of rects.
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            _rectIndex = 0;
            _rects [0] = new Rect (
                x: position.x,
                y: position.y + Padding,
                width: position.width,
                height: _fieldSize
            );
            _rects [1] = new Rect (
                x: position.x,
                y: position.y + _fieldSize + (Padding * 2),
                width: position.width,
                height: _fieldSize
            );
            _rects [2] = new Rect (
                x: position.x,
                y: position.y + (_fieldSize * 2) + (Padding * 2),
                width: position.width,
                height: _fieldSize
            );
            _rects [3] = new Rect (
                x: position.x,
                y: position.y + (_fieldSize * 3) + (Padding * 2),
                width: position.width,
                height: _fieldSize
            );
            _rects [4] = new Rect (
                x: position.x,
                y: position.y + (_fieldSize * 4) + (Padding * 2),
                width: position.width,
                height: _fieldSize
            );
            EditorGUI.indentLevel = indent;


            //  Getting the properties references.
            var dynProperty = property.FindPropertyRelative ("value");
            _dynRef.SetReference (property.FindPropertyRelative ("component").objectReferenceValue);
            _dynRef.SetProperty (property.FindPropertyRelative ("property").stringValue);

            //  Drawing object field.
            _dynRef.SetReference (EditorGUI.ObjectField (
                position: GetRect (),
                label: "Component",
                obj: _dynRef.Component,
                objType: _dynRef.Component != null ?
                                    _dynRef.Component.GetType () :
                                    typeof (Object),
                allowSceneObjects: true
            ));

            //  If component is different to null.
            if (_dynRef.Component != null) {

                //  Shortcuts.
                Object objectValue = _dynRef.Component;
                System.Type type = objectValue.GetType ();


                //  Component Validation.
                if (!_dynRef.Types.ContainsComponent (type.ToString ())) {

                    EditorGUI.BeginChangeCheck ();
                    EditorGUI.HelpBox (
                        position: GetRect (),
                        message: "This component type is not registered.",
                        type: MessageType.Error
                    );
                    GUI.Button (GetRect (), "Register Type");

                    //  If button is pressed.
                    if (EditorGUI.EndChangeCheck ()) {
                        Registrator.RegisterType(
                            _dynRef.Component.GetType (),
                            _dynRef.Types
                        );
                    }

                    //  Update properties values.
                    StoreData (property, dynProperty);
                    return;
                }


                //  Component Selection if object is a GameObject.
                if (type == typeof (GameObject)) {

                    _components = (objectValue as GameObject)?.GetComponents<Component> ();
                    _dropdown.Clear ();
                    _dropdown.Add ("As GameObject");
                    if (_components != null)
                    {
                        foreach (Component component in _components)
                            _dropdown.Add(component.GetType().Name);

                        var compIndex = 0;
                        compIndex = EditorGUI.Popup(GetRect(), "Select",
                            compIndex, _dropdown.ToArray());
                        if (compIndex != 0)
                            _dynRef.SetReference(_components[compIndex - 1]);
                    }
                }


                //  Property Selection.
                _propertiesInfo = type.GetProperties (BindingFlags.Public | BindingFlags.Instance);
                _dropdown.Clear ();
                _dropdown.Add ("- None -");
                foreach (PropertyInfo info in _propertiesInfo)
                    if (info.CanRead && info.CanWrite && !info.IsDefined (typeof (System.ObsoleteAttribute), true))
                        _dropdown.Add (info.Name);

                int propIndex = 0;
                bool inList = false;
                for (int i = 0; i < _dropdown.Count; i++)
                    if (property.FindPropertyRelative ("property").stringValue == _dropdown [i]) {
                        propIndex = i;
                        inList = true;
                    }
                if (!inList)
                    _dynRef.SetProperty (string.Empty);


                //  Draw properties.
                propIndex = EditorGUI.Popup (GetRect (), "Property", propIndex, _dropdown.ToArray ());

                if (propIndex != 0) {

                    _dynRef.SetProperty (_dropdown [propIndex]);
                    //  Property Validation.
                    if (!_dynRef.Types.ContainsProperty (type.ToString (), _dynRef.Property)) {
                        EditorGUI.BeginChangeCheck ();
                        EditorGUI.HelpBox (
                            position: GetRect (),
                            message: "This property does not exist.",
                            type: MessageType.Error
                        );
                        GUI.Button (GetRect (), "Register Property");

                        //  If button is pressed.
                        if (EditorGUI.EndChangeCheck ()) {
                            Registrator.RegisterType(
                                _dynRef.Component.GetType (),
                                _dynRef.Types
                            );
                        }

                        //  Update properties values.
                        StoreData (property, dynProperty);
                        return;
                    }

                    switch (_dynRef.DynVar.Type) {

                        case DataType.NULL:
                            EditorGUI.LabelField (GetRect (), Text + " null");
                            break;

                        case DataType.BOOLEAN:
                            _dynRef.SetValue (EditorGUI.Toggle (GetRect (), Text, _dynRef.DynVar.Boolean));
                            break;

                        case DataType.INTEGER:
                            _dynRef.SetValue (EditorGUI.IntField (GetRect (), Text, _dynRef.DynVar.Integer));
                            break;

                        case DataType.FLOAT:
                            _dynRef.SetValue (EditorGUI.FloatField (GetRect (), Text, _dynRef.DynVar.Float));
                            break;

                        case DataType.DOUBLE:
                            _dynRef.SetValue (EditorGUI.DoubleField (GetRect (), Text, _dynRef.DynVar.Double));
                            break;

                        case DataType.VECTOR2:
                            _dynRef.SetValue (EditorGUI.Vector2Field (GetRect (), Text, _dynRef.DynVar.Vector2));
                            break;

                        case DataType.VECTOR3:
                            _dynRef.SetValue (EditorGUI.Vector3Field (GetRect (), Text, _dynRef.DynVar.Vector3));
                            break;

                        case DataType.VECTOR4:
                            _dynRef.SetValue (EditorGUI.Vector4Field (GetRect (), Text, _dynRef.DynVar.Vector4));
                            break;

                        case DataType.QUATERNION:
                            Vector4 aux = EditorGUI.Vector4Field (GetRect (), Text, _dynRef.DynVar.Vector4);
                            _dynRef.SetValue (new Quaternion (aux.x, aux.y, aux.z, aux.w));
                            break;

                        case DataType.COLOR:
                            _dynRef.SetValue (EditorGUI.ColorField (GetRect (), Text, _dynRef.DynVar.Color));
                            break;

                        case DataType.STRING:
                            _dynRef.SetValue (EditorGUI.TextField (GetRect (), Text, _dynRef.DynVar.String));
                            break;

                        case DataType.CURVE:
                            _dynRef.SetValue (EditorGUI.CurveField (GetRect (), Text, _dynRef.DynVar.Curve));
                            break;

                        case DataType.ASSET:
                            _dynRef.SetValue (EditorGUI.ObjectField (
                                GetRect (),
                                Text,
                                _dynRef.DynVar.Asset,
                                _dynRef.DynVar.Asset != null ? _dynRef.DynVar.Asset.GetType () : typeof (Object),
                                true
                            ));
                            break;
                    }
                } else
                    _dynRef.SetProperty (string.Empty);
            } else
                _dynRef.SetProperty (string.Empty);

            //  Update properties values.
            StoreData (property, dynProperty);
        }

        #endregion



        #region Class Implementation

        /// <summary> Stores the data. </summary>
        private void StoreData (SerializedProperty property, SerializedProperty dynProperty) {

            property.FindPropertyRelative ("component").objectReferenceValue = _dynRef.Component;
            property.FindPropertyRelative ("property").stringValue = _dynRef.Property;
            dynProperty.FindPropertyRelative ("stringValue").stringValue = _dynRef.DynVar.String;
            dynProperty.FindPropertyRelative ("curve").animationCurveValue = _dynRef.DynVar.Curve;
            dynProperty.FindPropertyRelative ("asset").objectReferenceValue = _dynRef.DynVar.Asset;
            dynProperty.FindPropertyRelative ("vector").vector4Value = _dynRef.DynVar.Vector4;
            dynProperty.FindPropertyRelative ("type").enumValueIndex = (int)_dynRef.DynVar.Type;

            //  Assigning count of fields shown.
            _count = _rectIndex;
        }

        //  Returns the follow rect to draw.
        private Rect GetRect () {
            Rect rect = _rects [_rectIndex];
            _rectIndex++;
            return rect;
        }

        #endregion
    }
}