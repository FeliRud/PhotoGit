using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Photo
{
    public class PuzzleUI : MonoBehaviour
    {
        [SerializeField] private PuzzleInfo _puzzleInfo;
        [SerializeField] private Image _puzzleImage;
        
        public PuzzleInfo PuzzleInfo => _puzzleInfo;

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void CloseFast()
        {
            gameObject.SetActive(false);
        }

        public void Close()
        {
            _puzzleImage.color = Color.white;
            _puzzleImage.DOColor(new Color(1, 1, 1, 0), 1.5f)
                .SetEase(Ease.Linear).onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }
    }
}