using EmreBeratKR.IdleCash.Creator;
using UnityEditor;
using UnityEngine;

namespace EmreBeratKR.IdleCash.Editor
{
    [CustomPropertyDrawer(typeof(IdleCash))]
    public class IdleCashDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            var valueRect = new Rect(position.x, position.y, position.width * 0.69f, position.height);
            var typeRect = new Rect(position.x + position.width * 0.7f, position.y, position.width * 0.3f, position.height);

            var valueProperty = property.FindPropertyRelative("value");
            var typeProperty = property.FindPropertyRelative("type");
            
            var target = new IdleCash(valueProperty.floatValue, typeProperty.stringValue);
            valueProperty.floatValue = target.value;
            typeProperty.stringValue = target.type;

            EditorGUI.PropertyField(valueRect, valueProperty, label);
            var selectedIndex = IdleCashSettingsSO.GetTypeIndex(typeProperty.stringValue);
            var types = IdleCashSettingsSO.Types.ToArray();
            types[0] = types[0] == "" ? "-" : types[0];
            var index = EditorGUI.Popup(typeRect, selectedIndex, types);
            typeProperty.stringValue = types[index] == "-" ? "" : types[index];

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}