// <copyright file="Enums.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Enums
{
    /// <summary>
    /// Тип сортировки.
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// По возрастанию.
        /// </summary>
        Ascending = 0,

        /// <summary>
        /// По убыванию.
        /// </summary>
        Descending,
    }

    /// <summary>
    /// Тип серилизатора.
    /// </summary>
    public enum SerialazeType
    {
        /// <summary>
        /// XML.
        /// </summary>
        XML = 0,

        /// <summary>
        /// Json.
        /// </summary>
        Json,
    }

    /// <summary>
    /// Тип загрузки из разных типов файлов.
    /// </summary>
    public enum DownloadMode
    {
        /// <summary>
        /// XML.
        /// </summary>
        AllOrNothing = 0,

        /// <summary>
        /// Игнорировать.
        /// </summary>
        All,
    }

}
