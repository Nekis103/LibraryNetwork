// <copyright file="CreateReportToTXT.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Reports
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibraryNetwork.Interface;
    using LibraryNetwork.Models;

    /// <summary>
    /// Создание отчета в файл ТХТ.
    /// </summary>
    public class CreateReportToTXT : IStrategyCreateReport
    {
        private List<LibraryItem> libraryItems = new List<LibraryItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportToTXT"/> class.
        /// </summary>
        /// <param name="list">Лист объектов по которым нужно создать отчёт.</param>
        public CreateReportToTXT(List<LibraryItem> list)
        {
            this.libraryItems = list;
        }

        //TODO+ [Anna] Путь к файлу подразумевает и его название. Думаю, не стоит их разделять
        /// <summary>
        /// Метод создания отчета.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        public void CreateReport(string path)
        {
            //TODO+ [Anna] Проверку наличия файла лучше вынести в родительский метод, чтобы здесь были конкретно действия по построению отчёта

            if (this.libraryItems.Count == 0)
            {
                return;
            }

            //TODO+ [Anna] Если для построения отчёта нет данных, то его можно не строить

            //TODO+ [Anna] Выглядит довольно сложно + снова нарушеаешь SOLID. Предлагаю:
            /*
             * 1. Завести отдельную сущность для отчёта (тип, список объектов, количество)
             * 2. Завести отдельный класс для построения отчёта, в нём:
             *   a) Получать список объектов для построения отчёта (с помощью LINQ сразу вытаскивать в новый тип)
             *   b) Формировать список готовых строк для записи в отчёт. Рекомендую потренироваться с методами расширения и
             *      для LibraryItem реализовать метод, который генерит строку в зависимости от пришедшего типа
             *   с) Не забыть про количество записей
             * 3. И уже в этом классе и методе записывать в текстовый файл полученную коллекцию строк 
             *
             */

            var reportDesigner = new ReportDesigner();
            var listString = reportDesigner.CreateReport(this.libraryItems);
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                foreach (var item in listString)
                {
                    sw.WriteLine(item);
                }
            }
        }
    }
}
