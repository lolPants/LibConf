using System;
using System.IO;
using System.Collections.Generic;

namespace LibConf.Providers
{
    internal abstract class BaseProvider : IConfigProvider
    {
        #region Fields
        protected string _directory;
        protected string _name;

        protected readonly FileSystemWatcher _watcher;
        protected bool _dirty;

        protected readonly Dictionary<string, object> _defaults = new Dictionary<string, object>();
        #endregion

        #region Constructor
        protected BaseProvider(string directory, string name)
        {
            _directory = directory;
            _name = name;

            _watcher = new FileSystemWatcher(_directory)
            {
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite
                    | NotifyFilters.FileName
                    | NotifyFilters.DirectoryName,
            };

            _watcher.Changed += OnFileChange;
        }

        ~BaseProvider()
        {
            _watcher.Dispose();
        }
        #endregion

        public abstract string Name { get; }
        public string Filepath
        {
            get => Path.Combine(_directory, Name);
        }

        public bool StrictMode { get; set; }

        #region IO
        public abstract void Save();
        public abstract void Load();
        public Action FileModified { get; set; }

        protected bool IsFileReady(string path)
        {
            try
            {
                using (var file = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }

        protected void OnFileChange(object _, FileSystemEventArgs e)
        {
            if (e.FullPath != Filepath)
                return;

            if (_dirty)
                return;

            if (IsFileReady(e.FullPath) == false)
                return;

            FileModified?.Invoke();
        }
        #endregion

        #region Getters
        public abstract string GetString(string section, string key, string defaultValue);
        public string GetString(string key) => GetString(null, key, null);
        public string GetString(string key, string defaultValue) => GetString(null, key, defaultValue);

        public abstract bool? GetBoolean(string section, string key, bool? defaultValue);
        public bool? GetBoolean(string key) => GetBoolean(null, key, null);
        public bool? GetBoolean(string key, bool? defaultValue) => GetBoolean(null, key, defaultValue);
        public bool? GetBoolean(string section, string key) => GetBoolean(section, key, null);

        public abstract int? GetInt(string section, string key, int? defaultValue);
        public int? GetInt(string key) => GetInt(null, key, null);
        public int? GetInt(string key, int? defaultValue) => GetInt(null, key, defaultValue);
        public int? GetInt(string section, string key) => GetInt(section, key, null);

        public abstract float? GetFloat(string section, string key, float? defaultValue);
        public float? GetFloat(string key) => GetFloat(null, key, null);
        public float? GetFloat(string key, float? defaultValue) => GetFloat(null, key, defaultValue);
        public float? GetFloat(string section, string key) => GetFloat(section, key, null);
        #endregion

        #region Setters
        public abstract void ClearKey(string section, string key, bool autoSave);
        public void ClearKey(string key) => ClearKey(null, key, true);
        public void ClearKey(string key, bool autoSave) => ClearKey(null, key, autoSave);
        public void ClearKey(string section, string key) => ClearKey(section, key, true);

        public abstract void SetString(string section, string key, string value, bool autoSave);
        public void SetString(string key, string value) => SetString(null, key, value, true);
        public void SetString(string key, string value, bool autoSave) => SetString(null, key, value, autoSave);
        public void SetString(string section, string key, string value) => SetString(section, key, value, true);

        public abstract void SetBoolean(string section, string key, bool value, bool autoSave);
        public void SetBoolean(string key, bool value) => SetBoolean(null, key, value, true);
        public void SetBoolean(string key, bool value, bool autoSave) => SetBoolean(null, key, value, autoSave);
        public void SetBoolean(string section, string key, bool value) => SetBoolean(section, key, value, true);

        public abstract void SetInt(string section, string key, int value, bool autoSave);
        public void SetInt(string key, int value) => SetInt(null, key, value, true);
        public void SetInt(string key, int value, bool autoSave) => SetInt(null, key, value, autoSave);
        public void SetInt(string section, string key, int value) => SetInt(section, key, value, true);

        public abstract void SetFloat(string section, string key, float value, bool autoSave);
        public void SetFloat(string key, float value) => SetFloat(null, key, value, true);
        public void SetFloat(string key, float value, bool autoSave) => SetFloat(null, key, value, autoSave);
        public void SetFloat(string section, string key, float value) => SetFloat(section, key, value, true);
        #endregion

        #region Defaults
        public abstract void SaveDefaults();

        public void AddDefault(string key, string value)
        {
            _defaults[key] = value;
        }

        public void AddDefault(string key, bool value)
        {
            _defaults[key] = value;
        }

        public void AddDefault(string key, int value)
        {
            _defaults[key] = value;
        }

        public void AddDefault(string key, float value)
        {
            _defaults[key] = value;
        }
        #endregion
    }
}
