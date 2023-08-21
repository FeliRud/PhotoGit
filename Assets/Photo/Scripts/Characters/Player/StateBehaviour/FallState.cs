using System;
using UnityEngine;

namespace Photo
{
    public class FallState : BaseState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(RUN, false);
            if (Player.GroundChecker.Check() && Math.Abs(Player.Velocity.y) < 0.1f)
                animator.SetBool(FALL, false);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Player.GroundChecker.Check() && Math.Abs(Player.Velocity.y) < 0.1f)
                animator.SetBool(FALL, false);
        }
    }
}