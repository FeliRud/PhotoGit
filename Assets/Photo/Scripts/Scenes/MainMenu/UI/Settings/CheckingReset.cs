using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photo
{
    public class CheckingReset : MonoBehaviour
    {
        public event Action OnRestartProgressButtonEvent;
        
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        
        public void Show()
        {
            _yesButton.onClick.AddListener(OnYesButtonClicked);
            _noButton.onClick.AddListener(Close);
            gameObject.SetActive(true);
        }

        private void Close()
        {
            _yesButton.onClick.RemoveListener(OnYesButtonClicked);
            _noButton.onClick.RemoveListener(Close);
            gameObject.SetActive(false);
        }

        private void OnYesButtonClicked()
        {
            OnRestartProgressButtonEvent?.Invoke();
            Close();
        }
    }
}