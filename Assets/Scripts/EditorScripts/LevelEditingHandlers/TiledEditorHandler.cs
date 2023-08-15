using AncientGlyph.GameScripts.Geometry;
using AncientGlyph.GameScripts.LevelModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace AncientGlyph.EditorScripts.LevelEditingHandlers
{
    public class TileEditorHandler : ILevelEditingByMouseHandler
    {
        private Vector3 _firstPosition;
        private Vector3 _secondPosition;

        private Vector3Int _tilesInSelectedRegion;
        private Level _level;

        public TileEditorHandler(Level level)
        {
            _level = level;
        }

        public void OnMouseButtonClickHandler(Vector3 position)
        {
            throw new System.NotImplementedException();
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
        }
    }
}