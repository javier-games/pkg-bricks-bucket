using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Framework.Generics
{

    /// <summary>
    /// Dyn variable drawer.
    /// 
    /// Drawer for the DynVariable Class.
    /// By Javier García.
    /// </summary>
    [CustomPropertyDrawer(typeof(DynVariable))]
    public class DynVarDrawer : PropertyDrawer
    {

        #region Class Members

        //  Height of a standard single line.
        private readonly float _fieldSize = EditorGUIUtility.singleLineHeight;

        //  References of a dynamic variable.
        private readonly DynVariable _dynVariable = new DynVariable();

        private bool _vectorExtraSpace;

        //  Count of fields to draw.
        private const int Count = 2;

        //  Size of padding.
        private const float Padding = 4;

        //  Minimum width fore vectors.
        private const float MinWidth = 314;

        //  Text to show in value.
        private const string Text = "Value";

        #endregion



        #region Superclass Overrides

        //  Returns the height of the property.
        public override float GetPropertyHeight(SerializedProperty property,
            GUIContent label)
        {
            return _vectorExtraSpace
                ? (_fieldSize * (Count + 1)) + (Padding * (Count + 2))
                : (_fieldSize * Count) + (Padding * (Count + 1));
        }

        //  Called to draw in gui.
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {

            //  For vector properties the width modifies the height.
            _vectorExtraSpace = position.width < MinWidth;

            //  Declaration of rects.
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var rectType = new Rect(
                x: position.x,
                y: position.y + Padding,
                width: position.width,
                height: _fieldSize
            );
            var rectValue = new Rect(
                x: position.x,
                y: position.y + _fieldSize + (Padding * 2),
                width: position.width,
                height: _fieldSize
            );
            EditorGUI.indentLevel = indent;

            //  Getting the properties references.
            _dynVariable.Type = (DataType) property.FindPropertyRelative("type")
                .enumValueIndex;
            _dynVariable.Vector4 =
                property.FindPropertyRelative("vector").vector4Value;
            _dynVariable.Asset = property.FindPropertyRelative("asset")
                .objectReferenceValue;
            _dynVariable.Curve = property.FindPropertyRelative("curve")
                .animationCurveValue;
            _dynVariable.String =
                property.FindPropertyRelative("stringValue").stringValue;

            //  Draw properties.
            _dynVariable.Type =
                (DataType) EditorGUI.EnumPopup(rectType, "DynVariable",
                    _dynVariable.Type);
            switch (_dynVariable.Type)
            {

                case DataType.NULL:
                    EditorGUI.LabelField(rectValue, Text + " null");
                    break;

                case DataType.BOOLEAN:
                    _dynVariable.Boolean =
                        EditorGUI.Toggle(rectValue, Text, _dynVariable.Boolean);
                    break;

                case DataType.INTEGER:
                    _dynVariable.Integer =
                        EditorGUI.IntField(rectValue, Text,
                            _dynVariable.Integer);
                    break;

                case DataType.FLOAT:
                    _dynVariable.Float =
                        EditorGUI.FloatField(rectValue, Text,
                            _dynVariable.Float);
                    break;

                case DataType.DOUBLE:
                    _dynVariable.Double =
                        EditorGUI.DoubleField(rectValue, Text,
                            _dynVariable.Double);
                    break;

                case DataType.VECTOR2:
                    _dynVariable.Vector2 =
                        EditorGUI.Vector2Field(rectValue, Text,
                            _dynVariable.Vector2);
                    break;

                case DataType.VECTOR3:
                    _dynVariable.Vector3 =
                        EditorGUI.Vector3Field(rectValue, Text,
                            _dynVariable.Vector3);
                    break;

                case DataType.VECTOR4:
                    _dynVariable.Vector4 =
                        EditorGUI.Vector4Field(rectValue, Text,
                            _dynVariable.Vector4);
                    break;

                case DataType.QUATERNION:
                    _dynVariable.Vector4 =
                        EditorGUI.Vector4Field(rectValue, Text,
                            _dynVariable.Vector4);
                    break;

                case DataType.COLOR:
                    _dynVariable.Color =
                        EditorGUI.ColorField(rectValue, Text,
                            _dynVariable.Color);
                    break;

                case DataType.STRING:
                    _dynVariable.String =
                        EditorGUI.TextField(rectValue, Text,
                            _dynVariable.String);
                    break;

                case DataType.CURVE:
                    _dynVariable.Curve =
                        EditorGUI.CurveField(rectValue, Text,
                            _dynVariable.Curve);
                    break;

                case DataType.ASSET:
                    _dynVariable.Asset = EditorGUI.ObjectField(
                        rectValue,
                        Text,
                        _dynVariable.Asset,
                        _dynVariable.Asset != null
                            ? _dynVariable.Asset.GetType()
                            : typeof(Object),
                        true
                    );
                    break;

                default: throw new System.SystemException();
            }

            //  Update properties values..
            property.FindPropertyRelative("stringValue").stringValue =
                _dynVariable.String;
            property.FindPropertyRelative("curve").animationCurveValue =
                _dynVariable.Curve;
            property.FindPropertyRelative("object").objectReferenceValue =
                _dynVariable.Asset;
            property.FindPropertyRelative("vector").vector4Value =
                _dynVariable.Vector4;
            property.FindPropertyRelative("type").enumValueIndex =
                (int) _dynVariable.Type;

        }

        #endregion
    }
}