using System;
using UnityEngine;

namespace Photo
{
    public class IdleState : BaseState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Math.Abs(Player.Velocity.x) > 0.1f)
                animator.SetBool(RUN, true);
            
            if (Math.Abs(Player.Velocity.y) > 0.1f)
                animator.SetBool(FALL, true);
        }
    }
}