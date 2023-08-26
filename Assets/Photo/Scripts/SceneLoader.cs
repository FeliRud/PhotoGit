using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photo
{
    public class SceneLoader : MonoBehaviour
    {
        private Coroutine _loadSceneToIDRoutine;
        
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
            sceneAsync.allowSceneActivation = true;
            _loadSceneToIDRoutine = null;
        }
    }
}