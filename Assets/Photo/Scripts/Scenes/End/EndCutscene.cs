using UnityEngine;
using Zenject;

namespace Photo.End
{
    public class EndCutscene : MonoBehaviour
    {
        [SerializeField] private EndWindow _endWindow;
        [SerializeField] private LoadingPage _loadingPage;
        
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        public void ShowSelectOption() => 
            _endWindow.Show();

        public void LoadMenu()
        {
            _loadingPage.Show();
            _sceneLoader.LoadSceneToID(0);
        }
    }
}