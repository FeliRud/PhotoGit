using System;
using UnityEngine;

namespace Photo
{
    public class PhotoFrame : MonoBehaviour
    {
        public event Action OnPlayerTakePhotoEvent;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                OnPlayerTakePhotoEvent?.Invoke();
            }
        }
    }
}