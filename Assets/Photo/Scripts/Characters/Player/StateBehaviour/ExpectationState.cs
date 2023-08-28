using System;
using UnityEngine;

namespace Photo
{
    public class ExpectationState : BaseState
    {
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Math.Abs(Player.Velocity.x) > 0.01f || Math.Abs(Player.Velocity.y) > 0.01f)
                animator.SetInteger(EXPECTATION, 0);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetInteger(EXPECTATION, 0);
        }
    }
}