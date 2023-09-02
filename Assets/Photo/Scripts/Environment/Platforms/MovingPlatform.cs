using System;
using UnityEngine;

namespace Photo
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Interactable _interactable;
        [SerializeField] private Transform _model;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _speed = 5f;

        private Vector3 _startPosition;
        private Vector3 _nextPosition;

        private void Start()
        {
            if (_model == null)
                throw new ArgumentException("Missing model transform.");

            if (_interactable == null)
                throw new AggregateException("Missing interactable object.");

            _startPosition = transform.position;
            _nextPosition = _startPosition;
            _interactable.OnInteractionEvent += LeverInteraction;
        }

        private void Update()
        {
            if (_model.position == _nextPosition)
                return;

            MoveToPosition();
        }

        private void LeverInteraction() =>
            _nextPosition = _nextPosition == _startPosition ? _endPoint.position : _startPosition;

        private void MoveToPosition() =>
            _model.position = Vector3.MoveTowards(_model.position, _nextPosition, _speed * Time.deltaTime);
    }
}