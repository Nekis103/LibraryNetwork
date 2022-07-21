// <copyright file="LibraryItemValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Validation
{
    using FluentValidation;

    /// <summary>
    /// Валидаор объекта библиотеки.
    /// </summary>
    public class LibraryItemValidator : AbstractValidator<LibraryItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryItemValidator"/> class.
        /// </summary>
        public LibraryItemValidator()
        {
            this.RuleFor(x => x).SetInheritanceValidator(v =>
            {
                v.Add<Book>(new BookValidator());
                v.Add<Patent>(new PatentValidator());
                v.Add<Newspaper>(new NewspaperValidator());
            });
            this.RuleFor(x => x.Name).NotEmpty();
            this.RuleFor(x => x.Note).MaximumLength(500);
        }
    }
}
