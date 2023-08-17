using System;

namespace AncientGlyph.GameScripts.Serialization.Interfaces
{
    public interface IModelDataSerializer<T>
    {
        public Span<byte> SerializeElement(T element);

        public T DeserializeElement(Span<byte> bytes);
    }
}