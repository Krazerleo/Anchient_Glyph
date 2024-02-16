using System.Collections.Generic;
using System.Linq;

using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Geometry.Shapes
{
    public class CellSet : IShape2D
    {
        private IEnumerable<Vector2Int> _ceils;

        public CellSet()
        {
            _ceils = new List<Vector2Int>();
        }

        public CellSet(IEnumerable<Vector2Int> ceils)
        {
            _ceils = ceils;
        }

        public IEnumerable<Vector2Int> GetDefinedGeometry()
        {
            foreach (var ceil in _ceils)
            {
                yield return ceil;
            }
        }

        //TODO
        public IEnumerable<Vector2Int> GetDefinedGeometry(int rotations)
        {
            rotations %= 4;

            switch (rotations)
            {
                case 0:
                    foreach (var ceil in _ceils)
                    {
                        yield return ceil;
                    }
                    yield break;

                case 1:
                    foreach (var ceil in _ceils)
                    {
                        yield return ceil;
                    }
                    yield break;

                case 2:
                    foreach (var ceil in _ceils)
                    {
                        yield return ceil;
                    }
                    yield break;

                case 3:
                    foreach (var ceil in _ceils)
                    {
                        yield return ceil;
                    }
                    yield break;
            }
        }

        public RectInt GetBounds()
        {
            if (_ceils.Any() == false)
            {
                return new RectInt();
            }

            var xMax = _ceils.Select(cell => cell.x).Max();
            var xMin = _ceils.Select(cell => cell.x).Min();
            var yMax = _ceils.Select(cell => cell.y).Max();
            var yMin = _ceils.Select(cell => cell.y).Min();

            return new RectInt(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        public bool Contains(Vector2Int position)
        {
            return _ceils.Contains(position);
        }

        public void Add(Vector2Int position)
        {
            if (_ceils.Contains(position) == false)
            {
                _ceils = _ceils.Append(position);
            }
        }

        public void Remove(Vector2Int position)
        {
            if (_ceils.Contains(position))
            {
                _ceils = _ceils.Where(cell => cell != position);
            }
        }
    }
}