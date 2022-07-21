// <copyright file="IDBContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Interface
{
    using LibraryNetwork.Models;

    /// <summary>
    /// Контекст БД.
    /// </summary>
    public interface IDBContext
    {
        /// <summary>
        /// Получить все данные из контекста БД.
        /// </summary>
        /// <returns>Массив данных.</returns>
        public LibraryItem[] GetAllDate();
    }
}
