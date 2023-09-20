using System.Linq;
using Photo.Scripts.Services;
using Photo.StaticData;
using UnityEngine;
using Zenject;

namespace Photo
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private LoadingPage _loadingPage;
        [SerializeField] private RulesMenu _rulesMenu;

        private Menu _menu;
        private PhotoAlbum _photoAlbum;
        private Settings _settings;
        private IStaticDataService _staticDataService;
        private AudioChanger _audioChanger;
        private SaveLoader _saveLoader;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(Menu menu,
            PhotoAlbum photoAlbum,
            Settings settings,
            IStaticDataService staticDataService,
            AudioChanger audioChanger,
            SaveLoader saveLoader,
            SceneLoader sceneLoader)
        {
            _menu = menu;
            _photoAlbum = photoAlbum;
            _settings = settings;
            _staticDataService = staticDataService;
            _audioChanger = audioChanger;
            _saveLoader = saveLoader;
            _sceneLoader = sceneLoader;

            Init();
        }

        private void Init()
        {
            _menu.Show();

            _menu.OnPlayButtonClickedEvent += PlayButtonClicked;
            _menu.OnPhotoButtonClickedEvent += PhotoButtonClicked;
            _menu.OnSettingsButtonClickedEvent += SettingsButtonClicked;
            _menu.OnExitButtonClickedEvent += ExitButtonClicked;
            
            _photoAlbum.OnAgainButtonClickedEvent += PhotoAlbumAgainButtonClicked;
            _photoAlbum.OnCloseButtonClickedEvent += PhotoAlbumCloseButtonClicked;

            _settings.OnSoundButtonClickedEvent += SoundButtonClicked;
            _settings.OnResetProgressButtonClickedEvent += ResetProgressButtonClicked;
            _settings.OnRulesButtonClickedEvent += OnRulesButtonClicked;
            _settings.OnVolumeSliderValueChangeEvent += VolumeSliderValueChange;
            _settings.OnCloseButtonClickedEvent += SettingsCloseButtonClicked;
        }

        private void Start()
        {
            var soundValue = _saveLoader.Data.Setting.SoundValue;
            _settings.LoadSoundValue(soundValue);
            _audioChanger.ChangeMusicValue(soundValue);
        }

        private void PlayButtonClicked()
        {
            _loadingPage.Show();
            var nextLevel = _saveLoader.Data.Progress.Prehistory
                ? _saveLoader.Data.Progress.GetLevel() + 2
                : 1;

            if (nextLevel >= 6)
            {
                LevelStaticData nextLevelData = GetNextNotPassedLevel(nextLevel);
                nextLevelData = nextLevelData == null ? GetFirstNotPassedLevel() : nextLevelData;
                nextLevel = nextLevelData != null ? nextLevelData.SceneID : 0;
                if (nextLevel == 0)
                {
                    nextLevel = 1;
                    _saveLoader.Data.Progress.Reset();
                }
            }            
            _sceneLoader.LoadSceneToID(nextLevel); 
        }

        private void PhotoButtonClicked()
        {
            _menu.Close();
            _photoAlbum.Show();
        }

        private void SettingsButtonClicked()
        {
            _menu.Close();
            _settings.Show();
        }

        private void ExitButtonClicked() => 
            Application.Quit();

        private void PhotoAlbumAgainButtonClicked(int sceneID)
        {
            _loadingPage.Show();
            _sceneLoader.LoadSceneToID(sceneID);
        }

        private void PhotoAlbumCloseButtonClicked()
        {
            _photoAlbum.Close();
            _menu.Show();
        }

        private void VolumeSliderValueChange(float value)
        {
            _audioChanger.ChangeMusicValue(value);
            _saveLoader.Data.Setting.SoundValueChanged(value);
        }

        private void SoundButtonClicked() => 
            _settings.OnOffMusic();

        private void ResetProgressButtonClicked() => 
            _saveLoader.Data.Progress.Reset();

        private void OnRulesButtonClicked() => 
            _rulesMenu.Show();

        private void SettingsCloseButtonClicked()
        {
            _saveLoader.Save();
            _settings.Close();
            _menu.Show();
        }
        
        private LevelStaticData GetNextNotPassedLevel(int nextLevel) => 
            _staticDataService.GetLevels()
                .Where(level => level.LevelID > nextLevel)
                .FirstOrDefault(level => !_saveLoader.Data.Progress.PuzzleAvailable(level.PuzzleInfo.ID));

        private LevelStaticData GetFirstNotPassedLevel() =>
            _staticDataService.GetLevels()
                .FirstOrDefault(x => !_saveLoader.Data.Progress.PuzzleAvailable(x.PuzzleInfo.ID));
    }
}