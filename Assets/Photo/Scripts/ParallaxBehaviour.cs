using UnityEngine;
using Zenject;

namespace Photo
{
    public class ParallaxBehaviour : MonoBehaviour
    {
        [SerializeField] private Vector2 _parallaxStrength;
        
        private Transform _mainCameraTransform;
        private Vector3 _lastCameraPosition;

        private void Start()
        {
            _mainCameraTransform = Camera.main.transform;
            _lastCameraPosition = _mainCameraTransform.position;
        }

        private void LateUpdate()
        {
            Vector3 deltaMovement = _mainCameraTransform.position - _lastCameraPosition;
            transform.position += new Vector3(
                deltaMovement.x * _parallaxStrength.x, 
                deltaMovement.y * _parallaxStrength.y, 0);
            _lastCameraPosition = _mainCameraTransform.position;
        }
    }
}