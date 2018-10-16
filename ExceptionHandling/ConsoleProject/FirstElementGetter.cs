using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    class FirstElementGetter
    {
        public static char GetFirstElement(string text)
        {
            ValidateForErrorNullorEmpty(text);

            return text.ToCharArray().First();
        }

        public static bool GetAnswer(string text)
        {
            ValidateForErrorNullorEmpty(text);
           
            if (!string.Equals("Y", text, StringComparison.OrdinalIgnoreCase) && !string.Equals("N", text, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Bratan! Tak dela ne delautsa) Viberi Y or N!");

            return string.Equals("Y", text);
        }


        private static void ValidateForErrorNullorEmpty(string text)
        {
            if (text is null)
                throw new ArgumentNullException("Bratan, ti 4to to poputal, gde vvedennoe zna4enie? nafiga mne tvoi null?");

            if (text == "")
                throw new ArgumentException("Brataaaaann!!! Nu kak zhe taaak... Empty value...");
        }
    }
}
