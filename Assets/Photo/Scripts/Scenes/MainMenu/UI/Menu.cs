using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photo
{
    public class Menu : MonoBehaviour
    {
        public event Action OnPlayButtonClickedEvent;
        public event Action OnPhotoButtonClickedEvent;
        public event Action OnSettingsButtonClickedEvent;
        public event Action OnExitButtonClickedEvent;
        
        [SerializeField] private Button _play;
        [SerializeField] private Button _photo;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _exit;

        public void Show()
        {
            _play.onClick.AddListener(PlayButtonClicked);
            _photo.onClick.AddListener(PhotoButtonClicked);
            _settings.onClick.AddListener(SettingsButtonClicked);
            _exit.onClick.AddListener(ExitButtonClicked);
            
            gameObject.SetActive(true);
        }

        public void Close()
        {
            _play.onClick.RemoveListener(PlayButtonClicked);
            _photo.onClick.RemoveListener(PhotoButtonClicked);
            _settings.onClick.RemoveListener(SettingsButtonClicked);
            _exit.onClick.RemoveListener(ExitButtonClicked);
            
            gameObject.SetActive(false);
        }

        private void PlayButtonClicked() => OnPlayButtonClickedEvent?.Invoke();
        private void PhotoButtonClicked() => OnPhotoButtonClickedEvent?.Invoke();
        private void SettingsButtonClicked() => OnSettingsButtonClickedEvent?.Invoke();
        private void ExitButtonClicked() => OnExitButtonClickedEvent?.Invoke();
    }
}