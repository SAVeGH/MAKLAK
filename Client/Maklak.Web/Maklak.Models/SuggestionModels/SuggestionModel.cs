using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    

    public class SuggestionModel
    {
        Dictionary<int, string> suggestionValues;

        public SuggestionModel()
        {
            suggestionValues = new Dictionary<int, string>();

            for (int i = 0; i < 8; i++)
                suggestionValues.Add(i, "item_" + i.ToString());
        }

        public Dictionary<int, string> SuggestionValues { get { return suggestionValues; } }
        public SuggestionModelHelper.SuggestionKeys SuggestionKey { get; set; }
        public string InputValue { get; set; }
    }
}
