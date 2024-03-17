using AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour
{
    public class GroundedBehaviour : IMoveBehaviour
    {
        private const int FreeAxis = 2;
        private readonly PathFindingAlgorithm _algorithm;

        public GroundedBehaviour(LevelModel levelModel)
        {
            _algorithm = new PathFindingAlgorithm(levelModel, FreeAxis, GameConstants.MaxPathFindingSteps, 8);
        }

        public Vector3Int? CalculateNextStep(Vector3Int currentPosition, Vector3Int targetPosition)
        {
            if (_algorithm.TryCalculate(currentPosition, targetPosition, out var path))
            {
                return path[1] - path[0];
            }

            return null;
        }
    }
}