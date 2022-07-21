// <copyright file="AcceptString.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.ConfirmingDeletion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibraryNetwork.Interface;

    /// <summary>
    /// Подтвеждение удаления с помощью строки.
    /// </summary>
    public class AcceptString : IConfirmingDeletion
    {
        //TODO+ [Anna] Обычно для таких целей используют тип bool и переадают в метод true или false
        private bool accepted;

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptString"/> class.
        /// </summary>
        /// <param name="accepted">строка подтвержения.</param>
        public AcceptString(bool accepted)
        {
            this.accepted = accepted;
        }

        /// <summary>
        /// Метод подтвержения.
        /// </summary>
        /// <returns>Булевое значение говорящее нам удалять объект или нет.</returns>
        public bool Accept()
        {
            if (this.accepted == true)
            {
                return true;
            }
            else if (this.accepted == false)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
