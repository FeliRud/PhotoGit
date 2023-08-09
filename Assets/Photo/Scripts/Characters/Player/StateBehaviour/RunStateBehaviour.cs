using System;
using UnityEngine;

namespace Photo
{
    public class RunStateBehaviour : IStateBehaviour
    {
        private const string RUN = "Running";

        public event Action OnAnimationCompletedEvent;
        public Animator Animator { get; }
        public StateType Type { get; }

        public RunStateBehaviour(Animator animator, StateType type)
        {
            Animator = animator;
            Type = type;
        }
        
        public void Enter()
        {
            Animator.Play(RUN);
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}