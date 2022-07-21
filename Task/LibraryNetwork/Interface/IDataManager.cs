// <copyright file="IDataManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Менеджер данных для загрузки и сохранения данных из разных типов файлов.
    /// </summary>
    /// <typeparam name="T">LibraryItem.</typeparam>
    public interface IDataManager<T>
    {
        /// <summary>
        /// Десерилизовать данные из файла.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Список объектов.</returns>
        IEnumerable<T> GetData(string path); // помните про yield return

        /// <summary>
        /// Серелизовать данные в файл.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="path">Путь к файлу.</param>
        void SaveData(IEnumerable<T> data, string path);
    }
}
