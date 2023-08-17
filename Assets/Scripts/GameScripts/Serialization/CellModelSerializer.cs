using System;
using System.Linq;

using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.ModelElements;

namespace AncientGlyph.GameScripts.Serialization.Interfaces
{
    public class CellModelSerializer : IModelDataSerializer<CellModel>
    {
        public CellModel DeserializeElement(Span<byte> bytes)
        {
            var walls = new WallType[4];

            int i = 1;
            for (; i < bytes.Length; i += 4)
            {
                walls[i] = (WallType) BitConverter.ToUInt32(bytes[i..(i + 4)]);
            }

            var hasFloor = BitConverter.ToUInt32(bytes[i..(i + 4)]) == 1 ? true : false;

            return new CellModel(walls, hasFloor);
        }

        public Span<byte> SerializeElement(CellModel element)
        {
            var uintArray = new uint[CellModel.SizeOfCellElement];

            uintArray[4] = element.HasFloor ? 1u : 0u;
            element.GetWalls.CopyTo(uintArray);

            return uintArray.SelectMany(BitConverter.GetBytes).ToArray().AsSpan();
        }
    }
}