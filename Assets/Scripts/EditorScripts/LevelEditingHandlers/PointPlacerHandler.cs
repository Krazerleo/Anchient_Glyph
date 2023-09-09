using AncientGlyph.EditorScripts.LevelEditingHandlers.Interfaces;

using UnityEngine;

namespace AncientGlyph.EditorScripts.LevelEditingHandlers
{
    public class PointPlacerHandler : IAssetPlacerHandler
    {
        public void OnMouseButtonPressedHandler(Vector3 position)
        {
        }

        public void OnMouseButtonReleasedHandler(Vector3 position)
        {
        }

        public void OnMouseButtonClickHandler(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void OnMouseMoveHandler(Vector3 position)
        {
        }

        public void SetPrefabObject(GameObject prefab)
        {
        }
    }
}