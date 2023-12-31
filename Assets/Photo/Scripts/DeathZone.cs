﻿using Photo.Scripts.Environment;
using UnityEngine;
using Zenject;

namespace Photo
{
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private LoadingPage _loadingPage;
        [SerializeField] private DeathPanel _deathPanel;

        private SaveLoader _saveLoader;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SaveLoader saveLoader, SceneLoader sceneLoader)
        {
            _saveLoader = saveLoader;
            _sceneLoader = sceneLoader;

            Init();
        }

        private void Init()
        {
            _deathPanel.OnRestartButtonClickedEvent += DeathPanelRestartButtonClicked;
            _deathPanel.OnMenuButtonClickedEvent += DeathPanelMenuButtonClicked;
        }

        private void DeathPanelRestartButtonClicked()
        {
            _saveLoader.Save();
            _loadingPage.Show();
            _deathPanel.Close();
            _sceneLoader.RestartScene();
        }

        private void DeathPanelMenuButtonClicked()
        {
            _saveLoader.Save();
            _deathPanel.Close();
            _loadingPage.Show();
            _sceneLoader.LoadSceneToID(0);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                player.Die();
                _saveLoader.Save();
                _loadingPage.Show();
                _deathPanel.Close();
                _sceneLoader.RestartScene();
            }

            var box = col.GetComponentInParent<ChangeSpawnPoint>();
            if (box != null) 
                box.GetComponent<Box>().Respawn();
        }
    }
}