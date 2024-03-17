using System;
using System.Xml.Serialization;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel
{
    /// <summary>
    /// Basic description for entity model
    /// </summary>
    public interface IEntityModel : IXmlSerializable, IEquatable<IEntityModel>
    {
        /// <summary>
        /// Indicate if it cannot be placed in same cell with
        /// other full size entity
        /// </summary>
        public bool IsFullSize { get; }
        public Vector3Int Position { get; set; }
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