using System;
using Photo;
using UnityEngine;

namespace Photo
{
    public class PlayerInteraction : MonoBehaviour
    {
        public event Action<Lever> OnLeverChanged; 

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out Lever lever);
            OnLeverChanged?.Invoke(lever);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnLeverChanged?.Invoke(null);
        }
    }
}