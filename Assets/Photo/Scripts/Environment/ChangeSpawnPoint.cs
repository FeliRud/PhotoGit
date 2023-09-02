using System;
using UnityEngine;

namespace Photo.Scripts.Environment
{
    public class ChangeSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _firstPoint;
        [SerializeField] private Transform _secondPoint;
        [SerializeField] private Interactable _interactable;

        public Vector3 CurrentSpawnPosition { get; private set; }

        private void Start()
        {
            if (_interactable == null)
                throw new AggregateException("Missing interactable object.");

            CurrentSpawnPosition = _firstPoint.position;
            _interactable.OnInteractionEvent += OnInteractable;
        }

        private void OnInteractable() =>
            CurrentSpawnPosition = CurrentSpawnPosition == _firstPoint.position
                ? _secondPoint.position
                : _firstPoint.position;
    }
}