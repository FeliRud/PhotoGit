using System;
using Photo.UI;
using UnityEngine;
using Zenject;

namespace Photo
{
    public abstract class Interactable : MonoBehaviour
    {
        public event Action OnInteractionEvent;
        
        private Gears _gears;

        [Inject]
        private void Construct(Gears gears) => 
            _gears = gears;

        protected void OnInteraction()
        {
            _gears.Show();
            OnInteractionEvent?.Invoke();
        }
    }
}