using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Photo
{
    public class RulesMenu : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnRulesCompletedEvent;
        
        [SerializeField] private List<RulesPanel> _rulesPanels;
        [SerializeField] private TextMeshProUGUI _textContine;

        private RulesPanel _currentPanel;
        private Player _player;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }
        
        private void Start()
        {
            Show();
        }

        public void Show()
        {
            _player.DisablePlayerInput();
            _textContine.DOColor(Color.white, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            _currentPanel = _rulesPanels[0];
            _currentPanel.Show();
            gameObject.SetActive(true);
        }

        private void Close()
        {
            OnRulesCompletedEvent?.Invoke();
            _player.EnablePlayerInput();
            gameObject.SetActive(false);
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