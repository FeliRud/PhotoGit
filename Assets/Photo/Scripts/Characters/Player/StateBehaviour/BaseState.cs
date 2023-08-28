using UnityEngine;
using Zenject;

namespace Photo
{
    public abstract class BaseState : StateMachineBehaviour
    {
        protected const string RUN = "Run";
        protected const string JUMP = "Jump";
        protected const string FALL = "Fall";
        protected const string EXPECTATION = "Expectation";
        protected const string PUSHING = "Pushing";

        protected Player Player { get; private set; }

        [Inject]
        private void Construct(Player player)
        {
            Player = player;
            Player.OnJumpEvent += PlayerJump;
        }

        private void PlayerJump()
        {
            Player.Animator.SetBool(JUMP, true);
        }
    }
}