using System;
using UnityEngine;

namespace Photo
{
    public class PlayerInteraction : MonoBehaviour
    {
        public event Action OnInteractableInRangeEvent; 
        public event Action OnInteractableOutRangeEvent;

        private IInteractable _interactable;
        
        public void Interaction()
        {
            if (_interactable == null)
                return;

            _interactable.Interactable();
        }
        
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out IInteractable interactable);
            _interactable = interactable;
            OnInteractableInRangeEvent?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _interactable = null;
            OnInteractableOutRangeEvent?.Invoke();
        }
    }
}