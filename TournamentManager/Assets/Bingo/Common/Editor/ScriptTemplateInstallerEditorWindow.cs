using UnityEngine;
using UnityEditor;
using System.Collections;
using Bingo;
using System.IO;

namespace BingoEditor
{
	public class ScriptTemplateInstallerEditorWindow : EditorWindow
	{
        private static string scriptTemplatesPath = string.Concat(EditorApplication.applicationContentsPath, "/Resources/ScriptTemplates");

        [MenuItem("Bingo/Install Script Templates", false, 3001)]
		private static void InstallScriptTemplates()
		{
			DirectoryCopy(InternalConstants.SCRIPT_TEMPLATES_PATH, scriptTemplatesPath, true);

            EditorUtility.DisplayDialog("Install Script Templates",
                "Script templates have been successfully installed. You need to restart Unity for the changes to take effect.",
                "OK");
		}

        [MenuItem("Bingo/Uninstall Script Templates", false, 3002)]
        private static void UninstallScriptTemplates()
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(scriptTemplatesPath);
            string fileName = string.Format("-{0}__", InternalConstants.FRAMEWORK_NAME);

            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo file in files)
                {
                    if (file.Name.Contains(fileName))
                    {
                        file.Delete();
                    }
                }
            }

            EditorUtility.DisplayDialog("Uninstall Script Templates",
                "Script templates have been successfully uninstalled. You need to restart Unity for the changes to take effect.",
                "OK");
        }

		private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();
			// If the destination directory doesn't exist, create it.
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				if (file.Extension.Equals(".txt"))
			    {
					string temppath = Path.Combine(destDirName, file.Name);
					file.CopyTo(temppath, true);
				}
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}
	}
}
