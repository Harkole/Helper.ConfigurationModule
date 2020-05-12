# Helper.ConfigurationModule

A simple, light weight helper for getting both the IConfigurationRoot and/or a section from a json settings file, e.g. `appsettings.json`

## Table of Contents

1. [Usage](#Usage)
2. [Implementation](#Implementation)
    1. [GetConfiguration()](#GetConfiguration)
    2. [GetConfigurationSection()](#GetConfigurationSection)
3. [Permissions](#Permissions)

## Usage

Easiest use is to let T do all the hard work for you, this requires you to be using the `appsettings.json` default file, located in the root of the application

Example settings in a `JSON` file:

```json
{
    "SectionName": {
        "key": "Value",
        "number": 42
    }
}
```

Example data model:

```csharp
public class SectionName
{
    public string Key { get; set; }
    public int Number { get; set; }
}
```

Get the data from the section...

```csharp
var options = Helper.ConfigurationModule.Configuration
    .GetConfiguration()
    .GetConfigurationSection<SectionName>();
```

Alternatively you can provide parameters to control the settings file and the section to load as shown below:

```csharp
var options = Helper.ConfigurationModule.Configuration
    .GetConfiguration(fileName: "mySettings.json", isOptional: true, reloadOnChanges: false)
    .GetConfigurationSection<Name>(sectionName: "SectionName");
```

## Implementation

### GetConfiguration()

Loads the specified file from the root of the application, this defaults to `appsettings.json` but can be any `JSON` formatted file. Defaults to requiring the configuration file to be present and will not reload the file if it is changed

| Parameter | Type | Description |
| -- | -- | -- |
| fileName | `string` | The name of the settings file to load, should only be the file name and the file must be present in the root folder |
| isOptional | `bool` | Flags if the application requires the file to be present [`false`] or if it should continue to run if it is missing [`true`]
| reloadOnChange | `bool` | Flags if the system should reload the configuration file if it is changed [`true`]|

### GetConfigurationSection() - Extension

Returns the data from the settings file for the specified section, if no values are provided, the method uses the name of T to try get the section details. As this is an extension, it is expected that you will chain this method to `GetConfiguration()`

| Parameter | Type | Description |
| -- | -- | -- |
| [this] root | IConfigurationRoot | A configuration root object that's been loaded from a settings file, provided by `GetConfiguration()` |
| sectionName | `string` | Optional parameter to specification of a different section to the `T` being requested

## Permissions

Feel free to use where ever!
