using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Photo
{
    public class RulesMenu : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnRulesCompletedEvent;
        
        [SerializeField] private List<RulesPanel> _rulesPanels;
        [SerializeField] private TextMeshProUGUI _textContine;

        private RulesPanel _currentPanel;
        private Player _player;

        private void Start()
        {
            _player = FindObjectOfType<Player>();
            Show();
        }

        public void Show()
        {
            foreach (var rulesPanel in _rulesPanels)
            {
                rulesPanel.CloseFast();
            }
            
            if (_player != null)
                _player.DisablePlayerInput();
            _textContine.DOColor(Color.white, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            _currentPanel = _rulesPanels[0];
            _currentPanel.Show();
            gameObject.SetActive(true);
        }

        private void Close()
        {
            if (_player != null)
                _player.EnablePlayerInput();
            gameObject.SetActive(false);
            OnRulesCompletedEvent?.Invoke();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_currentPanel == _rulesPanels[^1])
            {
                Close();
                return;
            }
            
            _currentPanel.Close();
            _currentPanel = _rulesPanels[_currentPanel.ID + 1];
            _currentPanel.Show();
        }
    }
}