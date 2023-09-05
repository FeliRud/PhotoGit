using Photo.Scripts.StateMachine;
using UnityEngine;

namespace Photo.Scripts.Characters.Firefly.States
{
    public class FireflyFollowState : IState
    {
        private readonly float _speed;
        private readonly Transform _firefly;
        private readonly Transform _followTarget;
        
        public FireflyFollowState(float speed, Transform firefly, Transform followTarget)
        {
            _speed = speed;
            _firefly = firefly;
            _followTarget = followTarget;
        }
        
        public void Enter()
        {
            
        }

        public void Update()
        {
            var direction = (_followTarget.position - _firefly.position).normalized;
            if (direction.x != 0)
                _firefly.localScale = direction.x > 0 ? new Vector2(1, 1)  : new Vector2(-1, 1);
            _firefly.position = Vector3.MoveTowards(_firefly.position, _followTarget.position, _speed * Time.deltaTime);
        }

        public void Exit()
        {
            
        }
    }
}