using Photo;
using UnityEngine;

public class JumpState : StateMachineBehaviour
{
    [SerializeField] private LayerMask _ground;
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var isGround = Physics2D.OverlapCircle(animator.gameObject.transform.parent.position, 0.1f, _ground);
            
        if (isGround)
        {
            animator.SetBool("Jump", false);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
