using System;
using System.Collections.Generic;
using System.Linq;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;
using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes.PlanarShapes
{
    [Serializable]
    public class CellSet : IShape2D, IEquatable<CellSet>
    {
        [SerializeField]
        private List<Vector2Int> _cells = new();

        public CellSet() { }

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

        public CellSet GetRotatedGeometry(int rotations)
        {
            rotations %= 4;

            RectInt initialBoundingBox = GetBoundingBox();
            CellSet rotatedGeometry = new(RotateAroundZeroPivot(rotations));
            RectInt rotatedBoundingBox = rotatedGeometry.GetBoundingBox();
            Vector2Int offsetToAlign = new(rotatedBoundingBox.x - initialBoundingBox.x,
                                           rotatedBoundingBox.y - initialBoundingBox.y);
            rotatedGeometry.Translate(-offsetToAlign);

            return rotatedGeometry;
        }


        public RectInt GetBoundingBox()
        {
            if (_cells.Any() == false)
            {
                return new RectInt();
            }

            int xMax = _cells.Select(cell => cell.x).Max();
            int xMin = _cells.Select(cell => cell.x).Min();
            int yMax = _cells.Select(cell => cell.y).Max();
            int yMin = _cells.Select(cell => cell.y).Min();

            return new RectInt(xMin, yMin, xMax - xMin + 1, yMax - yMin + 1);
        }

        public void AddCell(Vector2Int position)
        {
            if (_cells.Contains(position) == false)
            {
                _cells.Add(position);
            }
        }

        public bool Contains(Vector2Int position) => _cells.Contains(position);

        public void RemoveCell(Vector2Int position) => _cells.Remove(position);

        public bool Equals(CellSet otherSet)
        {
            if (otherSet == null)
            {
                return false;
            }

            if (_cells.Count != otherSet._cells.Count)
            {
                return false;
            }

            Dictionary<Vector2Int, int> thisCellsCount = _cells.ToDictionary(k => k, _ => 1);

            foreach (Vector2Int cell in otherSet._cells)
            {
                if (thisCellsCount.ContainsKey(cell))
                {
                    thisCellsCount[cell]--;
                }
                else
                {
                    return false;
                }
            }

            foreach ((_, int value) in thisCellsCount)
            {
                if (value != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private List<Vector2Int> RotateAroundZeroPivot(int rotations)
        {
            Debug.Assert(rotations < 4);
            List<Vector2Int> rotatedCells = new(_cells.Count);

            switch (rotations)
            {
                case 0:
                    foreach (Vector2Int cell in _cells)
                    {
                        rotatedCells.Add(new Vector2Int(cell.x, cell.y));
                    }

                    break;

                case 1:
                    foreach (Vector2Int cell in _cells)
                    {
                        rotatedCells.Add(new Vector2Int(cell.y, -cell.x - 1));
                    }

                    break;

                case 2:
                    foreach (Vector2Int cell in _cells)
                    {
                        rotatedCells.Add(new Vector2Int(-cell.x - 1, -cell.y - 1));
                    }

                    break;

                case 3:
                    foreach (Vector2Int cell in _cells)
                    {
                        rotatedCells.Add(new Vector2Int(-cell.y - 1, cell.x));
                    }

                    break;
            }

            return rotatedCells;
        }

        private void Translate(Vector2Int translation)
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                _cells[i] += translation;
            }
        }

        public Vector2 FindCenterOfBoundingBox()
        {
            RectInt boundingBox = GetBoundingBox();
            return new Vector2(boundingBox.x + boundingBox.width / 2f,
                               boundingBox.y + boundingBox.height / 2f);
        }
    }
}