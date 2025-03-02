using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;


[InitializeOnLoad]
public static class InspectorDebugChecker
{
    private static bool isDebugInspector = false;
    static InspectorDebugChecker()
    {
        EditorApplication.update += CheckInspectorMode;

        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void CheckInspectorMode()
    {
        if (IsInspectorInDebugMode())
        {
            Debug.LogWarning("Inspector is in Debug Mode! Switch back to Normal Mode.");
        }
        isDebugInspector = IsInspectorInDebugMode();
    }

    public static bool IsInspectorInDebugMode()
    {
        var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
        if (inspectorType == null)
        {
            Debug.LogError("Couldn't find InspectorWindow type.");
            return false;
        }

        // Get all properties with both NonPublic + Public + Instance
        var allProperties = inspectorType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        // Find the property named "inspectorMode"
        var modeProperty = allProperties.FirstOrDefault(p => p.Name == "inspectorMode");
        if (modeProperty == null)
        {
            Debug.LogError("Couldn't find 'inspectorMode' property in InspectorWindow.");
            return false;
        }

        // Find all active Inspector windows
        var inspectors = Resources.FindObjectsOfTypeAll(inspectorType);
        foreach (var inspector in inspectors)
        {
            // Get the property value from the InspectorWindow instance
            var modeValue = modeProperty.GetValue(inspector, null);

            if (modeValue == null)
            {
                // If for some reason the property is null, just continue
                continue;
            }

            //Debug.Log("Inspector Mode Raw Value: " + modeValue + " (Type: " + modeValue.GetType().Name + ")");

            // Check if the value is an InspectorMode enum
            if (modeValue is InspectorMode mode)
            {
                //Debug.Log("Inspector Mode Detected: " + mode);
                if (mode == InspectorMode.Debug)
                {
                    return true;
                }
            }
            else
            {
                Debug.LogError("Unable to cast " + modeValue.GetType().Name + " to InspectorMode!");
            }
        }
        return false;
    }
    private static void OnSceneGUI(SceneView sceneView)
    {
        if (!isDebugInspector)
        {
            return;
        }

        Handles.BeginGUI();

        // Draw a semi-transparent red overlay
        GUI.color = new Color(1f, 0f, 0f, 0.2f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);

        // Reset GUI color for text
        GUI.color = Color.white;

        // Draw a label at the top left
        GUI.Label(new Rect(10, 10, 300, 20), "INSPECTOR IS IN DEBUG MODE!");

        Handles.EndGUI();
    }
}
