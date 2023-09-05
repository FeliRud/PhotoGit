using UnityEngine;

namespace Photo
{
    public class SpawnBox : MonoBehaviour
    {
        [SerializeField] private Box _box;
        [SerializeField] private Lever _lever;
        private bool _boxSpawned;

        private void Start() => 
            _lever.OnInteractionEvent += LeverInteraction;

        private void LeverInteraction()
        {
            if (_lever.Enabled)
                Spawn();
        }

        private void Spawn()
        {
            if (_boxSpawned)
            {
                _box.Respawn();
                return;
            }

            FirstSpawn();
        }

        private void FirstSpawn()
        {
            _boxSpawned = true;
            _box.GetComponentInChildren<Rigidbody2D>().gravityScale = 1;
        }
    }
}