using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.GameWorldModel;

namespace AncientGlyph.GameScripts.GameSystems.ActionSystem.EnvironmentActions
{
    public class BlowBarrelAction : IEnvironmentAction
    {
        public bool CheckCondition(CreatureModel creature, LevelModel levelModel)
        {
            return true;
        }

        public void Execute(LevelModel levelModel)
        {
            // TODO: Implement magic system
            throw new System.NotImplementedException();
        }
    }
}