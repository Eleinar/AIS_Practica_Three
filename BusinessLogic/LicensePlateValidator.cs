using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    /// <summary>
    /// Проверка российского формата госномера (А001АА777)
    /// </summary>
    public class LicensePlateValidator : ILicensePlateValidator
    {
        private static readonly Regex PlatePattern =
            new(@"^[АВЕКМНОРСТУХ][0-9]{3}[АВЕКМНОРСТУХ]{2}[0-9]{2,3}$", RegexOptions.Compiled);

        /// <inheritdoc/>
        public bool IsValid(string? plate) =>
            !string.IsNullOrWhiteSpace(plate) && PlatePattern.IsMatch(plate.Trim().ToUpperInvariant());
    }
}