using AncientGlyph.GameScripts.Interactors.Interaction.Interfaces;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Interfaces
{
    public interface IEntityModel : IInteractable
    {
        public void GetEntityInfo();

        public Vector3Int Position { get; }
        public bool IsFullSize { get; }
    }
}