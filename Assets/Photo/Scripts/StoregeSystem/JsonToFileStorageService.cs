using System;
using System.IO;
using UnityEngine;

namespace Photo
{
    public class JsonToFileStorageService : IStorageService
    {
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = BuildPath(key);
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
            callback?.Invoke(true);
        }

        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);

            if (!File.Exists(path))
                return;
            
            string jsonData = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(jsonData);
            callback.Invoke(data);
        }

        private string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key);
        }
    }
}