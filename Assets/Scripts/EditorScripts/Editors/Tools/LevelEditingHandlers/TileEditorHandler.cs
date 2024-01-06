using AncientGlyph.EditorScripts.Editors.UndoRedo;
using AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.Geometry.Extensions;
using AncientGlyph.GameScripts.Geometry.Shapes;

using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers
{
    public class TilePlacerHandler : IAssetPlacerHandler
    {
        private Vector3 _firstPosition;
        private LevelModelEditor _levelEditor;
        private LevelSceneEditor _sceneEditor;
        private Vector3 _secondPosition;
        private GameObject _tilePrefab;

        public TilePlacerHandler()
        {
            _levelEditor = new LevelModelEditor();
            _sceneEditor = new LevelSceneEditor();
        }

        public void OnMouseButtonClickHandler(Vector3 position)
        {
            _sceneEditor.PlaceTile(new Point(position.ToVector3Int()), _tilePrefab);
            _levelEditor.TryPlaceTile(new Point(position.ToVector3Int()));
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

        public void OnMouseMoveHandler(Vector3 secondPosition) { }

        public void SetPrefabObject(GameObject prefab)
        {
            _tilePrefab = prefab;
        }

        private void ResolveTilesFromSelectedArea()
        {
            var ceilRectangle = new Rectangle(_secondPosition.ToVector3Int(),
                                              _firstPosition.ToVector3Int());

            if (_levelEditor.TryPlaceTile(ceilRectangle))
            {
                _sceneEditor.PlaceTile(ceilRectangle, _tilePrefab);
                History.GetHistoryInstance.AddAction(
                    new AddTileAction(ceilRectangle));
            }
        }
    }
}