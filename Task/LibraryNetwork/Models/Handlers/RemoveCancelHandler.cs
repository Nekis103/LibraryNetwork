// <copyright file="RemoveCancelHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibraryNetwork.Interface;

    /// <summary>
    /// Класс события удаления из каталога библиотеки.
    /// </summary>
    public class RemoveCancelHandler
    {
        private IConfirmingDeletion confirming;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveCancelHandler"/> class.
        /// </summary>
        /// <param name="confirming">Контролёр удаления решающий удалить объект или нет.</param>
        public RemoveCancelHandler(IConfirmingDeletion confirming)
        {
            this.confirming = confirming;
        }

        /// <summary>
        /// Метод отмены удаления.
        /// </summary>
        /// <param name="sender">Удаляемый объект.</param>
        /// <param name="e">Событие отмены удаления.</param>
        public void RemoveCancel(object sender, CancelEventArgs e)
        {
            if (this.confirming.Accept())
            {
                e.Cancel = false;

                // throw new Exception("Операция удаления была отменена");
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
