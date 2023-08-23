using UnityEngine;
using Zenject;

namespace Photo
{
    public class LevelCompleted : MonoBehaviour
    {
        [SerializeField] private PhotoFrame _photoFrame;
        [SerializeField] private int _nextLevelSceneID;
        [SerializeField] private int _levelID;

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
            _saveLoader.Data.Progress.LevelCompleted(_levelID);
            _saveLoader.Save();
            _sceneLoader.LoadSceneToID(_nextLevelSceneID);
        }
    }
}