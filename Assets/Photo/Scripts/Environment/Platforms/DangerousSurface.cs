using Photo;
using UnityEngine;

namespace Photo
{
    public class DangerousSurface : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
                player.Die();
        }
    }
}