using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Photo
{
    public class LoadingPage : MonoBehaviour
    {
        [SerializeField] private Image _loadingImage;
        [SerializeField] private List<TextMeshProUGUI> _textHints;

        private void OnEnable()
        {
            foreach (var textHint in _textHints)
            {
                textHint.gameObject.SetActive(false);
            }
            
            _textHints[Random.Range(0, _textHints.Count)].gameObject.SetActive(true);
        }

        private void Update()
        {
            _loadingImage.rectTransform.Rotate(0f,0f, -Time.deltaTime * 10); 
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}