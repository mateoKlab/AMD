using UnityEngine;
using UnityEditor;

namespace BingoEditor
{
    [CustomEditor(typeof(Transform))]
    public class TransformInspector : Editor
    {
        public override void OnInspectorGUI()
        {

            Transform t = (Transform)target;

            // Replicate the standard transform inspector gui
            EditorGUIUtility.LookLikeControls();
            EditorGUI.indentLevel = 0;
            ////Vector3 position = EditorGUILayout.Vector3Field("P", t.localPosition);
            Vector3 position;
            Vector3 rotation;
            Vector3 scale;

            EditorGUILayout.BeginHorizontal();

            if (EditorTools.DrawButton("P", "Reset Position", IsResetPositionValid(t), GUILayout.Width(20f)))
            {
                Undo.RecordObject(t, "Reset Position");
                t.localPosition = Vector3.zero;
            }
            position = EditorGUILayout.Vector3Field("", t.localPosition, GUILayout.MaxHeight(0));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (EditorTools.DrawButton("R", "Reset Rotation", IsResetRotationValid(t), GUILayout.Width(20f)))
            {
                Undo.RecordObject(t, "Reset Rotation");
                t.localEulerAngles = Vector3.zero;
            }
            rotation = EditorGUILayout.Vector3Field("", t.localEulerAngles, GUILayout.MaxHeight(0));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (EditorTools.DrawButton("S", "Reset Scale", IsResetScaleValid(t), GUILayout.Width(20f)))
            {
                Undo.RecordObject(t, "Reset Scale");
                t.localScale = Vector3.one;
            }
            scale = EditorGUILayout.Vector3Field("", t.localScale, GUILayout.MaxHeight(0));

            EditorGUILayout.EndHorizontal();

            EditorGUIUtility.labelWidth = 0;
            EditorGUIUtility.fieldWidth = 0;

            if (GUI.changed)
            {
                Undo.RecordObject(t, "Transform Change");

                t.localPosition = FixIfNaN(position);
                t.localEulerAngles = FixIfNaN(rotation);
                t.localScale = FixIfNaN(scale);
            }
        }

        /*static bool DrawButton (string title, string tooltip, bool enabled, float width) {
            if (enabled) {
                // Draw a regular button
                return GUILayout.Button(new GUIContent(title, tooltip), GUILayout.Width(width));
            } else {
                // Button should be disabled -- draw it darkened and ignore its return value
                Color color = GUI.color;
                GUI.color = new Color(1f, 1f, 1f, 0.25f);
                GUILayout.Button(new GUIContent(title, tooltip), GUILayout.Width(width));
                GUI.color = color;
                return false;
            }
        }*/

        static bool IsResetPositionValid(Transform targetTransform)
        {
            Vector3 v = targetTransform.localPosition;
            return (v.x != 0f || v.y != 0f || v.z != 0f);
        }

        static bool IsResetRotationValid(Transform targetTransform)
        {
            Vector3 v = targetTransform.localEulerAngles;
            return (v.x != 0f || v.y != 0f || v.z != 0f);
        }

        static bool IsResetScaleValid(Transform targetTransform)
        {
            Vector3 v = targetTransform.localScale;
            return (v.x != 1f || v.y != 1f || v.z != 1f);
        }

        private Vector3 FixIfNaN(Vector3 v)
        {
            if (float.IsNaN(v.x))
            {
                v.x = 0;
            }
            if (float.IsNaN(v.y))
            {
                v.y = 0;
            }
            if (float.IsNaN(v.z))
            {
                v.z = 0;
            }
            return v;
        }

    }
}