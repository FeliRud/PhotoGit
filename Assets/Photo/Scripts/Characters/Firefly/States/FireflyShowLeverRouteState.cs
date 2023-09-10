using System.Linq;
using Photo.Scripts.Scenes.LevelMountains;
using Photo.Scripts.StateMachine;
using UnityEngine;

namespace Photo.Scripts.Characters.Firefly.States
{
    public class FireflyShowLeverRouteState : IState
    {
        private readonly Firefly _firefly;
        private readonly LeverPuzzles _leverPuzzles;
        
        private const float DELAY = 2f;
        
        private int _currentValue;
        private float _leftTime;
        private Vector3 _nextPosition;
        private Vector3 _randomPosition;

        public FireflyShowLeverRouteState(Firefly firefly, LeverPuzzles leverPuzzles)
        {
            _firefly = firefly;
            _leverPuzzles = leverPuzzles;
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

            _firefly.transform.position = Vector3.MoveTowards(fireflyPosition, _nextPosition, speed);
        }

        private void GetNextPosition()
        {
            if (_currentValue >= _leverPuzzles.Combination.Count)
            {
                _firefly.SetState(_firefly.GetState<FireflyIdleState>());
                return;
            }

            var nextLeverID = _leverPuzzles.Combination[_currentValue];
            Vector3 nextLampPosition = _leverPuzzles.Levers.First(lever => lever.ID == nextLeverID).transform.position;
            _nextPosition = new Vector3(nextLampPosition.x, nextLampPosition.y, 0);
        }

        private void RandomPosition()
        {
            var randomX = Random.Range(-1f, 1f);
            var randomY = Random.Range(-1f, 1f);

            _randomPosition = _nextPosition + new Vector3(randomX, randomY, 0);
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