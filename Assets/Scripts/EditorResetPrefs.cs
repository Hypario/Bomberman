using UnityEngine;
using UnityEditor;

public class EditorResetPrefs : MonoBehaviour {
    [MenuItem("Edit/Reset Preferences")]
    static void ResetPrefs()
    {
        if (EditorUtility.DisplayDialog("Reset editor preferences?", "Reset all editor preferences? This cannot be undone.", "Yes", "No"))
        {
            EditorPrefs.DeleteAll();
        }
    }
}
