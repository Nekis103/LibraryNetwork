// <copyright file="RemoveHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Событие удаления.
    /// </summary>
    public class RemoveHandler
    {
        private static string path = "Log";
        private static string fileName = "DeletedFilelog.txt";

        /// <summary>
        /// Запись сообщения о удалении в файл.
        /// </summary>
        /// <param name="sender">Объект библиотеки.</param>
        /// <param name="e">Событие.</param>
        public void Message(object sender, EventArgs e)
        {
            if (sender is LibraryItem libraryItem)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filePath = Path.Combine(path, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(string.Format("Удалён объект типа - {0,-10}, Название объекта: {1,-35} Дата удаления: {2,-17}", libraryItem.GetType().Name, libraryItem.Name, DateTime.Now));
                }
            }
            else
            {
                throw new Exception("передан неверный тип объекта.");
            }
        }
    }
}
