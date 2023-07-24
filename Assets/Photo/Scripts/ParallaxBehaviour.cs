using UnityEngine;

namespace Photo
{
    public class ParallaxBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _followTarget;
        [SerializeField, Range(0f, 1f)] private float _strength = 0.1f;
        [SerializeField] private bool _disableVertical;

        private Vector3 _targetPreviousPosition;

        private void Start()
        {
            if (!_followTarget)
                _followTarget = Camera.main.transform;

            _targetPreviousPosition = _followTarget.position;
        }

        private void Update()
        {
            var delta = _followTarget.position - _targetPreviousPosition;

            if (_disableVertical)
                delta.y = 0;

            _targetPreviousPosition = _followTarget.position;
            transform.position += delta * _strength;
        }
    }
}