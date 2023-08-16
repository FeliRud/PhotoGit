using System;
using UnityEngine;

namespace Photo
{
    public class ExpectationState : BaseState
    {
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Math.Abs(Player.Velocity.x) > 0 || Math.Abs(Player.Velocity.y) > 0)
                animator.SetInteger(EXPECTATION, 0);
        }
    }
}