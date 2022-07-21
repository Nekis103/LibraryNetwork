// <copyright file="BookValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Validation
{
    using FluentValidation;

    /// <summary>
    /// Валидация книги.
    /// </summary>
    public class BookValidator : AbstractValidator<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookValidator"/> class.
        /// Валидатор книги.
        /// </summary>
        public BookValidator()
        {
            this.RuleFor(x => x.Authors).NotEmpty();
            this.RuleFor(x => x.PublicationName).NotEmpty();
            this.RuleFor(x => x.PlacePublication).NotEmpty();
            this.RuleFor(x => x.YearPublishing).GreaterThan(1900);
            this.RuleFor(x => x.NumberPages).GreaterThanOrEqualTo(1);
            this.RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
            this.RuleFor(x => x.Cost).GreaterThanOrEqualTo(0);
        }
    }
}
