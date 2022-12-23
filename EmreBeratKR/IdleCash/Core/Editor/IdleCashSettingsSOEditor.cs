using UnityEditor;
using UnityEngine;
using EmreBeratKR.IdleCash.Creator;

namespace EmreBeratKR.IdleCash.Editor
{
    [CustomEditor(typeof(IdleCashSettingsSO))]
    public class IdleCashSettingsSOEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space();

            GUI.enabled = false;
            
            var typesProperty = serializedObject.FindProperty("m_Types");
            EditorGUILayout.PropertyField(typesProperty);

            GUI.enabled = true;
            
            EditorGUILayout.Space();

            if (GUILayout.Button("Reset to Default"))
            {
                IdleCashSettingsSO.ResetSettingsToDefault();
            }
        }
    }
}