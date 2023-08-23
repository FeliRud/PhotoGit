using System;
using UnityEngine;

namespace Photo
{
    public class LampCollision : MonoBehaviour
    {
        public event Action OnCollisionEnterEvent;
        public event Action OnCollisionExitEvent;

        [SerializeField] private SpriteRenderer _light;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out Player player))
            {
                _light.gameObject.SetActive(true);
                OnCollisionEnterEvent?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out Player player))
            {
                _light.gameObject.SetActive(false);
                OnCollisionExitEvent?.Invoke();
            }
        }
    }
}