using System;
using UnityEngine;

namespace Photo
{
    public class Lever : Interactable, IInteractable
    {
        public event Action OnLeverInteractionEvent;
        
        [SerializeField] private int _id;
        [SerializeField] private Transform _pivot;

        public bool Enabled { get; private set; } = false;
        public int ID => _id;

        public void Interactable()
        {
            _pivot.localEulerAngles = new Vector3(0,0,-_pivot.localEulerAngles.z);
            Enabled = !Enabled;
            OnLeverInteractionEvent?.Invoke();
            OnInteraction();
        }
    }
}