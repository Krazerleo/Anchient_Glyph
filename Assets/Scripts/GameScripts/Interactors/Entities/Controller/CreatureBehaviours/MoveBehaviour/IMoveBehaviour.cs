using AncientGlyph.GameScripts.GameWorldModel;
using UnityEngine;

namespace AncientGlyph.GameScripts.Interactors.Entities.Controller.CreatureBehaviours.MoveBehaviour
{
    public interface IMoveBehaviour
    {
        public Vector3Int? CalculateNextStep(Vector3Int currentPosition, Vector3Int targetPosition);
    }
}