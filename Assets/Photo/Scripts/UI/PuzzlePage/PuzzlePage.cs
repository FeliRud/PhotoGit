﻿using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Photo
{
    public class PuzzlePage : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private PuzzleUI[] _puzzles;

        private Player _player;
        private SaveLoader _saveLoader;

        [Inject]
        private void Construct(SaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
        }

        private void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        public void Show()
        {
            if (_player != null)
                _player.DisablePlayerInput();
            foreach (var puzzle in _puzzles)
            {
                if (_saveLoader.Data.Progress.PuzzleAvailable(puzzle.PuzzleInfo.ID))
                    puzzle.CloseFast();
            }
            
            gameObject.SetActive(true);
        }

        private void Close()
        {
            if (_player != null)
                _player.EnablePlayerInput();
            gameObject.SetActive(false);
            foreach (var puzzle in _puzzles)
            {
                puzzle.Show();
            }
        }

        public void InsertPuzzle(int puzzleID)
        {
            var puzzle = _puzzles.First(x => x.PuzzleInfo.ID == puzzleID);
            puzzle.Close();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Close();
        }
    }
}