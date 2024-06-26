using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.EnvironmentActions
{
    public interface IEnvironmentAction
    {
        bool CheckCondition(CreatureModel creature, LevelModel levelModel);

        void Execute(LevelModel levelModel);
    }
}