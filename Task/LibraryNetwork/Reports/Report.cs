// <copyright file="Report.cs" company="PlaceholderCompany">
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
    /// Отчет.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Report"/> class.
        /// </summary>
        /// <param name="libraryItems">Списко объектов для отчета.</param>
        public Report(List<LibraryItem> libraryItems)
        {
            this.List = libraryItems;
        }

        /// <summary>
        /// Gets or sets тип объекта.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets список объектов отчета.
        /// </summary>
        public List<LibraryItem> List {get; set; }

        /// <summary>
        /// Gets or sets количество.
        /// </summary>
        public int Count { get; set; }
        
     //1. Завести отдельную сущность для отчёта(тип, список объектов, количество)
    }
}
