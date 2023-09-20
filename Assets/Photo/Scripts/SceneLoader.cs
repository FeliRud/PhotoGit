using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Photo
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] private AudioClip _historyMusic;
        [SerializeField] private AudioClip _levelMusic;

        private Coroutine _loadSceneToIDRoutine;
        private AudioChanger _audioChanger;

        [Inject]
        private void Construct(AudioChanger audioChanger)
        {
            _audioChanger = audioChanger;
        }
        
        public void LoadSceneToID(int sceneID)
        {
            if (_loadSceneToIDRoutine != null)
                return;

            _loadSceneToIDRoutine = StartCoroutine(LoadSceneToIDRoutine(sceneID));
        }
        
        public void RestartScene()
        {
            if (_loadSceneToIDRoutine != null)
                return;
            
            _loadSceneToIDRoutine = StartCoroutine(LoadSceneToIDRoutine(SceneManager.GetActiveScene().buildIndex));
        }

        private IEnumerator LoadSceneToIDRoutine(int sceneID)
        {
            var sceneAsync = SceneManager.LoadSceneAsync(sceneID);

            sceneAsync.allowSceneActivation = false;
            yield return sceneAsync.progress < 0.9f;
            
            yield return new WaitForSeconds(1f);
            ChangeMusic(sceneID);
            sceneAsync.allowSceneActivation = true;
            _loadSceneToIDRoutine = null;
        }

        private void ChangeMusic(int sceneID)
        {
            switch (sceneID)
            {
                case 0:
                    _audioChanger.SetMusic(_menuMusic);
                    break;
                case 1:
                    _audioChanger.SetMusic(_historyMusic);
                    break;
                case 7: 
                    _audioChanger.SetMusic(_historyMusic);
                    break;
                default:
                    _audioChanger.SetMusic(_levelMusic);
                    break;
            }
        }
    }
}