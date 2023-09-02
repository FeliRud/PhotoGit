using Photo.Scripts.Environment;
using UnityEngine;

namespace Photo
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private ChangeSpawnPoint _spawnPoint;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        public void Respawn()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.transform.position = _spawnPoint.CurrentSpawnPosition;
        }
    }
}