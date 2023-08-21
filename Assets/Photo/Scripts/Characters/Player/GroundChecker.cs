using UnityEngine;

namespace Photo
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;

        public bool Check()
        {
            return Physics2D.OverlapCircle(transform.position, 0.5f, _groundLayer);
        }
    }
}