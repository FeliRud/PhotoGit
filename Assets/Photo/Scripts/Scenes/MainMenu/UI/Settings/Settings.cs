using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Photo
{
    public class Settings : MonoBehaviour
    {
        public event Action OnSoundButtonClickedEvent;
        public event Action OnResetProgressButtonClickedEvent;
        public event Action OnRulesButtonClickedEvent;
        public event Action<float> OnVolumeSliderValueChangeEvent;
        public event Action OnCloseButtonClickedEvent;

        [SerializeField] private Slider _volume;
        [SerializeField] private Button _sound;
        [SerializeField] private Button _resetProgress;
        [SerializeField] private Button _rulesButton;
        [SerializeField] private Button _close;
        [SerializeField] private TextMeshProUGUI _version;
        [SerializeField] private CheckingReset _checkingReset;

        private float _prevValueVolume;

        public void Show()
        {
            _version.text = $"Версия: {Application.version}";
            _close.onClick.AddListener(CloseButtonClicked);
            _sound.onClick.AddListener(SoundButtonClicked);
            _rulesButton.onClick.AddListener(OnRulesButtonClicked);
            _volume.onValueChanged.AddListener(VolumeSliderValueChange);
            gameObject.SetActive(true);
            if (_checkingReset == null)
                return;
            _resetProgress.onClick.AddListener(OnOpenCheckingResetPanel);
            _checkingReset.OnRestartProgressButtonEvent += OnResetProgressButtonClicked;
        }

        public void Close()
        {
            _close.onClick.RemoveListener(CloseButtonClicked);
            _sound.onClick.RemoveListener(SoundButtonClicked);
            _volume.onValueChanged.RemoveListener(VolumeSliderValueChange);
            gameObject.SetActive(false);
            
            if (_checkingReset == null)
                return;
            _resetProgress.onClick.RemoveListener(OnOpenCheckingResetPanel);
            _checkingReset.OnRestartProgressButtonEvent -= OnResetProgressButtonClicked;
        }

        private void OnOpenCheckingResetPanel() =>
            _checkingReset.Show();

        public void OnOffMusic()
        {
            if (_volume.value > 0)
            {
                _prevValueVolume = _volume.value;
                _volume.value = 0;
            }
            else
                _volume.value = _prevValueVolume;
        }

        public void LoadSoundValue(float value)
        {
            _volume.value = value;
            OnVolumeSliderValueChangeEvent?.Invoke(value);
        }

        private void SoundButtonClicked() => 
            OnSoundButtonClickedEvent?.Invoke();

        private void VolumeSliderValueChange(float value) => 
            OnVolumeSliderValueChangeEvent?.Invoke(value);

        private void OnRulesButtonClicked() => 
            OnRulesButtonClickedEvent?.Invoke();

        private void OnResetProgressButtonClicked() => 
            OnResetProgressButtonClickedEvent?.Invoke();

        private void CloseButtonClicked() =>
            OnCloseButtonClickedEvent?.Invoke();
    }
}