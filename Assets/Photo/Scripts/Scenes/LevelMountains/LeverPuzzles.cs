using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Photo.Scripts.Scenes.LevelMountains
{
    public class LeverPuzzles : Interactable
    {
        [SerializeField] private Lever[] _levers;
        [SerializeField] private string _combinationString;

        private readonly List<int> _combination = new();
        private int _consistency;

        public IReadOnlyList<Lever> Levers => _levers;
        public IReadOnlyList<int> Combination => _combination;

        private void Start()
        {
            ParseCombination();

            foreach (var lever in _levers)
                lever.OnLeverInteractionEvent += LeverInteraction;
        }

        private void LeverInteraction()
        {
            if (QuantityCheck())
                _consistency = 0;

            var levers = _levers.Where(x => x.Enabled).ToList();
            foreach (var id in _combination)
            {
                var lever = levers.FirstOrDefault(x => x.ID == id);
                if (lever == null)
                {
                    _consistency = 0;
                    break;
                }

                _consistency++;
                levers.Remove(lever);
            }
            
            if (levers.Count != 0)
                _consistency = 0;
            
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