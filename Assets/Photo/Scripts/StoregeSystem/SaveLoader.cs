using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Photo
{
    public class SaveLoader : MonoBehaviour
    {
        public GameData Data;
        
        private const string KEY = "Data.json";
        
        private IStorageService _storageService;

        [Inject]
        public void Construct()
        {
            Data = new GameData();
            _storageService = new JsonToFileStorageService();
            Load();
        }

        public void Start()
        {
            StartCoroutine(Autosave());
        }

        public void Save(Action<bool> callback = null)
        {
            _storageService.Save(KEY, Data, callback);
        }
        
        public void Load()
        {
            _storageService.Load<GameData>(KEY, LoadedData);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
                Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void LoadedData(GameData data)
        {
            if (data == null)
                return;

            Data = data;
        }
        
        private IEnumerator Autosave()
        {
            var delay = new WaitForSeconds(180);
            
            while (true)
            {
                yield return delay;
                Save();
            }
        }
    }
}