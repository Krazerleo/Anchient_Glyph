using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.EditingHandlers
{
    /// <summary>
    /// Mouse handlers for adding wall and tile
    /// objects to scene and level model.
    /// </summary>
    public interface IAssetPlacerHandler
    {
        void OnMouseButtonPressedHandler(Vector3 position);

        void OnMouseButtonReleasedHandler(Vector3 position);

        void OnMouseMoveHandler(Vector3 position);

        void SetPrefabObject(GameObject prefab);
    }
}