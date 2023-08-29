using UnityEngine;

namespace Photo
{
    public class PushingState : BaseState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!Player.BoxChecker.BoxInRange || Mathf.Abs(Player.Velocity.x) == 0)
            {
                animator.SetBool(PUSHING, false);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!Player.BoxChecker.BoxInRange || Mathf.Abs(Player.Velocity.x) == 0)
            {
                animator.SetBool(PUSHING, false);
            }
        }
    }
}