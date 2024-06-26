using System.Collections.Generic;
using AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour
{
    public abstract class MoveBehaviour
    {
        protected abstract int DoF { get; }
        private readonly PathFindingAlgorithm _pointAlgorithm;
        private readonly PathFindingAlgorithm _onLineAlgorithm;

        protected MoveBehaviour(LevelModel levelModel)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            _pointAlgorithm = new PathFindingAlgorithm(levelModel,
                                                       new PathFindingAlgoSettings(
                                                           DoF, GameConstants.MaxPathFindingSteps,
                                                           NodeExtensions.FillForPoint), 
                                                       8);
            
            // ReSharper disable once VirtualMemberCallInConstructor
            _onLineAlgorithm = new PathFindingAlgorithm(levelModel,
                                                        new PathFindingAlgoSettings(
                                                            DoF, GameConstants.MaxPathFindingSteps,
                                                            NodeExtensions.FillForLine), 
                                                        8);
        }

        public Vector3Int? CalculateNextStepToPoint(Vector3Int currentPosition, Vector3Int targetPosition)
        {
            if (_pointAlgorithm.TryCalculate(currentPosition, targetPosition, out IReadOnlyList<Vector3Int> path))
            {
                return path[^2] - path[^1];
            }

            return null;
        }

        public Vector3Int? CalculateNextStepToLine(Vector3Int currentPosition, Vector3Int targetPosition)
        {
            if (_onLineAlgorithm.TryCalculate(currentPosition, targetPosition, out IReadOnlyList<Vector3Int> path))
            {
                return path[^2] - path[^1];
            }

            return null;
        }
    }
}