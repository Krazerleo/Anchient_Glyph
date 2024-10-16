using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.EditingHandlers
{
    public class ItemPlacerHandler : IAssetPlacerHandler
    {
        private GameObject _itemPrefab;
        private readonly LevelSceneEditor _sceneEditor = new();
        
        public void OnMouseButtonPressedHandler(Vector3 position)
        {
        }

        public void OnMouseButtonReleasedHandler(Vector3 position)
        {
            _sceneEditor.PlaceItem(position, _itemPrefab);
        }

        public void OnMouseMoveHandler(Vector3 position)
        {
        }

        public void SetPrefabObject(GameObject prefab) => _itemPrefab = prefab;
    }
}