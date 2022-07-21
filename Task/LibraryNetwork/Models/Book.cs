// <copyright file="Book.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Книга.
    /// </summary>
    [Serializable]
    public class Book : LibraryItem
    {
        /// <summary>
        /// Gets or sets авторы.
        /// </summary>
        [Required]
        public string Authors { get; set; }

        /// <summary>
        /// Gets or sets место издания(Город).
        /// </summary>
        [Required]
        public string PlacePublication { get; set; }

        /// <summary>
        /// Gets or sets название издательства.
        /// </summary>
        [Required]
        public string PublicationName { get; set; }

        /// <summary>
        /// Gets or sets количество страниц.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int NumberPages { get; set; }

        /// <summary>
        /// Gets or sets международный стандартный номер серийного издания.
        /// </summary>
        public int ISBN { get; set; }

        /// <summary>
        /// Gets or sets стоимость продукта.
        /// </summary>
        [Range(0, double.MaxValue)]
        [Required]
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets количество экземпляров.
        /// </summary>
        [Range(-1, int.MaxValue)]
        public int Count { get; set; }
    }
}
