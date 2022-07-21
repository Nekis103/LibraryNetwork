// <copyright file="LibraryItemsDatabaseProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Providers
{
    using System;
    using System.Collections.Generic;
    using LibraryNetwork.Enums;
    using LibraryNetwork.Interface;

    /// <summary>
    /// Провайдер для работы с БД.
    /// </summary>
    public class LibraryItemsDatabaseProvider : ILibraryItemsProvider
    {
        /// <summary>
        /// Добавить предмет в библиотеку.
        /// </summary>
        /// <param name="libraryItem">Предмет.</param>
        public void Add(LibraryItem libraryItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Редактирование элемента библиотеки.
        /// </summary>
        /// <param name="id">Id редактируемого элемента.</param>
        /// <param name="libraryItem">Объект с полями с новым значением.</param>
        public void Edit(int id, LibraryItem libraryItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить массив всех предметов библиотеки.
        /// </summary>
        /// <returns>Массив предметов.</returns>
        public List<LibraryItem> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Групиировать по году публикации.
        /// </summary>
        /// <returns>Сгруппированный список.</returns>
        public IDictionary<int, List<LibraryItem>> GroupByYearPublishing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Вывод всех книг, название издательства которых начинается с заданного набора символов, с
        /// группировкой по издательству.
        /// </summary>
        /// <param name="publishingName">Название издательства.</param>
        /// <returns>Список всех книг которые были опубликованы издательствами, в которые содержится заданная строк, сгруппированные по издательству.</returns>
        public IDictionary<string, List<Book>> PrintBooksPublishingName(string publishingName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Удалить из библиотеки предмет.
        /// </summary>
        /// <param name="libraryItem">Предмет.</param>
        public void Remove(LibraryItem libraryItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Поиск по части названия среди предметов в библиотеке.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <returns>Список предметов удовлетворяющих требованию.</returns>
        public ICollection<LibraryItem> Search(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Поиск по имени автора среди предметов библиотеки.
        /// </summary>
        /// <param name="author">Имя автора.</param>
        /// <returns>Список предметов удовлетворяющих требованию.</returns>
        public ICollection<LibraryItem> SearchAuthor(string author)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Сортировать по году публикации по убыванию.
        /// </summary>
        /// <returns>Отсортированный и сгруппированный по году публикации по убыванию словарь.</returns>
        public IDictionary<int, List<LibraryItem>> SortDownYearPublishing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Сортировать по году публикации по возрастанию года.
        /// </summary>
        /// /// <returns>Отсортированный и сгруппированный по году публикации по возрастанию словарь.</returns>
        public IDictionary<int, List<LibraryItem>> SortUpYearPublishing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод поиска по предикату.
        /// </summary>
        /// <param name="list">Список объектов.</param>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список объектов библиотеки.</returns>
        public List<LibraryItem> Search(List<LibraryItem> list, Func<LibraryItem, bool> filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод сортировки по возрастанию по предикату.
        /// </summary>
        /// <typeparam name="T">тип объектов списка.</typeparam>
        /// <typeparam name="TKey">ключи сортировки.</typeparam>
        /// <param name="list">Список который нужно отсортировать.</param>
        /// <param name="filter">Критерий сортировки.</param>
        /// <param name="type">Тип сортировки, по убыванию или по возрастанию.</param>
        /// <returns>Отсортированный список.</returns>
        public List<T> Sort<T, TKey>(List<T> list, Func<T, TKey> filter, SortType type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод загрузки данных из файла.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="validator">Валидатор.</param>
        /// <param name="mode">Тип загрузки.</param>
        public void Load(List<LibraryItem> data, IValidation validator, DownloadMode mode)
        {
            throw new NotImplementedException();
        }
    }
}
