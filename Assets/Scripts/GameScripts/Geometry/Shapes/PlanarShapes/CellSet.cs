using System;
using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;
using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes.PlanarShapes
{
    [Serializable]
    public class CellSet : IShape2D
    {
        [SerializeField]
        private List<Vector2Int> _cells = new();

        public CellSet()
        {
        }

        public CellSet(IEnumerable<Vector2Int> cells)
        {
            _cells = cells.ToList();
        }

        public IEnumerable<Vector2Int> GetDefinedGeometry()
        {
            foreach (Vector2Int ceil in _cells)
            {
                yield return ceil;
            }
        }

        public IEnumerable<Vector2Int> GetRotatedGeometry(int rotations)
        {
            rotations %= 4;

            switch (rotations)
            {
                case 0:
                    foreach (var cell in _cells)
                    {
                        yield return cell;
                    }
                    yield break;

                case 1:
                    foreach (var cell in _cells)
                    {
                        yield return new Vector2Int(cell.y, -cell.x - 1);
                    }
                    yield break;

                case 2:
                    foreach (var cell in _cells)
                    {
                        yield return new Vector2Int(-cell.x - 1, -cell.y - 1);
                    }
                    yield break;

                case 3:
                    foreach (var cell in _cells)
                    {
                        yield return new Vector2Int(-cell.y - 1, cell.x);
                    }
                    yield break;
            }
        }

        public RectInt GetBounds()
        {
            if (_cells.Any() == false)
            {
                return new RectInt();
            }

            var xMax = _cells.Select(cell => cell.x).Max();
            var xMin = _cells.Select(cell => cell.x).Min();
            var yMax = _cells.Select(cell => cell.y).Max();
            var yMin = _cells.Select(cell => cell.y).Min();

            return new RectInt(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        public bool Contains(Vector2Int position)
        {
            return _cells.Contains(position);
        }

        public void AddCell(Vector2Int position)
        {
            if (_cells.Contains(position) == false)
            {
                _cells.Add(position);
            }
        }

        public void RemoveCell(Vector2Int position)
        {
            _cells.Remove(position);
        }
    }
}