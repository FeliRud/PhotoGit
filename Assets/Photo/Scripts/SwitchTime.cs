using UnityEngine;
using UnityEngine.InputSystem;

namespace Photo
{
    public class SwitchTime : MonoBehaviour
    {
        [SerializeField] private GameObject _day;
        [SerializeField] private GameObject _night;
        
        private PlayerInput _playerInput;

        private void Start()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _playerInput.Player.Switch.performed += Switch;
        }

        private void Switch(InputAction.CallbackContext obj)
        {
            if (_day.activeSelf)
            {
                _day.SetActive(false);
                _night.SetActive(true);
            }
            else
            {
                _night.SetActive(false);
                _day.SetActive(true);
            }
        }
    }
}