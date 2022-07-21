// <copyright file="IStrategyCreateReport.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Контракт создания отчета.
    /// </summary>
    public interface IStrategyCreateReport
    {
        /// <summary>
        /// Метод создания отчета.
        /// </summary>
        /// <param name="path">Путь файла.</param>
        void CreateReport(string path);
    }
}
