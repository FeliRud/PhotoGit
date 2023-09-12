using Photo.UI;
using UnityEngine;
using Zenject;

namespace Photo
{
    public class BootstrapLevel : MonoInstaller
    {
        [SerializeField] private Gears _gears;
        [SerializeField] private Player _player;
        [SerializeField] private SpawnPoint _spawnPoint;
        [SerializeField] private PlayerCharacteristics _characteristics;
        [SerializeField] private PhotoFrame _photoFrame;
        
        public override void InstallBindings()
        {
            BindGears();
            BindPhotoFrame();
            BindPlayerCharacteristics();
            BindSpawnPoint();
            BindPlayer();
        }

        private void BindGears() => 
            Container.Bind<Gears>().FromInstance(_gears).AsSingle();

        private void BindPhotoFrame() => 
            Container.Bind<PhotoFrame>().FromInstance(_photoFrame).AsSingle();

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