using System;
using System.Collections.Generic;
using System.IO;
using Nett;
using LibConf.Exceptions;

namespace LibConf.Providers
{
    internal sealed class TomlProvider : BaseProvider
    {
        private TomlTable _tomlData;

        public TomlProvider(string directory, string name) : base(directory, name)
        {
            Load();
        }

        public override string Name
        {
            get => $"{_name}.toml";
        }

        #region IO
        public override void Save()
        {
            Toml.WriteFile(_tomlData, Filepath);
        }

        public override void Load()
        {
            try
            {
                _tomlData = Toml.Create(); _tomlData = Toml.ReadFile(Filepath);
            }
            catch (FileNotFoundException)
            {
                _tomlData = Toml.Create();
            }
        }
        #endregion

        #region Getters
        public override string GetString(string section, string key, string defaultValue)
        {
            if (section == null)
            {
                try
                {
                    return _tomlData.Get<string>(key);
                }
                catch (KeyNotFoundException)
                {
                    if (defaultValue != null)
                        return defaultValue;

                    return null;
                }
            }
            else
            {
                TomlTable _table;
                try
                {
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (KeyNotFoundException)
                {
                    _tomlData[section] = Toml.Create();
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (InvalidOperationException ex)
                {
                    if (StrictMode) throw new IncorrectTypeException(ex.Message);
                    else return null;
                }

                try
                {
                    return _table.Get<string>(key);
                }
                catch (KeyNotFoundException)
                {
                    if (defaultValue != null)
                        return defaultValue;

                    return null;
                }
                catch (InvalidOperationException ex)
                {
                    if (StrictMode) throw new IncorrectTypeException(ex.Message);
                    else return null;
                }
            }
        }

        public override bool? GetBoolean(string section, string key, bool? defaultValue)
        {
            if (section == null)
            {
                try
                {
                    return _tomlData.Get<bool>(key);
                }
                catch (KeyNotFoundException)
                {
                    if (defaultValue != null)
                        return defaultValue;

                    return null;
                }
                catch (InvalidOperationException ex)
                {
                    if (StrictMode) throw new IncorrectTypeException(ex.Message);
                    else return null;
                }
            }
            else
            {
                TomlTable _table;
                try
                {
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (KeyNotFoundException)
                {
                    _tomlData[section] = Toml.Create();
                    _table = _tomlData.Get<TomlTable>(section);
                }

                try
                {
                    return _table.Get<bool>(key);
                }
                catch (KeyNotFoundException)
                {
                    if (defaultValue != null)
                        return defaultValue;

                    return null;
                }
                catch (InvalidOperationException ex)
                {
                    if (StrictMode) throw new IncorrectTypeException(ex.Message);
                    else return null;
                }
            }
        }

        public override int? GetInt(string section, string key, int? defaultValue)
        {
            if (section == null)
            {
                try
                {
                    return _tomlData.Get<int>(key);
                }
                catch (KeyNotFoundException)
                {
                    if (defaultValue != null)
                        return defaultValue;

                    return null;
                }
                catch (InvalidOperationException ex)
                {
                    if (StrictMode) throw new IncorrectTypeException(ex.Message);
                    else return null;
                }
            }
            else
            {
                TomlTable _table;
                try
                {
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (KeyNotFoundException)
                {
                    _tomlData[section] = Toml.Create();
                    _table = _tomlData.Get<TomlTable>(section);
                }

                try
                {
                    return _table.Get<int>(key);
                }
                catch (KeyNotFoundException)
                {
                    if (defaultValue != null)
                        return defaultValue;

                    return null;
                }
                catch (InvalidOperationException ex)
                {
                    if (StrictMode) throw new IncorrectTypeException(ex.Message);
                    else return null;
                }
            }
        }

        public override float? GetFloat(string section, string key, float? defaultValue)
        {
            if (section == null)
            {
                try
                {
                    return _tomlData.Get<float>(key);
                }
                catch (KeyNotFoundException)
                {
                    if (defaultValue != null)
                        return defaultValue;

                    return null;
                }
                catch (InvalidOperationException ex)
                {
                    if (StrictMode) throw new IncorrectTypeException(ex.Message);
                    else return null;
                }
            }
            else
            {
                TomlTable _table;
                try
                {
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (KeyNotFoundException)
                {
                    _tomlData[section] = Toml.Create();
                    _table = _tomlData.Get<TomlTable>(section);
                }

                try
                {
                    return _table.Get<float>(key);
                }
                catch (KeyNotFoundException)
                {
                    if (defaultValue != null)
                        return defaultValue;

                    return null;
                }
                catch (InvalidOperationException ex)
                {
                    if (StrictMode) throw new IncorrectTypeException(ex.Message);
                    else return null;
                }
            }
        }
        #endregion

        #region Setters
        public override void ClearKey(string section, string key, bool autoSave)
        {
            if (section == null)
            {
                if (_tomlData.ContainsKey(key))
                    _tomlData.Remove(key);
            }
            else
            {
                TomlTable _table;
                try
                {
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (KeyNotFoundException)
                {
                    _tomlData[section] = Toml.Create();
                    _table = _tomlData.Get<TomlTable>(section);
                }

                if (_table.ContainsKey(key))
                    _table.Remove(key);
            }

            if (autoSave) Save();
        }

        public override void SetString(string section, string key, string value, bool autoSave)
        {
            if (section == null)
            {
                _ = _tomlData.ContainsKey(key)
                    ? _tomlData.Update(key, value)
                    : _tomlData.Add(key, value);
            }
            else
            {
                TomlTable _table;
                try
                {
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (KeyNotFoundException)
                {
                    _tomlData[section] = Toml.Create();
                    _table = _tomlData.Get<TomlTable>(section);
                }

                _ = _table.ContainsKey(key)
                    ? _table.Update(key, value)
                    : _table.Add(key, value);
            }

            if (autoSave) Save();
        }

        public override void SetBoolean(string section, string key, bool value, bool autoSave)
        {
            if (section == null)
            {
                _ = _tomlData.ContainsKey(key)
                    ? _tomlData.Update(key, value)
                    : _tomlData.Add(key, value);
            }
            else
            {
                TomlTable _table;
                try
                {
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (KeyNotFoundException)
                {
                    _tomlData[section] = Toml.Create();
                    _table = _tomlData.Get<TomlTable>(section);
                }

                _ = _table.ContainsKey(key)
                    ? _table.Update(key, value)
                    : _table.Add(key, value);
            }

            if (autoSave) Save();
        }

        public override void SetInt(string section, string key, int value, bool autoSave)
        {
            if (section == null)
            {
                _ = _tomlData.ContainsKey(key)
                    ? _tomlData.Update(key, value)
                    : _tomlData.Add(key, value);
            }
            else
            {
                TomlTable _table;
                try
                {
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (KeyNotFoundException)
                {
                    _tomlData[section] = Toml.Create();
                    _table = _tomlData.Get<TomlTable>(section);
                }

                _ = _table.ContainsKey(key)
                    ? _table.Update(key, value)
                    : _table.Add(key, value);
            }

            if (autoSave) Save();
        }

        public override void SetFloat(string section, string key, float value, bool autoSave)
        {
            if (section == null)
            {
                _ = _tomlData.ContainsKey(key)
                    ? _tomlData.Update(key, value)
                    : _tomlData.Add(key, value);
            }
            else
            {
                TomlTable _table;
                try
                {
                    _table = _tomlData.Get<TomlTable>(section);
                }
                catch (KeyNotFoundException)
                {
                    _tomlData[section] = Toml.Create();
                    _table = _tomlData.Get<TomlTable>(section);
                }

                _ = _table.ContainsKey(key)
                    ? _table.Update(key, value)
                    : _table.Add(key, value);
            }

            if (autoSave) Save();
        }
        #endregion

        #region Defaults
        public override void SaveDefaults()
        {
            foreach (var entry in _defaults)
            {
                string key = entry.Key;
                object value = entry.Value;

                if (_tomlData.ContainsKey(key))
                    continue;

                if (value.GetType().Equals(typeof(string)))
                {
                    string v = (string)value;
                    _tomlData.Add(key, v);
                }
                else if (value.GetType().Equals(typeof(bool)))
                {
                    bool v = (bool)value;
                    _tomlData.Add(key, v);
                }
                else if (value.GetType().Equals(typeof(int)))
                {
                    int v = (int)value;
                    _tomlData.Add(key, v);
                }
                else if (value.GetType().Equals(typeof(float)))
                {
                    float v = (float)value;
                    _tomlData.Add(key, v);
                }
            }

            Save();
        }
        #endregion
    }
}
