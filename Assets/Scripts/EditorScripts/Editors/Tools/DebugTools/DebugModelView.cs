using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.DebugTools
{
    [EditorTool("Debug View")]
    public class DebugModelView : EditorTool
    {
        public static bool IsEnabled { get; private set; }
        public override void OnActivated()
        {
            IsEnabled = true;
        }

        public override void OnWillBeDeactivated()
        {
            IsEnabled = false;
        }

        public override GUIContent toolbarIcon => GetEditorIcon();

        private GUIContent GetEditorIcon()
        {
            return IsEnabled ? EditorGUIUtility.IconContent("d_DebuggerAttached") : EditorGUIUtility.IconContent("d_DebuggerDisabled");
        }
    }
}