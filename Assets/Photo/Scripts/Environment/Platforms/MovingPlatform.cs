using DG.Tweening;
using UnityEngine;

namespace Photo
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] public GameObject _interactableObject;
        [SerializeField] public Transform _endPoint;

        private Vector3 _startPosition;
        private Interactable _interactable;
        
        private void Start()
        {
            if (_interactableObject.TryGetComponent(out Interactable interactable))
            {
                _interactable = interactable;
            }
            else
            {
                Debug.LogError("Используется некорректный объект взаимодействия.");
                return;
            }

            _startPosition = transform.position;
            _interactable.OnInteractionEvent += LeverInteraction;
        }

        private void LeverInteraction()
        {
            if (transform.position == _startPosition)
                transform.DOMove(_endPoint.position, 1f).SetEase(Ease.Linear);
            else
                transform.DOMove(_startPosition, 1f).SetEase(Ease.Linear);
        }
    }
}