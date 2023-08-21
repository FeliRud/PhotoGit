using UnityEngine;
using Zenject;

namespace Photo
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private AudioValueChanger _audioValueChanger;
        [SerializeField] private SaveLoader _saveLoader;
        
        public override void InstallBindings()
        {
            SaveLoader saveLoader = Container.InstantiatePrefabForComponent<SaveLoader>(_saveLoader);
            Container.Bind<SaveLoader>().FromInstance(saveLoader).AsSingle();
            
            SceneLoader sceneLoader = new SceneLoader();
            Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();

            AudioValueChanger audioValueChanger =
                Container.InstantiatePrefabForComponent<AudioValueChanger>(_audioValueChanger);

            Container.Bind<AudioValueChanger>().FromInstance(audioValueChanger).AsSingle();
        }
    }
}