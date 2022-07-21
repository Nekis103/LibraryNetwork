// <copyright file="LibraryItemCollectionValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Validators.ValidationModel
{
    using System.Collections.Generic;
    using FluentValidation;
    using LibraryNetwork.Models.Validation;

    /// <summary>
    /// Валидатор для коллекции объекта библиотеки.
    /// </summary>
    public class LibraryItemCollectionValidator : AbstractValidator<List<LibraryItem>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryItemCollectionValidator"/> class.
        /// </summary>
        public LibraryItemCollectionValidator()
        {
            this.RuleForEach(x => x).SetValidator(new LibraryItemValidator());
        }
    }
}
