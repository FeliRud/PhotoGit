using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Photo
{
    public class PhotoAlbum : MonoBehaviour
    {
        public event Action<int> OnAgainButtonClickedEvent; 
        public event Action OnCloseButtonClickedEvent;

        [SerializeField] private List<Page> _pages;
        [SerializeField] private Button _prev;
        [SerializeField] private Button _next;
        [SerializeField] private Button _close;

        private Page _currentPage;
        private SaveLoader _saveLoader;

        [Inject]
        private void Construct(SaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
        }
        
        public void Show()
        {
            foreach (var page in _pages)
            {
                page.OnAgainButtonClickedEvent += AgainButtonClicked;
                page.Close();
            }

            _currentPage = _saveLoader.Data.Progress.GetLevel() >= 1 ? _pages[1] : _pages[0];
            _currentPage.Show();
            _prev.interactable = false;
            
            _prev.onClick.AddListener(PrevButtonClicked);    
            _next.onClick.AddListener(NextButtonClicked);
            _close.onClick.AddListener(CloseButtonClicked);
            
            gameObject.SetActive(true);
        }

        public void Close()
        {
            foreach (var page in _pages)
            {
                page.OnAgainButtonClickedEvent -= AgainButtonClicked;
            }
            
            _prev.onClick.RemoveListener(PrevButtonClicked);    
            _next.onClick.RemoveListener(NextButtonClicked);
            _close.onClick.RemoveListener(CloseButtonClicked);
            
            gameObject.SetActive(false);
        }

        private void PrevButtonClicked()
        {
            if (_currentPage.LevelID <= 0 || 
                _saveLoader.Data.Progress.GetLevel() >= 1 && _currentPage.LevelID <= 1)
                return;
            
            _currentPage.Close();
            _currentPage = _pages[_currentPage.LevelID - 1];
            _currentPage.Show();

            _next.interactable = true;
            if (_currentPage.LevelID == 0)
                _prev.interactable = false;
        }

        private void NextButtonClicked()
        {
            if (_currentPage.LevelID >= _pages.Count - 1 || 
                _currentPage.LevelID >= _saveLoader.Data.Progress.GetLevel()) 
                return;
            
            _currentPage.Close();
            _currentPage = _pages[_currentPage.LevelID + 1];
            _currentPage.Show();
            
            _prev.interactable = true;
            if (_currentPage.LevelID == _pages.Count - 1 || 
                _currentPage.LevelID == _saveLoader.Data.Progress.GetLevel())
                _next.interactable = false;
        }

        private void AgainButtonClicked(int sceneID) => OnAgainButtonClickedEvent?.Invoke(sceneID);
        
        private void CloseButtonClicked() => OnCloseButtonClickedEvent?.Invoke();
    }
}