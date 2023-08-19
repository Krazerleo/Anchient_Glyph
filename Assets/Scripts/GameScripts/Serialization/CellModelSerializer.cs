using System;
using System.IO;
using System.Runtime.InteropServices;

using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Serialization.Interfaces
{
    public class CellModelSerializer
    {
        public CellModel DeserializeElement(BinaryReader reader)
        {
            var bytes = reader.ReadBytes(CellModel.SizeOfElementBytes);
            var hasFloor = BitConverter.ToUInt32(bytes[0..4]) == 1 ? true : false;
            var walls = new WallType[4];

            for (var i = 0; i < bytes.Length; i += CellModel.SizeOfCellElement)
            {
                walls[i] = (WallType) BitConverter.ToUInt32(bytes[i..(i + 4)]);
            }

            return new CellModel(walls, hasFloor);
        }

        public void SerializeElement(CellModel element, BinaryWriter writer)
        {
            writer.Write(element.HasFloor ? 1 : 0);
            writer.Write(MemoryMarshal.Cast<uint, byte>(element.GetWalls));
        }
    }
}