using System;

using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.Serialization
{
    public class CellModelDeserializer
    {
        public static CellModel Deserialize(Span<byte> bytes)
        {
            var walls = new WallType[6];

            for (var i = 0; i < bytes.Length; i += sizeof(uint))
            {
                walls[i / sizeof(uint)] = (WallType) BitConverter.ToUInt32(bytes[i..(i + 4)]);
            }

            return new CellModel(walls);
        }
    }
}