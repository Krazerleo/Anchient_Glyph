using System.Collections.Generic;
using AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour
{
    public abstract class MoveBehaviour
    {
        protected abstract int FreeAxis { get; }
        private readonly PathFindingAlgorithm _algorithm;

        protected MoveBehaviour(LevelModel levelModel)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            _algorithm = new PathFindingAlgorithm(levelModel, FreeAxis, GameConstants.MaxPathFindingSteps, 8);
        }

        public Vector3Int? CalculateNextStep(Vector3Int currentPosition, Vector3Int targetPosition)
        {
            if (_algorithm.TryCalculate(currentPosition, targetPosition, out IReadOnlyList<Vector3Int> path))
            {
                return path[0] - path[1];
            }

            return null;
        }
    }
}