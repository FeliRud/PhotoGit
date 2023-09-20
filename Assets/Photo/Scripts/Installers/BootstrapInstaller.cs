using Photo.StaticData;
using UnityEngine;
using Zenject;

namespace Photo
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private AudioChanger _audioChanger;
        [SerializeField] private SaveLoader _saveLoader;
        [SerializeField] private SceneLoader _sceneLoader;
        
        public override void InstallBindings()
        {
            BindStaticDataService();
            BindSaveLoader();
            BindAudio();
            BindSceneLoader();
        }

        private void BindStaticDataService()
        {
            StaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadLevel();
            Container.BindInterfacesTo<StaticDataService>().FromInstance(staticDataService).AsSingle();
        }

        private void BindAudio()
        {
            AudioChanger audioValueChanger =
                Container.InstantiatePrefabForComponent<AudioChanger>(_audioChanger);
            Container.Bind<AudioChanger>().FromInstance(audioValueChanger).AsSingle();
            DontDestroyOnLoad(audioValueChanger);
        }

        private void BindSceneLoader()
        {
            SceneLoader sceneLoader = Container.InstantiatePrefabForComponent<SceneLoader>(_sceneLoader);
            Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
            DontDestroyOnLoad(sceneLoader);
        }

        private void BindSaveLoader()
        {
            SaveLoader saveLoader = Container.InstantiatePrefabForComponent<SaveLoader>(_saveLoader);
            Container.Bind<SaveLoader>().FromInstance(saveLoader).AsSingle();
            DontDestroyOnLoad(saveLoader);
        }
    }
}