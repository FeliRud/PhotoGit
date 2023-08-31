using System;
using UnityEngine;

namespace Photo
{
    public abstract class Interactable : MonoBehaviour
    {
        public event Action OnInteractionEvent;

        protected void OnInteraction() => 
            OnInteractionEvent?.Invoke();
    }
}