// ============================================================================
//    Author: Kenneth Perkins
//    Date:   May 10, 2021
//    Taken From: http://programmingnotes.org/
//    File:  Utils.cs
//    Description: Handles general utility functions
// ============================================================================
using System;

namespace Utils {
    public static class Json {
        /// <summary>
        /// Serializes the specified value to Json
        /// </summary>
        /// <param name="value">The value to serialize</param> 
        /// <param name="settings">The Newtonsoft.Json.JsonSerializerSettings used to serialize the object</param> 
        /// <returns>The value serialized to Json</returns>
        public static string Serialize(object value
                                    , Newtonsoft.Json.JsonSerializerSettings settings = null) {
            if (settings == null) {
                settings = GetDefaultSettings();
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(value, settings);
        }

        /// <summary>
        /// Deserializes the specified value from Json to Object T
        /// </summary>
        /// <param name="value">The value to deserialize</param> 
        /// <param name="settings">The Newtonsoft.Json.JsonSerializerSettings used to deserialize the object</param> 
        /// <returns>The value deserialized to Object T</returns>
        public static T Deserialize<T>(string value
                                        , Newtonsoft.Json.JsonSerializerSettings settings = null) {
            if (settings == null) {
                settings = GetDefaultSettings();
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, settings);
        }

        private static Newtonsoft.Json.JsonSerializerSettings GetDefaultSettings() {
            var settings = new Newtonsoft.Json.JsonSerializerSettings() {
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
            return settings;
        }
    }
}// http://programmingnotes.org/