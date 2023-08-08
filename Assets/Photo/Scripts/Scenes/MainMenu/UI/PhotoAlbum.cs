using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photo
{
    public class PhotoAlbum : MonoBehaviour
    {
        public event Action OnPrevButtonClickedEvent;
        public event Action OnNextButtonClickedEvent;
        public event Action OnCloseButtonClickedEvent;
        
        [SerializeField] private Button _prev;
        [SerializeField] private Button _next;
        [SerializeField] private Button _close;

        public void Show()
        {
            _prev.onClick.AddListener(PrevButtonClicked);    
            _next.onClick.AddListener(NextButtonClicked);
            _close.onClick.AddListener(CloseButtonClicked);
            
            gameObject.SetActive(true);
        }

        public void Close()
        {
            _prev.onClick.RemoveListener(PrevButtonClicked);    
            _next.onClick.RemoveListener(NextButtonClicked);
            _close.onClick.RemoveListener(CloseButtonClicked);
            
            gameObject.SetActive(false);
        }

        private void PrevButtonClicked() => OnPrevButtonClickedEvent?.Invoke();
        private void NextButtonClicked() => OnNextButtonClickedEvent?.Invoke();
        private void CloseButtonClicked() => OnCloseButtonClickedEvent?.Invoke();
    }
}