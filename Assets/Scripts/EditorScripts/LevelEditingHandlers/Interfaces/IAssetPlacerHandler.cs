using UnityEngine;

namespace AncientGlyph.EditorScripts.LevelEditingHandlers.Interfaces
{
    /// <summary>
    /// Mouse handlers for adding wall and tile
    /// objects to scene and level model.
    /// </summary>
    public interface IAssetPlacerHandler
    {
        #region Public Methods

        void OnMouseButtonPressedHandler(Vector3 position);

        void OnMouseButtonReleasedHandler(Vector3 position);

        void OnMouseMoveHandler(Vector3 position);

        void OnMouseButtonClickHandler(Vector3 position);

        void SetPrefabObject(GameObject prefab);

        #endregion Public Methods
    }
}