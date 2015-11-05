using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Bingo;

namespace BingoEditor
{
    [InitializeOnLoad]
    public class MessengerEventTagEditorWindow : EditorWindow
    {
        private static MessengerData messengerData;
        private static SerializedObject messengerDataSerializedObject;

        private ReorderableList list;
        private Vector2 scrollPosition = Vector2.zero;
        private GUIStyle boxStyle;

        private GUIContent generateFileContent;
        private GUIContent createContent;

        private MessengerData previousData;

        private Regex pattern = new Regex("[^a-zA-Z0-9 -]");
        private static readonly string path = string.Concat(InternalConstants.DATA_PATH, "EventTags.cs");

        static MessengerEventTagEditorWindow()
        {
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
        }

        [MenuItem("Bingo/Event Tags... %e", false, 2000)]
        private static void Init()
        {
            MessengerEventTagEditorWindow window = EditorWindow.GetWindow<MessengerEventTagEditorWindow>(true, "Event Tags");
            window.Show();
        }

        private void OnEnable()
        {
            LoadAsset();
            if (messengerData == null)
            {
                CreateAsset();
            }

            messengerDataSerializedObject = new SerializedObject(messengerData);

            list = new ReorderableList(messengerDataSerializedObject,
                    messengerDataSerializedObject.FindProperty("eventTypes"),
                    true, true, true, true);

            list.drawHeaderCallback += DrawHeader;
            list.drawElementCallback += DrawElement;

            list.onAddCallback += AddItem;
            list.onRemoveCallback += RemoveItem;

            generateFileContent = new GUIContent("Generate Code");
            createContent = new GUIContent("+", "Create a new MessengerData asset");

            WriteCodeFile("EventTags");
        }

        private void OnDisable()
        {
            list.drawElementCallback -= DrawElement;

            list.onAddCallback -= AddItem;
            list.onRemoveCallback -= RemoveItem;
        }

        private void ChangeData(MessengerData newData)
        {
            messengerDataSerializedObject = newData != null? new SerializedObject(newData) : null;

            if (messengerDataSerializedObject != null)
            {
                list = new ReorderableList(messengerDataSerializedObject,
                        messengerDataSerializedObject.FindProperty("eventTypes"),
                        true, true, true, true);
            }
            else
            {
                list = new ReorderableList(new System.Collections.Generic.List<string>(), typeof(string),
                    true, true, true, true);
            }


            list.drawHeaderCallback += DrawHeader;
            list.drawElementCallback += DrawElement;

            list.onAddCallback += AddItem;
            list.onRemoveCallback += RemoveItem;
        }

        private void OnDestroy()
        {
            //WriteCodeFile();
        }

        private static void CreateAsset(string path = null)
        {
            messengerData = ScriptableObject.CreateInstance<MessengerData>();
            AssetDatabase.CreateAsset(messengerData, path?? MessengerData.ASSET_PATH);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = messengerData;
        }

        private static void LoadAsset()
        {
            UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(MessengerData.ASSET_PATH, typeof(MessengerData));
            messengerData = (MessengerData)asset;
        }

        private static void ReplaceAsset()
        {
            MessengerData temp = messengerData;
            messengerData = ScriptableObject.CreateInstance<MessengerData>();
            messengerData.eventTypes = temp.eventTypes.ConvertAll(e => e.Clone() as MessengerEventTag);
        }

        private void OnGUI()
        {
            if (messengerDataSerializedObject != null)
            {
                messengerDataSerializedObject.Update();
            }

            boxStyle = new GUIStyle();
            boxStyle.padding = new RectOffset(5, 5, 5, 5);

            EditorGUILayout.BeginVertical(boxStyle);

            EditorGUILayout.BeginHorizontal();

            previousData = messengerData;
            messengerData = EditorGUILayout.ObjectField("Asset", messengerData, typeof(MessengerData), false) as MessengerData;

            if (GUILayout.Button(createContent, GUILayout.Width(25f)))
            {
                var path = EditorUtility.SaveFilePanelInProject("Create new asset", string.Empty, "asset", string.Empty);
                if (path.Length != 0)
                {
                    EditorApplication.delayCall += () => OnAssetCreate(path);
                }
            }

            if (previousData != messengerData)
            {
                ChangeData(messengerData);
                previousData = messengerData;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            GUI.enabled = messengerDataSerializedObject != null;

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
            list.DoLayoutList();
            GUILayout.EndScrollView();

            if (GUILayout.Button(generateFileContent))
            {
                WriteCodeFile(messengerData.name);
            }
            EditorGUILayout.EndVertical();

            if (messengerDataSerializedObject != null)
            {
                messengerDataSerializedObject.ApplyModifiedProperties();
            }
        }

        private void OnAssetCreate(string path)
        {
            CreateAsset(path);
            ChangeData(messengerData);
            previousData = messengerData;
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Event Tags");
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, rect.height - 3), element.FindPropertyRelative("name"), GUIContent.none);
        }

        private void AddItem(ReorderableList list)
        {
            MessengerEventTag e = new MessengerEventTag();
            messengerData.eventTypes.Add(e);
            EditorUtility.SetDirty(messengerData);

        }

        private void RemoveItem(ReorderableList list)
        {
            messengerData.eventTypes.RemoveAt(list.index);
            EditorUtility.SetDirty(messengerData);
        }

        private void WriteCodeFile(string name)
        {

            if (!Directory.Exists(InternalConstants.DATA_PATH))
            {
                Directory.CreateDirectory(InternalConstants.DATA_PATH);
            }

            string path = AssetDatabase.GetAssetPath(messengerData).Replace(".asset", ".cs");

            try
            {
                // opens the file if it allready exists, creates it otherwise
                using (FileStream stream = File.Open(path,
                    FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine("// ----- AUTO GENERATED CODE ----- //");
                        builder.AppendLine("public enum " + name);
                        builder.AppendLine("{");
                        foreach (MessengerEventTag tag in messengerData.eventTypes)
                        {
                            if (!string.IsNullOrEmpty(tag.name.Trim()))
                            {
                                string key = pattern.Replace(tag.name, "").Replace(' ', '_').ToUpper();
                                builder.AppendLine(string.Format("    {0},", key));
                            }
                        }
                        builder.AppendLine("    DEFAULT");
                        builder.AppendLine("}");
                        writer.Write(builder.ToString());
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);

                // if we have an error, it is certainly that the file is screwed up. Delete to be save
                if (File.Exists(path) == true)
                    File.Delete(path);
            }

            AssetDatabase.Refresh();
        }
    }
}
