using System;

namespace LibConf.Providers
{
    /// <summary>
    /// Conf provider.
    /// Contains all methods to interact with a config store.
    /// </summary>
    public interface IConfigProvider
    {
        #region Metadata
        /// <summary>
        /// Config Name.
        /// Used in file paths.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Full filepath to config store.
        /// </summary>
        string Filepath { get; }
        #endregion

        #region IO
        /// <summary>
        /// Flush store to disk.
        /// </summary>
        void Save();
        /// <summary>
        /// Read config from disk.
        /// Merges existing config in.
        /// </summary>
        void Load();
        /// <summary>
        /// Fired whenever config changes on disk.
        /// </summary>
        Action FileModified { get; set; }
        #endregion

        #region Getters
        /// <summary>
        /// Get a string from the config store.
        /// Defaults to null.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <returns></returns>
        string GetString(string key);
        /// <summary>
        /// Get a string from the config store.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="defaultValue">Returned if key does not exist</param>
        /// <returns></returns>
        string GetString(string key, string defaultValue);
        /// <summary>
        /// Get a string from the config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="defaultValue">Returned if key does not exist</param>
        /// <returns></returns>
        string GetString(string section, string key, string defaultValue);

        /// <summary>
        /// Get a boolean from the config store.
        /// Defaults to null.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <returns></returns>
        bool? GetBoolean(string key);
        /// <summary>
        /// Get a boolean from the config store.
        /// Defaults to null.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <returns></returns>
        bool? GetBoolean(string section, string key);
        /// <summary>
        /// Get a boolean from the config store.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="defaultValue">Returned if key does not exist</param>
        /// <returns></returns>
        bool? GetBoolean(string key, bool? defaultValue);
        /// <summary>
        /// Get a boolean from the config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="defaultValue">Returned if key does not exist</param>
        /// <returns></returns>
        bool? GetBoolean(string section, string key, bool? defaultValue);

        /// <summary>
        /// Get an integer from the config store.
        /// Defaults to null.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <returns></returns>
        int? GetInt(string key);
        /// <summary>
        /// Get an integer from the config store.
        /// Defaults to null.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <returns></returns>
        int? GetInt(string section, string key);
        /// <summary>
        /// Get an integer from the config store.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="defaultValue">Returned if key does not exist</param>
        /// <returns></returns>
        int? GetInt(string key, int? defaultValue);
        /// <summary>
        /// Get an integer from the config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="defaultValue">Returned if key does not exist</param>
        /// <returns></returns>
        int? GetInt(string section, string key, int? defaultValue);

        /// <summary>
        /// Get a float from the config store.
        /// Defaults to null.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <returns></returns>
        float? GetFloat(string key);
        /// <summary>
        /// Get a float from the config store.
        /// Defaults to null.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <returns></returns>
        float? GetFloat(string section, string key);
        /// <summary>
        /// Get a float from the config store.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="defaultValue">Returned if key does not exist</param>
        /// <returns></returns>
        float? GetFloat(string key, float? defaultValue);
        /// <summary>
        /// Get a float from the config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="defaultValue">Returned if key does not exist</param>
        /// <returns></returns>
        float? GetFloat(string section, string key, float? defaultValue);
        #endregion

        #region Setters
        /// <summary>
        /// Clear a key from config store.
        /// Autosaves after setting.
        /// </summary>
        /// <param name="key">Config Key</param>
        void ClearKey(string key);
        /// <summary>
        /// Clear a key from config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Save after clearing</param>
        void ClearKey(string section, string key);
        /// <summary>
        /// Clear a key from config store.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="autoSave">Save after clearing</param>
        void ClearKey(string key, bool autoSave);
        /// <summary>
        /// Clear a key from config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="autoSave">Save after clearing</param>
        void ClearKey(string section, string key, bool autoSave);

        /// <summary>
        /// Set a string in the config store.
        /// Autosaves after setting.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void SetString(string key, string value);
        /// <summary>
        /// Set a string in the config store.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        /// <param name="autoSave">Save after clearing</param>
        void SetString(string key, string value, bool autoSave);
        /// <summary>
        /// Set a string in the config store.
        /// Autosaves after setting.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void SetString(string section, string key, string value);
        /// <summary>
        /// Set a string in the config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        /// <param name="autoSave">Save after clearing</param>
        void SetString(string section, string key, string value, bool autoSave);

        /// <summary>
        /// Set a boolean in the config store.
        /// Autosaves after setting.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void SetBoolean(string key, bool value);
        /// <summary>
        /// Set a boolean in the config store.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        /// <param name="autoSave">Save after clearing</param>
        void SetBoolean(string key, bool value, bool autoSave);
        /// <summary>
        /// Set a boolean in the config store.
        /// Autosaves after setting.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void SetBoolean(string section, string key, bool value);
        /// <summary>
        /// Set a boolean in the config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        /// <param name="autoSave">Save after clearing</param>
        void SetBoolean(string section, string key, bool value, bool autoSave);

        /// <summary>
        /// Set an int in the config store.
        /// Autosaves after setting.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void SetInt(string key, int value);
        /// <summary>
        /// Set an int in the config store.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        /// <param name="autoSave">Save after clearing</param>
        void SetInt(string key, int value, bool autoSave);
        /// <summary>
        /// Set an int in the config store.
        /// Autosaves after setting.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void SetInt(string section, string key, int value);
        /// <summary>
        /// Set an int in the config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        /// <param name="autoSave">Save after clearing</param>
        void SetInt(string section, string key, int value, bool autoSave);

        /// <summary>
        /// Set a float in the config store.
        /// Autosaves after setting.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void SetFloat(string key, float value);
        /// <summary>
        /// Set a float in the config store.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        /// <param name="autoSave">Save after clearing</param>
        void SetFloat(string key, float value, bool autoSave);
        /// <summary>
        /// Set a float in the config store.
        /// Autosaves after setting.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void SetFloat(string section, string key, float value);
        /// <summary>
        /// Set a float in the config store.
        /// </summary>
        /// <param name="section">Config Section</param>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        /// <param name="autoSave">Save after clearing</param>
        void SetFloat(string section, string key, float value, bool autoSave);
        #endregion

        #region Defaults
        /// <summary>
        /// Save default values to disk.
        /// </summary>
        void SaveDefaults();
        /// <summary>
        /// Set a default string
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void AddDefault(string key, string value);
        /// <summary>
        /// Set a default boolean
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void AddDefault(string key, bool value);
        /// <summary>
        /// Set a default integer
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void AddDefault(string key, int value);
        /// <summary>
        /// Set a default float.
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value"></param>
        void AddDefault(string key, float value);
        #endregion
    }
}
