using System;
using System.Linq;

using AncientGlyph.GameScripts.Enums;

using UnityEngine.Assertions;

namespace AncientGlyph.GameScripts.ModelElements
{
    public struct CellModel
    {
        public const int SizeOfCellElement = 5;
        public const int SizeOfElementBytes = 5 * sizeof(uint);

        private uint[] _cellData;

        public CellModel(WallType[] walls, bool hasFloor)
        {
            Assert.IsTrue(walls.Length == 4);
            _cellData = new uint[4];
            _cellData[0] = hasFloor ? 1u : 0u;
        }

        public void SetWall(WallType wall, Direction direction)
        {
            _cellData[(uint) direction] = (uint) wall;
        }

        public Span<uint> GetWalls => _cellData.AsSpan(1, 4);

        public bool HasFloor => _cellData[0] == 1 ? true : false;

        public Span<byte> SerializeElement()
        {
            var uintArray = new uint[SizeOfCellElement];

            uintArray[0] = HasFloor ? 1u : 0u;

            for (var i = 1; i < 5; i++)
            {
                uintArray[i] = _cellData[i];
            }

            return uintArray.SelectMany(BitConverter.GetBytes).ToArray().AsSpan();
        }

        public CellModel DeserializeElement(Span<byte> bytes)
        {
            var walls = new WallType[4];
            bool hasFloor = BitConverter.ToUInt32(bytes[0..4]) == 1 ? true : false;

            for (var i = 1; i < bytes.Length; i += 4)
            {
                walls[i] = (WallType) BitConverter.ToUInt32(bytes[i..(i + 4)]);
            }

            return new CellModel(walls, hasFloor);
        }
    }
}