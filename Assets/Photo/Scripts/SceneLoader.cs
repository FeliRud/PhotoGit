using UnityEngine.SceneManagement;

namespace Photo
{
    public class SceneLoader
    {
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}