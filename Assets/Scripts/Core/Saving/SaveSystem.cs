using Assets.Scripts.Core.Saving.Data;
using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Core.Saving
{
    [DefaultExecutionOrder(-1000)]
    internal class SaveSystem : MonoBehaviour
    {
        [SerializeField] private string _file = "save.json";
        [SerializeField] private PlayerSaveData _defaultSaveDataIfNoneSave = new();
        private PlayerSaveData _loadedData;

        private void Awake()
        {
            if (!Load(out _))
            {
                _defaultSaveDataIfNoneSave.PlayerName = Guid.NewGuid().ToString().Substring(0, 5); // todo: name system in future...
                _loadedData = _defaultSaveDataIfNoneSave;
            }
        }

        public void Save(PlayerSaveData sd)
        {
            string path = GetPath();

            var json = JsonUtility.ToJson(sd);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Get or load save data
        /// </summary>
        /// <param name="save"></param>
        /// <returns></returns>
        public bool Load(out PlayerSaveData save)
        {
            if (_loadedData != null)
            {
                save = _loadedData;
                return true;
            }

            string path = GetPath();

            if (!File.Exists(path))
            {
                save = null;
                return false;
            }
            else
            {
                var json = File.ReadAllText(path);
                save = new PlayerSaveData();
                JsonUtility.FromJsonOverwrite(json, save);
                _loadedData = save;
                return true;
            }
        }

        public string GetPath()
        {
            return Path.Combine(Application.dataPath, _file);
        }
    }
}