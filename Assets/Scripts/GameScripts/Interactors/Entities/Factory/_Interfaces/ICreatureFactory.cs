using System;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities.Factory._Interfaces
{
    public interface ICreatureFactory
    {
        /// <summary>
        /// Instantiate creature on scene and
        /// configure its model traits
        /// </summary>
        /// <param name="position">Position on scene</param>
        /// <param name="creatureModel">Creature model</param>
        /// <returns>Creature Controller</returns>
        /// <exception cref="ArgumentException"></exception>
        public UniTask<CreatureController> CreateCreature(Vector3Int position,
                                                          CreatureModel creatureModel);
    }
}