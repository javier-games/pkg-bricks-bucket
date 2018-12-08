using System.Reflection;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Framework.Generics {

    #if UNITY_EDITOR
    /// <summary>
    /// Dyn reference drawer.
    /// 
    /// Drawer for the DynVar Class.
    /// By Javier García.
    /// </summary>
    [CustomPropertyDrawer (typeof (DynRef))]
    public class DynRefDrawer : PropertyDrawer {



        #region Class Members

        //  Height of a standard singleline.
        private readonly float fieldSize = EditorGUIUtility.singleLineHeight;

        //  Array of possible rects.
        private readonly Rect [] rects = new Rect [5];

        //  References of a dynamic variable.
        private readonly DynRef dynRef = new DynRef ();

        //  List that the stores the properties and components.
        private readonly List<string> dropdown = new List<string> ();


        private PropertyInfo [] propertiesInfo;      //  Reference to proertiesInfo.
        private Component [] components;             //  Reference to components.
        private int rectIndex;                      //  Curren count of rects.
        private int count;                          //  Count of fields.
        private StreamWriter writer;                //  Writer for RegisteredTypes.cs

        private const float padding = 4;            //  Size of padding.
        private const string text = "Value";        //  Text to show in value.

        #endregion



        #region Superclass Overrides

        //  Returns the height of the property.
        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            return (fieldSize * count) + (padding * (count + 1));
        }

        //  Called to draw in gui.
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

            //  Declaration of rects.
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            rectIndex = 0;
            rects [0] = new Rect (
                x: position.x,
                y: position.y + padding,
                width: position.width,
                height: fieldSize
            );
            rects [1] = new Rect (
                x: position.x,
                y: position.y + fieldSize + (padding * 2),
                width: position.width,
                height: fieldSize
            );
            rects [2] = new Rect (
                x: position.x,
                y: position.y + (fieldSize * 2) + (padding * 2),
                width: position.width,
                height: fieldSize
            );
            rects [3] = new Rect (
                x: position.x,
                y: position.y + (fieldSize * 3) + (padding * 2),
                width: position.width,
                height: fieldSize
            );
            rects [4] = new Rect (
                x: position.x,
                y: position.y + (fieldSize * 4) + (padding * 2),
                width: position.width,
                height: fieldSize
            );
            EditorGUI.indentLevel = indent;


            //  Getting the properties references.
            SerializedProperty dynProperty = property.FindPropertyRelative ("m_Value");
            dynRef.SetReference (property.FindPropertyRelative ("m_Component").objectReferenceValue);
            dynRef.SetProperty (property.FindPropertyRelative ("m_Property").stringValue);

            //  Drawing object field.
            dynRef.SetReference (EditorGUI.ObjectField (
                position: GetRect (),
                label: "Component",
                obj: dynRef.Component,
                objType: dynRef.Component != null ?
                                    dynRef.Component.GetType () :
                                    typeof (Object),
                allowSceneObjects: true
            ));

            //  If component is different to null.
            if (dynRef.Component != null) {

                //  Shortcuts.
                Object objectValue = dynRef.Component;
                System.Type type = objectValue.GetType ();


                //  Component Validation.
                if (!RegisteredTypes.ContainsComponent (type.ToString ())) {

                    EditorGUI.BeginChangeCheck ();
                    EditorGUI.HelpBox (
                        position: GetRect (),
                        message: "This component type is not registered.",
                        type: MessageType.Error
                    );
                    GUI.Button (GetRect (), "Register Type");

                    //  If button is pressed.
                    if (EditorGUI.EndChangeCheck ()) {
                        RegisteredTypesCreator.RegisterType (dynRef.Component.GetType ());
                    }

                    //  Update properties values.
                    StoreData (property, dynProperty);
                    return;
                }


                //  Component Selection if object is a GameObject.
                if (type == typeof (GameObject)) {

                    components = (objectValue as GameObject).GetComponents<Component> ();
                    dropdown.Clear ();
                    dropdown.Add ("As GameObject");
                    foreach (Component component in components)
                        dropdown.Add (component.GetType ().Name);

                    int compIndex = 0;
                    compIndex = EditorGUI.Popup (GetRect (), "Select", compIndex, dropdown.ToArray ());
                    if (compIndex != 0)
                        dynRef.SetReference (components [compIndex - 1]);
                }


                //  Property Selection.
                propertiesInfo = type.GetProperties (BindingFlags.Public | BindingFlags.Instance);
                dropdown.Clear ();
                dropdown.Add ("- None -");
                foreach (PropertyInfo info in propertiesInfo)
                    if (info.CanRead && info.CanWrite && !info.IsDefined (typeof (System.ObsoleteAttribute), true))
                        dropdown.Add (info.Name);

                int propIndex = 0;
                bool inList = false;
                for (int i = 0; i < dropdown.Count; i++)
                    if (property.FindPropertyRelative ("m_Property").stringValue == dropdown [i]) {
                        propIndex = i;
                        inList = true;
                    }
                if (!inList)
                    dynRef.SetProperty (string.Empty);


                //  Draw properties.
                propIndex = EditorGUI.Popup (GetRect (), "Property", propIndex, dropdown.ToArray ());

                if (propIndex != 0) {

                    dynRef.SetProperty (dropdown [propIndex]);
                    //  Property Validation.
                    if (!RegisteredTypes.ContainsProperty (type.ToString (), dynRef.Property)) {
                        EditorGUI.BeginChangeCheck ();
                        EditorGUI.HelpBox (
                            position: GetRect (),
                            message: "This property does not exist.",
                            type: MessageType.Error
                        );
                        GUI.Button (GetRect (), "Register Property");

                        //  If button is pressed.
                        if (EditorGUI.EndChangeCheck ()) {
                            RegisteredTypesCreator.RegisterType (dynRef.Component.GetType ());
                        }

                        //  Update properties values.
                        StoreData (property, dynProperty);
                        return;
                    }

                    switch (dynRef.DynVar.Type) {

                        case DataType.Null:
                            EditorGUI.LabelField (GetRect (), text + " null");
                            break;

                        case DataType.Boolean:
                            dynRef.SetValue (EditorGUI.Toggle (GetRect (), text, dynRef.DynVar.Boolean));
                            break;

                        case DataType.Integer:
                            dynRef.SetValue (EditorGUI.IntField (GetRect (), text, dynRef.DynVar.Integer));
                            break;

                        case DataType.Float:
                            dynRef.SetValue (EditorGUI.FloatField (GetRect (), text, dynRef.DynVar.Float));
                            break;

                        case DataType.Double:
                            dynRef.SetValue (EditorGUI.DoubleField (GetRect (), text, dynRef.DynVar.Double));
                            break;

                        case DataType.Vector2:
                            dynRef.SetValue (EditorGUI.Vector2Field (GetRect (), text, dynRef.DynVar.Vector2));
                            break;

                        case DataType.Vector3:
                            dynRef.SetValue (EditorGUI.Vector3Field (GetRect (), text, dynRef.DynVar.Vector3));
                            break;

                        case DataType.Vector4:
                            dynRef.SetValue (EditorGUI.Vector4Field (GetRect (), text, dynRef.DynVar.Vector4));
                            break;

                        case DataType.Quaternion:
                            Vector4 aux = EditorGUI.Vector4Field (GetRect (), text, dynRef.DynVar.Vector4);
                            dynRef.SetValue (new Quaternion (aux.x, aux.y, aux.z, aux.w));
                            break;

                        case DataType.Color:
                            dynRef.SetValue (EditorGUI.ColorField (GetRect (), text, dynRef.DynVar.Color));
                            break;

                        case DataType.String:
                            dynRef.SetValue (EditorGUI.TextField (GetRect (), text, dynRef.DynVar.String));
                            break;

                        case DataType.Curve:
                            dynRef.SetValue (EditorGUI.CurveField (GetRect (), text, dynRef.DynVar.Curve));
                            break;

                        case DataType.Asset:
                            dynRef.SetValue (EditorGUI.ObjectField (
                                GetRect (),
                                text,
                                dynRef.DynVar.Asset,
                                dynRef.DynVar.Asset != null ? dynRef.DynVar.Asset.GetType () : typeof (Object),
                                true
                            ));
                            break;
                    }
                } else
                    dynRef.SetProperty (string.Empty);
            } else
                dynRef.SetProperty (string.Empty);

            //  Update properties values.
            StoreData (property, dynProperty);
        }

        #endregion



        #region Class Implementation

        /// <summary> Stores the data. </summary>
        private void StoreData (SerializedProperty property, SerializedProperty dynProperty) {

            property.FindPropertyRelative ("m_Component").objectReferenceValue = dynRef.Component;
            property.FindPropertyRelative ("m_Property").stringValue = dynRef.Property;
            dynProperty.FindPropertyRelative ("m_String").stringValue = dynRef.DynVar.String;
            dynProperty.FindPropertyRelative ("m_Curve").animationCurveValue = dynRef.DynVar.Curve;
            dynProperty.FindPropertyRelative ("m_Object").objectReferenceValue = dynRef.DynVar.Asset;
            dynProperty.FindPropertyRelative ("m_Vector").vector4Value = dynRef.DynVar.Vector4;
            dynProperty.FindPropertyRelative ("m_Type").enumValueIndex = (int)dynRef.DynVar.Type;

            //  Assigning count of fields shown.
            count = rectIndex;
        }

        //  Returns the follow rect to draw.
        private Rect GetRect () {
            Rect rect = rects [rectIndex];
            rectIndex++;
            return rect;
        }

        #endregion
    }
    #endif
}