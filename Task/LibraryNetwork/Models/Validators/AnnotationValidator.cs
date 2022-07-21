// <copyright file="AnnotationValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Validators
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using LibraryNetwork.Interface;

    /// <summary>
    /// DataAnnotations валидатор.
    /// </summary>
    public class AnnotationValidator : IValidation
    {
        /// <summary>
        /// Метод проверки на валидность.
        /// </summary>
        /// <param name="libraryItem">Объект библиотеки.</param>
        /// <returns>Список ошибок.</returns>
        public ResultValidation Check(LibraryItem libraryItem)
        {
            var context = new ValidationContext(libraryItem);
            var results = new List<ValidationResult>();

            //TODO [Anna] Лучше завести отдельный объект, в котором будешь хранить ошибки. Одно из полей, например, bool IsValid, второе - твой список сообщений
            var resList = new ResultValidation();
            if (!Validator.TryValidateObject(libraryItem, context, results, true))
            {
                foreach (var item in results)
                {
                    resList.ErrorList.Add(item.ErrorMessage);
                    resList.IsValid = false;
                }
            }

            return resList;
            //TODO [Anna] В else нет необходимости, т.к. в предыдущем блоке при лююых условиях в конце будет вызван return
        }
    }
}
