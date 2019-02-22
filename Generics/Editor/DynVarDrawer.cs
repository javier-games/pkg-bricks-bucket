using UnityEngine;
using UnityEditor;

namespace Framework.Generics {

    #if UNITY_EDITOR
    /// <summary>
    /// Dyn variable drawer.
    /// 
    /// Drawer for the DynVar Class.
    /// By Javier García.
    /// </summary>
    [CustomPropertyDrawer (typeof (DynVar))]
    public class DynVarDrawer : PropertyDrawer {



        #region Class Members

        //  Height of a standard singleline.
        private readonly float fieldSize = EditorGUIUtility.singleLineHeight;

        //  References of a dynamic variable.
        private readonly DynVar dynVar = new DynVar ();


        private bool vectorExtraSpace;              //  Flag to add extra espace.


        private const int count = 2;          //  Count of fields to draw.
        private const float padding = 4;        //  Size of padding.
        private const float minWidth = 314;     //  Minimum width fore vectors.
        private const string text = "Value";     //  Text to show in value.

        #endregion



        #region Superclass Overrides

        //  Returns the height of the property.
        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            return vectorExtraSpace ?
                (fieldSize * (count + 1)) + (padding * (count + 2)) :
                (fieldSize * count) + (padding * (count + 1));
        }

        //  Called to draw in gui.
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

            //  For vector properties the widht modifies the height.
            vectorExtraSpace = position.width < minWidth;

            //  Declaration of rects.
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            Rect rectType = new Rect (
                x: position.x,
                y: position.y + padding,
                width: position.width,
                height: fieldSize
            );
            Rect rectValue = new Rect (
                x: position.x,
                y: position.y + fieldSize + (padding * 2),
                width: position.width,
                height: fieldSize
            );
            EditorGUI.indentLevel = indent;

            //  Getting the properties references.
            dynVar.Type = (DataType)property.FindPropertyRelative ("_type").enumValueIndex;
            dynVar.Vector4 = property.FindPropertyRelative ("_vector").vector4Value;
            dynVar.Asset = property.FindPropertyRelative ("_object").objectReferenceValue;
            dynVar.Curve = property.FindPropertyRelative ("_curve").animationCurveValue;
            dynVar.String = property.FindPropertyRelative ("_string").stringValue;

            //  Draw properties.
            dynVar.Type = (DataType)EditorGUI.EnumPopup (rectType, "DynVar", dynVar.Type);
            switch (dynVar.Type) {

                case DataType.Null:
                    EditorGUI.LabelField (rectValue, text + " null");
                    break;

                case DataType.Boolean:
                    dynVar.Boolean = EditorGUI.Toggle (rectValue, text, dynVar.Boolean);
                    break;

                case DataType.Integer:
                    dynVar.Integer = EditorGUI.IntField (rectValue, text, dynVar.Integer);
                    break;

                case DataType.Float:
                    dynVar.Float = EditorGUI.FloatField (rectValue, text, dynVar.Float);
                    break;

                case DataType.Double:
                    dynVar.Double = EditorGUI.DoubleField (rectValue, text, dynVar.Double);
                    break;

                case DataType.Vector2:
                    dynVar.Vector2 = EditorGUI.Vector2Field (rectValue, text, dynVar.Vector2);
                    break;

                case DataType.Vector3:
                    dynVar.Vector3 = EditorGUI.Vector3Field (rectValue, text, dynVar.Vector3);
                    break;

                case DataType.Vector4:
                    dynVar.Vector4 = EditorGUI.Vector4Field (rectValue, text, dynVar.Vector4);
                    break;

                case DataType.Quaternion:
                    dynVar.Vector4 = EditorGUI.Vector4Field (rectValue, text, dynVar.Vector4);
                    break;

                case DataType.Color:
                    dynVar.Color = EditorGUI.ColorField (rectValue, text, dynVar.Color);
                    break;

                case DataType.String:
                    dynVar.String = EditorGUI.TextField (rectValue, text, dynVar.String);
                    break;

                case DataType.Curve:
                    dynVar.Curve = EditorGUI.CurveField (rectValue, text, dynVar.Curve);
                    break;

                case DataType.Asset:
                    dynVar.Asset = EditorGUI.ObjectField (
                        rectValue,
                        text,
                        dynVar.Asset,
                        dynVar.Asset != null ? dynVar.Asset.GetType () : typeof (Object),
                        true
                    );
                    break;
            }

            //  Update properties values..
            property.FindPropertyRelative ("_string").stringValue = dynVar.String;
            property.FindPropertyRelative ("_curve").animationCurveValue = dynVar.Curve;
            property.FindPropertyRelative ("_object").objectReferenceValue = dynVar.Asset;
            property.FindPropertyRelative ("_vector").vector4Value = dynVar.Vector4;
            property.FindPropertyRelative ("_type").enumValueIndex = (int)dynVar.Type;

        }
        #endregion
    }
    #endif
}