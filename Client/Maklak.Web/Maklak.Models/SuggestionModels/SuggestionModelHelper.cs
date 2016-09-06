using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    public static class SuggestionModelHelper
    {
        public enum SuggestionKeys { PROUDUCT, MODEL, PRODUCER, PROPERTY, PROPERTYVALUE, TAG }

        public static SuggestionModel GenerateModel()
        {
            SuggestionModel model = new SuggestionModel();
            //model.InputValue = inputValue;
            //model.SuggestionKey = (SuggestionKeys)Enum.Parse(typeof(SuggestionKeys), key);
            return model;
        }

        public static void InitModel(SuggestionModel model,string inputValue, string key)
        {
            
            model.InputValue = inputValue;
            model.SuggestionKey = (SuggestionKeys)Enum.Parse(typeof(SuggestionKeys), key);
            
        }
    }
}
