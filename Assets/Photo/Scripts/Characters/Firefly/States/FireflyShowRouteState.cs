using UnityEngine;

namespace Photo
{
    public class FireflyShowRouteState : IState
    {
        private const float DELAY = 2f;
        
        private readonly Firefly _firefly;
        private readonly LampPuzzles _lampPuzzles;

        private int _currentValue;
        private Lamp _currentLamp;
        private float _leftTime;
        private Vector3 _nextPosition;
        private Vector3 _randomPosition;
        
        public FireflyShowRouteState(Firefly firefly, LampPuzzles lampPuzzles)
        {
            _firefly = firefly;
            _lampPuzzles = lampPuzzles;
        }
        
        public void Enter()
        {
            _currentValue = 0;
            UpdateCurrentLamp();
        }

        public void Update()
        {
            if (_leftTime > 0)
            {
                MoveToRandomPosition();
                return;
            }
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

        public void Exit()
        {
        }
        
        private void RandomPosition()
        {
            var randomX = Random.Range(-1f, 1f);
            var randomY = Random.Range(-1f, 1f);

            _randomPosition = _currentLamp.transform.position + new Vector3(randomX, randomY, 0);
        }

        private void UpdateCurrentLamp() => 
            _currentLamp = _lampPuzzles.Lamps[_currentValue];
    }
}