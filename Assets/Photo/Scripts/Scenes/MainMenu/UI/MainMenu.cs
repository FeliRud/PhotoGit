using UnityEngine;
using Zenject;

namespace Photo
{
    public class MainMenu : MonoBehaviour
    {
        private Menu _menu;
        private Settings _settings;
        private AudioValueChanger _audioValueChanger;

        [Inject]
        private void Construct(Menu menu, Settings settings, AudioValueChanger audioValueChanger)
        {
            _menu = menu;
            _menu.Show();
            _menu.OnPlayButtonClickedEvent += PlayButtonClicked;
            _menu.OnPhotoButtonClickedEvent += PhotoButtonClicked;
            _menu.OnSettingsButtonClickedEvent += SettingsButtonClicked;
            _menu.OnExitButtonClickedEvent += ExitButtonClicked;

            _settings = settings;
            _settings.OnSoundButtonClickedEvent += SoundButtonClicked;
            _settings.OnResetProgressButtonClickedEvent += ResetProgressButtonClicked;
            _settings.OnVolumeSliderValueChangeEvent += VolumeSliderValueChange;
            _settings.OnCloseButtonClickedEvent += SettingsCloseButtonClicked;

            _audioValueChanger = audioValueChanger;
        }

        private void PlayButtonClicked()
        {
            
        }

        private void PhotoButtonClicked()
        {
            _menu.Close();
        }

        private void SettingsButtonClicked()
        {
            _menu.Close();
            _settings.Show();
        }

        private void ExitButtonClicked()
        {
            
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