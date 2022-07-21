// <copyright file="NewspaperValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentValidation;

    /// <summary>
    /// Валидатор газеты.
    /// </summary>
    public class NewspaperValidator : AbstractValidator<Newspaper>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewspaperValidator"/> class.
        /// </summary>
        public NewspaperValidator()
        {
            this.RuleFor(x => x.PublicationName).NotEmpty();
            this.RuleFor(x => x.YearPublishing).GreaterThan(1900);
            this.RuleFor(x => x.NumberPages).GreaterThanOrEqualTo(1);
            this.RuleFor(x => x.Number).GreaterThan(0);
            this.RuleFor(x => x.Cost).GreaterThanOrEqualTo(0);
            this.RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
        }
    }
}
