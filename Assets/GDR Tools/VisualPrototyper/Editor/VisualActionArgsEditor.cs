using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEditor;
using System.IO;

namespace D_Framework.VisualPrototyping
{
    [CustomEditor(typeof(VisualActionArgs))]
    public class VisualActionArgsEditor : Editor
    {
        [SerializeField] private VisualActionArgType addedArgumentType;

        private VisualActionArgumentFactory argsFactory = new VisualActionArgumentFactory();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(20f);

            addedArgumentType = (VisualActionArgType)EditorGUILayout.EnumPopup("Created Argument Type", addedArgumentType);

            SerializedProperty argsProperty = serializedObject.FindProperty("args");

/*            if (GUILayout.Button("Add Argument"))
            {
                VisualActionArgument addedArgument = argsFactory.GetElement(addedArgumentType);
                
                string preliminaryAssetPath = $"{Utilities.GetAssetDirectory(target)}/{target.name}_{addedArgumentType}_Argument";
                string assetPath = GetNameWithNextIndex(preliminaryAssetPath);

                AssetDatabase.CreateAsset(addedArgument, assetPath);

                int addedArgumentIndex = argsProperty.arraySize;
                argsProperty.InsertArrayElementAtIndex(addedArgumentIndex);
                VisualActionArgument[] args = argsProperty.GetAsArray<VisualActionArgument>();
                args[addedArgumentIndex] = addedArgument;
                argsProperty.SetAsArray(args);
            }*/

            if (GUILayout.Button("Add Argument"))
            {
                VisualActionArgument addedArgument = argsFactory.GetElement(addedArgumentType);
                addedArgument.name = GetNameWithNextIndexInAsset($"{target.name}_{addedArgumentType}_Argument");
                AssetDatabase.AddObjectToAsset(addedArgument, AssetDatabase.GetAssetPath(target));

                int addedArgumentIndex = argsProperty.arraySize;
                argsProperty.InsertArrayElementAtIndex(addedArgumentIndex);
                VisualActionArgument[] args = argsProperty.GetAsArray<VisualActionArgument>();
                args[addedArgumentIndex] = addedArgument;
                argsProperty.SetAsArray(args);

                AssetDatabase.SaveAssets();
            }

            if (GUILayout.Button("Clear Arguments"))
            {
                if (EditorUtility.DisplayDialog("Are you sure you want to destroy all arguments?", 
                    "All arguments within this asset will be destroyed.", "Yes", "Cancel"))
                {
                    DestroyAllSubAssets(target);
                    VisualActionArgument[] args = new VisualActionArgument[] { };
                    argsProperty.SetAsArray(args);
                    AssetDatabase.SaveAssets();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private string GetNameWithNextIndex(string defaultEntry, int assetIndex = 0)
        {
            string currentEntry = $"{defaultEntry}_{assetIndex}.asset";

            if (!File.Exists(currentEntry))
                return currentEntry;

            return GetNameWithNextIndex(defaultEntry, ++assetIndex);
        }

        private string GetNameWithNextIndexInAsset(string defaultEntry, int assetIndex = 0)
        {
            string currentEntry = $"{defaultEntry}_{assetIndex}.asset";

            if (!AssetWithNameExists(target, currentEntry))
                return currentEntry;

            return GetNameWithNextIndexInAsset(defaultEntry, ++assetIndex);
        }

        private bool AssetWithNameExists(UnityEngine.Object mainAsset, string assetName)
        {
            UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(mainAsset));

            for (int i = 0; i < assets.Length; i++)
            {
                if (string.Equals(assets[i].name, assetName))
                {
                    return true;
                }
            }

            return false;
        }

        private void DestroyAllSubAssets(UnityEngine.Object mainAsset)
        {
            UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(mainAsset));

            for (int i = 0; i < assets.Length; i++)
            {
                if (assets[i] == null || !AssetDatabase.IsMainAsset(assets[i]))
                {
                    DestroyImmediate(assets[i], true);
                }
            }
        }
    }
}