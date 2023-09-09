using AncientGlyph.EditorScripts.Editors;
using AncientGlyph.EditorScripts.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Shapes;
using AncientGlyph.GameScripts.Geometry.Extensions;

using UnityEngine;
using UnityEngine.Assertions;

namespace AncientGlyph.EditorScripts.LevelEditingHandlers
{
    public class TilePlacerHandler : IAssetPlacerHandler
    {
        #region Private Fields

        private const float DistanceTolerance = 0.01f;

        #endregion Private Fields



        #region Private Fields

        private Vector3 _firstPosition;
        private LevelModelEditor _modelEditor;
        private LevelSceneEditor _sceneEditor;
        private Vector3 _secondPosition;
        private GameObject _tilePrefab;

        #endregion Private Fields

        #region Public Constructors

        public TilePlacerHandler(LevelModel levelModel, GameObject tilePrefab)
        {
            _tilePrefab = tilePrefab;
            _modelEditor = new LevelModelEditor(levelModel);
            _sceneEditor = new LevelSceneEditor();
        }

        #endregion Public Constructors

        #region Public Methods

        public void OnMouseButtonClickHandler(Vector3 position)
        {
            _sceneEditor.PlaceTile(new Point(position.ToVector3Int()), _tilePrefab);
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
            var diagonalVector = _secondPosition - _firstPosition;

            if (diagonalVector.magnitude < DistanceTolerance)
            {
                _sceneEditor.PlaceTile(new Point(_secondPosition.ToVector3Int()), _tilePrefab);
                return;
            }

            int firstPositionX, secondPositionX, firstPositionZ, secondPositionZ;

            if (diagonalVector.x > DistanceTolerance)
            {
                firstPositionX = Mathf.FloorToInt(_firstPosition.x);
                secondPositionX = Mathf.FloorToInt(_secondPosition.x);
            }
            else
            {
                firstPositionX = Mathf.CeilToInt(_firstPosition.x);
                secondPositionX = Mathf.CeilToInt(_secondPosition.x);
            }

            if (diagonalVector.z > DistanceTolerance)
            {
                firstPositionZ = Mathf.FloorToInt(_firstPosition.z);
                secondPositionZ = Mathf.FloorToInt(_secondPosition.z);
            }
            else
            {
                firstPositionZ = Mathf.CeilToInt(_firstPosition.z);
                secondPositionZ = Mathf.CeilToInt(_secondPosition.z);
            }

            var level = Mathf.FloorToInt(_firstPosition.y);

            var ceilRectangle = new Rectangle(
                new Vector3Int(firstPositionX, level, firstPositionZ),
                new Vector3Int(secondPositionX, level, secondPositionZ));

            _sceneEditor.PlaceTile(ceilRectangle, _tilePrefab);
        }

        #endregion Private Methods
    }
}