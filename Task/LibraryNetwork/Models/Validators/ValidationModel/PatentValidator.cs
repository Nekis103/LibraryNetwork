// <copyright file="PatentValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Validation
{
    using FluentValidation;

    /// <summary>
    /// Валидатор патента.
    /// </summary>
    public class PatentValidator : AbstractValidator<Patent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatentValidator"/> class.
        /// </summary>
        public PatentValidator()
        {
            this.RuleFor(x => x.Inventors).NotEmpty();
            this.RuleFor(x => x.Country).NotEmpty();
            this.RuleFor(x => x.ApplicationDate.Year).GreaterThanOrEqualTo(1950);
            this.RuleFor(x => x.DatePublishing.Year).GreaterThanOrEqualTo(1950);
            this.RuleFor(x => x.NumberPages).GreaterThanOrEqualTo(1);
            this.RuleFor(x => x.Cost).GreaterThanOrEqualTo(0);
        }
    }
}
