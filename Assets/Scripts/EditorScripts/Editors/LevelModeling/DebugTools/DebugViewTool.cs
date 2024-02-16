using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.DebugTools
{
    [EditorTool("Debug View")]
    public class DebugViewTool : EditorTool
    {
        public static bool IsEnabled { get; private set; }

        public override void OnActivated()
        {
            IsEnabled = true;
            DebugDrawer.RedrawLevelModel();
        }

        public override void OnWillBeDeactivated()
        {
            IsEnabled = false;
            DebugDrawer.RemovePreviousDebugView();
        }

        public override GUIContent toolbarIcon => GetEditorIcon();

        private GUIContent GetEditorIcon()
        {
            return IsEnabled ? EditorGUIUtility.IconContent("d_DebuggerAttached") : EditorGUIUtility.IconContent("d_DebuggerDisabled");
        }
    }
}