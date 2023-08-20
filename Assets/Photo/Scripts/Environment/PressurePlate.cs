using UnityEngine;

namespace Photo
{
    public class PressurePlate : Interactable
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Transform _plate;

        private bool _isPressed;

        private void OnTriggerStay2D(Collider2D other)
        {
            Press();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isPressed = false;
            _plate.position = new Vector3(_plate.position.x, _plate.position.y + 0.15f, _plate.position.z);
            OnInteraction();
        }
        
        private void Press()
        {
            if (_isPressed)
                return;

            _isPressed = true;
            _plate.position = new Vector3(_plate.position.x, _plate.position.y - 0.15f, _plate.position.z);
            OnInteraction();
        }
    }
}