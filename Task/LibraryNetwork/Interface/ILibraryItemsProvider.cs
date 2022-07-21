// <copyright file="ILibraryItemsProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Interface
{
    using System;
    using System.Collections.Generic;
    using LibraryNetwork.Enums;
    using LibraryNetwork.Models;

    /// <summary>
    /// Работа с бд.
    /// </summary>
    public interface ILibraryItemsProvider
    {
        /// <summary>
        /// Добавить предмет в библиотеку.
        /// </summary>
        /// <param name="libraryItem">Предмет.</param>
        void Add(LibraryItem libraryItem);

        /// <summary>
        /// Удалить из библиотеки предмет.
        /// </summary>
        /// <param name="libraryItem">Предмет.</param>
        void Remove(LibraryItem libraryItem);

        /// <summary>
        /// Получить массив всех предметов библиотеки.
        /// </summary>
        /// <returns>Массив предметов.</returns>
        List<LibraryItem> GetAll();

        /// <summary>
        /// Поиск по части названия среди предметов в библиотеке.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <returns>Список предметов удовлетворяющих требованию.</returns>
        ICollection<LibraryItem> Search(string name);

        /// <summary>
        /// Поиск по имени автора среди предметов библиотеки.
        /// </summary>
        /// <param name="author">Имя автора.</param>
        /// <returns>Список предметов удовлетворяющих требованию.</returns>
        ICollection<LibraryItem> SearchAuthor(string author);

        /// <summary>
        /// Сортировать по году публикации по возрастанию года.
        /// </summary>
        /// <returns>Словарь отсортированный сгруппированный и отсортированный по году публикации по возрастанию.</returns>
        IDictionary<int, List<LibraryItem>> SortUpYearPublishing();

        /// <summary>
        /// Сортировать по году публикации по убыванию.
        /// </summary>
        /// <returns>Словарь отсортированный сгруппированный и отсортированный по году публикации по убыванию.</returns>
        IDictionary<int, List<LibraryItem>> SortDownYearPublishing();

        /// <summary>
        /// Групиировать по году публикации.
        /// </summary>
        /// <returns>Сгруппированный список.</returns>
        IDictionary<int, List<LibraryItem>> GroupByYearPublishing();

        /// <summary>
        /// Вывод всех книг, название издательства которых начинается с заданного набора символов, с
        /// группировкой по издательству.
        /// </summary>
        /// <param name="publishingName">Название издательства.</param>
        /// <returns>Список всех книг которые были опубликованы издательствами, в которые содержится заданная строк, сгруппированные по издательству.</returns>
        IDictionary<string, List<Book>> PrintBooksPublishingName(string publishingName);

        /// <summary>
        /// Редактирование элемента.
        /// </summary>
        /// <param name="id">Id редактируемого элемента.</param>
        /// <param name="libraryItem">объект с отредактированными полями.</param>
        void Edit(int id, LibraryItem libraryItem);

        /// <summary>
        /// Метод поиска по предикату.
        /// </summary>
        /// <param name="list">Список объектов.</param>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список объектов библиотеки.</returns>
        List<LibraryItem> Search(List<LibraryItem> list, Func<LibraryItem, bool> filter);

        /// <summary>
        /// Метод сортировки по возрастанию по предикату.
        /// </summary>
        /// <typeparam name="T">тип объектов списка.</typeparam>
        /// <typeparam name="TKey">ключи сортировки.</typeparam>
        /// <param name="list">Список который нужно отсортировать.</param>
        /// <param name="filter">Критерий сортировки.</param>
        /// <param name="type">Тип сортировки, по убыванию или по возрастанию.</param>
        /// <returns>Отсортированный список.</returns>
        List<T> Sort<T, TKey>(List<T> list, Func<T, TKey> filter, SortType type);

        /// <summary>
        /// Метод загрузки данных из файла.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="validator">Валидатор.</param>
        /// <param name="mode">Тип загрузки.</param>
        void Load(List<LibraryItem> data, IValidation validator, DownloadMode mode);
    }
}
