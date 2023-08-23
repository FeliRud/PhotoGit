using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photo
{
    public class Page : MonoBehaviour
    {
        public event Action<int> OnAgainButtonClickedEvent;
        
        [SerializeField] private Button _againButton;
        [SerializeField] private int _levelID;
        [SerializeField] private int _sceneID;

        public int LevelID => _levelID;

        public void Show()
        {
            _againButton.onClick.AddListener(AgainButtonClicked);
            gameObject.SetActive(true);
        }

        public void Close()
        {
            _againButton.onClick.RemoveListener(AgainButtonClicked);
            gameObject.SetActive(false);
        }

        private void AgainButtonClicked() => OnAgainButtonClickedEvent?.Invoke(_sceneID);
    }
}