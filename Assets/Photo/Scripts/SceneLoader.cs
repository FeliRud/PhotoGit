using UnityEngine.SceneManagement;

namespace Photo
{
    public class SceneLoader
    {
        private const string LEVEL_CLOUD = "LevelCloud"; 
        
        public void LoadLevelCloud()
        {
            SceneManager.LoadScene(LEVEL_CLOUD);
        }
        
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}