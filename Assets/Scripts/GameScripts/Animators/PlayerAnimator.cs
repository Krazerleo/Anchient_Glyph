using UnityEngine;

namespace AncientGlyph.GameScripts.Animators
{
    public class PlayerAnimator : MonoBehaviour
    {
        public void Move(Vector3Int offset)
        {
            transform.position += offset;
        }
    }
}