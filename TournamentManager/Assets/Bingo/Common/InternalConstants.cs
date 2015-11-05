using UnityEngine;
using System.CodeDom.Compiler;

namespace Bingo
{
    public static class InternalConstants
    {
        public const string FRAMEWORK_NAME = "Bingo";
        public const string FRAMEWORK_VERSION = "0.2.2";

        public static readonly string DATA_ASSET_PATH = string.Concat("Assets/", FRAMEWORK_NAME, "/Data/");
        public static readonly string DATA_PATH = string.Concat(Application.dataPath, "/", FRAMEWORK_NAME, "/Data/");

        public static readonly string SCRIPT_TEMPLATES_PATH = string.Concat("Assets/", FRAMEWORK_NAME, "/Common/ScriptTemplates");
    }

    public class InternalResources
    {
        private static Texture2D _bingoIcon;
        public static Texture2D bingoIcon
        {
            get
            {
                return _bingoIcon ?? (_bingoIcon = Resources.Load<Texture2D>("bingo-icon-badge"));
            }
        }
    }
}
