using AncientGlyph.GameScripts.Geometry;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;
using UnityEngine.Assertions;

using AncientGlyph.EditorScripts.Editors;

namespace AncientGlyph.EditorScripts.LevelEditingHandlers
{
    public class TilePlacerHandler : IAssetPlacerHandler
    {
        private Vector3 _firstPosition;
        private Vector3 _secondPosition;

        private GameObject _tilePrefab;
        private LevelModel _level;

        private LevelModelEditor _modelEditor;
        private LevelSceneEditor _sceneEditor;

        public TilePlacerHandler(LevelModel level, GameObject tilePrefab)
        {
            _level = level;
            _modelEditor = new LevelModelEditor();
            _sceneEditor = new LevelSceneEditor();
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
            throw new System.NotImplementedException();
        }

        private void ResolveTilesFromSelectedArea()
        {
            var diagonalVector = _secondPosition - _firstPosition;
            int firstPositionX, firstPositionZ, secondPositionX, secondPositionZ;

            if (diagonalVector.x > 0)
            {
                firstPositionX = Mathf.FloorToInt(_firstPosition.x);
                secondPositionX = Mathf.CeilToInt(_secondPosition.x);
            }
            else
            {
                firstPositionX = Mathf.CeilToInt(_firstPosition.x);
                secondPositionX = Mathf.FloorToInt(_secondPosition.x);
            }

            if (diagonalVector.z > 0)
            {
                firstPositionZ = Mathf.FloorToInt(_firstPosition.z);
                secondPositionZ = Mathf.CeilToInt(_secondPosition.z);
            }
            else
            {
                firstPositionZ = Mathf.CeilToInt(_firstPosition.z);
                secondPositionZ = Mathf.FloorToInt(_secondPosition.z);
            }

            Assert.AreApproximatelyEqual(_firstPosition.y, _secondPosition.y, 0.1f);
            var level = Mathf.FloorToInt(_firstPosition.y);

            var ceilRectangle = new Rectangle(new Vector3Int(firstPositionX, level, firstPositionZ), new Vector3Int(secondPositionX, level, secondPositionZ));

            _sceneEditor.PlaceTile(ceilRectangle, null);
        }
    }
}