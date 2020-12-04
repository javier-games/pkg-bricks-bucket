using UnityEngine;
using UnityEditor;

namespace Framework.Generics
{

    /// <summary>
    /// Dyn variable drawer.
    /// 
    /// Drawer for the DynVar Class.
    /// By Javier García.
    /// </summary>
    [CustomPropertyDrawer(typeof(DynVar))]
    public class DynVarDrawer : PropertyDrawer
    {

        #region Class Members

        //  Height of a standard singleline.
        private readonly float _fieldSize = EditorGUIUtility.singleLineHeight;

        //  References of a dynamic variable.
        private readonly DynVar _dynVar = new DynVar();

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
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            Rect rectType = new Rect(
                x: position.x,
                y: position.y + Padding,
                width: position.width,
                height: _fieldSize
            );
            Rect rectValue = new Rect(
                x: position.x,
                y: position.y + _fieldSize + (Padding * 2),
                width: position.width,
                height: _fieldSize
            );
            EditorGUI.indentLevel = indent;

            //  Getting the properties references.
            _dynVar.Type = (DataType) property.FindPropertyRelative("type")
                .enumValueIndex;
            _dynVar.Vector4 =
                property.FindPropertyRelative("vector").vector4Value;
            _dynVar.Asset = property.FindPropertyRelative("asset")
                .objectReferenceValue;
            _dynVar.Curve = property.FindPropertyRelative("curve")
                .animationCurveValue;
            _dynVar.String =
                property.FindPropertyRelative("stringValue").stringValue;

            //  Draw properties.
            _dynVar.Type =
                (DataType) EditorGUI.EnumPopup(rectType, "DynVar",
                    _dynVar.Type);
            switch (_dynVar.Type)
            {

                case DataType.NULL:
                    EditorGUI.LabelField(rectValue, Text + " null");
                    break;

                case DataType.BOOLEAN:
                    _dynVar.Boolean =
                        EditorGUI.Toggle(rectValue, Text, _dynVar.Boolean);
                    break;

                case DataType.INTEGER:
                    _dynVar.Integer =
                        EditorGUI.IntField(rectValue, Text, _dynVar.Integer);
                    break;

                case DataType.FLOAT:
                    _dynVar.Float =
                        EditorGUI.FloatField(rectValue, Text, _dynVar.Float);
                    break;

                case DataType.DOUBLE:
                    _dynVar.Double =
                        EditorGUI.DoubleField(rectValue, Text, _dynVar.Double);
                    break;

                case DataType.VECTOR2:
                    _dynVar.Vector2 =
                        EditorGUI.Vector2Field(rectValue, Text,
                            _dynVar.Vector2);
                    break;

                case DataType.VECTOR3:
                    _dynVar.Vector3 =
                        EditorGUI.Vector3Field(rectValue, Text,
                            _dynVar.Vector3);
                    break;

                case DataType.VECTOR4:
                    _dynVar.Vector4 =
                        EditorGUI.Vector4Field(rectValue, Text,
                            _dynVar.Vector4);
                    break;

                case DataType.QUATERNION:
                    _dynVar.Vector4 =
                        EditorGUI.Vector4Field(rectValue, Text,
                            _dynVar.Vector4);
                    break;

                case DataType.COLOR:
                    _dynVar.Color =
                        EditorGUI.ColorField(rectValue, Text, _dynVar.Color);
                    break;

                case DataType.STRING:
                    _dynVar.String =
                        EditorGUI.TextField(rectValue, Text, _dynVar.String);
                    break;

                case DataType.CURVE:
                    _dynVar.Curve =
                        EditorGUI.CurveField(rectValue, Text, _dynVar.Curve);
                    break;

                case DataType.ASSET:
                    _dynVar.Asset = EditorGUI.ObjectField(
                        rectValue,
                        Text,
                        _dynVar.Asset,
                        _dynVar.Asset != null
                            ? _dynVar.Asset.GetType()
                            : typeof(Object),
                        true
                    );
                    break;
            }

            //  Update properties values..
            property.FindPropertyRelative("stringValue").stringValue =
                _dynVar.String;
            property.FindPropertyRelative("curve").animationCurveValue =
                _dynVar.Curve;
            property.FindPropertyRelative("object").objectReferenceValue =
                _dynVar.Asset;
            property.FindPropertyRelative("vector").vector4Value =
                _dynVar.Vector4;
            property.FindPropertyRelative("type").enumValueIndex =
                (int) _dynVar.Type;

        }

        #endregion
    }
}