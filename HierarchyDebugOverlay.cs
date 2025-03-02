using UnityEditor;
using UnityEngine;


[InitializeOnLoad]
public static class HierarchyDebugOverlay
{
        static bool debugMode;

        static HierarchyDebugOverlay()
        {
            // Called whenever the Hierarchy changes or is redrawn
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
            EditorApplication.update += () =>
            {
                // Suppose you detect Debug Mode in your Inspector here:
                debugMode = InspectorDebugChecker.IsInspectorInDebugMode();
            };
        }

        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            if (!debugMode) return;

            // Draw label or icon next to each hierarchy item
            GUI.Label(new Rect(selectionRect.xMax - 110, selectionRect.y, 150, selectionRect.height), "INSPECTOR DEBUG!");
        }
    
}
