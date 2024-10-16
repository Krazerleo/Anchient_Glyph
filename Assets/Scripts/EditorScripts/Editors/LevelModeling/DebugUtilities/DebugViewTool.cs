using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.DebugUtilities
{
    [EditorTool("Debug View")]
    public class DebugViewTool : EditorTool
    {
        private static bool IsEnabled { get; set; }

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