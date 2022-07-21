// <copyright file="Patent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Патент.
    /// </summary>
    [Serializable]
    public class Patent : LibraryItem
    {
        /// <summary>
        /// Gets or sets изобретатели.
        /// </summary>
        [Required]
        public string Inventors { get; set; }

        /// <summary>
        /// Gets or sets страна.
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets регистрационный номер.
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or sets дата подачи заявки.
        /// </summary>
        [Range(typeof(DateTime), "1/1/1950", "1/1/9999")]
        [Required]
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// Gets or sets количество страниц.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int NumberPages { get; set; }

        /// <summary>
        /// Gets or sets стоимость экземпляра.
        /// </summary>
        [Range(-double.Epsilon, double.MaxValue)]
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets Дата публикации.
        /// </summary>
        [Range(typeof(DateTime), "1/1/1950", "1/1/9999")]
        [Required]
        public DateTime DatePublishing { get; set; }
    }
}
