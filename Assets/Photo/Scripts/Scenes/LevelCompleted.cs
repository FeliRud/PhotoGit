using UnityEngine;
using Zenject;

namespace Photo
{
    public class LevelCompleted : MonoBehaviour
    {
        [SerializeField] private PhotoFrame _photoFrame;
        [SerializeField] private int _nextLevelID;

        private SaveLoader _saveLoader;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SaveLoader saveLoader, SceneLoader sceneLoader)
        {
            _saveLoader = saveLoader;
            _sceneLoader = sceneLoader;
        }
        
        private void Start()
        {
            if (_photoFrame == null)
            {
                Debug.LogError("Не задана фотография для окончания уровня.");
                return;
            }
            
            _photoFrame.OnPlayerTakePhotoEvent += PlayerTakePhoto;
        }

        private void PlayerTakePhoto()
        {
            _saveLoader.Data.Progress.LevelCompleted();
            _saveLoader.Save();
            _sceneLoader.LoadSceneToInt(_nextLevelID);
        }
    }
}