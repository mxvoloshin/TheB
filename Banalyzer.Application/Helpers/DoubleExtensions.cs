using System;
using System.Globalization;

namespace Banalyzer.Application.Helpers
{
    public static class DoubleExtensions
    {
        public static Double ToCurrency(this Double value)
        {
            var stringValue = value.ToString("C");
            return Double.Parse(stringValue, NumberStyles.Currency);
        }
    }
}