using UnityEngine;
using Zenject;

namespace Photo
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private AudioValueChanger _audioValueChanger;
        
        public override void InstallBindings()
        {
            SceneLoader sceneLoader = new SceneLoader();
            Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();

            AudioValueChanger audioValueChanger =
                Container.InstantiatePrefabForComponent<AudioValueChanger>(_audioValueChanger);

            Container.Bind<AudioValueChanger>().FromInstance(audioValueChanger).AsSingle();
        }
    }
}