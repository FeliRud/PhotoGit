using System;
using UnityEngine;

namespace Photo
{
    public class Puzzle : MonoBehaviour
    {
        public event Action<Puzzle> OnTakePuzzleEvent;

        [SerializeField] private PuzzleInfo _puzzleInfo;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public int ID => _puzzleInfo.ID;
        
        private void Awake()
        {
            _spriteRenderer.sprite = _puzzleInfo.Sprite;
        }

        public void HidePuzzle()
        {
            gameObject.SetActive(false);
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                HidePuzzle();
                OnTakePuzzleEvent?.Invoke(this);
            }
        }
    }
}