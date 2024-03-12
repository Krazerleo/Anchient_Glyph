using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours
{
    public class CreatureBehaviour : ICreatureBehaviour
    {
        private IMoveBehaviour _moveBehaviour;
        
        private CreatureBehaviour() { }
        
        public static ICreatureBehaviour CreateFromOptions(MovementType type)
        {
            var creatureBehaviour = new CreatureBehaviour
            {
                _moveBehaviour = MoveBehaviourFactory.CreateCreatureBehaviour(type)
            };

            return creatureBehaviour;
        }


        public void PlanForTurn(CreatureModel creatureModel, LevelModel levelModel)
        {
            foreach (var cell in _moveBehaviour.YieldFromNeighborCells(creatureModel.Position, levelModel))
            {
                // TODO: DO SOMETHING WITH ACTION SCORING
            }
        }
    }
}