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
        private IPriorityQueue<string, IAction> _feedbackPriority;
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
                else
                {
                    _feedbackPriority.Enqueue(action.GetFeedback(creatureModel, playerModel));
                }
            }

            if (_actionPriority.Count != 0)
            {
                var bestAction = _actionPriority.Dequeue();
                _actionPriority.Clear();
                _feedbackPriority.Clear();
                return bestAction;
            }

            if (_feedbackPriority.Count != 0)
            {
                var bestFeedback = _feedbackPriority.Dequeue();
                _feedbackPriority.Clear();
                return bestFeedback;
            }

            return new NullAction();
        }
    }
}