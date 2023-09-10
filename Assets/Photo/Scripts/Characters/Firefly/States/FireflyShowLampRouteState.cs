using System.Linq;
using Photo.Scripts.StateMachine;
using UnityEngine;

namespace Photo.Scripts.Characters.Firefly.States
{
    public class FireflyShowLampRouteState : IState
    {
        private const float DELAY = 2f;

        private readonly Firefly _firefly;
        private readonly LampPuzzles _lampsPuzzle;
        
        private int _currentValue;
        private float _leftTime;
        private Vector3 _nextPosition;
        private Vector3 _randomPosition;

        public FireflyShowLampRouteState(Firefly firefly, LampPuzzles lampsPuzzle)
        {
            _firefly = firefly;
            _lampsPuzzle = lampsPuzzle;
        }

        public void Enter()
        {
            _currentValue = 0;
            _leftTime = 0;
        }

        public void Update()
        {
            if (_leftTime > 0)
            {
                MoveToRandomPosition();
                return;
            }

            GetNextPosition();
            MoveToPosition();
        }

        public void Exit() => 
            _firefly.ItHelps = false;

        private void MoveToRandomPosition()
        {
            _leftTime -= Time.deltaTime;
            
            if (_randomPosition == _firefly.transform.position)
                RandomPosition();

            SetDirection(_randomPosition);
            _firefly.transform.position = Vector3.MoveTowards(_firefly.transform.position, _randomPosition, 3 * Time.deltaTime);
        }

        private void GetNextPosition()
        {
            if (_currentValue >= _lampsPuzzle.Combination.Count)
            {
                _firefly.SetState(_firefly.GetState<FireflyFollowState>());
                return;
            }

            var nextLampID = _lampsPuzzle.Combination[_currentValue];
            Vector3 nextLampPosition = _lampsPuzzle.Lamps.First(lamp => lamp.ID == nextLampID).transform.position;
            _nextPosition = new Vector3(nextLampPosition.x, nextLampPosition.y, 0);
        }

        private void MoveToPosition()
        {
            Vector3 fireflyPosition = _firefly.transform.position;
            float speed = Time.deltaTime * 7f;

            if (fireflyPosition == _nextPosition)
            {
                _leftTime = DELAY;
                _currentValue++;
                RandomPosition();
                return;
            }

            SetDirection(_nextPosition);
            _firefly.transform.position = Vector3.MoveTowards(fireflyPosition, _nextPosition, speed);
        }

        private void RandomPosition()
        {
            var randomX = Random.Range(-1f, 1f);
            var randomY = Random.Range(-1f, 1f);

            _randomPosition = _nextPosition + new Vector3(randomX, randomY, 0);
        }

        private void SetDirection(Vector3 nextPosition)
        {
            Vector3 direction = (nextPosition - _firefly.transform.position).normalized;
            if (direction.x != 0)
                _firefly.transform.localScale = direction.x > 0 ? new Vector2(1, 1) : new Vector2(-1, 1);
        }
    }
}