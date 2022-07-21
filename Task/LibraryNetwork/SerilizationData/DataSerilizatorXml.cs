// <copyright file="DataSerilizatorXml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.SerilizationData
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using LibraryNetwork.Interface;

    /// <summary>
    /// Серилизация данных в XML.
    /// </summary>
    /// <typeparam name="T">Libraryitem.</typeparam>
    public class DataSerilizatorXml<T> : IDataManager<T>
    {
        /// <summary>
        /// Десерилизовать данные из файла XML.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Листо объектов..</returns>
        public IEnumerable<T> GetData(string path)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            var list = new List<T>();
            using (var stream = File.OpenRead(path))
            {
                var other = (List<T>)serializer.Deserialize(stream);
                list.Clear();
                list.AddRange(other);
            }

            return list;
        }

        /// <summary>
        /// Серилизовать данные в файл XML.
        /// </summary>
        /// <param name="data">источник данных.</param>
        /// <param name="path">Путь к файлу.</param>
        public void SaveData(IEnumerable<T> data, string path)
        {
            var serializer = new XmlSerializer(typeof(List<T>));

            using (StreamWriter sw = new StreamWriter(path))
            {
                serializer.Serialize(sw, data);
            }
        }
    }
}