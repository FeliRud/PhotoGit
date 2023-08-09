using System;
using UnityEngine;

namespace Photo
{
    public class JumpStateBehaviour : IStateBehaviour
    {
        private const string JUMP = "Jump";

        public event Action OnAnimationCompletedEvent;
        public Animator Animator { get; }
        public StateType Type { get; }
        public LayerMask Ground { get; }

        public JumpStateBehaviour(Animator animator, StateType type, LayerMask ground)
        {
            Animator = animator;
            Type = type;
        }
        
        public void Enter()
        {
            Animator.Play(JUMP);
        }

        public void Update()
        {
            var isGround = Physics2D.OverlapCircle(Animator.gameObject.transform.parent.position, 0.1f, Ground);
            
            if (isGround)
                Exit();
        }

        public void Exit()
        {
            OnAnimationCompletedEvent?.Invoke();
        }
    }
}