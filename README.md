<h1>Unity Debug Overlays</h1>

<p>
A collection of editor scripts that let you detect and highlight when Unity's Inspector is in Debug Mode, 
and optionally show warnings or overlays in the Hierarchy and Project windows.
</p>

<hr/>

<h2>Overview</h2>
<p>
These scripts use Unity's editor events and reflection to detect the Inspector's "Debug Mode" state, 
then display overlays or warnings in various parts of the editor (Hierarchy, Project, Scene View).
</p>

<ul>
  <li><strong>InspectorDebugChecker.cs</strong> – Detects and logs when the Inspector is in Debug Mode.</li>
  <li><strong>HierarchyDebugOverlay.cs</strong> – Displays a simple overlay/label in the Hierarchy window if in Debug Mode.</li>
  <li><strong>ProjectDebugOverlay.cs</strong> – Displays a label next to items in the Project window if in Debug Mode.</li>
</ul>

<hr/>

<h2>Project Structure</h2>
<p>
The recommended folder structure inside <code>Assets</code>:
</p>

<pre>
Assets/
  Editor/
    InspectorDebugChecker.cs
    HierarchyDebugOverlay.cs
    ProjectDebugOverlay.cs
  Scripts/
    (your other runtime scripts)
  ...
</pre>

<p>
Unity automatically treats any scripts in an <code>Editor</code> folder as Editor-only code.
</p>

<hr/>

<h2>How It Works</h2>

<h3>InspectorDebugChecker</h3>
<p>
- Uses <code>[InitializeOnLoad]</code> and <code>EditorApplication.update</code> to check every frame if the Inspector is in Debug Mode.<br/>
- Relies on reflection to find the hidden <code>inspectorMode</code> property in Unity's <code>InspectorWindow</code>.<br/>
- Logs a warning or displays a custom overlay whenever <code>InspectorMode.Debug</code> is detected.
</p>

<h3>HierarchyDebugOverlay</h3>
<p>
- Hooks into <code>EditorApplication.hierarchyWindowItemOnGUI</code>.<br/>
- If the Inspector is in Debug Mode (detected via <code>InspectorDebugChecker</code>), draws a small label for each Hierarchy item.
  - It is possible to highlight or other options here as well or as opposed to
</p>

<h3>ProjectDebugOverlay</h3>
<p>
- Similar to the Hierarchy approach, but uses <code>EditorApplication.projectWindowItemOnGUI</code>.<br/>
- Displays a label next to assets in the Project window if in Debug Mode.
- It is possible to highlight or other options here as well or as opposed to
</p>

<hr/>

<h2>Installation</h2>
<ol>
  <li>Download or clone this repository.</li>
  <li>Place the <code>Editor</code> folder (containing all three scripts) anywhere under <code>Assets</code> in the Unity project.</li>
  <li>Restart Unity (or recompile) to ensure the editor scripts load properly.</li>
</ol>

<hr/>

<h2>Usage</h2>
<ol>
  <li>Open any Unity scene.</li>
  <li>Select an object and switch the Inspector to <strong>Debug Mode</strong> (by right-clicking the Inspector tab, choosing Debug).</li>
  <li>You should see logs in the Console and overlays/warnings in the Hierarchy/Project windows (depending on which scripts you have active).</li>
</ol>

<hr/>

<h2>Notes and Customization</h2>
<ul>
  <li><strong>Performance:</strong> I read that checking every frame is typically fine in the Editor, 
      but this can be switched to <code>Selection.selectionChanged</code> or other events if fewer checks are preferred/necessary.</li>
  <li><strong>Overlay Appearance:</strong> Currently draws a simple label. 
      You can customize colors, fonts, or add icons as needed.</li>
  <li><strong>Disabling Overlays:</strong> For just the Inspector check (without Hierarchy or Project overlays), 
      simply remove or comment out the overlay scripts.  Comment out or modify to taste</li>
</ul>

<hr/>
