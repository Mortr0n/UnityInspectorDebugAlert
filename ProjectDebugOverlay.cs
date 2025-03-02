using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ProjectDebugOverlay
{
    static bool debugMode;

    static ProjectDebugOverlay()
    {
        // Called whenever the Project Window is redrawn
        EditorApplication.projectWindowItemOnGUI += OnProjectGUI;
        EditorApplication.update += () =>
        {
            debugMode = InspectorDebugChecker.IsInspectorInDebugMode();
        };
    }

    private static void OnProjectGUI(string guid, Rect selectionRect)
    {
        if (!debugMode) return;

        GUI.Label(new Rect(selectionRect.xMax - 50, selectionRect.y, 50, selectionRect.height), "DEBUG!");
    }
}
