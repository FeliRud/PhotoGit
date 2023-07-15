using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Photo.Architecture.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody2D;
        private PlayerCharacteristics _characteristics;
        
        [Inject]
        private void Construct(PlayerCharacteristics characteristics)
        {
            _characteristics = characteristics;
            Init();
        }

        private void Init()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float value = _playerInput.Player.Move.ReadValue<float>();
            Vector3 direction = new Vector3(value, 0, 0);
            _rigidbody2D.MovePosition(transform.position + direction * _characteristics.Speed * Time.deltaTime);
        }
    }
}