using System;
using UnityEngine;

namespace Photo
{
    public class LampCollision : MonoBehaviour
    {
        public event Action OnCollisionEnterEvent;
        public event Action OnCollisionExitEvent;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out Player player))
            {
                Debug.Log("Горю блять");
                OnCollisionEnterEvent?.Invoke();
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out Player player))
            {
                Debug.Log("Погасло нахуй");
                OnCollisionExitEvent?.Invoke();
            }
        }
    }
}