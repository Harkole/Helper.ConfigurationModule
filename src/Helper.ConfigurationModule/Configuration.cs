using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Helper.ConfigurationModule
{
    public static class Configuration
    {
        /// <summary>
        /// Builds the ConfigurationRoot object used to get information from a json file in to the application
        /// </summary>
        /// <param name="fileName">Optional - the name of the settings file to load</param>
        /// <param name="isOptional">Optional - flags if the system should error if the file is not present (true) or run anyway (false)</param>
        /// <param name="reloadOnChange">Optional - flags if the application should reload the file if a change is detected</param>
        /// <returns></returns>
        public static IConfigurationRoot GetConfiguration(string fileName = "appsettings.json", bool isOptional = false, bool reloadOnChange = false)
        {
            // Get the appsettings file path
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), fileName);

            // Build the configuration extension
            return new ConfigurationBuilder()
                .AddJsonFile(path, isOptional, reloadOnChange)
                .Build();
        }

        /// <summary>
        /// Returns the object populated from the section in the configuration root
        /// </summary>
        /// <typeparam name="T">The model to use for the section</typeparam>
        /// <param name="root">Configuration Root object</param>
        /// <param name="sectionName">Optional - The name of the section to map to the object <code>T</code></param>
        /// <returns>The mapped data from the section specified</returns>
        public static T GetConfigurationSection<T>(this IConfigurationRoot root, string sectionName = null)
        {
            // Use the object name if the sectionName was null
            sectionName ??= typeof(T).Name;
            return root
                .GetSection(sectionName)
                .Get<T>();
        }
    }
}
