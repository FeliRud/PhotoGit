using System;
using UnityEngine;

namespace Photo
{
    public interface IStateBehaviour
    {
        public event Action OnAnimationCompletedEvent;
        
        public Animator Animator { get; }
        public StateType Type { get; }

        public void Enter();
        public void Update();
        public void Exit();
    }

    public enum StateType
    {
        Idle,
        Run,
        Jump
    }
}