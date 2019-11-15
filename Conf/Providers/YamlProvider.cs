using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using LibConf.Exceptions;
using LibConf.Yaml;
using LibConf.Utils;

namespace LibConf.Providers
{
    internal sealed class YamlProvider : BaseProvider
    {
        private IDeserializer _deserializer;
        private ISerializer _serializer;
        private Dictionary<object, object> _data;

        public YamlProvider(string directory, string name) : base(directory, name)
        {
            _deserializer = new DeserializerBuilder().WithNodeTypeResolver(new ScalarTypeResolver()).Build();
            _serializer = new SerializerBuilder().WithTypeConverter(new FloatTypeConverter()).Build();

            _watcher.Filter = "*.yaml";

            Load();
        }

        public override string Name
        {
            get => $"{_name}.yaml";
        }

        #region IO
        public override void Save()
        {
            _dirty = true;

            IOUtils.EnsureDirectory(_directory);
            using (StreamWriter file = File.CreateText(Filepath))
            {
                _serializer.Serialize(file, _data);
            }

            _dirty = false;
        }

        public override void Load()
        {
            try
            {
                Dictionary<object, object> newData;

                try
                {
                    using (TextReader reader = new StreamReader(Filepath))
                    {
                        newData = (Dictionary<object, object>)_deserializer.Deserialize(reader);
                    }
                }
                catch (IOException)
                {
                    newData = null;
                }

                if (newData == null)
                {
                    newData = new Dictionary<object, object>();
                }

                if (_data == null)
                {
                    _data = newData;
                    return;
                }

                foreach (var entry in newData)
                {
                    object key = entry.Key;
                    object value = entry.Value;

                    if (_data.ContainsKey(key))
                    {
                        _data[key] = value;
                    }
                    else
                    {
                        _data.Add(key, value);
                    }
                }
            }
            catch (FileNotFoundException) { }

            if (_data == null)
                _data = new Dictionary<object, object>();

        }
        #endregion

        #region Getters
        private T GetValue<T>(string section, string key, T defaultValue)
        {
            Dictionary<object, object> _table;
            if (section == null)
            {
                _table = _data;
            }
            else
            {
                try
                {
                    _table = (Dictionary<object, object>)_data[section];
                }
                catch (KeyNotFoundException)
                {
                    _table = new Dictionary<object, object>();
                }
            }

            try
            {
                object value = _table[key];
                Type type = value.GetType();

                if (type.IsAssignableFrom(typeof(T)))
                {
                    return (T)value;
                }
                else if (typeof(T) == typeof(bool?) && type == typeof(bool))
                {
                    return (T)value;
                }
                else if (typeof(T) == typeof(int?) && type == typeof(int))
                {
                    return (T)value;
                }
                else if (typeof(T) == typeof(float?) && type == typeof(float))
                {
                    return (T)value;
                }
                else if (typeof(T) == typeof(float?) && type == typeof(int))
                {
                    object retVal = null;
                    try
                    {
                        retVal = Convert.ToSingle(value);
                    }
                    catch { }
                    return (T)retVal;
                }
            }
            catch (KeyNotFoundException) { }

            if (defaultValue != null)
            {
                return defaultValue;
            }
            else if (_defaults.TryGetValue(key, out object v) && v.GetType().Equals(typeof(T)))
            {
                return (T)v;
            }
            else
            {
                if (StrictMode) throw new IncorrectTypeException();
                else return default;
            }
        }

        public override string GetString(string section, string key, string defaultValue) => GetValue(section, key, defaultValue);

        public override bool? GetBoolean(string section, string key, bool? defaultValue) => GetValue(section, key, defaultValue);

        public override int? GetInt(string section, string key, int? defaultValue) => GetValue(section, key, defaultValue);

        public override float? GetFloat(string section, string key, float? defaultValue) => GetValue(section, key, defaultValue);
        #endregion

        #region Setters
        public override void ClearKey(string section, string key, bool autoSave)
        {
            if (section == null)
            {
                _data.Remove(key);
            }
            else
            {
                Dictionary<object, object> _table;
                try
                {
                    _table = (Dictionary<object, object>)_data[section];
                }
                catch (KeyNotFoundException)
                {
                    _table = new Dictionary<object, object>();
                }

                _table.Remove(key);
                _data[section] = _table;
            }

            if (autoSave) Save();
        }

        private void SetValue<T>(string section, string key, T value, bool autoSave)
        {
            if (section == null)
            {
                _data[key] = value;
            }
            else
            {
                Dictionary<object, object> _table;
                try
                {
                    _table = (Dictionary<object, object>)_data[section];
                }
                catch (KeyNotFoundException)
                {
                    _table = new Dictionary<object, object>();
                }

                _table[key] = value;
                _data[section] = _table;
            }

            if (autoSave) Save();
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
                object value = entry.Value;

                try
                {
                    _ = _data[key];
                    continue;
                }
                catch (KeyNotFoundException)
                {
                    _data[entry.Key] = value;
                }
            }

            Save();
        }
        #endregion
    }
}
