using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Photo
{
    public class RulesPanel : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        public int ID => _id;
        
        public void Show()
        {
            gameObject.SetActive(true);
            
            _image.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0);
            _image.DOColor(Color.white, 0.5f);
            _text.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0);
            _text.DOColor(Color.white, 0.5f);
        }

        public void CloseFast()
        {
            _image.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0);
            _text.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0);
        }
        
        public void Close()
        {
            Tweener tween = _image.DOColor(new Color(Color.white.r, Color.white.g, Color.white.b, 0), 0.5f);
            _text.DOColor(new Color(Color.white.r, Color.white.g, Color.white.b, 0), 0.5f);
            tween.onComplete += CloseComplete;
        }

        private void CloseComplete()
        {
            gameObject.SetActive(false);
        }
    }
}