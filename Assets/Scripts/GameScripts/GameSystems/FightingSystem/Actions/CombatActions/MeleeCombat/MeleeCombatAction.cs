using AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding;
using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.FeedbackActions;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;
using AncientGlyph.GameScripts.Interactors.Entities.Extensions;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.CombatActions.MeleeCombat
{
    public class MeleeCombatAction : IAction
    {
        private readonly MeleeCombatActionTraits _traits;
        private readonly LevelModel _levelModel;

        public MeleeCombatAction(MeleeCombatActionTraits traits, LevelModel levelModel)
        {
            _traits = traits;
            _levelModel = levelModel;
        }

        public string Id => nameof(_traits.ActionName);
        
        public int CalculatePower()
        {
            // TODO : Implement more complex and feature based power calculation.
            return _traits.DamageHitValue;
        }

        public bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel)
        {
            var creatureCell = creatureModel.GetEntityCell(_levelModel);
            var playerCell = playerModel.GetEntityCell(_levelModel);
            var offset = playerModel.Position - creatureModel.Position;

            return creatureCell.CheckIsReachable(playerCell, offset);
        }

        public void Execute(CreatureController creatureController, PlayerModel playerModel)
        {
            creatureController.ExecuteMeleeCombatAction(this);
        }

        public IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel)
        {
            var playerPosition = playerModel.Position;
            var creaturePosition = creatureModel.Position;
            var algorithm = new PathFindingAlgorithm(_levelModel);

            if (algorithm.TryCalculate(creaturePosition, playerPosition, out var path))
            {
                return new GoToAction(path[1] - path[0]);
            }

            return new NullAction();
        }
    }
}