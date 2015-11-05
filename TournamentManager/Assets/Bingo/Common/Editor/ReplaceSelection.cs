﻿using UnityEngine;
using UnityEditor;

namespace BingoEditor
{
    public class ReplaceSelection : ScriptableWizard
    {
        static GameObject replacement = null;
        static bool keep = false;

        public GameObject ReplacementObject = null;
        public bool KeepOriginals = false;

        [MenuItem("GameObject/Replace Selection...")]

        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard(
                "Replace Selection", typeof(ReplaceSelection), "Replace");
        }

        public ReplaceSelection()
        {
            ReplacementObject = replacement;
            KeepOriginals = keep;
        }

        void OnWizardUpdate()
        {
            replacement = ReplacementObject;
            keep = KeepOriginals;
        }

        void OnWizardCreate()
        {
            if (replacement == null)
                return;

            Transform[] transforms = Selection.GetTransforms(
                SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);

            Undo.RecordObjects(transforms, "Replace Selection");

            foreach (Transform t in transforms)
            {
                GameObject g;
                PrefabType pref = PrefabUtility.GetPrefabType(replacement);

                if (pref == PrefabType.Prefab || pref == PrefabType.ModelPrefab)
                {
                    g = (GameObject)PrefabUtility.InstantiatePrefab(replacement);
                }
                else
                {
                    g = (GameObject)Editor.Instantiate(replacement);
                }

                Transform gTransform = g.transform;
                gTransform.parent = t.parent;
                g.name = replacement.name;
                gTransform.localPosition = t.localPosition;
                gTransform.localScale = t.localScale;
                gTransform.localRotation = t.localRotation;
            }

            if (!keep)
            {
                foreach (GameObject g in Selection.gameObjects)
                {
                    GameObject.DestroyImmediate(g);
                }
            }
        }
    }
}