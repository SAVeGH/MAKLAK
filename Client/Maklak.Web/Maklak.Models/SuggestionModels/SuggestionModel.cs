using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    

    public class SuggestionModel : BaseModel
    {
        Dictionary<int, string> suggestionValues;

        public SuggestionModel() 
        {
            
            suggestionValues = new Dictionary<int, string>();

            for (int i = 0; i < 8; i++)
                suggestionValues.Add(i, "item_" + i.ToString());
        }

        public Dictionary<int, string> SuggestionValues
        {
            get
            {
                return suggestionValues.Where(i=> !string.IsNullOrEmpty(this.InputValue) &&
                                                  i.Value.Contains(this.InputValue) && 
                                                  !i.Value.Equals(this.InputValue,StringComparison.InvariantCultureIgnoreCase)
                                                  )
                                       .ToDictionary(kvp=> kvp.Key,kvp=> kvp.Value);
            }
        }
        public SuggestionModelHelper.SuggestionKeys SuggestionKey { get; set; }
        public string InputValue { get; set; }
        public int ItemId { get; set; }
    }
}
