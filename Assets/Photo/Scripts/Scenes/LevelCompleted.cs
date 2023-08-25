using UnityEngine;
using Zenject;

namespace Photo
{
    public class LevelCompleted : MonoBehaviour
    {
        [SerializeField] private LoadingPage _loadingPage;
        [SerializeField] private int _nextLevelSceneID;
        [SerializeField] private int _levelID;

        private PhotoFrame _photoFrame;
        private SaveLoader _saveLoader;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SaveLoader saveLoader, SceneLoader sceneLoader, PhotoFrame photoFrame)
        {
            _saveLoader = saveLoader;
            _sceneLoader = sceneLoader;
            _photoFrame = photoFrame;
            
            _photoFrame.OnPlayerTakePhotoEvent += PlayerTakePhoto;
        }

        private void PlayerTakePhoto()
        {
            _saveLoader.Data.Progress.LevelCompleted(_levelID);
            _saveLoader.Save();
            _loadingPage.Show();
            _sceneLoader.LoadSceneToID(_nextLevelSceneID);
        }
    }
}