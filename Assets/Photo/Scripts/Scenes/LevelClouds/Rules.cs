using UnityEngine;
using Zenject;

namespace Photo
{
    public class Rules : MonoBehaviour
    {
        [SerializeField] public RulesMenu _rulesMenu;

        private SaveLoader _saveLoader;

        [Inject]
        private void Construct(SaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
        }
        
        private void Start()
        {
            if (_saveLoader.Data.Setting.RulesIsCompleted)
                return;
            
            _rulesMenu.Show();
            _rulesMenu.OnRulesCompletedEvent += RulesCompleted;
        }

        private void RulesCompleted()
        {
            _rulesMenu.OnRulesCompletedEvent -= RulesCompleted;
            _saveLoader.Data.Setting.RulesCompleted();
        }
    }
}