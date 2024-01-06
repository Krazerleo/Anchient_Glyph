using AncientGlyph.GameScripts.Interactors.Interaction.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Interfaces
{
    public interface IEntityModel : IInteractable
    {
        public bool IsFullSize { get; }
        public Vector3Int Position { get; }
        public string Identifier { get; }
        public string Name { get; }
    }
}