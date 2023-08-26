using UnityEngine;
using Zenject;

namespace Photo
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private AudioValueChanger _audioValueChanger;
        [SerializeField] private SaveLoader _saveLoader;
        [SerializeField] private SceneLoader _sceneLoader;
        
        public override void InstallBindings()
        {
            SaveLoader saveLoader = Container.InstantiatePrefabForComponent<SaveLoader>(_saveLoader);
            Container.Bind<SaveLoader>().FromInstance(saveLoader).AsSingle();
            
            SceneLoader sceneLoader = Container.InstantiatePrefabForComponent<SceneLoader>(_sceneLoader);
            Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
            DontDestroyOnLoad(sceneLoader);

            AudioValueChanger audioValueChanger =
                Container.InstantiatePrefabForComponent<AudioValueChanger>(_audioValueChanger);
            Container.Bind<AudioValueChanger>().FromInstance(audioValueChanger).AsSingle();
            DontDestroyOnLoad(audioValueChanger);

        }
    }
}