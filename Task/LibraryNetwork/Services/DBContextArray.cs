// <copyright file="DBContextArray.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Services
{
    using System;
    using LibraryNetwork.Interface;
    using LibraryNetwork.Models;

    /// <summary>
    /// БД размещенное в массиве.
    /// </summary>
    public class DBContextArray : IDBContext
    {
        /// <summary>
        /// Каталог данных размещенный в массиве сущностей библиотеки..
        /// </summary>
        private readonly LibraryItem[] catalog =
            {
            new Book() { Authors = "Толстой", Id = 3, ISBN = 975 - 3 - 15 - 111113 - 6, Name = "Война и Мир", Note = "О борьбе и протеворечии.", NumberPages = 2151, PlacePublication = "Россия", PublicationName = "Российский издатель", YearPublishing = 1942 },
            new Book() { Authors = "Антуан", Id = 3, ISBN = 975 - 3 - 15 - 111113 - 2, Name = "Маленький принц", Note = "О мечтах.", NumberPages = 61, PlacePublication = "Россия", PublicationName = "Росмаский издатель", YearPublishing = 2010 },
            new Book() { Authors = "Илья Ильф, Евгений Петров", Id = 3, ISBN = 975 - 3 - 15 - 111113 - 3, Name = "12 стульев", Note = "Очень интересно.", NumberPages = 267, PlacePublication = "Россия", PublicationName = "Украинский издатель", YearPublishing = 2010 },
            new Book() { Authors = "Евгений Петров", Id = 3, ISBN = 975 - 3 - 15 - 111113 - 4, Name = "Золотой телёнок", Note = "Классный рассказ.", NumberPages = 32, PlacePublication = "Швеция", PublicationName = "Варшавский издатель", YearPublishing = 2010 },
            new Book() { Authors = "Толстой, Лермонтов", Id = 3, ISBN = 975 - 3 - 15 - 111113 - 5, Name = "Что-то да бы и вышло.", Note = "Было бы очень интересно.", NumberPages = 9871, PlacePublication = "Аргентина", PublicationName = "Российский издатель", YearPublishing = 1997 },
            new Patent() { Inventors = "Илон Маск", Id = 3, Country = "США", Name = "Улучшенная солнечная панель", Note = "Возобновляемый источник энергии.", NumberPages = 2580, RegistrationNumber = "891282312375", DatePublishing = DateTime.Now.AddYears(-30), ApplicationDate = DateTime.Now.AddYears(-30) },
            new Patent() { Inventors = "Гейб", Id = 3, Country = "США", Name = "Dota 2", Note = "Игра и бренд", NumberPages = 981, RegistrationNumber = "891287662375", DatePublishing = DateTime.Now.AddYears(-25), ApplicationDate = DateTime.Now.AddYears(-25) },
            new Patent() { Inventors = "Томас Эддисон", Id = 3, Country = "Англия", Name = "Электричество", Note = "Открыл что такое электричество и с чем его едят.", NumberPages = 8761, RegistrationNumber = "891287615375", DatePublishing = DateTime.Now.AddYears(-30), ApplicationDate = DateTime.Now.AddYears(-30) },
            new Patent() { Inventors = "Никола Тесла", Id = 3, Country = "Сербия", Name = "Электродвигатель", Note = "Изобрёл электрический двигатель.", NumberPages = 9812, RegistrationNumber = "891287612372", DatePublishing = DateTime.Now.AddYears(-20), ApplicationDate = DateTime.Now.AddYears(-20) },
            new Patent() { Inventors = "Менделеев", Id = 3, Country = "Российская империя", Name = "Таблица менделеева", Note = "Структурировал порядок химический веществ.", NumberPages = 7781, RegistrationNumber = "891287612371", DatePublishing = DateTime.Now.AddYears(-20), ApplicationDate = DateTime.Now.AddYears(-20) },
            new Newspaper() { ISSN = "975-3-15-141413-4", Id = 3, Number = 1, Name = "Комсомольская правда", Note = "Самая популярная газета.", NumberPages = 60, PlacePublication = "Россия - Москва", Date = DateTime.Now.AddYears(-1).AddMonths(-1), YearPublishing = 1997, PublicationName = "АО ИД «Комсомольская правда»" },
            new Newspaper() { ISSN = "973-3-14-142411-3", Id = 3, Number = 2, Name = "Metro", Note = "Вторая самая поплурная газета.", NumberPages = 90, PlacePublication = "Россия - Москва", Date = DateTime.Now.AddDays(-10), YearPublishing = 2010, PublicationName = "«Эксмо»" },
            new Newspaper() { ISSN = "971-3-13-143416-5", Id = 3, Number = 3, Name = "Российская газета", Note = "Третья самая популярная газета.", NumberPages = 52, PlacePublication = "Россия - Москва", Date = DateTime.Now.AddDays(-52), YearPublishing = 2006, PublicationName = "ФГБУ «Редакция „Российской газеты“»" },
            new Newspaper() { ISSN = "973-3-12-144413-6", Id = 3, Number = 4, Name = "Москва Вечерняя", Note = "Четвертая самая поплуярная газета.", NumberPages = 88, PlacePublication = "Россия - Москва", Date = DateTime.Now.AddDays(-28), YearPublishing = 2002, PublicationName = "ОАО Редакция газеты Вечерняя Москва" },
            new Newspaper() { ISSN = "975-3-11-145415-2", Id = 3, Number = 52, Name = "Известия", Note = "Шестая самая поплурная газета.", NumberPages = 24, PlacePublication = "Россия - Москва", Date = DateTime.Now.AddDays(-16), YearPublishing = 2005, PublicationName = "ООО «МИЦ „Известия“» " },
            };

        /// <summary>
        /// Получить все данные.
        /// </summary>
        /// <returns>Вертуть бд в дальнейшее использовани.</returns>
        public LibraryItem[] GetAllDate()
        {
            return this.catalog;
        }
    }
}
