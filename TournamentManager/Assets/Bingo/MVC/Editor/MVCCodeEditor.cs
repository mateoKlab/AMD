using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using Bingo;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace BingoEditor
{
    public class MVCCodeEditor<T> : Editor where T : Element
    {
        private T[] elements;
        private T elementInstance;

        //private int[] elementInterfaces;
        //private string[] elementNames;
        private string path;

        internal struct MVCElement
        {
            public bool flag;
            public int interfaceIndex;
            public string name;
            public bool hasError;
            public bool isDirectChild;
        }

        private MVCElement[] mvcElements;

        private const string startMarker = "// MVCCodeEditor GENERATED CODE - DO NOT MODIFY //";
        private const string endMarker =   "//////// END MVCCodeEditor GENERATED CODE ////////";

        private Dictionary<string, int> elementMap = new Dictionary<string, int>();
        private Dictionary<string, Type[]> interfaceMap = new Dictionary<string, Type[]>();

        private List<string> duplicateElementList = new List<string>();

        private bool hasChanged;
        private bool hasError;

        private int elementCount = 0;
        private bool selectAllState = false;
        private bool doSelectAll = false;

        private string typeName;

        private bool searchGlobally;
        private bool showAllChildren;

        public virtual void OnEnable()
        {
            elementInstance = target as T;
            elements = searchGlobally ? FindObjectsOfType<T>() :
                elementInstance.GetComponentsInChildren<T>(true);

            elementMap.Clear();
            duplicateElementList.Clear();

            mvcElements = new MVCElement[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                mvcElements[i] = new MVCElement();

                T current = elements[i];
                while (current != null)
                {
                    Transform tp = current.transform.parent;
                    if (tp != null)
                    {
                        T parent = tp.GetComponent<T>();
                        if (parent != null)
                        {
                            mvcElements[i].isDirectChild = parent == elementInstance;
                            break;
                        }
                        current = parent;
                    }
                    else
                    {
                        current = null;
                    }
                }

                string name = elements[i].GetType().ToString();
                if (!elementMap.ContainsKey(name))
                {
                    mvcElements[i].name = EditorTools.ToCamelCase(name, false);
                    elementMap.Add(name, i);
                    interfaceMap.Add(name, elements[i].GetType().GetInterfaces());
                }
                else
                {
                    if (!duplicateElementList.Contains(name))
                    {
                        duplicateElementList.Add(name);
                    }
                }
            }

            for (int i = 0; i < duplicateElementList.Count; i++)
            {
                elementMap.Remove(duplicateElementList[i]);
            }

            List<string> resultList = new List<string>();
            SearchFiles("Assets", target.GetType() + ".cs", resultList);

            if (resultList.Count > 0)
            {
                path = resultList[0];
            }

            QueryFile();
            hasChanged = true;

            string[] splitWholeTypeName = typeof(T).ToString().Split('.');
            typeName = splitWholeTypeName[splitWholeTypeName.Length - 1];
        }

        public override void OnInspectorGUI()
        {
            string header = elements.Length > 1 ? string.Format("Available {0}s:", typeName) :
                string.Format("No {0}s available.", typeName);

            GUILayout.Label(header, EditorStyles.boldLabel);

            Color normalColor = GUI.backgroundColor;

            if (elements.Length > 1)
            {
                bool s = selectAllState;
                EditorGUILayout.BeginHorizontal();
                selectAllState = EditorGUILayout.Toggle(selectAllState, GUILayout.MaxWidth(15));
                EditorGUILayout.LabelField("Select all");

                showAllChildren = EditorGUILayout.Toggle(showAllChildren, GUILayout.MaxWidth(15));
                EditorGUILayout.LabelField("Show all children");
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();

                if (s != selectAllState)
                {
                    doSelectAll = true;
                }

                if (doSelectAll)
                {
                    for (int i = 0; i < elements.Length; i++)
                    {
                        if (elements[i] != elementInstance && elementMap.ContainsKey(elements[i].GetType().ToString()))
                        {
                            mvcElements[i].flag = selectAllState;
                        }
                    }

                    hasChanged = true;
                    doSelectAll = false;
                }

                for (int i = 0; i < elements.Length; i++)
                {
                    if (showAllChildren || (!showAllChildren && mvcElements[i].isDirectChild))
                    {
                        if (elements[i] != elementInstance && elementMap.ContainsKey(elements[i].GetType().ToString()))
                        {
                            elementCount++;

                            string name = elements[i].GetType().ToString();
                            bool val = mvcElements[i].flag;

                            EditorGUILayout.BeginHorizontal();
                            bool flagValue = mvcElements[i].flag;
                            mvcElements[i].flag = EditorGUILayout.Toggle(mvcElements[i].flag, GUILayout.MaxWidth(15));

                            hasChanged |= flagValue != mvcElements[i].flag;

                            EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            Type[] interfaceTypes;
                            if (interfaceMap.TryGetValue(name, out interfaceTypes))
                            {
                                string[] options = new string[interfaceTypes.Length + 1];
                                options[0] = name;
                                for (int j = 0; j < interfaceTypes.Length; j++)
                                {
                                    options[j + 1] = interfaceTypes[j].ToString();
                                }

                                int oldSelection = mvcElements[i].interfaceIndex;
                                mvcElements[i].interfaceIndex = EditorGUILayout.Popup(mvcElements[i].interfaceIndex, options, GUILayout.MaxWidth(150));
                            }

                            string propertyName = mvcElements[i].name;

                            GUI.backgroundColor = mvcElements[i].hasError ? Color.red : normalColor;
                            mvcElements[i].name = EditorGUILayout.TextField(mvcElements[i].name).Trim();
                            GUI.backgroundColor = normalColor;

                            hasChanged |= !propertyName.Equals(mvcElements[i].name);

                            if (hasChanged)
                            {
                                if (string.IsNullOrEmpty(mvcElements[i].name))
                                {
                                    mvcElements[i].name = name;
                                }

                                hasError = false;
                                mvcElements[i].hasError = false;
                                hasChanged = true;
                                mvcElements[i].name = EditorTools.ToCamelCase(mvcElements[i].name, false);

                                for (int k = 0; k < mvcElements.Length; k++)
                                {
                                    mvcElements[k].hasError = false;
                                    if (i != k && elements[k] != elementInstance && elementMap.ContainsKey(elements[k].GetType().ToString()))
                                    {
                                        if (mvcElements[i].flag && mvcElements[k].flag && mvcElements[i].name.Equals(mvcElements[k].name))
                                        {
                                            mvcElements[i].hasError = true;
                                            mvcElements[k].hasError = true;
                                            hasError = true;
                                            break;
                                        }
                                    }

                                }
                            }

                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.Space();

                            if (val != mvcElements[i].flag)
                            {
                                hasChanged = true;
                                selectAllState = false;
                            }
                        }
                    }        
                }

                EditorGUILayout.Space();

                GUI.enabled = hasChanged && !hasError;
                if (GUILayout.Button("Save"))
                {
                    UpdateFile();
                    hasChanged = false;
                }
                GUI.enabled = true;
            }

            GUI.backgroundColor = normalColor;

            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }

        protected virtual void QueryFile()
        {
            int startIndex = -1;
            int endIndex = -1;

            string[] lines = File.ReadAllLines(path);

            Dictionary<string, string> queriedItems = new Dictionary<string, string>(elements.Length);

            string className = target.GetType().ToString();

            string pattern = @"typeof\((\w+)\)";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            for (int i = 0; i < lines.Length; i++)
            {
                if (startIndex == -1 && lines[i].Contains(startMarker))
                {
                    startIndex = i;
                }

                if (startIndex != -1)
                {
                    if (lines[i].Contains("[Inject"))
                    {
                        Match m = regex.Match(lines[i]);
                        if (m.Success)
                        {
                            queriedItems.Add(m.Groups[1].Captures[0].ToString(), lines[i + 1].Trim());
                        }
                        else
                        {
							// TEMP FIX. duplicate key check.
							if (!queriedItems.ContainsKey (string.Empty)) {
								queriedItems.Add(string.Empty, lines[i + 1].Trim());
							}
                        }

                        i++;
                    }
                }

                if (endIndex == -1 && lines[i].Contains(endMarker))
                {
                    endIndex = i;
                    break;
                }
            }

            foreach (KeyValuePair<string, string> pair in queriedItems)
            {
                int index;
                string[] tokens = pair.Value.Split(' ');

                if (!string.IsNullOrEmpty(pair.Key))
                {
                    string name = pair.Key;
                    if (elementMap.TryGetValue(name, out index))
                    {
                        mvcElements[index].flag = true;
                        mvcElements[index].name = tokens[2];
                    }

                    Type[] interfaceTypes;
                    if (interfaceMap.TryGetValue(name, out interfaceTypes))
                    {
                        for (int j = 0; j < interfaceTypes.Length; j++)
                        {
                            if (interfaceTypes[j].ToString().Equals(tokens[1]))
                            {
                                mvcElements[index].interfaceIndex = j + 1;
                            }
                        }
                    }
                }
                else
                {
                    if(elementMap.TryGetValue(tokens[1], out index))
                    {
                        mvcElements[index].flag = true;
                        mvcElements[index].name = tokens[2];
                    }
                }
            }
        }

        protected virtual void UpdateFile()
        {
            int startIndex = -1;
            int endIndex = -1;
            int classIndex = -1;
            int leftBraceIndex = -1;
            string[] lines = File.ReadAllLines(path);

            string className = target.GetType().ToString();

            for (int i = 0; i < lines.Length; i++)
            {
                if (classIndex == -1 && lines[i].Contains(className))
                {
                    classIndex = i;
                }

                if (leftBraceIndex == -1 && classIndex != -1 && lines[i].EndsWith("{"))
                {
                    leftBraceIndex = i;
                }

                if (startIndex == -1 && lines[i].Contains(startMarker))
                {
                    startIndex = i;
                }
                else if (endIndex == -1 && lines[i].Contains(endMarker))
                {
                    endIndex = i;
                    break;
                }
            }

            List<string> modifiedLines = new List<string>(lines);

            if (startIndex == -1 || endIndex == -1)
            {
                if (startIndex != -1)
                {
                    modifiedLines.RemoveAt(startIndex);
                }

                if (endIndex != -1)
                {
                    modifiedLines.RemoveAt(endIndex);
                }

                if (leftBraceIndex != -1)
                {
                    modifiedLines.InsertRange(leftBraceIndex + 1, new string[] { "    " + startMarker, "    " + endMarker, "    " });
                    startIndex = leftBraceIndex + 1;
                    endIndex = startIndex + 1;
                }
            }

            if (startIndex > -1 && endIndex > -1 && startIndex < endIndex)
            {
                modifiedLines.RemoveRange(startIndex + 1, endIndex - startIndex - 1);
                endIndex = startIndex + 1;

                for (int i = 0; i < elements.Length; i++)
                {
                    if (mvcElements[i].flag)
                    {
                        string name = elements[i].GetType().ToString();
                        string interfaceType = null;

                        if (mvcElements[i].interfaceIndex != 0)
                        {
                            Type[] interfaceTypes;
                            if (interfaceMap.TryGetValue(name, out interfaceTypes))
                            {
                                name = interfaceTypes[mvcElements[i].interfaceIndex - 1].ToString();
                                interfaceType = "(typeof("  + elements[i].GetType().ToString() + "))";
                            }

                        }

                        string declarationLine = string.Format("    public {0} {1} {{ get; private set; }}",
                            name,
                            EditorTools.ToCamelCase(mvcElements[i].name, false));
                        string injectLine = string.Format("    [Inject{0}]", interfaceType);
                        modifiedLines.InsertRange(endIndex, new string[] { injectLine, declarationLine, "    " });
                    }
                }

                modifiedLines.Insert(startIndex + 1, "    ");

                File.WriteAllLines(path, modifiedLines.ToArray());
            }
        }

        protected void SearchFiles(string dir, string query, List<string> resultList)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(dir))
                {
                    foreach(string f in Directory.GetFiles(d, query))
                    {
                        resultList.Add(f);
                    }
                    SearchFiles(d, query, resultList);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

}
