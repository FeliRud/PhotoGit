using System;
using UnityEngine;

namespace Photo
{
    public class Puzzle : MonoBehaviour
    {
        public event Action<Puzzle> OnTakePuzzleEvent;
        
        [SerializeField] private int _id;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                gameObject.SetActive(false);
                OnTakePuzzleEvent?.Invoke(this);
            }
        }
    }
}