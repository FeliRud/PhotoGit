using System;
using DG.Tweening;
using UnityEngine;

namespace Photo
{
    public class PhotoFrame : MonoBehaviour
    {
        public event Action OnPlayerTakePhotoEvent;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _duration = 0.8f;
        private Tweener _tween;

        private void OnDisable()
        {
            _tween.Kill();
        }

        private void Start()
        {
            _spriteRenderer.color = new Color(0, 1, 1);
            ChangeBlue();
        }

        private void ChangeRed()
        {
            _tween.onComplete -= ChangeRed;
            float currentR = _spriteRenderer.color.r;

            if (currentR == 0)
            {
                _tween = _spriteRenderer
                    .DOColor(new Color(1, _spriteRenderer.color.g, _spriteRenderer.color.b), _duration)
                    .SetEase(Ease.Linear);
            }
            else
            {
                _tween = _spriteRenderer
                    .DOColor(new Color(0, _spriteRenderer.color.g, _spriteRenderer.color.b), _duration)
                    .SetEase(Ease.Linear);;
            }

            _tween.onComplete += ChangeGreen;
        }

        private void ChangeGreen()
        {
            _tween.onComplete -= ChangeGreen;
            float currentR = _spriteRenderer.color.g;

            if (currentR == 0)
            {
                _tween = _spriteRenderer
                    .DOColor(new Color(_spriteRenderer.color.r, 1, _spriteRenderer.color.b), _duration)
                    .SetEase(Ease.Linear);
            }
            else
            {
                _tween = _spriteRenderer
                    .DOColor(new Color(_spriteRenderer.color.r, 0,_spriteRenderer.color.b), _duration)
                    .SetEase(Ease.Linear);
            }

            _tween.onComplete += ChangeBlue;
        }
        
        private void ChangeBlue()
        {
            if (_tween != null)
                _tween.onComplete -= ChangeBlue;
            float currentR = _spriteRenderer.color.b;

            if (currentR == 0)
            {
                _tween = _spriteRenderer
                    .DOColor(new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, 1), _duration)
                    .SetEase(Ease.Linear);
            }
            else
            {
                _tween = _spriteRenderer
                    .DOColor(new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, 0), _duration)
                     .SetEase(Ease.Linear);
            }

            _tween.onComplete += ChangeRed;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                OnPlayerTakePhotoEvent?.Invoke();
            }
        }
    }
}