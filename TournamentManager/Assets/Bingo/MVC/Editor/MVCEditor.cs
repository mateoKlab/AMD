using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;

namespace BingoEditor
{
    public class MVCEditor : EditorWindow
    {
        public string applicationName;
        private string applicationClassName;
        private string lastApplicationName;

        private GUIStyle boxStyle;
        private Vector2 scrollPosition;
        private GUIContent generateFileContent;
        private GUIContent generateHierarchyContent;

        void OnEnable()
        {
            generateFileContent = new GUIContent("Generate Files");
            generateHierarchyContent = new GUIContent("Generate GameObjects");
        }

        [MenuItem("Bingo/Create MVC...  %m")]
        static void CreateWindow()
        {
            GetWindow<MVCEditor>(true, "Create MVC");
        }

        private void OnGUI()
        {
            boxStyle = new GUIStyle();
            boxStyle.padding = new RectOffset(5, 5, 5, 5);

            EditorGUILayout.BeginVertical(boxStyle);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
            applicationName = EditorGUILayout.TextField("Application Name", applicationName);

            GUILayout.EndScrollView();

            GUI.enabled = isValidFileName(applicationName) && !VerifyFiles(applicationName);
            if (GUILayout.Button(generateFileContent))
            {
                CreateDirectories();
            }

            GUI.enabled = VerifyFiles(applicationName);
            if (GUILayout.Button(generateHierarchyContent))
            {
                CreateMVCObjects();
            }

            GUI.enabled = true;
            EditorGUILayout.EndVertical();
        }

        bool isValidFileName(string identifier)
        {
            return EditorTools.cSharpProvider.IsValidIdentifier(identifier);
        }

        bool CreateDirectories()
        {
            applicationName = applicationName.Trim();
            string path = string.Concat("Assets/", applicationName, "/Scripts/");

            if (Directory.Exists(path))
            {
                return false;
            }

            applicationClassName = ToFileNameCase(applicationName);

            Directory.CreateDirectory(path);

            char[] a = applicationName.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            applicationClassName = new string(a);

            var modelPath = path + "/Model/";
            Directory.CreateDirectory(modelPath);
            CreateFile(modelPath, applicationClassName + "Model", string.Format("Model<{0}>", applicationClassName));

            var viewPath = path + "/View/";
            Directory.CreateDirectory(viewPath);
            CreateFile(viewPath, applicationClassName + "View", string.Format("View<{0}>", applicationClassName));

            var controllerPath = path + "/Controller/";
            Directory.CreateDirectory(controllerPath);
            CreateFile(controllerPath, applicationClassName + "Controller", string.Format("Controller<{0}>", applicationClassName));

            CreateFile(path, applicationClassName, string.Format("BaseApplication<{0}Model, {0}View, {0}Controller>", applicationClassName));

            AssetDatabase.Refresh();

            return true;
        }

        internal static string ToFileNameCase(string str)
        {
            char[] a = str.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        void CreateFile(string dirPath, string fileName, string type)
        {
            // the path we want to write to
            string path = string.Concat(dirPath, fileName, ".cs");

            try
            {
                // opens the file if it allready exists, creates it otherwise
                using (FileStream stream = File.Open(path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine("using UnityEngine;");
                        builder.AppendLine("using System.Collections;");
                        builder.AppendLine("using Bingo;");
                        builder.AppendLine(string.Empty);
                        builder.AppendLine(string.Format("public class {0} : {1}", fileName, type));
                        builder.AppendLine("{");
                        builder.AppendLine("    ");
                        builder.AppendLine("}");
                        builder.AppendLine(string.Empty);
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
        }

        void CreateMVCObjects()
        {
            applicationClassName = ToFileNameCase(applicationName);

            if (!VerifyFiles(applicationClassName))
            {
                return;
            }

            string assemblyName = ",Assembly-CSharp";

            GameObject go = new GameObject("Application");
            go.AddComponent(Type.GetType(applicationName + assemblyName));
            go.AddComponent(Type.GetType(string.Concat(applicationName, "Model", assemblyName)));
            go.AddComponent(Type.GetType(string.Concat(applicationName, "View", assemblyName)));
            go.AddComponent(Type.GetType(string.Concat(applicationName, "Controller", assemblyName)));
        }

        static bool VerifyFiles(string name)
        {
            string rootPath = "Assets/{0}/Scripts/";
            if (!File.Exists(string.Format(rootPath + "{0}.cs", name)))
            {
                return false;
            }
            if (!File.Exists(string.Format(rootPath + "Model/{0}Model.cs", name)))
            {
                return false;
            }
            if (!File.Exists(string.Format(rootPath + "View/{0}View.cs", name)))
            {
                return false;
            }
            if (!File.Exists(string.Format(rootPath + "Controller/{0}Controller.cs", name)))
            {
                return false;
            }

            return true;
        }
    }
}
