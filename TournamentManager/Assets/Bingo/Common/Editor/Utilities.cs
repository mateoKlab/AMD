using UnityEditor;
using System.Text.RegularExpressions;
using Bingo;

namespace BingoEditor
{
    public class Utilities
    {
        [MenuItem("Bingo/Export Package", false, 3000)]
        static void ExportPackage()
        {
            AssetDatabase.ExportPackage(string.Concat("Assets/", InternalConstants.FRAMEWORK_NAME),
                string.Concat("../../build/", InternalConstants.FRAMEWORK_NAME, "_", InternalConstants.FRAMEWORK_VERSION, ".unitypackage"),
                ExportPackageOptions.Recurse);

            EditorUtility.DisplayDialog("Export Package", "Unity package version " +
                InternalConstants.FRAMEWORK_VERSION +
                " has been exported successfully.", "OK");
        }

        static bool IsFilenameValid(string filename)
        {
            Regex containsBadChar = new Regex("(^(PRN|AUX|NUL|CON|COM[1-9]|LPT[1-9]|(\\.+)$)(\\..*)?$)|(([\\x00-\\x1f\\\\?*:\";‌​|/<>‌​])+)|([\\. ]+)");
            return !containsBadChar.IsMatch(filename);
        }
    }

}
