using UnityEngine;
using Zenject;

namespace Photo.Scripts.Scenes.Prehistory
{
    public class BootstrapPrehistory : MonoInstaller
    {
        [SerializeField] private PrehistoryComplete _prehistoryComplete;
        
        public override void InstallBindings() => 
            BindPrehistoryComplete();

        private void BindPrehistoryComplete() => 
            Container.Bind<PrehistoryComplete>().FromInstance(_prehistoryComplete).AsSingle();
    }
}