using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Photo
{
    public class IdleState : BaseState
    {
        private CancellationTokenSource _token;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _token = new CancellationTokenSource();
            Expectation(animator, _token.Token);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => 
            _token.Cancel();

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Player.BoxChecker.BoxInRange)
                animator.SetBool(PUSHING, true);

            if (Math.Abs(Player.Velocity.x) > 0.01f)
                animator.SetBool(RUN, true);
            
            if (Math.Abs(Player.Velocity.y) > 0.01f)
                animator.SetBool(FALL, true);
        }

        private async Task Expectation(Animator animator, CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(20f), token);
            animator.SetInteger(EXPECTATION, Random.Range(1, 3));
        }
    }
}