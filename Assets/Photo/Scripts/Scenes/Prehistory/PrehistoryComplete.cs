using UnityEngine;
using Zenject;

namespace Photo
{
    public class PrehistoryComplete : MonoBehaviour
    {
        [SerializeField] private LoadingPage _loadingPage;
        
        private SaveLoader _saveLoader;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SaveLoader saveLoader, SceneLoader sceneLoader)
        {
            _saveLoader = saveLoader;
            _sceneLoader = sceneLoader;
        }

        public void Complete()
        {
            _saveLoader.Data.Progress.PrehistoryComplete();
            _saveLoader.Save();
            _loadingPage.Show();
            _sceneLoader.LoadSceneToID(2);
        }    
    }
}