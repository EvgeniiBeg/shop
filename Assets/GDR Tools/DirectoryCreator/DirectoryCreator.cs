using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR

public class DirectoryCreator : EditorWindow
{
    private const string WINDOW_TITLE = "Directory Creator";
    private const string APPLY_CONFIG_MESSAGE = "Apply Directory Configuration";
    private const string BUILD_CONFIG_LABEL_TEXT = "Directory Configuration";

    private const float SMALL_SPACE_SIZE = 15f;

    private Object directoryConfiguration;

    [MenuItem("Tools/Directory Creator")]
    private static void ShowWindow()
    {
        GetWindow<DirectoryCreator>(false, WINDOW_TITLE, true);
    }

    private void OnGUI()
    {
        GUILayout.Label(BUILD_CONFIG_LABEL_TEXT, EditorStyles.boldLabel);
        directoryConfiguration = EditorGUILayout.ObjectField(directoryConfiguration, typeof(Object), false);

        if (directoryConfiguration == null || !(directoryConfiguration is DirectoryConfiguration))
            return;

        ShowCreateButton();
    }

    private void ShowCreateButton()
    {
        GUILayout.Space(SMALL_SPACE_SIZE);

        if (GUILayout.Button(APPLY_CONFIG_MESSAGE))
        {
            CreateDirectories(directoryConfiguration as DirectoryConfiguration);
        }
    }

    private void CreateDirectories(DirectoryConfiguration directoryConfiguration)
    {
        for (int i = 0; i < directoryConfiguration.DirectoryPaths.Count; i++)
        {
            Directory.CreateDirectory($"{Application.dataPath}/{directoryConfiguration.DirectoryPaths[i]}");
        }

        AssetDatabase.Refresh();
    }
}

#endif
