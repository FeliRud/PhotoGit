using UnityEngine;
using Zenject;

namespace Photo
{
    public class Restart : MonoBehaviour
    {
        private Player _player;
        private SpawnPoint _spawnPoint;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(Player player, SpawnPoint spawnPoint, SceneLoader sceneLoader)
        {
            _player = player;
            _spawnPoint = spawnPoint;
            _sceneLoader = sceneLoader;
            
            Init();
        }

        private void Init()
        {
            _player.OnDie += PlayerDie;
        }

        private void PlayerDie()
        {
            _sceneLoader.RestartScene();
        }
    }
}