// <copyright file="LibraryItemsArrayProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Providers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using LibraryNetwork.Enums;
    using LibraryNetwork.Interface;
    using LibraryNetwork.Models.Handlers;
    using NLog;

    /// <summary>
    /// Провайдер для работы с массивами.
    /// </summary>
    public class LibraryItemsArrayProvider : ILibraryItemsProvider
    {
        private List<LibraryItem> listItems = new ();
        private int id = 0;
        private List<int> listFreeId = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryItemsArrayProvider"/> class.
        /// </summary>
        /// <param name="dBContext">Массив данных.</param>
        public LibraryItemsArrayProvider(IDBContext dBContext)
        {
            var a = new LibraryItemsArrayProvider();
            this.FundAdded += new AddHandler().Message;
            this.FundDeleting += new RemoveHandler().Message;
            var list = dBContext.GetAllDate();
            foreach (var item in list)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryItemsArrayProvider"/> class.
        /// </summary>
        /// <param name="dBContext">Массив данных.</param>
        /// <param name="confirming">Способ подтверждения удаления объекта из библиотеки.</param>
        public LibraryItemsArrayProvider(IDBContext dBContext, IConfirmingDeletion confirming)
        {
            this.FundAdded += new AddHandler().Message;
            this.FundDeleting += new CancelEventHandler(new RemoveCancelHandler(confirming).RemoveCancel);
            this.FundDeleting += new RemoveHandler().Message;

            var list = dBContext.GetAllDate();
            foreach (var item in list)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryItemsArrayProvider"/> class.
        /// </summary>
        public LibraryItemsArrayProvider()
        {
        }

        /// <summary>
        /// Делегат возвращающсй строку и принимающий объект библиотеки.
        /// </summary>
        /// <param name="libraryItem">Объект библиотеки.</param>
        /// <returns> Информация о добавленном объекте.</returns>
        public delegate string EventHandler(LibraryItem libraryItem);

        /// <summary>
        /// Событие говорящие о добавлении объекта в библиотку.
        /// </summary>
        public event EventHandler FundAdded;

        /// <summary>
        /// Событие говорящие о удалении объекта из библиотки.
        /// </summary>
        public event CancelEventHandler FundDeleting;

        /// <summary>
        /// Добавить предмет в библиотеку.
        /// </summary>
        /// <param name="libraryItem">Предмет.</param>
        public void Add(LibraryItem libraryItem)
        {
            var elementExists = this.listItems.Find(x => x.Equals(libraryItem));

            if (elementExists != null)
            {
                throw new Exception("Элемент уже существует в этой коллекции");
            }

            if (this.listFreeId.Count != 0)
            {
                libraryItem.Id = this.listFreeId[^1];
                this.listFreeId.Remove(this.listFreeId[^1]);
            }
            else if (this.listFreeId.Count == 0)
            {
                libraryItem.Id = this.id;
            }

            this.FundAdded?.Invoke(libraryItem);
            this.listItems.Add(libraryItem);
            this.id++;
        }

        /// <summary>
        /// Метод загрузки данных из файла.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="validator">Валидатор.</param>
        /// <param name="mode">Ти загрузки.</param>
        public void Load(List<LibraryItem> data, IValidation validator, DownloadMode mode)
        {
            if (mode == DownloadMode.AllOrNothing)
            {
                if (data.All(x => validator.Check(x).IsValid))
                {
                    this.ResetDate();
                    this.AddList(data);
                }
            }

            if (mode == DownloadMode.All)
            {
                var a = data.Select(x => validator.Check(x));

                Logger logger = LogManager.GetCurrentClassLogger();
                foreach (var item in data)
                {
                    var validObj = validator.Check(item);
                    if (validObj.IsValid == false)
                    {
                        var sb = new StringBuilder();
                        foreach (var error in validObj.ErrorList)
                        {
                            sb.Append(error + Environment.NewLine);
                        }

                        logger.Info("Была попытка добавления объекта типа {0}{1}", item.GetType(), Environment.NewLine + sb);
                    }
                }

                this.ResetDate();
                this.AddList(data);
            }
        }

        /// <summary>
        /// Получить массив всех предметов библиотеки.
        /// </summary>
        /// <returns>Массив предметов.</returns>
        public List<LibraryItem> GetAll()
        {
            return this.listItems;
        }

        /// <summary>
        /// Групиировать по году публикации.
        /// </summary>
        /// <returns>Сгруппированный список.</returns>
        public IDictionary<int, List<LibraryItem>> GroupByYearPublishing()
        {
            var dictionary = this.listItems.Select(x => x).GroupBy(
                x => x.YearPublishing,
                x => x,
                (k, list) => new
                {
                    Key = k,
                    list = list.ToList(),
                }).ToDictionary(x => x.Key, x => x.list);
            return dictionary;
        }

        /// <summary>
        /// Удалить из библиотеки предмет.
        /// </summary>
        /// <param name="libraryItem">Предмет.</param>
        public void Remove(LibraryItem libraryItem)
        {
            var elementExists = this.listItems.Find(x => x.Equals(libraryItem));

            if (elementExists == null)
            {
                throw new Exception("Элемент не существует в этой коллекции");
            }

            var e = new CancelEventArgs();

            // Какой вариант оставить с исключением или нет?
            //TODO [Anna] Вот оба варианта бы объединить и будет красиво:
            /*
             * 1. Вызываешь FundDeleting?.Invoke(libraryItem, e);
             * 2. Проверяешь отменили или нет, если нет - удаляешь
             */

            // try
            // {
            //    this.FundDeleting?.Invoke(libraryItem, e);
            // }
            // catch (Exception)
            // {
            //    string.Format("{0}", "Удаление было отменено");
            // }
            foreach (var item in this.FundDeleting.GetInvocationList())
            {
                if (e.Cancel == false)
                {
                    item.DynamicInvoke(libraryItem, e);
                }
            }
            //TODO [Anna] Для чего так сложно?) Выше описала вариант попроще
            if (e.Cancel == false)
            {
                this.listFreeId.Add(elementExists.Id);
                this.listItems.Remove(libraryItem);
                this.id--;
            }
        }

        /// <summary>
        /// Поиск по части названия среди предметов в библиотеке.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <returns>Список предметов удовлетворяющих требованию.</returns>
        public ICollection<LibraryItem> Search(string name)
        {
            var list = this.listItems.Select(x => x).Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            if (list.Count == 0)
            {
                return null;
            }

            return list;
        }

        /// <summary>
        /// Поиск по имени автора среди предметов библиотеки.
        /// </summary>
        /// <param name="author">Имя автора.</param>
        /// <returns>Список предметов удовлетворяющих требованию.</returns>
        public ICollection<LibraryItem> SearchAuthor(string author)
        {
            var list = this.listItems.FindAll(x => (x is Book book) && book.Authors.ToLower().Contains(author.ToLower()));
            if (list.Count == 0)
            {
                return null;
            }

            return list;
        }

        /// <summary>
        /// Вывод всех книг, название издательства которых начинается с заданного набора символов, с
        /// группировкой по издательству.
        /// </summary>
        /// <param name="publishingName">Название издательства.</param>
        /// <returns>Список всех книг которые были опубликованы издательствами, в которые содержится заданная строк, сгруппированные по издательству.</returns>
        public IDictionary<string, List<Book>> PrintBooksPublishingName(string publishingName)
        {
            if (publishingName == null)
            {
                return null;
            }

            var resultDictionary = this.listItems.Select(x => x as Book).
                Where(x => x != null && x.PublicationName.ToLower().StartsWith(publishingName.ToLower())).GroupBy(
                x => x.PublicationName,
                x => x,
                (key, g) => new
                {
                    Key = key,
                    list = g.ToList(),
                }).ToDictionary(x => x.Key, x => x.list);
            if (resultDictionary.Count == 0)
            {
                return null;
            }
            else
            {
                return resultDictionary;
            }
        }

        /// <summary>
        /// Сортировать по году публикации по убыванию.
        /// </summary>
        /// <returns>Словарь отсортированный сгруппированный и отсортированный по году публикации по убыванию.</returns>
        public IDictionary<int, List<LibraryItem>> SortDownYearPublishing()
        {
            return this.GroupByYearPublishing().OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Сортировать по году публикации по возрастанию года.
        /// </summary>
        /// <returns>Словарь отсортированный сгруппированный и отсортированный по году публикации по возрастанию.</returns>
        public IDictionary<int, List<LibraryItem>> SortUpYearPublishing()
        {
            return this.GroupByYearPublishing().OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Метод редактирования.
        /// </summary>
        /// <param name="itemIdFromCollection">id объекта из коллекции библиотеки.</param>
        /// <param name="libraryItem">"Объект с новыми данными.</param>
        public void Edit(int itemIdFromCollection, LibraryItem libraryItem)
        {
            var libraryItemFromCollection = this.listItems.Find(x => x.Id == itemIdFromCollection);
            if (libraryItemFromCollection == null)
            {
                throw new Exception("Такого элемента на надйено.");
            }

            if (libraryItem.GetType() != libraryItemFromCollection.GetType())
            {
                throw new Exception("Типы не соответствуют");
            }

            var type = libraryItem.GetType();
            var listFields = type.GetProperties();
            foreach (var item in listFields)
            {
                if (item.Name == nameof(libraryItem.Id))
                {
                    continue;
                }

                var newValueField = item.GetValue(libraryItem);

                // [Anna]+ Для чего используется данная проверка? Насколько вижу, код внутри идентичен
                // Ответ - для проверки типа, ведь если в переданном объекте некоторые свойства буду незаполнены,
                // то и присваивать эти свойства по новой на пустые значения не надо. думаю надо добавлять 3 параметр
                // в метод, например bool и в зависимости от параметра думать обнулять или нет
                // или вообще менять параметры метода
                //TODO+ [Anna] Почему не нужно перезаписывать на пустые значения? Это как раз случай, когда значение свойства изменилось. Если есть сомнения, отпишись в Скайпе, обсудим
                    // [Anna] Это лишнее условие, т.к.пользователь может захотеть занулить свойство
                    // убрал условие и теперь все свойства зануляются надо или не
                if (newValueField != item.GetValue(libraryItemFromCollection))
                {
                    item.SetValue(libraryItemFromCollection, newValueField);
                }
            }
        }

        /// <summary>
        /// Метод поиска по предикату.
        /// </summary>
        /// <param name="list">Список объектов.</param>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список объектов библиотеки.</returns>
        public List<LibraryItem> Search(List<LibraryItem> list, Func<LibraryItem, bool> filter)
        {
            //TODO+ [Anna] Всё-таки по заданию требуется не поиск, а сортировка, поэтому лучше использовать Where
            return list.Select(x => x).Where(filter).ToList();
        }

        /// <summary>
        /// Метод сортировки по возрастанию по предикату.
        /// </summary>
        /// <typeparam name="T">тип объектов списка.</typeparam>
        /// <typeparam name="TKey">ключи сортировки.</typeparam>
        /// <param name="list">Список который нужно отсортировать.</param>
        /// <param name="filter">Критерий сортировки.</param>
        /// <param name="type">Тип сортировки, по возрастанию или по убыванию.</param>
        /// <returns>Отсортированный список.</returns>
        public List<T> Sort<T, TKey>(List<T> list, Func<T, TKey> filter, SortType type)
        {
            if (type == SortType.Ascending)
            {
                return list.OrderBy(filter).ToList();
            }
            else
            {
                return list.OrderByDescending(filter).ToList();
            }
        }

        private void AddList(List<LibraryItem> list)
        {
            foreach (var item in list)
            {
                this.listItems.Add(item);
            }
        }

        private void ResetDate()
        {
            this.listItems.Clear();
            this.listFreeId.Clear();
            this.id = 0;
        }
    }
}
