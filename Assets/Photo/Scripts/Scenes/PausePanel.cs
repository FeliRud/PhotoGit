﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Photo
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private LoadingPage _loadingPage;
        [SerializeField] private Settings _settingsPanel;
        [SerializeField] private RulesMenu _rulesMenu;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _menuButton;

        private Player _player;
        private AudioChanger _audioChanger;
        private SaveLoader _saveLoader;
        private SceneLoader _sceneLoader;
        private PauseInput _pauseInput;

        [Inject]
        private void Construct(Player player, AudioChanger audioChanger, SaveLoader saveLoader,
            SceneLoader sceneLoader)
        {
            _player = player;
            _audioChanger = audioChanger;
            _saveLoader = saveLoader;
            _sceneLoader = sceneLoader;

            _pauseInput = new PauseInput();
            _pauseInput.Enable();
            _pauseInput.Pause.Pause.performed += OnPauseButtonClicked;
        }

        private void OnDestroy()
        {
            _pauseInput.Pause.Pause.performed -= OnPauseButtonClicked;
        }

        private void OnPauseButtonClicked(InputAction.CallbackContext obj)
        {
            if (!gameObject.activeSelf)
                Show();
            else
                Close();
        }

        private void Show()
        {
            _player.DisablePlayerInput();

            _settingsPanel.OnSoundButtonClickedEvent += OnSoundButtonClicked;
            _settingsPanel.OnRulesButtonClickedEvent += OnRulesButtonClicked;
            _settingsPanel.OnVolumeSliderValueChangeEvent += OnVolumeSliderValueChange;
            _settingsPanel.OnCloseButtonClickedEvent += OnSettingsCloseButtonClicked;
            _rulesMenu.OnRulesCompletedEvent += OnRulesCompleted;

            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _menuButton.onClick.AddListener(OnMenuButtonClicked);

            Unfold();
        }

        private void Close()
        {
            _player.EnablePlayerInput();

            _settingsPanel.OnSoundButtonClickedEvent -= OnSoundButtonClicked;
            _settingsPanel.OnRulesButtonClickedEvent -= OnRulesButtonClicked;
            _settingsPanel.OnVolumeSliderValueChangeEvent -= OnVolumeSliderValueChange;
            _settingsPanel.OnCloseButtonClickedEvent -= OnSettingsCloseButtonClicked;
            _rulesMenu.OnRulesCompletedEvent -= OnRulesCompleted;

            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);

            Fold();
        }

        private void Unfold() =>
            gameObject.SetActive(true);

        private void Fold() =>
            gameObject.SetActive(false);

        private void OnContinueButtonClicked() =>
            Close();

        private void OnSettingsButtonClicked()
        {
            Fold();
            _pauseInput.Disable();
            _settingsPanel.Show();
        }

        private void OnMenuButtonClicked()
        {
            _loadingPage.Show();
            _sceneLoader.LoadSceneToID(0);
        }

        private void OnRulesCompleted()
        {
            _player.DisablePlayerInput();
            _settingsPanel.Show();
        }

        private void OnVolumeSliderValueChange(float value) =>
            _audioChanger.ChangeMusicValue(value);

        private void OnSoundButtonClicked() =>
            _settingsPanel.OnOffMusic();

        private void OnRulesButtonClicked()
        {
            _settingsPanel.Close();
            _rulesMenu.Show();
        }

        private void OnSettingsCloseButtonClicked()
        {
            _saveLoader.Save();
            _settingsPanel.Close();
            _pauseInput.Enable();
            Unfold();
        }
    }
}