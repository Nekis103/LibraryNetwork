// <copyright file="CreateReport.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibraryNetwork.Models;

    /// <summary>
    /// Создание отчета.
    /// </summary>
    public class ReportDesigner
    {
        private List<LibraryItem> libraryItems = new List<LibraryItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReport"/> class.
        /// </summary>
        /// <param name="list">Лист объектов по которым нужно создать отчёт.</param>
        public List<string> CreateReport(List<LibraryItem> list)
        {
            var listReports = new List<Report>();
            var groupeType = list.GroupBy(
            x => x.GetType(),
            x => x,
            (g, list) => new
            {
                Key = g,
                list = list.ToList(),
            });

            foreach (var type in groupeType)
            {
                listReports.Add(new Report(type.list) { Type = type.Key, Count = type.list.Count });
            }

            var stringBuilder = new StringBuilder();
            var listString = new List<string>();
            foreach (var item in listReports)
            {
                stringBuilder.Clear();
                stringBuilder.AppendLine(item.Type.Name + Environment.NewLine);
                stringBuilder.AppendJoin(Environment.NewLine, item.List.Select(x => x.GetWriteString()));
                stringBuilder.AppendLine(Environment.NewLine + item.Count + Environment.NewLine);
                listString.Add(stringBuilder.ToString());
            }
            
            return listString;
        }
    }

    public static class MyExtensions
    {
        public static string GetWriteString(this LibraryItem libraryItem)
        {
            if (libraryItem is Book book)
            {
                return string.Format("{0,-35} {1,-30}", book.Authors, book.Name);
            }

            if (libraryItem is Newspaper newspaper)
            {
                return string.Format("{0,-35} {1,-30}", newspaper.Name, newspaper.YearPublishing);
            }

            if (libraryItem is Patent patent)
            {
                return string.Format("{0,-35} {1,-30}", patent.Name, patent.DatePublishing);
            }

            throw new Exception("Не верный тип.");
        }
    }
}
