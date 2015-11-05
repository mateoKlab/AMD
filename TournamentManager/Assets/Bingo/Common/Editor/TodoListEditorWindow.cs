using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.IO;
using Bingo;

namespace BingoEditor
{
    public class TodoListEditorWindow : EditorWindow
    {
        private int selectedIndex = -1;
        private GUIStyle normalBoxStyle;
        private GUIStyle selectedBoxStyle;
        private Texture2D boxBGTexture;

        private bool hasInit;

        private class DetectChanges : AssetPostprocessor
        {
            // Updates list on script update
            static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
            {
                UpdateList();
            }
        }

        private struct TodoInfo
        {
            public string filePath;
            public string note;
            public int lineNum;

            public TodoInfo(string filePath, string note, int lineNum)
            {
                this.filePath = filePath;
                this.note = note;
                this.lineNum = lineNum;
            }
        };

        private Vector2 scrollPos = Vector2.zero;
        public static TodoListEditorWindow Instance { get; private set; }

        private static List<TodoInfo> todoList;
        private static List<TodoInfo> TodoList
        {
            get
            {
                if (todoList == null)
                {
                    todoList = new List<TodoInfo>();
                    UpdateList();
                }
                return todoList;
            }
        }

        // Creates window
        [MenuItem("Bingo/TODO List... %t", false, 2000)]
        public static void ShowWindow()
        {
            if (!Instance)
            {
                GetWindow<TodoListEditorWindow>("To-do");
                UpdateList();
            }
            else
            {
                GetWindow<TodoListEditorWindow>("To-do").Close();
            }
        }

        void Init()
        {
            normalBoxStyle = new GUIStyle(GUI.skin.box);
            normalBoxStyle.richText = true;
            normalBoxStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            normalBoxStyle.alignment = TextAnchor.MiddleLeft;

            selectedBoxStyle = new GUIStyle(normalBoxStyle);
            selectedBoxStyle.normal.textColor = Color.white;
            boxBGTexture = MakeTex(2, 2, EditorGUIUtility.isProSkin ? new Color(0.243f, 0.373f, 0.588f) : new Color(0.196f, 0.392f, 0.725f));
            selectedBoxStyle.normal.background = boxBGTexture;
        }

        private void OnDestroy()
        {
            DestroyImmediate(boxBGTexture);
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        // Sets Instance when opened
        void OnEnable() { Instance = this; selectedIndex = -1; hasInit = false; }

        void OnGUI()
        {
            if (!hasInit)
            {
                Init();
                hasInit = true;
            }

            var e = Event.current;
            int clickCount = 0;

            if (e.isMouse && e.type == EventType.MouseDown)
            {
                clickCount = e.clickCount;
            }
            if (focusedWindow == this && e.isKey && e.type == EventType.KeyDown)
            {
                if (selectedIndex > -1)
                {
                    switch (e.keyCode)
                    {
                        case KeyCode.DownArrow:
                            selectedIndex = Mathf.Min(selectedIndex + 1, TodoList.Count - 1);
                            break;
                        case KeyCode.UpArrow:
                            selectedIndex = Mathf.Max(selectedIndex - 1, 0);
                            break;
                        case KeyCode.KeypadEnter:
                        case KeyCode.Return:
                            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(TodoList[selectedIndex].filePath, TodoList[selectedIndex].lineNum);
                            break;
                        default:
                            break;
                    }

                    Instance.Repaint();
                }
            }

            if (TodoList.Count == 0)
            {
                GUILayout.Space(20);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("No pending TODOs");
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                return;
            }
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            GUIStyle boxStyle;

            // Display buttons
            for (int i = 0; i < TodoList.Count; i++)
            {
                boxStyle = selectedIndex == i ? selectedBoxStyle : normalBoxStyle;

                GUILayout.Box(GetDisplayButtonText(TodoList[i].filePath, TodoList[i].note, TodoList[i].lineNum), boxStyle, GUILayout.ExpandWidth(true));

                if (GUILayoutUtility.GetLastRect().Contains(e.mousePosition))
                {
                    if (clickCount == 1)
                    {
                        selectedIndex = i;
                    }

                    if (clickCount > 0)
                    {
                        Instance.Repaint();
                    }

                    if (clickCount == 2)
                    {
                        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(TodoList[i].filePath, TodoList[i].lineNum);
                    }
                }
            }

            EditorGUILayout.EndScrollView();
        }

        // Updates m_ToDoList
        private static void UpdateList()
        {
            if (!Instance) { return; }
            TodoList.Clear();
            ScanDirectories(Application.dataPath);
            Instance.Repaint();
        }

        // Scans through directories recursively
        private static void ScanDirectories(string dirPath)
        {
            string[] directories = Directory.GetDirectories(dirPath);
            ScanFiles(dirPath);
            if (directories.Length > 0)
            {
                foreach (string directory in directories)
                {
                    ScanDirectories(directory);
                }
            }
        }

        // Scans through files in current directory
        private static void ScanFiles(string dirPath)
        {
            string[] files = Directory.GetFiles(dirPath);
            foreach (string file in files)
            {
                if ((file.EndsWith(".cs") || file.EndsWith(".shader")) && !file.EndsWith("TodoListEditorWindow.cs"))
                {
                    AddToList(file);
                }
            }
        }

        // Adds lines with "TODO:" string to list
        private static void AddToList(string filePath)
        {
            string[] contents = GetFileContents(filePath).Split('\n');
            int lineNumber = 0;
            foreach (string line in contents)
            {
                if (line.Contains("TODO:"))
                {
                    TodoList.Add(new TodoInfo(filePath, line.Substring(line.IndexOf("TODO:")), lineNumber));
                }
                lineNumber++;
            }
        }

        // Gets texts of files
        private static string GetFileContents(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string contents = sr.ReadToEnd();
            sr.Close();
            return contents;
        }

        // Gets string to display per button
        private static string GetDisplayButtonText(string filePath, string content, int lineNumber)
        {
            int ln = lineNumber + 1;
            #if UNITY_EDITOR_WIN
            return string.Concat(string.Concat(filePath.Substring(filePath.LastIndexOf("\\") + 1), ":", ln).ToBold(), " " + content.Substring(content.IndexOf("TODO:") + 4));
            #else
            return string.Concat(string.Concat(filePath.Substring(filePath.LastIndexOf("/") + 1), ":", ln).ToBold(), " " + content.Substring(content.IndexOf("TODO:") + 4));
            #endif
        }
    }
}
