using AncientGlyph.GameScripts.Enums;
using UnityEngine.Assertions;

namespace AncientGlyph.GameScripts.LevelModel
{
    public struct Cell
    {
        public Cell(WallType[] walls, bool hasFloor)
        {
            Assert.IsTrue(walls.Length == 4);

            Walls = walls;
            HasFloor = hasFloor;
        }

        public void SetWall(WallType wall, Direction direction)
        {
            Walls[(uint)direction] = wall;
        }

        public WallType[] Walls { get; private set; }
        public bool HasFloor { get; set; }
    }
}