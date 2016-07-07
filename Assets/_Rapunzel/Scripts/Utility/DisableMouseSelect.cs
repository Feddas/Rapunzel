#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

/// <summary>
/// This script disables a gameobject from being selectable using left click in the UnityEditor's Scene window
/// http://answers.unity3d.com/questions/173950/disable-mouse-selection-in-editor-view.html
/// </summary>
public class DisableMouseSelect : MonoBehaviour
{
    public void OnSceneGUI()
    {
        UnityEditor.HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
    }
}
#endif
