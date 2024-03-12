using System.Collections.Generic;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour
{
    public class GroundedBehaviour : IMoveBehaviour
    {
        public IEnumerable<Vector3Int> YieldFromNeighborCells(Vector3Int position, LevelModel levelModel)
        {
            // TODO : create algorithm for traversing all neighbor cells
            throw new System.NotImplementedException();
        }
    }
}