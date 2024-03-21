using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Utils
{
    public static class HideEntitiesMarkers
    {
        private const string EntityMarkerTag = "EditorEntityMarker";

        [InitializeOnLoadMethod]
        public static void ToggleEntityVisibility()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.EnteredPlayMode)
            {
                var markers = GameObject.FindGameObjectsWithTag(EntityMarkerTag);

                foreach (var marker in markers)
                {
                    marker.SetActive(false);
                }
            }

            if (change == PlayModeStateChange.ExitingPlayMode)
            {
                var markers = GameObject.FindGameObjectsWithTag(EntityMarkerTag);

                foreach (var marker in markers)
                {
                    marker.SetActive(true);
                }
            }
        }
    }
}