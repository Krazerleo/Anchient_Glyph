using System.Collections.Generic;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour
{
    public class FlyingBehaviour : IMoveBehaviour
    {
        public IEnumerable<Vector3Int> YieldFromNeighborCells(Vector3Int position, LevelModel levelModel)
        {
            throw new System.NotImplementedException("TODO : create algorithm for traversing all neighbor cells");
        }
    }
}