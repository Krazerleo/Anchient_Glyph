using System.Xml.Serialization;

using AncientGlyph.GameScripts.Interactors.Interfaces;

namespace AncientGlyph.GameScripts.Serialization.Interfaces
{
    public interface IModelDataSerializer<T> where T : IEntityModel
    {
        public void SerializeElement(T element, XmlSerializationWriter XmlWriter);

        public T DeserializeElement(XmlSerializationReader XmlReader);
    }
}