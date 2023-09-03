using UnityEngine;
using Zenject;

namespace Photo
{
    public class BootstrapLevelClouds : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private SpawnPoint _spawnPoint;
        [SerializeField] private PlayerCharacteristics _characteristics;
        [SerializeField] private PhotoFrame _photoFrame;
        
        public override void InstallBindings()
        {
            Container.Bind<PhotoFrame>().FromInstance(_photoFrame).AsSingle();
            BindPlayerCharacteristics();
            BindSpawnPoint();
            BindPlayer();
        }

        private void BindSpawnPoint() =>
            Container.Bind<SpawnPoint>().FromInstance(_spawnPoint).AsSingle();

        private void BindPlayer()
        {
            Player player =
                Container.InstantiatePrefabForComponent<Player>(
                    _player, 
                    _spawnPoint.GetPosition(), 
                    Quaternion.identity, 
                    null);

            Container.Bind<Player>().FromInstance(player).AsSingle();
        }

        private void BindPlayerCharacteristics() =>
            Container.Bind<PlayerCharacteristics>().FromInstance(_characteristics).AsSingle();
    }
}