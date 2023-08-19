using System;
using UnityEngine;

namespace Photo
{
    public abstract class Interactable : MonoBehaviour
    {
        public event Action OnInteractionEvent;
        
        public void OnInteraction() => OnInteractionEvent?.Invoke();
    }
}