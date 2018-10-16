using System;
using System.CodeDom;
using System.ComponentModel;
using System.Globalization;


namespace StringToIntConverter
{
    public class StringToIntConverter
    {
        private static int ConvertCharToInt(char c)
        {
            return c - '0';
        }
        public static int ConvertToInt(string text)
        {
            if (text is null) {
                throw new ArgumentNullException("NULL");
            }

            if (text.Length == 0) {
                throw new ArgumentException("EMPTY");
            }

            if (text.Length == 1) {
                throw new ArgumentException("Wuuut?");
            }

            var startIndex = 0;
            var isNegative = false;

            if (text[0] == '-') {
                startIndex = 1;
                isNegative = true;
            }

            var result = 0;
            for (int i = startIndex; i < text.Length; i++) {
                if (text[i] < '0' || text[i] > '9') throw new ArgumentException("What the hell??");
                result = checked(result * 10 + ConvertCharToInt(text[i]));
            }

            return isNegative ? -result : result;
        }
    }
}
