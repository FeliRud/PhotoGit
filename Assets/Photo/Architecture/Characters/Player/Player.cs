using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Photo.Architecture.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput; 
     
        private Rigidbody2D _rigidbody2D;
        
        [Inject]
        private void Construct()
        {
            Init();
        }

        private void Init()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float horizointal = Input.GetAxis("Horizontal");
            
            _rigidbody2D.MovePosition(transform.position + new Vector3(horizointal, 0, 0));
        }
    }
}