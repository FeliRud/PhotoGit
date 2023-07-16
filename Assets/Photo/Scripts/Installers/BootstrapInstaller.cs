using Zenject;

namespace Photo
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoader();
        }

        private void BindSceneLoader()
        {
            SceneLoader sceneLoader = new SceneLoader();

            Container
                .Bind<SceneLoader>()
                .FromInstance(sceneLoader)
                .AsSingle();
        }
    }
}