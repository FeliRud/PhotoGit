using System;
using UnityEngine;
using Zenject;

namespace Photo
{
    public class PuzzleSystem : MonoBehaviour
    {
        [SerializeField] private Puzzle _puzzle;
        [SerializeField] private PuzzlePage _puzzlePage;

        private SaveLoader _saveLoader;

        [Inject]
        private void Construct(SaveLoader saveLoader) => 
            _saveLoader = saveLoader;

        private void Start()
        {
            if (_saveLoader.Data.Progress.PuzzleAvailable(_puzzle.ID))
                _puzzle.HidePuzzle();
            
            if (_puzzle.gameObject.activeSelf)
                _puzzle.OnTakePuzzleEvent += TakePuzzle;
        }

        private void TakePuzzle(Puzzle puzzle)
        {
            _puzzlePage.Show();
            _puzzlePage.InsertPuzzle(puzzle.ID);
            _saveLoader.Data.Progress.AddPuzzle(puzzle.ID);
            _saveLoader.Save();
        }
    }
}