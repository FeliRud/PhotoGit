using UnityEngine;
using Zenject;

namespace Photo
{
    public class MainMenu : MonoBehaviour
    {
        private Menu _menu;
        private PhotoAlbum _photoAlbum;
        private Settings _settings;
        private AudioValueChanger _audioValueChanger;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(
            Menu menu,
            PhotoAlbum photoAlbum,
            Settings settings,
            AudioValueChanger audioValueChanger,
            SceneLoader sceneLoader)
        {
            _menu = menu;
            _menu.Show();
            _menu.OnPlayButtonClickedEvent += PlayButtonClicked;
            _menu.OnPhotoButtonClickedEvent += PhotoButtonClicked;
            _menu.OnSettingsButtonClickedEvent += SettingsButtonClicked;
            _menu.OnExitButtonClickedEvent += ExitButtonClicked;

            _photoAlbum = photoAlbum;
            _photoAlbum.OnPrevButtonClickedEvent += PrevButtonClicked;
            _photoAlbum.OnNextButtonClickedEvent += NextButtonClicked;
            _photoAlbum.OnCloseButtonClickedEvent += PhotoAlbumCloseButtonClicked;
            
            _settings = settings;
            _settings.OnSoundButtonClickedEvent += SoundButtonClicked;
            _settings.OnResetProgressButtonClickedEvent += ResetProgressButtonClicked;
            _settings.OnVolumeSliderValueChangeEvent += VolumeSliderValueChange;
            _settings.OnCloseButtonClickedEvent += SettingsCloseButtonClicked;

            _audioValueChanger = audioValueChanger;
            _sceneLoader = sceneLoader;
        }

        private void PlayButtonClicked()
        {
            _sceneLoader.LoadLevelCloud();
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

        private void ExitButtonClicked()
        {
            Application.Quit();
        }

        private void PrevButtonClicked()
        {
            
        }

        private void NextButtonClicked()
        {
            
        }

        private void PhotoAlbumCloseButtonClicked()
        {
            _photoAlbum.Close();
            _menu.Show();
        }

        private void VolumeSliderValueChange(float value)
        {
            _audioValueChanger.ChangeMusicValue(value);
        }

        private void SoundButtonClicked()
        {
            _settings.OnOffMusic();
        }

        private void ResetProgressButtonClicked()
        {
            
        }

        private void SettingsCloseButtonClicked()
        {
            _settings.Close();
            _menu.Show();
        }
    }
}