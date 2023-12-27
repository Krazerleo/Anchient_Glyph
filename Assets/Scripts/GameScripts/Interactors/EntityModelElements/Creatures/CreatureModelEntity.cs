using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

using AncientGlyph.GameScripts.Interactors.Interaction;
using AncientGlyph.GameScripts.Interactors.Interaction.Interfaces;
using AncientGlyph.GameScripts.Interactors.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Creatures
{
    public class CreatureEntityModel : IInteractable, IEntityModel
    {
        #region Private Fields

        private Vector3Int _creaturePosition;
        private CreatureTraits _creatureTraits;

        #endregion Private Fields

        #region Public Constructors

        public CreatureEntityModel(CreatureTraits traits, Vector3Int position)
        {
            _creatureTraits = traits;
            _creaturePosition = position;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool IsFullSize => _creatureTraits.IsFullSize;

        public Vector3Int Position => _creaturePosition;

        #endregion Public Properties

        #region Public Methods

        public void GetEntityInfo()
        {
            //TODO
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void Hit(IEntityModel toEntity)
        {
            //TODO
            new HitInteraction(10).InteractTo(toEntity);
        }

        public void InteractWith(HitInteraction hit)
        {
            //TODO
        }

        public void InteractWith(FunctionalInteraction func)
        {
            //TODO
        }

        public void InteractWith(ICollection<object> listItems)
        {
            //TODO
        }

        public void ReadXml(XmlReader reader)
        {
            //TODO
        }

        public void WriteXml(XmlWriter writer)
        {
            //TODO
        }

        #endregion Public Methods
    }
}