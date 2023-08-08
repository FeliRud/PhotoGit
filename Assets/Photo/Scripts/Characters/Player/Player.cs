using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Photo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public event Action OnDie;
        
        private const int LAYER_PLAYER = 7;
        private const int LAYER_BASE_PLATFORM = 8;
        private const int LAYER_MOVE_PLATFORM = 9;

        private const string ANIMATION_RUN = "Move";
        private const string ANIMATION_JUMP = "Jump";

        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerInteraction _interaction;
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private LayerMask _groundLayer;
        
        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody2D;
        private PlayerCharacteristics _characteristics;
        private Lever _lever;
        
        [Inject]
        private void Construct(PlayerCharacteristics characteristics)
        {
            _characteristics = characteristics;
            Init();
        }

        private void OnDisable()
        {
            _interaction.OnLeverChanged -= OnLeverChanged;
            _playerInput.Player.Jump.performed -= OnJump;
            _playerInput.Player.Fall.performed -= OnFall;
            _playerInput.Player.Use.performed -= OnUse;
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        public void Die()
        {
            OnDie?.Invoke();
        }
        
        private void Init()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _interaction.OnLeverChanged += OnLeverChanged;
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _playerInput.Player.Jump.performed += OnJump;
            _playerInput.Player.Fall.performed += OnFall;
            _playerInput.Player.Use.performed += OnUse;
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
            
            if (!_animator.GetBool(ANIMATION_JUMP) && !_animator.GetBool(ANIMATION_RUN) && velocity.x != 0)
            {
                _animator.SetBool(ANIMATION_RUN, true);
            }
            else if (velocity.x == 0)
            {
                _animator.SetBool(ANIMATION_RUN, false);
            }
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            var isGround = Physics2D.OverlapCircle(_groundChecker.position, 0.1f, _groundLayer);
            
            if (isGround)
            {
                _animator.SetBool(ANIMATION_JUMP, true);
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
                _rigidbody2D.AddForce(Vector2.up * _characteristics.JumpForce, ForceMode2D.Impulse);
            }
        }
        
        private void OnFall(InputAction.CallbackContext obj)
        {
            var isGround = Physics2D.OverlapCircle(_groundChecker.position, 0.1f, _groundLayer);

            if (isGround)
                StartCoroutine(FallRoutine());
        }
        
        private void OnLeverChanged(Lever lever)
        {
            _lever = lever;
        }
        
        private void OnUse(InputAction.CallbackContext obj)
        {
            if (_lever != null)
                _lever.Use();
        }

        private IEnumerator FallRoutine()
        {
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_BASE_PLATFORM, true);
            yield return new WaitForSeconds(0.25f);
            Physics2D.IgnoreLayerCollision(LAYER_PLAYER, LAYER_BASE_PLATFORM, false);
        }
    }
}