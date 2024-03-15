using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.FeedbackActions;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour;
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

        public bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour)
        {
            var creatureCell = creatureModel.GetEntityCell(_levelModel);
            var playerCell = playerModel.GetEntityCell(_levelModel);
            var offset = playerModel.Position - creatureModel.Position;

            return creatureCell.CheckIsReachable(playerCell, offset);
        }

        public void Execute(CreatureController creatureController, PlayerController playerController)
        {
            creatureController.ExecuteMeleeCombatAction(this, playerController);
        }

        public IAction GetFeedback(CreatureModel creatureModel, PlayerModel playerModel,
            IMoveBehaviour moveBehaviour)
        {
            var offset = moveBehaviour.CalculateNextStep(creatureModel.Position, playerModel.Position);

            if (offset == null)
            {
                return new NullAction();
            }

            return new GoToAction(offset.Value);
        }
    }
}