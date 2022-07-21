// <copyright file="IValidation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Interface
{
    using System.Collections.Generic;
    using LibraryNetwork.Models;
    using LibraryNetwork.Models.Validators;
    using LibraryNetwork.Models.Validators.ValidationModel;

    /// <summary>
    /// Валидация.
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// Проверка на валидность элемента.
        /// </summary>
        /// <param name="libraryItem">Проверяемыё объект.</param>
        /// <returns>Объект валидный или нет.</returns>
        ResultValidation Check(LibraryItem libraryItem);
    }
}
