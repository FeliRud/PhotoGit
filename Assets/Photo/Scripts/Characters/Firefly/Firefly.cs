using System;
using System.Collections.Generic;
using Photo.Scripts.Characters.Firefly.States;
using Photo.Scripts.Scenes.LevelMountains;
using Photo.Scripts.StateMachine;
using UnityEngine;
using Zenject;

namespace Photo.Scripts.Characters.Firefly
{
    public class Firefly : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private LampPuzzles _lampPuzzle;
        [SerializeField] private LeverPuzzles _leverPuzzles;

        private Dictionary<Type, IState> _states;
        private IState _currentState;
        private Player _player;
        private Vector3 _followIdlePosition = Vector3.zero;

        public bool ItHelps { get; set; }

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
            InitState();
            SetStateByDefault();
            
            _player.OnFireflyHelpEvent += OnFireflyHelp;
        }

        private void Update()
        {
            if (ItHelps)
                return;
            
            if (transform.position == _player.FollowTarget.position && _player.FollowTarget.position != _followIdlePosition)
            {
                SetState(GetState<FireflyIdleState>());
                _followIdlePosition = _player.FollowTarget.position;
            }

            if (_player.FollowTarget.position != _followIdlePosition)
                SetState(GetState<FireflyFollowState>());
        }

        private void FixedUpdate() => 
            _currentState?.Update();

        public void SetState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public IState GetState<T>() where T : IState
        {
            var type = typeof(T);
            return _states[type];
        }

        private void InitState()
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(FireflyFollowState)] = new FireflyFollowState(_speed, transform, _player.FollowTarget),
                [typeof(FireflyIdleState)] = new FireflyIdleState(transform, _player.FollowTarget)
            };

            if (_lampPuzzle != null)
                _states[typeof(FireflyShowLampRouteState)] = new FireflyShowLampRouteState(this, _lampPuzzle);

            if (_leverPuzzles != null)
                _states[typeof(FireflyShowLeverRouteState)] = new FireflyShowLeverRouteState(this, _leverPuzzles);
        }

        private void SetStateByDefault()
        {
            var stateByDefault = GetState<FireflyIdleState>();
            SetState(stateByDefault);
        }

        private void OnFireflyHelp()
        {
            TrySetStateLampRoute();
            TrySetStateLeverRoute();
        }

        private void TrySetStateLeverRoute()
        {
            if (_leverPuzzles == null) 
                return;
            
            ItHelps = true;
            SetState(GetState<FireflyShowLeverRouteState>());
        }

        private void TrySetStateLampRoute()
        {
            if (_lampPuzzle == null) 
                return;
            
            ItHelps = true;
            SetState(GetState<FireflyShowLampRouteState>());
        }
    }
}