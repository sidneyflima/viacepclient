using System;
using System.Text.Json;

namespace ViaCepClient.Converter
{
    /// <summary>
    /// PlainJsonObject represents a reader wrapper 
    /// for plain json reading
    /// </summary>
    public class PlainJsonObject : IPlainJsonObject
    {
        private JsonDocument _jsonDocument;

        /// <summary>
        /// PlainJsonObject represents a reader wrapper 
        /// for plain json reading
        /// </summary>
        public PlainJsonObject(JsonDocument jsonDocument)
        {
            _jsonDocument = jsonDocument;
        }

        /// <summary>
        /// Get json value by key. If key not exists, return empty string
        /// </summary>
        public string this[string key] 
        { 
            get
            {
                JsonElement root = _jsonDocument.RootElement;
                
                if(!root.TryGetProperty(key, out JsonElement property))
                    return null;

                return property.GetString();
            }
        }
    }
}