using System;
using UnityEngine;

namespace Photo
{
    public class RunState : BaseState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Math.Abs(Player.Rigidbody2D.velocity.y) > 0)
                animator.SetBool(FALL, true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Player.Rigidbody2D.velocity.x == 0)
                animator.SetBool(RUN, false);
            
            if (Math.Abs(Player.Rigidbody2D.velocity.y) > 0)
                animator.SetBool(FALL, true);
        }
    }
}