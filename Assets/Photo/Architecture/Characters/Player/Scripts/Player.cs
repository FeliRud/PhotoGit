using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Photo.Architecture.Characters.Sprites.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private const int LAYER_PLAYER = 7;
        private const int LAYER_PLATFORM = 8;
        
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private LayerMask _groundLayer;
        
        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody2D;
        private PlayerCharacteristics _characteristics;
        private bool _isGround;
        
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
            _playerInput.Player.Jump.performed += OnJump;
            _playerInput.Player.Fall.performed += OnFall;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float value = _playerInput.Player.Move.ReadValue<float>();
            Vector2 velocity = new Vector2(value * _characteristics.Speed,  _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = velocity;
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            _isGround = Physics2D.OverlapCircle(_groundChecker.position, 0.1f, _groundLayer);
            
            if (_isGround)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                _rigidbody2D.AddForce(Vector2.up * _characteristics.JumpForce, ForceMode2D.Impulse);
            }
        }
        
        private void OnFall(InputAction.CallbackContext obj)
        {
            _isGround = Physics2D.OverlapCircle(_groundChecker.position, 0.1f, _groundLayer);

            if (_isGround)
                StartCoroutine(FallRoutine());
        }

        private IEnumerator FallRoutine()
        {
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_PLATFORM, true);
            yield return new WaitForSeconds(0.25f);
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_PLATFORM, false);

        }
        
        private void OnDrawGizmos()
        {
            if (_groundChecker != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_groundChecker.position, 0.1f);
            }
        }
        
        
    }
}