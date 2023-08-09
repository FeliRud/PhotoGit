using UnityEngine;

namespace Photo
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;

        public bool Check()
        {
            return Physics2D.Raycast(transform.position, Vector2.down,0.1f, _groundLayer);
        }
    }
}