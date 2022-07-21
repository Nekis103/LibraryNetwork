// <copyright file="LibraryNetworkTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetworkTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using LibraryNetwork.Enums;
    using LibraryNetwork.Interface;
    using LibraryNetwork.Models;
    using LibraryNetwork.Models.ConfirmingDeletion;
    using LibraryNetwork.Models.Providers;
    using LibraryNetwork.Models.Validation;
    using LibraryNetwork.Models.Validators;
    using LibraryNetwork.Reports;
    using LibraryNetwork.Services;
    using NUnit.Framework;

    /// <summary>
    /// Класс который тестирует класс библиотеки.
    /// </summary>
    public class LibraryNetworkTests
    {
        private DBContextArray dbContext;
        private LibraryItemsArrayProvider libraryItemsArrayProvider;
        private string pathJson = @"test\Json.jSoN";
        private string pathXML = @"test\Xml.XML";

        //TODO [Anna] В тестах очень много копипасты создания, например, книги. Можно вынести её в отдельное свойство и использовать, когда нужно

        /// <summary>
        /// Метот автоустановки значения перед каждым тестом.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.dbContext = new DBContextArray();
            this.libraryItemsArrayProvider = new LibraryItemsArrayProvider(this.dbContext);
        }

        /// <summary>
        /// Тестирование метода добавления в каталог.
        /// </summary>
        [Test]
        public void TestMethod_AddInCatalog_input_book_result_LibraryItemAddBook()
        {
            var book = new Book();
            var catalog = new CatalogSerivice();
            LibraryItem[] expectedList = { book };

            catalog.Add(book);

            Assert.AreEqual(expectedList, catalog.GetAll());
        }

        /// <summary>
        /// Тестирование метода удаления из каталога.
        /// </summary>
        [Test]
        public void TestMethod_RemoveFromCatalog_input_book_result_LibraryItemRemoveBook()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var expexted = catalog.GetAll().Count;
            var actual = catalog.GetAll();
            var removeItem = new LibraryItem();

            catalog.Add(removeItem);
            catalog.Remove(removeItem);

            Assert.AreEqual(expexted, actual.Count);
        }

        /// <summary>
        /// Тестирование метода получить все объекты из каталога библиотеки.
        /// </summary>
        [Test]
        public void TestMethod_GetAll_Return_LibraryItemsArray()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var actual = catalog.GetAll().Count;
            var expexted = catalog.GetAll().Count;
            Assert.AreEqual(expexted, actual);
        }

        /// <summary>
        /// Тестирование метода поиска , критерием выбрано имя, при условии что есть хотябы 1 объект.
        /// </summary>
        [Test]
        public void TestMethod_Search_Return_LibraryItemsArray_When_Not_Null()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var actual = catalog.Search("Во").Count;
            var expexted = 2;
            Assert.AreEqual(expexted, actual);
        }

        /// <summary>
        /// Тестирование метода поиска , критерием выбрано имя, при условии что нет не 1 объекта в списке.
        /// </summary>
        [Test]
        public void TestMethod_Search_Return_LibraryItemsArray_When_Null()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var actual = catalog.Search("nqwej");
            Assert.AreEqual(null, actual);
        }

        /// <summary>
        /// Тестирование метода поиска по автору, при условии что список не пустой.
        /// </summary>
        [Test]
        public void TestMethod_SearchAuthor_Return_LibraryItemsArray_When_Not_Null()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var actual = catalog.SearchAuthor("Тол").Count;
            var expexted = 2;
            Assert.AreEqual(expexted, actual);
        }

        /// <summary>
        /// Тестирование метода поиска по автору, при условии что список пустой.
        /// </summary>
        [Test]
        public void TestMethod_SearchAuthor_Return_LibraryItemsArray_When_Null()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var actual = catalog.SearchAuthor("nqwej");

            Assert.AreEqual(null, actual);
        }

        /// <summary>
        /// Тестирование метода сортировки по году побликации от меньшего к большему.
        /// </summary>
        [Test]
        public void TestMethod_SortUpYearPublishing_Return_LibraryItemsArray()
        {
            var catalog = new CatalogSerivice();
            var book1 = new Book() { YearPublishing = 2012 };
            var book2 = new Book() { YearPublishing = 2002 };

            catalog.Add(book1);
            catalog.Add(book2);
            var dictionary = catalog.SortUpYearPublishing();

            Assert.True(dictionary.Keys.First() < dictionary.Keys.Skip(1).First());
        }

        /// <summary>
        /// Тестирование метода сортировки по году побликации от большего к меньшему.
        /// </summary>
        [Test]
        public void TestMethod_SortDownYearPublishing_Return_LibraryItemsArray()
        {
            var catalog = new CatalogSerivice();
            var book1 = new Book() { YearPublishing = 2002 };
            var book2 = new Book() { YearPublishing = 2012 };

            catalog.Add(book1);
            catalog.Add(book2);
            var dictionary = catalog.SortDownYearPublishing();

            Assert.True(dictionary.Keys.First() > dictionary.Keys.Skip(1).First());
        }

        /// <summary>
        /// Тестирование метода вывод всех книг сгруппированных по названию издательства.
        /// </summary>
        [Test]
        public void TestMethod_PrintBooksPublishingName_Return_LibraryItemsArray()
        {
            var catalog = new CatalogSerivice();
            var book1 = new Book() { PublicationName = "Group 1" };
            var book2 = new Book() { PublicationName = "Group 2" };
            var book3 = new Book() { PublicationName = "Group 2" };
            var patent = new Patent() { };
            var expectedCount = 3;
            var expectedCountGroup = 2;

            catalog.Add(book1);
            catalog.Add(book2);
            catalog.Add(book3);
            catalog.Add(patent);
            var list = catalog.PrintBooksPublishingName("Group");
            var actualCount = list.SelectMany(x => x.Value).Count();
            var actualCountGroup = list.Keys.Count();
            Assert.True(expectedCount == actualCount && expectedCountGroup == actualCountGroup);
        }

        /// <summary>
        /// Тестирование метода вывод всех книг сгруппированных по названию издательства переданный список пуст.
        /// </summary>
        [Test]
        public void TestMethod_PrintBooksPublishingName_Return_LibraryItemsArray_Null()
        {
            var catalog = new CatalogSerivice();
            var book1 = new Book() { PublicationName = "Group 1" };
            var book2 = new Book() { PublicationName = "Group 2" };
            var book3 = new Book() { PublicationName = "Group 2" };

            catalog.Add(book1);
            catalog.Add(book2);
            catalog.Add(book3);

            var actual = catalog.PrintBooksPublishingName("erq");

            Assert.AreEqual(null, actual);
        }

        /// <summary>
        /// Тестирование метода, группировка всех объектов библиотеки по году издательства.
        /// </summary>
        [Test]
        public void TestMethod_GroupByYearPublishing_Return_LibraryItemsArray()
        {
            var catalog = new CatalogSerivice();
            var book1 = new Book() { YearPublishing = 2012 };
            var book2 = new Book() { YearPublishing = 2002 };
            var book3 = new Newspaper() { YearPublishing = 2002 };
            var book4 = new Patent() { YearPublishing = 1992 };
            var expectedLenght = 4;
            var expectedCountGroup = 3;

            catalog.Add(book1);
            catalog.Add(book2);
            catalog.Add(book3);
            catalog.Add(book4);
            var list = catalog.GroupByYearPublishing();
            var actualCountGroup = list.Keys.Count;
            var actualCount = list.SelectMany(x => x.Value).Count();
            Assert.True((expectedLenght == actualCount) && (expectedCountGroup == actualCountGroup));
        }

        /// <summary>
        /// Тестирование метода, группировка всех объектов библиотеки по году издательства, когда список пуст.
        /// </summary>
        [Test]
        public void TestMethod_PrintBooksPublishingName_input_IsIt_Null_Return_Null()
        {
            var catalog = new CatalogSerivice();

            var actual = catalog.PrintBooksPublishingName(null);

            Assert.AreEqual(null, actual);
        }

        /// <summary>
        /// Тестирование метода, группировка всех объектов библиотеки по году издательства.
        /// </summary>
        [Test]
        public void TestUniqueId()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var book = new Book() { Name = "Good Test", YearPublishing = 2012 };
            var book2 = new Book() { Name = "Good Test2", YearPublishing = 2012 };
            var patent = new Patent() { DatePublishing = DateTime.Now.AddYears(-30) };
            var newspaper = new Newspaper() { YearPublishing = 2002 };
            var expected1 = 15;
            var expected2 = 18;

            catalog.Add(book);
            catalog.Add(patent);
            catalog.Add(newspaper);
            catalog.SortDownYearPublishing();
            catalog.Remove(book);
            catalog.Add(book);
            var list = catalog.GetAll();
            var actual1 = list[^1].Id;
            catalog.Add(book2);
            list = catalog.GetAll();
            var actual2 = list[^1].Id;

            Assert.True(actual1 == expected1 && expected2 == actual2);
        }

        /// <summary>
        /// Проверка валидации пустого объекта библиотеки.
        /// </summary>
        [Test]
        public void TestCheckValidationEmptyLibraryItems()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var libraryItemEmpty = new LibraryItem() { };
            var countErrors = 1;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), libraryItemEmpty);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), libraryItemEmpty);
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации объекта библиотеки с правильно заполненными свойствами.
        /// </summary>
        [Test]
        public void TestCheckValidationLibraryItemsTrueObject()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var libraryItemEmpty = new LibraryItem() { Id = 4, Name = "Имя", Note = "Описание" };
            var countErrors = 0;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), libraryItemEmpty);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), libraryItemEmpty);
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации объекта библиотеки с неправильно заполненными свойствами.
        /// </summary>
        [Test]
        public void TestCheckValidationLibraryItemsFalseObject()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var libraryItemEmpty = new LibraryItem() { Id = 4, Note = "Описание Описание Описание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание ОписаниеОписание Описание " };
            var countErrors = 2;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), libraryItemEmpty);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), libraryItemEmpty);
            //TODO+ [Anna] Операторы &&, || и подобные всегда оставляем на предыдущей строке
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count && 
                resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации пустого объекта книги.
        /// </summary>
        [Test]
        public void TestCheckValidationEmptyBook()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var bookEmpty = new Book() { };
            var countErrors = 6;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), bookEmpty);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), bookEmpty);

            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации книги с правильно заполненными свойствами.
        /// </summary>
        [Test]
        public void TestCheckValidationBookTrueObject()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var book = new Book() { Name = "Good Test", YearPublishing = 1992, Note = " qwef", Authors = "qwef", PlacePublication = " q", PublicationName = "r ", NumberPages = 2, Cost = 4, Count = 2 };
            var countErrors = 0;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), book);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), book);
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации книги с неправильно заполненными свойствами.
        /// </summary>
        [Test]
        public void TestCheckValidationBookFalseObject()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var book = new Book() { YearPublishing = 10, Cost = -123, NumberPages = -123 };
            var countErrors = 7;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), book);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), book);
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации пустого объекта патента.
        /// </summary>
        [Test]
        public void TestCheckValidationEmptyPatent()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var patentEmpty = new Patent() { };
            var countErrors = 6;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), patentEmpty);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), patentEmpty);

            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации патента, с правильно заполненными свойствами.
        /// </summary>
        [Test]
        public void TestCheckValidationPatentTrueObject()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var patent = new Patent() { Inventors = "Илон Маск", Id = 3, Country = "США", Name = "Улучшенная солнечная панель", Note = "Возобновляемый источник энергии.", NumberPages = 2580, RegistrationNumber = "891282312375", DatePublishing = DateTime.Now.AddYears(-30), ApplicationDate = DateTime.Now.AddYears(-30), Cost = 10 };
            var countErrors = 0;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), patent);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), patent);
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации патента, с неправильно заполненными свойствами.
        /// </summary>
        [Test]
        public void TestCheckValidationPatentFalseObject()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var patent = new Patent() { DatePublishing = DateTime.Now.AddYears(-2000), Cost = -123, NumberPages = -123 };
            var countErrors = 7;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), patent);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), patent);
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации пустого объекта газеты.
        /// </summary>
        [Test]
        public void TestCheckValidationEmptyNewspaper()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var newspaperEmpty = new Newspaper() { };
            var countErrors = 5;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), newspaperEmpty);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), newspaperEmpty);

            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Прверка валидации газеты с правильными параметрами.
        /// </summary>
        [Test]
        public void TestCheckValidationNewspaperTrueObject()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var newspaper = new Newspaper() { ISSN = "975-3-15-141413-4", Id = 3, Number = 1, Name = "Комсомольская правда", Note = "Самая популярная газета.", NumberPages = 60, PlacePublication = "Россия - Москва", Date = DateTime.Now.AddYears(-1).AddMonths(-1), YearPublishing = 1997, PublicationName = "АО ИД «Комсомольская правда»", Cost = 10, Count = 12 };
            var countErrors = 0;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), newspaper);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), newspaper);
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Проверка валидации газеты с неправильными параметрами.
        /// </summary>
        [Test]
        public void TestCheckValidationNewspaperFalseObject()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var patent = new Newspaper() { Cost = -123, NumberPages = -123 };
            var countErrors = 6;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), patent);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), patent);
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }

        /// <summary>
        /// Тест проверки удаления с использыванием отмены действия удаления.
        /// Отмена удаления зависит от параметра в класс AcceptString - строка да/нет,
        /// если ввести что-то кроме этого, то операция удаления будет отменена.
        /// </summary>
        [Test]
        public void TestRemoveWithCancelDelete()
        {
            var libraryItemsArrayProviderRemoveHandler = new LibraryItemsArrayProvider(this.dbContext, new AcceptString(false));
            var catalog = new CatalogSerivice(libraryItemsArrayProviderRemoveHandler);
            var book = new Book() { Name = "Good Test", YearPublishing = 1992, Note = " qwef", Authors = string.Empty, PlacePublication = " q", PublicationName = "r ", NumberPages = 2, Cost = 4, Count = 2 };
            var resultCount = catalog.GetAll().Count + 1;

            catalog.Add(book);
            catalog.Remove(book);
            var actualCount = catalog.GetAll().Count;

            Assert.True(actualCount == resultCount);
        }

        /// <summary>
        /// Тест на проверку работы поиска с абстрактным критерием.
        /// </summary>
        [Test]
        public void TestSearchWithArbitraryCriteria()
        {
            var libraryItemsArrayProviderRemoveHandler = new LibraryItemsArrayProvider(this.dbContext, new AcceptString(true));
            var catalog = new CatalogSerivice(libraryItemsArrayProviderRemoveHandler);
            var book = new Book() { Name = "Good Test", YearPublishing = 1992, Note = " qwef", Authors = string.Empty, PlacePublication = " q", PublicationName = "r ", NumberPages = 2, Cost = 4, Count = 2 };

            var resultCount = catalog.GetAll().Select(x => x).Where(x => x.Id > 5).Count();
            var actualCount = catalog.Search(catalog.GetAll(), x => x.Id > 5).Count;

            Assert.True(actualCount == resultCount);
        }

        /// <summary>
        /// Тест на проверку работы сортировки с абстрактным критерием сортировки по убыванию.
        /// </summary>
        [Test]
        public void TestSortDownhWithArbitraryCriteria()
        {
            var libraryItemsArrayProviderRemoveHandler = new LibraryItemsArrayProvider(this.dbContext, new AcceptString(false));
            var catalog = new CatalogSerivice(libraryItemsArrayProviderRemoveHandler);

            var resultCount = catalog.GetAll().OrderByDescending(x => x.Name).Skip(1).First();
            var actualCount = catalog.Sort(catalog.GetAll(), x => x.Name, SortType.Descending).Skip(1).First();

            Assert.True(actualCount.Name == resultCount.Name);
        }

        /// <summary>
        /// Тест на проверку работы сортировки с абстрактным критерием сортировки по возрастанию.
        /// </summary>
        [Test]
        public void TestSortUphWithArbitraryCriteria()
        {
            var libraryItemsArrayProviderRemoveHandler = new LibraryItemsArrayProvider(this.dbContext, new AcceptString(false));
            var catalog = new CatalogSerivice(libraryItemsArrayProviderRemoveHandler);

            var resultCount = catalog.GetAll().OrderBy(x => x.Name).Skip(1).First();
            var actualCount = catalog.Sort(catalog.GetAll(), x => x.Name,SortType.Ascending).Skip(1).First();

            Assert.True(actualCount.Name == resultCount.Name);
        }

        /// <summary>
        /// Тест создания отчета.
        /// </summary>
        [Test]
        public void TestCreateReport()
        {
            var libraryItemsArrayProviderRemoveHandler = new LibraryItemsArrayProvider(this.dbContext, new AcceptString(false));
            var catalog = new CatalogSerivice(libraryItemsArrayProviderRemoveHandler);
            var path = @"test\report.txt";
            catalog.CreateReport(path, new CreateReportToTXT(catalog.GetAll()));

            Assert.True(File.Exists(path));
        }

        /// <summary>
        /// Проверка метода редактирования с верным типом.
        /// </summary>
        [Test]
        public void TestEdit()
        {
            var libraryItemsArrayProviderRemoveHandler = new LibraryItemsArrayProvider(this.dbContext, new AcceptString(false));
            var catalog = new CatalogSerivice(libraryItemsArrayProviderRemoveHandler);
            var book = new Book() { Name = "Good Test", YearPublishing = 1992, Note = " qwef", Authors = string.Empty, PlacePublication = " q", PublicationName = "r ", NumberPages = 2, Cost = 4, Count = 2 };

            catalog.Edit(4, book);

            Assert.True(catalog.GetAll().First(x => x.Id == 4).Name == book.Name);
        }

        /// <summary>
        /// Проверка метода редактирования с не верным типом.
        /// </summary>
        [Test]
        public void TestEditErrorType()
        {
            var libraryItemsArrayProviderRemoveHandler = new LibraryItemsArrayProvider(this.dbContext, new AcceptString(false));
            var catalog = new CatalogSerivice(libraryItemsArrayProviderRemoveHandler);
            var book = new Book() { Name = "Good Test", YearPublishing = 1992, Note = " qwef", Authors = string.Empty, PlacePublication = " q", PublicationName = "r ", NumberPages = 2, Cost = 4, Count = 2 };

            Assert.Throws<Exception>(() => catalog.Edit(5, book));
        }

        /// <summary>
        /// Временное тестирование.
        /// </summary>
        [Test]
        public void Test()
        {
            var catalog = new CatalogSerivice(this.libraryItemsArrayProvider);
            var book = new Book() { Name = "Good Test", YearPublishing = 1992, Note = " qwef", Authors = string.Empty, PlacePublication = " q", PublicationName = "r ", NumberPages = 2, Cost = 4, Count = 2 };
            var book1 = new Book();
            catalog.Add(book1);
            var listResult = catalog.GetAll();
            var bol = new List<bool>();

            catalog.Save(this.pathJson);
            catalog.Load(this.pathJson,new AnnotationValidator(), DownloadMode.All);

            var listResultJson = catalog.GetAll();
            for (int i = 0; i < listResult.Count; i++)
            {
                if (listResult[i].Equals(listResultJson[i]))
                {
                    bol.Add(true);
                }
                else
                {
                    bol.Add(false);
                }
            }

            catalog.Save(this.pathXML);
            catalog.Load(this.pathXML, new AnnotationValidator(), DownloadMode.All);

            var listResultXML = catalog.GetAll();
            bol.Clear();
            for (int i = 0; i < listResult.Count; i++)
            {
                if (listResult[i].Equals(listResultXML[i]))
                {
                    bol.Add(true);
                }
                else
                {
                    bol.Add(false);
                }
            }
            var newspaper = new Newspaper() { ISSN = "975-3-15-141413-4", Id = 3, Number = 1, Name = "Комсомольская правда", Note = "Самая популярная газета.", NumberPages = 60, PlacePublication = "Россия - Москва", Date = DateTime.Now.AddYears(-1).AddMonths(-1), YearPublishing = 1997, PublicationName = "АО ИД «Комсомольская правда»", Cost = 10, Count = 12 };
            var countErrors = 0;

            var resultValidatorFirst = catalog.CheckValidation(new ChainOfResponsibilityValidator(), newspaper);
            var resultValidatorSecond = catalog.CheckValidation(new ChainOfResponsibilityValidator(), newspaper);
            Assert.True(resultValidatorFirst.ErrorList.Count == resultValidatorSecond.ErrorList.Count
                && resultValidatorFirst.ErrorList.Count == countErrors);
        }
    }
}