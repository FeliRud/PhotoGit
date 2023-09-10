using UnityEngine;
using Zenject;

namespace Photo.End
{
    public class EndCutscene : MonoBehaviour
    {
        [SerializeField] private GameObject[] _firstObjects;
        [SerializeField] private EndWindow _endWindow;
        [SerializeField] private LoadingPage _loadingPage;
        
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        public void ShowSelectOption()
        {
            foreach (var firstObject in _firstObjects) 
                firstObject.SetActive(false);
            _endWindow.Show();
        }

        public void LoadMenu()
        {
            _loadingPage.Show();
            _sceneLoader.LoadSceneToID(0);
        }
    }
}