using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Utils
{
    public class CameraHelper
    {
        /// <summary>
        /// Find vec3 world position from cursor in editor window.
        /// </summary>
        /// <remarks>
        /// Can be used only in SceneView context.
        /// </remarks>
        /// <param name="cursorInWorldPosition"></param>
        /// <returns></returns>
        public static bool EditorCursorToWorldPosition(out Vector3 cursorInWorldPosition)
        {
            var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                cursorInWorldPosition = hit.point;
                return true;
            }

            cursorInWorldPosition = Vector3.zero;
            return false;
        }
    }
}