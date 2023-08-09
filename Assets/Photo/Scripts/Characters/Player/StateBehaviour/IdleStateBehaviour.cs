using System;
using UnityEngine;

namespace Photo
{
    public class IdleStateBehaviour : IStateBehaviour
    {
        private const string IDLE = "Idle";

        public event Action OnAnimationCompletedEvent;
        public Animator Animator { get; }
        public StateType Type { get; }

        public IdleStateBehaviour(Animator animator, StateType type)
        {
            Animator = animator;
            Type = type;
        }
        
        public void Enter()
        {
            Animator.Play(IDLE);
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}