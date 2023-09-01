using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Photo
{
    public class LampPuzzles : Interactable
    {
        [SerializeField] private Lamp[] _lamps;
        [SerializeField] private string _combinationString;
        
        private readonly List<int> _combination = new();
        private int _consistency = 0;

        public IReadOnlyList<Lamp> Lamps => _lamps;
        public IReadOnlyList<int> Combination => _combination;

        private void Start()
        {
            ParseCombination();

            foreach (var lamp in _lamps) 
                lamp.OnLampTouchEvent += LampTouch;
        }

        private void LampTouch(Lamp lamp)
        {
            if (QuantityCheck())
                _consistency = 0;

            _consistency = lamp.ID == _combination[_consistency] ? _consistency + 1 : _consistency = 0;
            
            if (QuantityCheck()) 
                OnInteraction();
        }

        private void ParseCombination()
        {
            foreach (char symbol in _combinationString)
            {
                if (int.TryParse(symbol.ToString(), out int result))
                    _combination.Add(result);
                else
                    Debug.LogWarning("You have entered the wrong order for the lamps.");
            }
        }

        private bool QuantityCheck() => 
            _combination.Count <= _consistency;
    }
}