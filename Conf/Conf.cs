using LibConf.Providers;
using LibConf.Utils;

namespace LibConf
{
    /// <summary>
    /// Static class to create Conf instances
    /// </summary>
    public static class Conf
    {
        /// <summary>
        /// Create a new config provider.
        /// </summary>
        /// <param name="type">Provider type</param>
        /// <param name="directory">Directory to store file in</param>
        /// <param name="name">Config name. Used in filename and as section headers (where appropriate)</param>
        /// <returns>Config provider</returns>
        public static IConfigProvider CreateConfig(ConfigType type, string directory, string name)
        {
            IOUtils.EnsureDirectory(directory);

            switch (type)
            {
                case ConfigType.Ini:
                    return new IniProvider(directory, name);
                case ConfigType.YAML:
                    return new YamlProvider(directory, name);
                case ConfigType.TOML:
                    return new TomlProvider(directory, name);
                default:
                    return new IniProvider(directory, name);
            }
        }

        /// <summary>
        /// Create a new .ini config provider.
        /// </summary>
        /// <param name="directory">Directory to store file in</param>
        /// <param name="name">Config name. Used in filename and as section headers (where appropriate)</param>
        /// <returns>Config provider</returns>
        public static IConfigProvider CreateConfig(string directory, string name)
        {
            return CreateConfig(ConfigType.Ini, directory, name);
        }
    }
}
