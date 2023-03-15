using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Directory Configuration", menuName = "Custom Objects/Tools/Directory Configuration")]
public class DirectoryConfiguration : ScriptableObject
{
    [SerializeField] private List<string> directoryPaths = null;

    public List<string> DirectoryPaths => directoryPaths;

}
