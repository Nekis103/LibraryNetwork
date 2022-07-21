// <copyright file="Newspaper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Газета.
    /// </summary>
    [Serializable]
    public class Newspaper : LibraryItem
    {
        /// <summary>
        /// Gets or sets место издания(Город).
        /// </summary>
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
        /// Gets or sets номер.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets дата.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets международный стандартный номер серийного издания.
        /// </summary>
        public string ISSN { get; set; }

        /// <summary>
        /// Gets or sets стоимость экземпляра.
        /// </summary>
        //TODO+ [Anna] Стоимость не может быть отрицательной
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets количество экземпляров.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Count { get; set; }
        //TODO+ [Anna] А почему здесь и в других сущностях у года публикации тип данных DateTime? Это же всего лишь int.
        //Его тоже можно вынести в общий класс. Да будет некрасиво с патентами, но зато лучше реализуется группировка по году публикации (не забудь поправить)
    }
}
