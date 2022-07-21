// <copyright file="DataSerilizatorJson.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.SerilizationData
{
    using System.Collections.Generic;
    using System.IO;
    using LibraryNetwork.Interface;
    using Newtonsoft.Json;

    /// <summary>
    /// Серилизация данных в Json.
    /// </summary>
    /// <typeparam name="T">Libraryitem.</typeparam>
    public class DataSerilizatorJson<T> : IDataManager<T>
    {
        private JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };

        /// <summary>
        /// Десерилизовать данные из файла Json.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Листо объектов..</returns>
        public IEnumerable<T> GetData(string path)
        {
            var set = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            using StreamReader sr = new StreamReader(path);
            var a = JsonConvert.DeserializeObject<IEnumerable<T>>(sr.ReadToEnd(), set);
            return a;
        }

        /// <summary>
        /// Серилизовать данные в файл Json.
        /// </summary>
        /// <param name="data">источник данных.</param>
        /// <param name="path">Путь к файлу.</param>
        public void SaveData(IEnumerable<T> data, string path)
        {
            var json = JsonConvert.SerializeObject(data, this.settings);

            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.WriteRawValue(json);
            }
        }
    }
}
