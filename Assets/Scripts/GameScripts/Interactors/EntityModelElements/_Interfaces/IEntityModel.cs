using System.Xml.Serialization;

using AncientGlyph.GameScripts.Interactors.Interaction.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Interfaces
{
    public interface IEntityModel : IInteractable, IXmlSerializable
    {
        #region Public Properties

        public bool IsFullSize { get; }

        public Vector3Int Position { get; }

        #endregion Public Properties

        #region Public Methods

        public void GetEntityInfo();

        #endregion Public Methods
    }
}