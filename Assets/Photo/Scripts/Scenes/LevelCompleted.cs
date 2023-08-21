using UnityEngine;
using Zenject;

namespace Photo
{
    public class LevelCompleted : MonoBehaviour
    {
        [SerializeField] private PhotoFrame _photoFrame;

        private SaveLoader _saveLoader;
        
        [Inject]
        private void Construct(SaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
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
        }
    }
}