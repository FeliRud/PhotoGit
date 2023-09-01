using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Photo
{
    public class Firefly : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private LampPuzzles _lampPuzzle;

        private Dictionary<Type, IState> _states;
        private IState _currentState;

        private Player _player;
        private Vector3 _followIdlePosition = Vector3.zero;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
            InitState();
            SetStateByDefault();
        }

        private void Update()
        {
            
            
            if (transform.position == _player.FollowTarget.position && _player.FollowTarget.position != _followIdlePosition)
            {
                SetState(GetState<FireflyIdleState>());
                _followIdlePosition = _player.FollowTarget.position;
            }

            if (_player.FollowTarget.position != _followIdlePosition)
                SetState(GetState<FireflyFollowState>());
        }

        private void FixedUpdate()
        {
            _currentState?.Update();
        }

        private void InitState()
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(FireflyFollowState)] = new FireflyFollowState(_speed, transform, _player.FollowTarget),
                [typeof(FireflyIdleState)] = new FireflyIdleState(transform, _player.FollowTarget)
            };

            if (_lampPuzzle != null)
                _states[typeof(FireflyShowRouteState)] = new FireflyShowRouteState(this, _lampPuzzle);
        }

        private void SetState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private void SetStateByDefault()
        {
            var stateByDefault = GetState<FireflyIdleState>();
            SetState(stateByDefault);
        }
        
        private IState GetState<T>() where T : IState
        {
            var type = typeof(T);
            return _states[type];
        }
    }
}