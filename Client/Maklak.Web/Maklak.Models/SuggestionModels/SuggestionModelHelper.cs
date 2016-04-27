using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    public static class SuggestionModelHelper
    {
        public enum SuggestionKeys { PROUDUCT, MODEL, PRODUCER, PROPERTY, PROPERTYVALUE }

        public static SuggestionModel GenerateModel(string inputValue, string key )
        {
            SuggestionModel model = new SuggestionModel();
            model.InputValue = inputValue;
            model.SuggestionKey = (SuggestionKeys)Enum.Parse(typeof(SuggestionKeys), key);
            return model;
        }
    }
}
