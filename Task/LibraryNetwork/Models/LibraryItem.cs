// <copyright file="LibraryItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>


namespace LibraryNetwork.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    /// <summary>
    /// Любая вещь библиотеки, будь то книга, патент или газета, которую можно взять.
    /// </summary>
    [XmlInclude(typeof(Book))]
    [XmlInclude(typeof(Newspaper))]
    [Serializable]
    [XmlInclude(typeof(Patent))]
    public class LibraryItem
    {
        /// <summary>
        /// Gets or sets идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets название.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets примечание.
        /// </summary>
        [MaxLength(500)]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets Год публикации.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int YearPublishing { get; set; }
    }
}
