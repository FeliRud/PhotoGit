using UnityEngine;
using Zenject;

namespace Photo
{
    public class Rules : MonoBehaviour
    {
        [SerializeField] private RulesMenu _rulesMenu;
        [SerializeField] private RulesType _rulesType;

        private SaveLoader _saveLoader;

        [Inject]
        private void Construct(SaveLoader saveLoader) => 
            _saveLoader = saveLoader;

        private void Start()
        {
            if (_saveLoader.Data.Setting.RulesIsCompleted.StartRules ||
                _rulesType != RulesType.Start)
                return;

            ShowRules();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out Player player))
                return;

            switch (_rulesType)
            {
                case RulesType.Start:
                    break;
                case RulesType.Photo:
                    if (!_saveLoader.Data.Setting.RulesIsCompleted.PhotoRules)
                        ShowRules();        
                    break;
                case RulesType.Interactable:
                    if (!_saveLoader.Data.Setting.RulesIsCompleted.InteractableRules)
                        ShowRules();        
                    break;
                case RulesType.Firefly:
                    if (!_saveLoader.Data.Setting.RulesIsCompleted.FireflyRules)
                        ShowRules();        
                    break;
            }
        }

        private void ShowRules()
        {
            _rulesMenu.Show();
            _rulesMenu.OnRulesCompletedEvent += RulesCompleted;
        }

        private void RulesCompleted()
        {
            _rulesMenu.OnRulesCompletedEvent -= RulesCompleted;
            switch (_rulesType)
            {
                case RulesType.Start:
                    _saveLoader.Data.Setting.RulesIsCompleted.StartRulesCompleted();
                    break;
                case RulesType.Photo:
                    _saveLoader.Data.Setting.RulesIsCompleted.PhotoRulesCompleted();
                    break;
                case RulesType.Interactable:
                    _saveLoader.Data.Setting.RulesIsCompleted.InteractableRulesCompleted();
                    break;
                case RulesType.Firefly:
                    _saveLoader.Data.Setting.RulesIsCompleted.FireflyRulesCompleted();
                    break;
            }
            _saveLoader.Save();
        }
    }

    public enum RulesType
    {
        Start,
        Photo,
        Interactable,
        Firefly
    }
}