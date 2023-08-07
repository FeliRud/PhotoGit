using UnityEngine;

namespace Photo
{
    public class Respawn : MonoBehaviour
    {
        [SerializeField] private Transform _spwanPoint;

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.transform.position = _spwanPoint.position;
        }
    }
}