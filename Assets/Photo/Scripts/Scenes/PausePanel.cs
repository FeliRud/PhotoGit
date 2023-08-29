using UnityEngine;
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
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _menuButton;

        private Player _player;
        private AudioValueChanger _audioValueChanger;
        private SaveLoader _saveLoader;
        private SceneLoader _sceneLoader;
        private PauseInput _pauseInput;

        [Inject]
        private void Construct(Player player, AudioValueChanger audioValueChanger, SaveLoader saveLoader, SceneLoader sceneLoader)
        {
            _player = player;
            _audioValueChanger = audioValueChanger;
            _saveLoader = saveLoader;
            _sceneLoader = sceneLoader;

            _pauseInput = new PauseInput();
            _pauseInput.Enable();
            _pauseInput.Pause.Pause.performed += OnPauseButtonClicked;
            _rulesMenu.OnRulesCompletedEvent += OnRulesCompleted;
        }

        private void OnDestroy()
        {
            _pauseInput.Pause.Pause.performed -= OnPauseButtonClicked;
        }

        private void OnPauseButtonClicked(InputAction.CallbackContext obj)
        {
            if (!gameObject.activeSelf)
            {
                Show();
            }
            else
            {
                Close();
            }
        }

        private void Show()
        {
            _player.DisablePlayerInput();

            _settingsPanel.OnSoundButtonClickedEvent += OnSoundButtonClicked;
            _settingsPanel.OnRulesButtonClickedEvent += OnRulesButtonClicked;
            _settingsPanel.OnVolumeSliderValueChangeEvent += OnVolumeSliderValueChange;
            _settingsPanel.OnCloseButtonClickedEvent += OnSettingsCloseButtonClicked;

            _restartButton.onClick.AddListener(OnRestartButtonClicked);
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
            
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
            
            Fold();
        }

        private void Unfold()
        {
            gameObject.SetActive(true);
        }

        private void Fold()
        {
            gameObject.SetActive(false);
        }

        private void OnRestartButtonClicked()
        {
            _loadingPage.Show();
            _sceneLoader.RestartScene();
        }

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
            _settingsPanel.Show();
            _player.DisablePlayerInput();
        }

        private void OnVolumeSliderValueChange(float value)
        {
            _audioValueChanger.ChangeMusicValue(value);
        }

        private void OnSoundButtonClicked()
        {
            _settingsPanel.OnOffMusic();
        }

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