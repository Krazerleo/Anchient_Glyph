#nullable enable
using AncientGlyph.GameScripts.EntityModel.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel.Factory
{
    public interface ICreatureFactory
    {
        public UniTask<CreatureController?> CreateCreature(Vector3Int position,
                                                          CreatureModel creatureModel,
                                                          PlayerController playerController);
    }
}