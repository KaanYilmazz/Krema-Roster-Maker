using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Krema_Roster_Maker.Helpers
{
    public static class JsonSerializerHelper
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true, // Pretty-print the JSON output
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Use camelCase for JSON properties
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) // Serialize enums as strings
            }
        };

        /// <summary>
        /// Serializes an object to JSON.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="data">The object to serialize.</param>
        /// <returns>The JSON string representation of the object.</returns>
        public static string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, _options);
        }

        /// <summary>
        /// Deserializes a JSON string to an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _options);
        }
        public static void WriteToFile<T>(T data, string filePath)
        {
            string json = Serialize(data);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Reads a JSON string from a file and deserializes it into an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="filePath">The path of the file to read from.</param>
        /// <returns>The deserialized object.</returns>
        public static T ReadFromFile<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return Deserialize<T>(json);
        }
    }

}
