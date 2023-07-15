using Photo.Architecture.Characters.Player;
using UnityEngine;
using Zenject;

namespace Photo.Architecture.Installer
{
    public class BootstrapLevelSee : MonoInstaller
    {
        [SerializeField] private PlayerCharacteristics _characteristics;
        
        public override void InstallBindings()
        {
            BindPlayerCharacteristics();
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