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
        private readonly LevelModel _levelModel;

        protected MoveBehaviour(LevelModel levelModel)
        {
            _levelModel = levelModel;

            // ReSharper disable once VirtualMemberCallInConstructor
            _pointAlgorithm = new PathFindingAlgorithm(levelModel,
                                                       new PathFindingAlgoSettings(
                                                           DoF, GameConstants.MaxPathFindingSteps,
                                                           NodeExtensions.FillForPoint),
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

        public Vector3Int? CalculateNextStepToLine(Vector3Int currentPosition,
            Vector3Int targetPosition, int maxDistance)
        {
            Vector3Int xProjectedPosition = new(targetPosition.x, currentPosition.y, currentPosition.z);
            (Vector3Int? dir, int? dist) possibleOffsetToXProjection =
                GetBestFitDirection(currentPosition, targetPosition, xProjectedPosition, maxDistance);

            Vector3Int zProjectedPosition = new(currentPosition.x, currentPosition.y, targetPosition.z);
            (Vector3Int? dir, int? dist) possibleOffsetToZProjection =
                GetBestFitDirection(currentPosition, targetPosition, zProjectedPosition, maxDistance);

            if (possibleOffsetToXProjection.dir != null && possibleOffsetToZProjection.dir != null)
            {
                return possibleOffsetToXProjection.dist!.Value < possibleOffsetToZProjection.dist!.Value
                    ? possibleOffsetToXProjection.dir
                    : possibleOffsetToZProjection.dir;
            }

            if (possibleOffsetToXProjection.dir != null)
            {
                return possibleOffsetToXProjection.dir;
            }

            if (possibleOffsetToZProjection.dir != null)
            {
                return possibleOffsetToZProjection.dir;
            }

            if (_pointAlgorithm.TryCalculate(currentPosition, targetPosition, out IReadOnlyList<Vector3Int> path))
            {
                return path[^2] - path[^1];
            }

            return null;
        }

        private (Vector3Int? direction, int? distance) GetBestFitDirection(
            Vector3Int currentPosition, Vector3Int targetPosition,
            Vector3Int projectedPosition, int maxDistance)
        {
            if ((projectedPosition - targetPosition).sqrMagnitude >= maxDistance * maxDistance)
            {
                return (null, null);
            }

            if (_levelModel.IsRayCollided(projectedPosition, targetPosition))
            {
                return (null, null);
            }

            if (!_pointAlgorithm.TryCalculate(currentPosition, projectedPosition,
                                              out IReadOnlyList<Vector3Int> path))
            {
                return (null, null);
            }

            return (path[^2] - path[^1], path.Count);
        }
    }
}