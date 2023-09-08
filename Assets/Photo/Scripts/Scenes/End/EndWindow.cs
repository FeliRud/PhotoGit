using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Photo.End
{
    public class EndWindow : MonoBehaviour
    {
        [SerializeField] private GameObject[] _startItems;
        
        [Header("First Option")] 
        [SerializeField] private PlayableDirector _firstPlayable;
        [SerializeField] private GameObject[] _firstItems;
        [SerializeField] private Button _firstOptionButton;

        [Header("Second Option")]
        [SerializeField] private PlayableDirector _secondPlayable;
        [SerializeField] private GameObject[] _secondItems;
        [SerializeField] private Button _secondOptionButton;
        
        private void OnDisable()
        {
            _firstOptionButton.onClick.RemoveListener(FirstOptionButtonClicked);
            _secondOptionButton.onClick.RemoveListener(SecondOptionButtonClicked);
        }

        public void Show()
        {
            _firstOptionButton.onClick.AddListener(FirstOptionButtonClicked);
            _secondOptionButton.onClick.AddListener(SecondOptionButtonClicked);
            gameObject.SetActive(true);
        }

        private void FirstOptionButtonClicked()
        {
            CloseWindow();
            CloseItems(_startItems);
            OpenItems(_firstItems);
            PlayCutscene(_firstPlayable);
        }

        private void SecondOptionButtonClicked()
        {
            CloseWindow();            
            CloseItems(_startItems);
            OpenItems(_secondItems);
            PlayCutscene(_secondPlayable);
        }

        private void OpenItems(GameObject[] items)
        {
            foreach (var item in items) 
                item.SetActive(true);
        }

        private void CloseItems(GameObject[] items)
        {
            foreach (var item in items) 
                item.SetActive(false);
        }

        private void CloseWindow() => 
            gameObject.SetActive(false);

        private void PlayCutscene(PlayableDirector playableDirector) => 
            playableDirector.Play();
    }
}