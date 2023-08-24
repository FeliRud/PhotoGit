using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Photo
{
    public class SwitchTime : MonoBehaviour
    {
        [SerializeField] private GameObject _day;
        [SerializeField] private GameObject _night;
        
        private PlayerInput _playerInput;
        private Player _player;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }
        
        private void Start()
        {
            _player.PlayerInput.Player.Switch.performed += Switch;
            _day.SetActive(true);
            _night.SetActive(false);
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