using UnityEngine;
using Zenject;

namespace Photo
{
    public class RestartPlayer : MonoBehaviour
    {
        private Player _player;
        private SpawnPoint _spawnPoint;

        [Inject]
        private void Construct(Player player, SpawnPoint spawnPoint)
        {
            _player = player;
            _spawnPoint = spawnPoint;
            
            Init();
        }

        private void Init()
        {
            _player.OnDie += PlayerDie;
        }

        private void PlayerDie()
        {
            _player.SetPosition(_spawnPoint.GetPosition());
        }
    }
}