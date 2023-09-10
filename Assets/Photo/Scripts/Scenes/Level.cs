using System.Linq;
using Photo.Scripts.Services;
using Photo.StaticData;
using UnityEngine;
using Zenject;

namespace Photo
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private LevelStaticData _currentLevelData;
        [SerializeField] private LoadingPage _loadingPage;
        [SerializeField] private int _nextLevelSceneID;
        [SerializeField] private int _levelID;

        private IStaticDataService _staticDataService;
        private PhotoFrame _photoFrame;
        private SaveLoader _saveLoader;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(IStaticDataService staticDataService, 
            SaveLoader saveLoader, 
            SceneLoader sceneLoader, 
            PhotoFrame photoFrame)
        {
            _staticDataService = staticDataService;
            _saveLoader = saveLoader;
            _sceneLoader = sceneLoader;
            _photoFrame = photoFrame;
            _photoFrame.OnPlayerTakePhotoEvent += PlayerTakePhoto;
        }

        private void PlayerTakePhoto()
        {
            _saveLoader.Data.Progress.LevelCompleted(_levelID);
            _saveLoader.Save();
            int nextLevelSceneID = 7;
            if (!CheckCollectedPuzzles())
            {
                LevelStaticData nextLevelData = GetNextNotPassedLevel();
                nextLevelData = nextLevelData == null ? GetFirstNotPassedLevel() : nextLevelData;
                nextLevelSceneID = nextLevelData != null ? nextLevelData.SceneID : 0;
            }
            _loadingPage.Show();
            _sceneLoader.LoadSceneToID(nextLevelSceneID);
        }

        private bool CheckCollectedPuzzles() => 
            _staticDataService.GetLevels()
                .All(level => _saveLoader.Data.Progress.PuzzleAvailable(level.PuzzleInfo.ID));

        private LevelStaticData GetNextNotPassedLevel() => 
            _staticDataService.GetLevels()
                .Where(level => level.LevelID > _currentLevelData.LevelID)
                .FirstOrDefault(level => !_saveLoader.Data.Progress.PuzzleAvailable(level.PuzzleInfo.ID));

        private LevelStaticData GetFirstNotPassedLevel() =>
            _staticDataService.GetLevels()
                .FirstOrDefault(x => !_saveLoader.Data.Progress.PuzzleAvailable(x.PuzzleInfo.ID));
    }
}