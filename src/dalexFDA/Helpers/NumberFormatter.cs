using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace dalexFDA
{
    public class NumberFormatter
    {
        const string NIG_PHONE_FORMAT = "### #### ####";
        const string US_PHONE_FORMAT = "(###) ###-####";

        public static string FormatPhone(string phoneNum, string country)
        {
            string phoneFormat = US_PHONE_FORMAT;
            bool isNigeria = country?.ToLower() == "nigeria";

            if (string.IsNullOrEmpty(phoneNum))
                phoneNum = "";

            if (isNigeria)
                phoneFormat = NIG_PHONE_FORMAT;

            // First, remove everything except of numbers
            Regex regexObj = new Regex(@"[^\d]");
            phoneNum = regexObj.Replace(phoneNum, "");

            // Second, format numbers to phone string 
            if (double.TryParse(phoneNum, out double phoneDouble))
                phoneNum = phoneDouble.ToString(phoneFormat);

            if (isNigeria)
                phoneNum = $"0{phoneNum}";

            return phoneNum;
        }

        public static string FormatAmount(string amount)
        {
            if (!decimal.TryParse(amount, out decimal value))
                return amount;

            var formattedAmount = String.Format("{0:N2}", value);
            return formattedAmount;
        }

        public static string FormatToCurrency(string amount, string country)
        {
            if (!decimal.TryParse(amount, out decimal value))
                return amount;

            var cultureInfo = CountryCulture(country);

            var formattedAmount = String.Format(cultureInfo, "{0:C}", value);
            return formattedAmount;
        }

        public static string ExtractNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                number = "";

            Regex regexObj = new Regex(@"[^0-9.]");
            number = regexObj.Replace(number, "");

            return number;
        }

        public static CultureInfo CountryCulture(string country)
        {
            CultureInfo culture;

            switch (country.ToLower())
            {
                case "nigeria":
                    culture = new System.Globalization.CultureInfo("ig-NG");
                    break;
                case "united states":
                    culture = new System.Globalization.CultureInfo("en-US");
                    break;
                case "united kingdom":
                    culture = new System.Globalization.CultureInfo("en-GB");
                    break;
                default:
                    culture = new System.Globalization.CultureInfo("ig-NG");
                    break;
            }

            return culture;
        }
    }
}
