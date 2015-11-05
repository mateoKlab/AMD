using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using Bingo;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;

namespace BingoEditor
{
    [InitializeOnLoad]
    public class ConstantsGenerator
    {
        // a flag if the dataset has changed
        private static bool _hasChanged = false;
        // time when we start to count
        private static double _startTime = 0.0;
        // the time that should elapse between the change of tags and the File write
        // this is importend because changed are triggered as soon as you start typing and this can cause lag
        private static double _timeToWait = 1.0;

        private static Dictionary<string, string[]> tagMap = new Dictionary<string, string[]>();

        private CodeDomProvider provider;

        static ConstantsGenerator()
        {


            // subscribe to event
            EditorApplication.update += Update;
            // get tags
            Process("Tags", InternalEditorUtility.tags);
            Process("Layers", InternalEditorUtility.layers);
            Process("SortingLayers", GetSortingLayers());
            // write file
            WriteCodeFile();

        }

        private static void Update()
        {
            // returns if we are in play mode
            if (Application.isPlaying == true)
                return;

            Wait();

            Process("Tags", InternalEditorUtility.tags);
            Process("Layers", InternalEditorUtility.layers);
            Process("SortingLayers", GetSortingLayers());

        }

        private static string[] GetSortingLayers()
        {
            var type = typeof(UnityEditorInternal.InternalEditorUtility);
            var prop = type.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);

            return prop.GetValue(null, null) as string[];
        }

        private static void Process(string name, string[] tags)
        {
            if (!tagMap.ContainsKey(name))
            {
                tagMap.Add(name, tags);
                _hasChanged = true;
                _startTime = EditorApplication.timeSinceStartup;
                return;
            }
            else
            {
                if (tags.Length != tagMap[name].Length)
                {
                    tagMap[name] = tags;
                    _hasChanged = true;
                    _startTime = EditorApplication.timeSinceStartup;
                    return;
                }
                else
                {
                    // loop thru all new tags and compare them to the old ones
                    for (int i = 0; i < tags.Length; i++)
                    {
                        if (string.Equals(tags[i], tagMap[name][i]) == false)
                        {
                            tagMap[name] = tags;
                            _hasChanged = true;
                            _startTime = EditorApplication.timeSinceStartup;
                            return;
                        }
                    }
                }
            }
        }

        private static void Wait()
        {
            // if nothing has changed return
            if (_hasChanged == false)
                return;

            // if the time delta between now and the last change, is greater than the time we schould wait Than write the file
            if (EditorApplication.timeSinceStartup - _startTime > _timeToWait)
            {
                WriteCodeFile();
                _hasChanged = false;
            }
        }

        // writes a file to the project folder
        private static void WriteCodeFile()
        {

            // the path we want to write to
            string path = string.Concat(InternalConstants.DATA_PATH, "GeneratedConstants.cs");

            try
            {
                // opens the file if it allready exists, creates it otherwise
                using (FileStream stream = File.Open(path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine("// ----- AUTO GENERATED CODE ----- //");

                        foreach (KeyValuePair<string, string[]> pair in tagMap)
                        {
                            builder.AppendLine(string.Format("public static class {0}", pair.Key));
                            builder.AppendLine("{");
                            foreach (string tag in pair.Value)
                            {
                                builder.AppendLine(string.Format("    public static readonly string {0} = \"{1}\";", EditorTools.ToCamelCase(tag), tag));
                            }

                            builder.AppendLine("}");
                            builder.AppendLine();
                        }

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
