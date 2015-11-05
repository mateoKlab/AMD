using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Bingo
{
    public class Log
    {
        private static readonly string FORMAT = "<b>{0}:</b> {1}";

        /*#if UNITY_EDITOR
        private static readonly string COLOR_NORMAL = UnityEditor.EditorGUIUtility.isProSkin? "white" : "black";
        #else
        private static readonly string COLOR_NORMAL = "white"
        #endif*/

        private static readonly string COLOR_WARNING = "yellow";
        private static readonly string COLOR_ERROR = "red";

        public static void Assert(bool condition)
        {
            #if !NO_DEBUG
            if (!condition)
            {
                StackTrace stackTrace = new StackTrace(true);
                StackFrame frame = stackTrace.GetFrame(1);
                string filename = frame.GetFileName().Replace('\\', '/').Replace(Application.dataPath, "Assets");
                E("Assert", "failed at", string.Concat(filename.ToBold(), ":", frame.GetFileLineNumber()));
            }
            #endif
        }

        public static void D(string title, params object[] args)
        {
            #if !NO_DEBUG
            string[] stringData = Array.ConvertAll<object, string>(args, o => o.ToString());
            Debug.LogFormat(FORMAT, title, string.Join(" ", stringData));
            #endif
        }

        public static void W(string title, params object[] args)
        {
            #if !NO_DEBUG
            string[] stringData = Array.ConvertAll<object, string>(args, o => o.ToString());
            Debug.LogWarningFormat(FORMAT, title.SetColor(COLOR_WARNING), string.Join(" ", stringData));
            #endif
        }

        public static void E(string title, params object[] args)
        {
            #if !NO_DEBUG
            string[] stringData = Array.ConvertAll<object, string>(args, o => o.ToString());
            Debug.LogErrorFormat(FORMAT, title.SetColor(COLOR_ERROR), string.Join(" ", stringData));
            #endif
        }
    }

    public static class LogExtensions
    {
        private static readonly string ANCHOR_BOLD = "<b>";
        private static readonly string ANCHOR_BOLD_CLOSE = "</b>";
        private static readonly string ANCHOR_ITALIC = "<i>";
        private static readonly string ANCHOR_ITALIC_CLOSE = "</i>";

        private static readonly string ANCHOR_COLOR_START = "<color=";
        private static readonly string ANCHOR_COLOR_END = ">";
        private static readonly string ANCHOR_COLOR_CLOSE = "</color>";

        public static string ToBold(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return string.Concat(ANCHOR_BOLD, s, ANCHOR_BOLD_CLOSE);
            }

            return s;
        }

        public static string ToItalic(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return string.Concat(ANCHOR_ITALIC, s, ANCHOR_ITALIC_CLOSE);
            }

            return s;
        }

        public static string SetColor(this string s, string color)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return string.Concat(ANCHOR_COLOR_START, color, ANCHOR_COLOR_END, s, ANCHOR_COLOR_CLOSE);
            }

            return s;
        }
    }
}
