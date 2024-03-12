using AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Interactors.Entities;
using AncientGlyph.GameScripts.Interactors.Entities.Controller;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.FightingSystem.Actions.FeedbackActions
{
    public class GoToAction : IAction
    {
        private readonly Vector3Int _startPosition;
        private readonly Vector3Int _endPosition;
        private readonly LevelModel _levelModel;
        private readonly CreatureController _controller;

        public GoToAction(Vector3Int startPosition,
            Vector3Int endPosition,
            LevelModel levelModel,
            CreatureController controller)
        {
            _startPosition = startPosition;
            _endPosition = endPosition;
            _levelModel = levelModel;
        }
        
        public string Id => nameof(GoToAction);
        
        public int CalculatePower() => 1;

        public bool CanExecute(CreatureModel creatureModel, PlayerModel playerModel) => true;

        public void Execute()
        {
            var algo = new PathFindingAlgorithm(_levelModel, 32, 4);

            var path = algo.Calculate(_startPosition, _endPosition);
        }

        public IAction GetFeedback()
        {
            throw new System.NotImplementedException();
        }
    }
}