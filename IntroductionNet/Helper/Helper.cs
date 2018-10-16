using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using Helper.Properties;

namespace HelperProject
{
    public class GreetingHelper
    {
        private const string DefaultHello = "Hello";
        private string _sayHello;

        public GreetingHelper()
        {
            FillDictionary();
            _sayHello = DefaultHello;
        }

        public Dictionary<string, string> HelloWords { get; private set; }

        public string Greeting(string name)
        {
            return $"{DateTime.Now.ToLocalTime()} {_sayHello}, {name}";
        }

        public void ChooseHelloType(string country)
        {
            if (string.IsNullOrEmpty(country)) { 
                return;
            }

            _sayHello = HelloWords.First(h => h.Key.Equals(country)).Value;
        }

        private void FillDictionary()
        {
            var myResourceClass = new ResourceManager(typeof(Resources));
            var resourceSet = myResourceClass.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            HelloWords = new Dictionary<string, string>();
            foreach (DictionaryEntry entry in resourceSet) {
                HelloWords.Add(entry.Key.ToString(), entry.Value.ToString());
            }
        }
    }
}
