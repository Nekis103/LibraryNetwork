// <copyright file="CatalogSerivice.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using LibraryNetwork.Enums;
    using LibraryNetwork.Interface;
    using LibraryNetwork.Models.Providers;
    using LibraryNetwork.Models.Validators;
    using LibraryNetwork.SerilizationData;

    /// <summary>
    /// Каталог всех вещей в библиотеке.
    /// </summary>
    public class CatalogSerivice
    {
        private ILibraryItemsProvider libraryItemsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogSerivice"/> class.
        /// </summary>
        /// <param name="libraryItemsProvider">Провайдер данных.</param>
        public CatalogSerivice(ILibraryItemsProvider libraryItemsProvider)
        {
            this.libraryItemsProvider = libraryItemsProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogSerivice"/> class.
        /// </summary>
        public CatalogSerivice()
        {
            this.libraryItemsProvider = new LibraryItemsArrayProvider();
        }

        /// <summary>
        /// Добавить в список вещей библиотеки новую вещь.
        /// </summary>
        /// <param name="libraryItem">Вещь.</param>
        public void Add(LibraryItem libraryItem)
        {
            this.libraryItemsProvider.Add(libraryItem);
        }

        /// <summary>
        /// Удалить вещь из списка библиотеки. Поиск будет происзводится по полному названию и первая вещь в списке с таким названием будет удалена.
        /// </summary>
        /// <param name="libraryItem">Вещь.</param>
        public void Remove(LibraryItem libraryItem)
        {
            this.libraryItemsProvider.Remove(libraryItem);
        }

        /// <summary>
        /// Вывод всех вещей находящихся в библиотеке.
        /// </summary>
        /// <returns>Список всех вещей.</returns>
        public List<LibraryItem> GetAll()
        {
            return this.libraryItemsProvider.GetAll();
        }

        /// <summary>
        /// Осуществляет поиск по части названия.
        /// </summary>
        /// <param name="name">Название объекта.</param>
        /// <returns>Список всех вещей с похожим именем.</returns>
        public ICollection<LibraryItem> Search(string name)
        {
            return this.libraryItemsProvider.Search(name);
        }

        /// <summary>
        /// Сортировка объектов по году издания от меньшего к большему.
        /// </summary>
        /// <returns>Сгруппированный по году издания отсортированный по возрастанию словарь.</returns>
        public IDictionary<int, List<LibraryItem>> SortUpYearPublishing()
        {
            return this.libraryItemsProvider.SortUpYearPublishing();
        }

        /// <summary>
        /// Сортировка объектов по году издания от большего к меньшему.
        /// </summary>
        /// <returns>Сгруппированный по году издания отсортированный по убыванию словарь.</returns>
        public IDictionary<int, List<LibraryItem>> SortDownYearPublishing()
        {
            return this.libraryItemsProvider.SortDownYearPublishing();
        }

        /// <summary>
        /// Поиск среди книг по автору.
        /// </summary>
        /// <param name="author">Автор.</param>
        /// <returns>Список всех вещей с заданным автором.</returns>
        public ICollection<LibraryItem> SearchAuthor(string author)
        {
            return this.libraryItemsProvider.SearchAuthor(author);
        }

        /// <summary>
        /// Вывод всех книг, название издательства которых начинается с заданного набора символов, с
        /// группировкой по издательству.
        /// </summary>
        /// <param name="publishingName">Название издательства.</param>
        /// <returns>Список всех книг которые были опубликованы издательствами, в которые содержится заданная строк, сгруппированные по издательству.</returns>
        public IDictionary<string, List<Book>> PrintBooksPublishingName(string publishingName)
        {
            return this.libraryItemsProvider.PrintBooksPublishingName(publishingName);
        }

        /// <summary>
        /// Группировка всех объектов библиотеки по году издательства.
        /// </summary>
        /// <returns>Список всех вещей сгруппированных по году издательства.</returns>
        public IDictionary<int, List<LibraryItem>> GroupByYearPublishing()
        {
            return this.libraryItemsProvider.GroupByYearPublishing();
        }

        /// <summary>
        /// Проверка валидации объекта.
        /// </summary>
        /// <param name="validation">Валидатор.</param>
        /// <param name="libraryItem">Объект.</param>
        /// <returns>Валидность объекта.</returns>
        public ResultValidation CheckValidation(IValidation validation, LibraryItem libraryItem)
        {
            return validation.Check(libraryItem);
        }

        /// <summary>
        /// Редактирование объекта библиотеки.
        /// </summary>
        /// <param name="id">Id редактируемого объекта.</param>
        /// <param name="libraryItem">Объект с новыми значениями для полей редактируемого объекта.</param>
        public void Edit(int id, LibraryItem libraryItem)
        {
            this.libraryItemsProvider.Edit(id, libraryItem);
        }

        /// <summary>
        /// Метод создания отчета.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="stategy">Стратеги создания отчета.</param>
        ///
        //TODO+ [Anna] По заданию метод должен принимать на вход 2 параметра, а не 3
        public void CreateReport(string path, IStrategyCreateReport stategy)
        {
            var dirictory = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            if (!Directory.Exists(dirictory))
            {
                Directory.CreateDirectory(dirictory);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            //else
            //{
            //    File.Create(Path.GetFileName(path));
            //}

            stategy.CreateReport(path);
        }

        /// <summary>
        /// Метод поиска по предикату.
        /// </summary>
        /// <param name="list">Список объектов.</param>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список объектов библиотеки.</returns>
        //TODO+ [Anna] Почему в качестве поиска T, если у нас есть вполне конкретный LibraryItem?
        public List<LibraryItem> Search(List<LibraryItem> list, Func<LibraryItem, bool> filter)
        {
            return this.libraryItemsProvider.Search(list, filter);
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
        //TODO+ [Anna] Два метода ниже можно объединить в один, передавая дополнительным параметром направление сортировки
        public List<T> Sort<T, TKey>(List<T> list, Func<T, TKey> filter, SortType type)
        {
            return this.libraryItemsProvider.Sort(list, filter, type);
        }

        /// <summary>
        /// Загрузить данные из файла.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="validator">Валидатор.</param>
        /// <param name="mode">Тип загрузки.</param>
        public void Load(string path, IValidation validator, DownloadMode mode)
        {
            var factory = new FactoryDateSerilizator<LibraryItem>();
            var serilizator = factory.GetSerilizator(path);
            var a = serilizator.GetData(path).ToList();
            this.libraryItemsProvider.Load(a, validator, mode);
        }

        /// <summary>
        /// Сохранить данные в файл.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        public void Save(string path)
        {
            var factory = new FactoryDateSerilizator<LibraryItem>();
            var serilizator = factory.GetSerilizator(path);
            serilizator.SaveData(this.libraryItemsProvider.GetAll(), path);
        }
    }
}
