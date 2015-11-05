using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
using UnityEngine;

namespace BingoEditor
{
    public class EditorTools
    {
        public static CodeDomProvider cSharpProvider = CodeDomProvider.CreateProvider("C#");

        public static bool DrawButton(string title, string tooltip, bool enabled, params GUILayoutOption[] options)
        {
            if (enabled)
            {
                // Draw a regular button
                return GUILayout.Button(new GUIContent(title, tooltip), options);
            }
            else
            {
                // Button should be disabled -- draw it darkened and ignore its return value
                Color color = GUI.color;
                GUI.color = new Color(1f, 1f, 1f, 0.25f);
                GUILayout.Button(new GUIContent(title, tooltip), options);
                GUI.color = color;
                return false;
            }
        }

        public  static string ToFileNameCase(string str)
        {
            char[] a = str.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static string ToCamelCase(string input, bool capitalizeStart = true, bool removeInvalidChars = true)
        {
            char[] c = input.ToCharArray();
            if (capitalizeStart)
            {
                c[0] = char.ToUpper(c[0]);
            }
            else
            {
                c[0] = char.ToLower(c[0]);
            }


            bool performUpper = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (c[i] == ' ' || c[i] == '-' || c[i] == '_')
                {
                    performUpper = true;
                    continue;
                }
                else
                {
                    if (performUpper)
                    {
                        c[i] = char.ToUpper(c[i]);
                        performUpper = false;
                    }
                }
            }

            input = Regex.Replace(new string(c), "[_ -]+", "", RegexOptions.Compiled);

            if (removeInvalidChars)
            {
                input = Regex.Replace(input, "[^A-Za-z0-9]", "", RegexOptions.Compiled);
            }

            if (char.IsDigit(input[0]))
                return (capitalizeStart? "K" : "k") + input;

            return input;
        }
    }
}
