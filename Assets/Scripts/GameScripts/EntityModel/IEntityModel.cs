using System;
using System.Xml.Serialization;
using AncientGlyph.GameScripts.GameWorldModel;
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
        bool IsFullSize { get; }

        Vector3Int Position { get; }

        /// <summary>
        /// Some unique identifier
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Name of the creature (also used for asset management
        /// at this moment)
        /// </summary>
        string Name { get; }

        bool TryMoveToNextCell(Direction moveDirection, LevelModel levelModel);
        bool TryMoveToNextCell(Vector3Int offset, LevelModel levelModel);
    }
}