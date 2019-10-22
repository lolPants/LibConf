# 📝 LibConf &nbsp;[![Actions Status](https://github.com/lolPants/LibConf/workflows/.NET%20Build/badge.svg)](https://github.com/lolPants/LibConf/actions)
_.NET framework configuration library that's somehow cooler than the rest_

## 🔧 Using LibConf
Everything you need is in the `LibConf.Conf` class.  
LibConf ships with XML documentation for all config provider methods.

```cs
using LibConf;

// Create config provider
Conf.CreateConfig(ConfigType.YAML, Directory.GetCurrentDirectory(), "name");

// Provider defaults to YAML if not specified
Conf.CreateConfig(Directory.GetCurrentDirectory(), "name");
```

### Loading and Saving
By default, all operations that modify config automatically save to disk. Loading is also performed automatically when a config provider is instantiated.

Reloading is not automatically performed when the file on disk is externally modified. However, the action `FileModified` is provided if you wish to do it manually.

Saving and loading can be performed manually with `.Load()` and `.Save()`

### Supported Types
LibConf types are all nullable, in the event the key you're requesting doesn't exist. Getter and Setter functions follow a consistent naming scheme; refer to the table below.

| Type | Getter | Setter |
| - | - | - |
| `string` | GetString | SetString |
| `bool?` | GetBoolean | SetBoolean |
| `int?` | GetInt | SetInt |
| `float?` | GetFloat | SetFloat |
