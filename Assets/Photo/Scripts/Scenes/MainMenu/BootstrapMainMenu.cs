using UnityEngine;
using Zenject;

namespace Photo
{
    public class BootstrapMainMenu : MonoInstaller
    {
        [SerializeField] private Menu _menu;
        [SerializeField] private Settings _settings;
        
        public override void InstallBindings()
        {
            Container.Bind<Menu>().FromInstance(_menu).AsSingle();
            Container.Bind<Settings>().FromInstance(_settings).AsSingle();
        }
    }
}