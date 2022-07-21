// <copyright file="FactoryDateSerilizator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.SerilizationData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibraryNetwork.Enums;
    using LibraryNetwork.Interface;
    using LibraryNetwork.Models;

    /// <summary>
    /// Фабрика серилизаторов данных.
    /// </summary>
    /// <typeparam name="T">LibraryItem.</typeparam>
    public class FactoryDateSerilizator<T>
    {
        /// <summary>
        /// Получить серилизатор.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Серилизатор.</returns>
        public IDataManager<T> GetSerilizator(string path)
        {
            var a = Path.GetExtension(path).Replace(".", string.Empty);
            if (Enum.TryParse(a, true, out SerialazeType myName))
            {
                switch (myName)
                {
                    case SerialazeType.Json:
                        return new DataSerilizatorJson<T>();
                    case SerialazeType.XML:
                        return new DataSerilizatorXml<T>();
                }
            }

            throw new Exception("Error type in path");
        }

    }
}
