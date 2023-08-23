using UnityEngine.SceneManagement;

namespace Photo
{
    public class SceneLoader
    {
        private const string LEVEL_CLOUD = "LevelCloud"; 
        private const string LEVEL_CITY = "LevelCity"; 
        
        public void LoadLevelCloud()
        {
            SceneManager.LoadScene(LEVEL_CLOUD);
        }
        
        public void LoadSceneToInt(int sceneID)
        {
            SceneManager.LoadScene(sceneID);
        }
        
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}