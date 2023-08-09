using UnityEngine;
using Zenject;

namespace Photo
{
    public abstract class BaseState : StateMachineBehaviour
    {
        public const string RUN = "Run";
        public const string JUMP = "Jump";
        public const string FALL = "Fall";
        
        public Player Player { get; private set; }

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