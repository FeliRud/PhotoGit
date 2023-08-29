using UnityEngine;

namespace Photo
{
    public class FireflyIdleState : IState
    {
        private const float SPEED = 2f;
        
        private readonly Transform _firefly;
        private readonly Transform _followTarget;
        
        private Vector3 _targetPosition;
        
        public FireflyIdleState(Transform firefly, Transform followTarget)
        {
            _firefly = firefly;
            _followTarget = followTarget;
        }
        
        public void Enter()
        {
            RandomPosition();
        }

        public void Update()
        {
            if (_targetPosition == _firefly.position)
                RandomPosition();
            
            var direction = (_targetPosition - _firefly.position).normalized;
            if (direction.x != 0)
                _firefly.localScale = direction.x > 0 ? new Vector2(1, 1)  : new Vector2(-1, 1);
            _firefly.position = Vector3.MoveTowards(_firefly.position, _targetPosition, SPEED * Time.deltaTime);

        }

        public void Exit()
        {
            
        }

        private void RandomPosition()
        {
            var randomX = Random.Range(-1f, 1f);
            var randomY = Random.Range(-1f, 1f);

            _targetPosition = _followTarget.position + new Vector3(randomX, randomY, 0);
        }
    }
}