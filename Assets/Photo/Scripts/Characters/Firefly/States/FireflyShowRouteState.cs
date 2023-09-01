using System.Linq;
using UnityEngine;

namespace Photo
{
    public class FireflyShowRouteState : IState
    {
        private const float DELAY = 2f;

        private readonly Firefly _firefly;
        private readonly LampPuzzles _lampsPuzzle;
        
        private int _currentValue;
        private Lamp _currentLamp;
        private float _leftTime;
        private Vector3 _nextPosition;
        private Vector3 _randomPosition;

        public FireflyShowRouteState(Firefly firefly, LampPuzzles lampsPuzzle)
        {
            _firefly = firefly;
            _lampsPuzzle = lampsPuzzle;
        }

        public void Enter()
        {
            _currentValue = 0;
            _leftTime = 0;
            _currentLamp = _lampsPuzzle.Lamps[_currentValue];
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
            _firefly.HelpLamp = false;

        private void MoveToPosition()
        {
            Vector3 fireflyPosition = _firefly.transform.position;
            float speed = Time.deltaTime * 7f;

            if (fireflyPosition == _nextPosition)
            {
                _currentValue++;
                if (_currentValue < _lampsPuzzle.Lamps.Count)
                    _currentLamp = _lampsPuzzle.Lamps[_currentValue];
                _leftTime = DELAY;
                return;
            }

            _firefly.transform.position = Vector3.MoveTowards(fireflyPosition, _nextPosition, speed);
        }

        private void GetNextPosition()
        {
            if (_currentValue >= _lampsPuzzle.Combination.Count)
            {
                _firefly.SetState(_firefly.GetState<FireflyIdleState>());
                return;
            }

            var nextLampID = _lampsPuzzle.Combination[_currentValue];
            Vector3 nextLampPosition = _lampsPuzzle.Lamps.First(lamp => lamp.ID == nextLampID).transform.position;
            _nextPosition = new Vector3(nextLampPosition.x, nextLampPosition.y, 0);
        }

        private void RandomPosition()
        {
            var randomX = Random.Range(-1f, 1f);
            var randomY = Random.Range(-1f, 1f);

            _randomPosition = _currentLamp.transform.position + new Vector3(randomX, randomY, 0);
        }

        private void MoveToRandomPosition()
        {
            _leftTime -= Time.deltaTime;

            Vector3 fireflyPosition = _firefly.transform.position;

            if (_randomPosition == fireflyPosition)
                RandomPosition();

            Vector3 direction = (_randomPosition - fireflyPosition).normalized;
            if (direction.x != 0)
                _firefly.transform.localScale = direction.x > 0 ? new Vector2(1, 1) : new Vector2(-1, 1);
            _firefly.transform.position = Vector3.MoveTowards(fireflyPosition, _randomPosition, 3 * Time.deltaTime);
        }
    }
}