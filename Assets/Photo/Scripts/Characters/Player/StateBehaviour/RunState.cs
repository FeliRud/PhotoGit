using System;
using UnityEngine;

namespace Photo
{
    public class RunState : BaseState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Math.Abs(Player.Velocity.y) > 0)
                animator.SetBool(FALL, true);
            
            if (Player.Velocity.x == 0)
                animator.SetBool(RUN, false);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Player.Velocity.x == 0)
            {
                animator.SetBool(RUN, false);
                return;
            }

            if (Math.Abs(Player.Velocity.y) > 0.01f)
                animator.SetBool(FALL, true);
            
            if (Player.BoxChecker.BoxInRange)
                animator.SetBool(PUSHING, true);
        }
    }
}