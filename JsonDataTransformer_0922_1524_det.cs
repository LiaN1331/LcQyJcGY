// 代码生成时间: 2025-09-22 15:24:57
using System;
using System.Text.Json;

namespace JsonDataTransformation
{
    /// <summary>
    /// A utility class to transform JSON data.
    /// </summary>
    public class JsonDataTransformer
    {
        /// <summary>
        /// Converts a JSON string to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to convert to.</typeparam>
        /// <param name="json">The JSON string to convert.</param>
        /// <returns>An object of type T.</returns>
        /// <exception cref="JsonException">Thrown when the JSON is invalid or cannot be deserialized.</exception>
        public static T ConvertFromJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("The JSON string cannot be null or empty.", nameof(json));
            }

            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Failed to deserialize the JSON string.", ex);
            }
        }

        /// <summary>
        /// Converts an object to a JSON string.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>A JSON string representation of the object.</returns>
        /// <exception cref="JsonException">Thrown when the object cannot be serialized.</exception>
        public static string ConvertToJson(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "The object to serialize cannot be null.");
            }

            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Failed to serialize the object to JSON.", ex);
            }
        }
    }
}