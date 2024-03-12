using System.Collections.Generic;
using AncientGlyph.GameScripts.AlgorithmsAndStructures.PriorityQueue;
using AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours
{
    public class CreatureBehaviour : ICreatureBehaviour
    {
        private IPriorityQueue<string, IAction> _actionPriority;
        private IMoveBehaviour _moveBehaviour;
        
        private CreatureBehaviour() { }
        
        public static ICreatureBehaviour CreateFromOptions(MovementType type)
        {
            var comparer = Comparer<IAction>.Create((a, b) => b.CalculatePower().CompareTo(a.CalculatePower()));
            
            var creatureBehaviour = new CreatureBehaviour
            {
                _moveBehaviour = MoveBehaviourFactory.CreateCreatureBehaviour(type),
                _actionPriority = new BinaryHeap<string, IAction>(comparer, action => action.Id, 0)
            };
            
            return creatureBehaviour;
        }


        public IAction PlanForTurn(CreatureModel creatureModel, PlayerModel playerModel, LevelModel levelModel)
        {
            foreach (var action in creatureModel.Actions)
            {
                if (action.CanExecute(creatureModel, playerModel))
                {
                    _actionPriority.Enqueue(action);
                }
            }

            if (_actionPriority.Count == 0)
            {
                return null;
            }

            var bestAction = _actionPriority.Dequeue();
            _actionPriority.Clear();
            return bestAction;
        }
    }
}