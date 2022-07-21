// <copyright file="AddHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Класс события возникающе при добавлении объекта в библиотеку.
    /// </summary>
    public class AddHandler
    {
        /// <summary>
        /// Метод возвращающий сообщение о добавлении.
        /// </summary>
        /// <param name="libraryItem">добавляемый объект.</param>
        /// <returns>Строка с информацией о добавлении и добавленном объекте.</returns>
        //TODO [Anna] Некорректное название метода. Подобный коммент с объяснением оставила в задаче по мониторингу файлов
        public string Message(LibraryItem libraryItem)
        {
            return string.Format("Добален новый объект {0,-10} {1,-35} {2,-17}", libraryItem.GetType().Name, libraryItem.Name, DateTime.Now);
        }
    }
}
