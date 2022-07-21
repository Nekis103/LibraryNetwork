// <copyright file="ResultValidation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Результат валидации.
    /// </summary>
    public class ResultValidation
    {
        private bool isValid = true;

        private List<string> errorList = new List<string>();

        /// <summary>
        /// Gets or sets a value indicating whether валидно или нет.
        /// </summary>
        public bool IsValid { get => this.isValid; set => this.isValid = value; }

        /// <summary>
        /// Gets or sets список ошибок.
        /// </summary>
        public List<string> ErrorList { get => this.errorList; set => this.errorList = value; }
    }
}
