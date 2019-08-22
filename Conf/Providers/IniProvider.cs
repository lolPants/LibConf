using IniParser;
using IniParser.Exceptions;
using IniParser.Model;
using IniParser.Model.Configuration;
using IniParser.Parser;
using LibConf.Utils;

namespace LibConf.Providers
{
    internal sealed class IniProvider : BaseProvider
    {
        private readonly IniParserConfiguration _config = new IniParserConfiguration();
        private readonly FileIniDataParser _parser;
        private readonly IniDataParser _dataParser;
        private IniData _iniData;

        public IniProvider(string directory, string name) : base(directory, name)
        {
            _config.OverrideDuplicateKeys = true;
            _config.SkipInvalidLines = true;
            _config.ThrowExceptionsOnError = true;
            _config.AllowKeysWithoutSection = true;

            _dataParser = new IniDataParser(_config);
            _parser = new FileIniDataParser(_dataParser);

            _watcher.Filter = "*.ini";

            Load();
        }

        public override string Name
        {
            get => $"{_name}.ini";
        }

        #region IO
        public override void Save()
        {
            _dirty = true;

            IOUtils.EnsureDirectory(_directory);
            _parser.WriteFile(Filepath, _iniData);

            _dirty = false;
        }

        public override void Load()
        {
            try
            {
                var data = _parser.ReadFile(Filepath);

                if (_iniData == null)
                {
                    _iniData = data;
                }
                else
                {
                    _iniData.Merge(data);
                }
            }
            catch (ParsingException)
            {
                _iniData = new IniData();
            }
        }
        #endregion

        #region Getters
        public override string GetString(string section, string key, string defaultValue)
        {
            string sect = section ?? _name;
            string value = _iniData[sect][key];

            if (value != null)
            {
                return value;
            }
            else if (defaultValue != null)
            {
                return defaultValue;
            }
            else if (_defaults.TryGetValue(key, out object v) && v.GetType().Equals(typeof(string)))
            {
                return (string)v;
            }
            else
            {
                return null;
            }
        }

        public override bool? GetBoolean(string section, string key, bool? defaultValue)
        {
            string sect = section ?? _name;
            string value = _iniData[sect][key];

            if (value != null && bool.TryParse(value, out bool val))
            {
                return val;
            }
            else if (defaultValue != null)
            {
                return defaultValue;
            }
            else if (_defaults.TryGetValue(key, out object v) && v.GetType().Equals(typeof(bool)))
            {
                return (bool)v;
            }
            else
            {
                return null;
            }
        }

        public override int? GetInt(string section, string key, int? defaultValue)
        {
            string sect = section ?? _name;
            string value = _iniData[sect][key];

            if (value != null && int.TryParse(value, out int val))
            {
                return val;
            }
            else if (defaultValue != null)
            {
                return defaultValue;
            }
            else if (_defaults.TryGetValue(key, out object v) && v.GetType().Equals(typeof(int)))
            {
                return (int)v;
            }
            else
            {
                return null;
            }
        }

        public override float? GetFloat(string section, string key, float? defaultValue)
        {
            string sect = section ?? _name;
            string value = _iniData[sect][key];

            if (value != null && float.TryParse(value, out float val))
            {
                return val;
            }
            else if (defaultValue != null)
            {
                return defaultValue;
            }
            else if (_defaults.TryGetValue(key, out object v) && v.GetType().Equals(typeof(float)))
            {
                return (float)v;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Setters
        public override void ClearKey(string section, string key, bool autoSave)
        {
            string sect = section ?? _name;
            _iniData[sect].RemoveKey(key);

            if (autoSave)
                Save();
        }

        private void SetValue<T>(string section, string key, T value, bool autoSave)
        {
            string sect = section ?? _name;
            _iniData[sect][key] = value.ToString();

            if (autoSave)
                Save();
        }

        public override void SetString(string section, string key, string value, bool autoSave) => SetValue(section, key, value, autoSave);

        public override void SetBoolean(string section, string key, bool value, bool autoSave) => SetValue(section, key, value, autoSave);

        public override void SetInt(string section, string key, int value, bool autoSave) => SetValue(section, key, value, autoSave);

        public override void SetFloat(string section, string key, float value, bool autoSave) => SetValue(section, key, value, autoSave);
        #endregion

        #region Defaults
        public override void SaveDefaults()
        {
            foreach (var entry in _defaults)
            {
                string key = entry.Key;
                string value = entry.Value.ToString();

                if (_iniData[_name][key] != null)
                    continue;

                _iniData[_name][entry.Key] = value;
            }

            Save();
        }
        #endregion
    }
}
