using UnityEngine;

namespace AncientGlyph.GameScripts.EntityModel.Controller.CreatureBehaviours.MoveBehaviour
{
    public interface IMoveBehaviour
    {
        public Vector3Int? CalculateNextStep(Vector3Int currentPosition, Vector3Int targetPosition);
    }
}