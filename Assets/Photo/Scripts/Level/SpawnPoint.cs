using UnityEngine;

namespace Photo
{
    public class SpawnPoint : MonoBehaviour
    {
        public Vector3 GetPosition() => 
            transform.position;
    }
}