using AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Extensions;
using AncientGlyph.GameScripts.Geometry.Shapes;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers
{
    public class TilePlacerHandler : IAssetPlacerHandler
    {
        #region Private Fields

        private const float DistanceTolerance = 0.01f;

        private Vector3 _firstPosition;
        private LevelModelEditor _levelEditor;
        private LevelSceneEditor _sceneEditor;
        private Vector3 _secondPosition;
        private GameObject _tilePrefab;

        #endregion Private Fields

        #region Public Constructors

        public TilePlacerHandler()
        {
            _levelEditor = new LevelModelEditor();
            _sceneEditor = new LevelSceneEditor();
        }

        #endregion Public Constructors

        #region Public Methods

        public void OnMouseButtonClickHandler(Vector3 position)
        {
            _sceneEditor.PlaceTile(new Point(position.ToVector3Int()), _tilePrefab);
            _levelEditor.PlaceTile(new Point(position.ToVector3Int()));
        }

        public void OnMouseButtonPressedHandler(Vector3 firstPosition)
        {
            _firstPosition = firstPosition;
        }

        public void OnMouseButtonReleasedHandler(Vector3 secondPosition)
        {
            _secondPosition = secondPosition;
            ResolveTilesFromSelectedArea();
        }

        public void OnMouseMoveHandler(Vector3 secondPosition)
        {
            return;
        }

        public void SetPrefabObject(GameObject prefab)
        {
            _tilePrefab = prefab;
        }

        #endregion Public Methods

        #region Private Methods

        private void ResolveTilesFromSelectedArea()
        {
            var ceilRectangle = new Rectangle(_secondPosition.ToVector3Int(), _firstPosition.ToVector3Int());

            _sceneEditor.PlaceTile(ceilRectangle, _tilePrefab);
            _levelEditor.PlaceTile(ceilRectangle);
        }

        #endregion Private Methods
    }
}