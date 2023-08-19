using UnityEngine;

namespace Photo
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private Transform _firstPosition;
        [SerializeField] private Transform _secondPosition;

        private Transform _targetPosition;
        private void Start()
        {
            _targetPosition = _firstPosition;
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, _firstPosition.position) < 0.1f)
                _targetPosition = _secondPosition;
            
            if (Vector2.Distance(transform.position, _secondPosition.position) < 0.1f)
                _targetPosition = _firstPosition;

            transform.position = Vector2.MoveTowards(
                transform.position, 
                _targetPosition.position, 
                _speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            col.transform.SetParent(transform);
        }
        
        private void OnCollisionExit2D(Collision2D col)
        {
            col.transform.SetParent(null);
        }
    }
}