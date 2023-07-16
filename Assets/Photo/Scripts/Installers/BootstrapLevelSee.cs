using UnityEngine;
using Zenject;

namespace Photo
{
    public class BootstrapLevelSee : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private SpawnPoint _spawnPoint;
        [SerializeField] private PlayerCharacteristics _characteristics;
        
        public override void InstallBindings()
        {
            BindPlayerCharacteristics();
            BindSpawnPoint();
            BindPlayer();
        }

        private void BindSpawnPoint()
        {
            Container
                .Bind<SpawnPoint>()
                .FromInstance(_spawnPoint)
                .AsSingle();
        }

        private void BindPlayer()
        {
            Player player =
                Container.InstantiatePrefabForComponent<Player>(
                    _player, 
                    _spawnPoint.GetPosition(), 
                    Quaternion.identity,
                    null);

            Container
                .Bind<Player>()
                .FromInstance(player)
                .AsSingle();
        }

        private void BindPlayerCharacteristics()
        {
            Container
                .Bind<PlayerCharacteristics>()
                .FromInstance(_characteristics)
                .AsSingle();
        }
    }
}