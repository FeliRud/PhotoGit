using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photo
{
    public class DeathPanel : MonoBehaviour
    {
        public event Action OnRestartButtonClickedEvent;
        public event Action OnMenuButtonClickedEvent;
        
        [SerializeField] private Button _restratButton;
        [SerializeField] private Button _menuButton;

        private void Init()
        {
            _restratButton.onClick.AddListener(OnRestartButtonClicked);
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void Deinit()
        {
            _restratButton.onClick.RemoveListener(OnRestartButtonClicked);
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
        }

        public void Show()
        {
            Init();
            gameObject.SetActive(true);
        }

        public void Close()
        {
            Deinit();
            gameObject.SetActive(false);
        }
        
        private void OnRestartButtonClicked() => OnRestartButtonClickedEvent?.Invoke();
        private void OnMenuButtonClicked() => OnMenuButtonClickedEvent?.Invoke();
    }
}