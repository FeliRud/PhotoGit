using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Photo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public event Action OnJumpEvent;
        public event Action OnRunEvent;
        public event Action OnIdleEvent;
        public event Action OnDie;
        
        private const int LAYER_PLAYER = 7;
        private const int LAYER_BASE_PLATFORM = 8;
        private const int LAYER_MOVE_PLATFORM = 9;

        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerInteraction _playerInteraction;
        [SerializeField] private GroundChecker _groundChecker;
        
        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody2D;
        private PlayerCharacteristics _characteristics;
        private bool _isRun;

        public PlayerInput PlayerInput => _playerInput;
        public Vector2 Velocity => _rigidbody2D.velocity;
        public Animator Animator => _animator;
        public GroundChecker GroundChecker => _groundChecker;
        
        [Inject]
        private void Construct(PlayerCharacteristics characteristics)
        {
            _characteristics = characteristics;
            Init();
        }

        private void Init()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerInteraction.OnInteractableInRangeEvent += InteractableInRange;
            _playerInteraction.OnInteractableOutRangeEvent += InteractableOutRange;
            _playerInput = new PlayerInput();
            EnablePlayerInput();
            _playerInput.Player.Jump.performed += OnJump;
            _playerInput.Player.Fall.performed += OnFall;
            _playerInput.Player.Use.performed += OnUse;
        }

        private void OnDisable()
        {
            _playerInteraction.OnInteractableInRangeEvent -= InteractableInRange;
            _playerInteraction.OnInteractableOutRangeEvent -= InteractableOutRange;
            _playerInput.Player.Jump.performed -= OnJump;
            _playerInput.Player.Fall.performed -= OnFall;
            _playerInput.Player.Use.performed -= OnUse;
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void EnablePlayerInput()
        {
            _playerInput.Enable();
        }
        
        public void DisablePlayerInput()
        {
            _playerInput.Disable();
        }
        
        public void Die()
        {
            DisablePlayerInput();
            OnDie?.Invoke();
        }

        private void Move()
        {
            float value = _playerInput.Player.Move.ReadValue<float>();
            Vector2 velocity = new Vector2(value * _characteristics.Speed,  _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = velocity;

            if (value != 0)
            {
                transform.localScale = value >= 0 ? new Vector3(1, transform.localScale.y, transform.localScale.y) :
                    new Vector3(-1, transform.localScale.y, transform.localScale.y);
            }
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            if (_groundChecker.Check() && Math.Abs(Velocity.y) < 0.1f)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                _rigidbody2D.AddForce(Vector2.up * _characteristics.JumpForce, ForceMode2D.Impulse);
                OnJumpEvent?.Invoke();
            }
        }

        private void OnFall(InputAction.CallbackContext obj)
        {
            if (_groundChecker.Check() && Math.Abs(Velocity.y) < 0.1f)
                StartCoroutine(FallRoutine());
        }

        private void InteractableInRange(){}

        private void InteractableOutRange(){}

        private void OnUse(InputAction.CallbackContext obj)
        {
            _playerInteraction.Interaction();
        }

        private IEnumerator FallRoutine()
        {
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_BASE_PLATFORM, true);
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_MOVE_PLATFORM, true);
            yield return new WaitForSeconds(0.3f);
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_BASE_PLATFORM, false);
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_MOVE_PLATFORM, false);
        }
    }
}