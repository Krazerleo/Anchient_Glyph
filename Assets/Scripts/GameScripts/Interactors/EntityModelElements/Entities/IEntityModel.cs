using System.Xml.Serialization;
using AncientGlyph.GameScripts.Interactors.Interaction;

using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors
{
    /// <summary>
    /// Basic description for entity model
    /// </summary>
    public interface IEntityModel : IInteractable, IXmlSerializable
    {
        /// <summary>
        /// Indicate if cannot be placed in same cell with
        /// other full size entity
        /// </summary>
        public bool IsFullSize { get; }
        public Vector3Int Position { get; }
        /// <summary>
        /// Some unique identifier
        /// </summary>
        public string Identifier { get; }
        /// <summary>
        /// Name of the creature (also used for asset managment
        /// at this moment)
        /// </summary>
        public string Name { get; }
    }
}