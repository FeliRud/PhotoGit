using System;
using System.Collections.Generic;
using UnityEngine;

namespace Photo
{
    public class PlayerInteraction : MonoBehaviour
    {
        public event Action OnInteractableInRangeEvent; 
        public event Action OnInteractableOutRangeEvent;

        private readonly List<IInteractable> _interactable = new();
        
        public void Interaction()
        {
            foreach (var interactable in _interactable) 
                interactable.Interactable();
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out IInteractable interactable)) 
                return;
            _interactable.Add(interactable);
            OnInteractableInRangeEvent?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (!col.TryGetComponent(out IInteractable interactable)) 
                return;
            _interactable.Remove(interactable);
            OnInteractableOutRangeEvent?.Invoke();
        }
    }
}