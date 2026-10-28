using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    /// <summary>
    /// Проверка формата государственного номера
    /// </summary>
    public interface ILicensePlateValidator
    {
        /// <summary>Проверяет формат госномера</summary>
        /// <param name="plate">Проверяемый госномер</param>
        /// <returns>true, если формат корректен</returns>
        bool IsValid(string? plate);
    }
}
