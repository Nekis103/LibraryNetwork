// <copyright file="ChainOfResponsibilityValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LibraryNetwork.Models.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using LibraryNetwork.Interface;
    using LibraryNetwork.Models.Validators;
    using LibraryNetwork.Models.Validators.ValidationModel;

    /// <summary>
    /// Валидатор.
    /// </summary>
    //TODO+ [Anna] Название валидаторов не говорят о том, какого они типа (где цепочка обязанностей, а где через аннотации)
    public class ChainOfResponsibilityValidator : IValidation
    {
        /// <summary>
        /// Проверка на валидность объекта.
        /// </summary>
        /// <param name="libraryItem">Объкт библиотеки.</param>
        /// <returns>Валидный или нет.</returns>
        // [Anna] Валидацию необходимо реализовать с использованием паттерна "цепочка обязанностей"
        // Основная идея: в метод валидации передается экземпляр LibraryItem, проверяется его настоящий тип, и если валидацию может сделать текущий класс, то выполнить ее, иначе - вызвать следующий валидатор в цепочке
        //TODO [Anna] По-прежнему не вижу паттерна "цепочка обязанностей". Как-то скидывала ссылку на метанит, перечитай или поищи сам источники, которые тебе будут понятны
        public ResultValidation Check(LibraryItem libraryItem)
        {
            LibraryItemValidator libraryItemValidator = new LibraryItemValidator();
            var a = libraryItemValidator.Validate(libraryItem).Errors;
            var resList = new ResultValidation();

            foreach (var item in a)
            {
                resList.ErrorList.Add(item.ErrorMessage);
                resList.IsValid = false;
            }

            return resList;
        }

        /// <summary>
        /// Проверка на валидность списка объектов.
        /// </summary>
        /// <param name="libraryItem">Список объектов.</param>
        /// <returns>Валидные или нет.</returns>
        public bool Check(List<LibraryItem> libraryItem)
        {
            LibraryItemCollectionValidator libraryItemValidator = new LibraryItemCollectionValidator();
            return libraryItemValidator.Validate(libraryItem).IsValid;
        }
    }
}
