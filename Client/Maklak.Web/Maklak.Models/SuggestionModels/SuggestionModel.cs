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
            this.OnModelReady += SuggestionModel_OnModelReady;
            
        }

        private void SuggestionModel_OnModelReady()
        {
            

            for (int i = 0; i < 8; i++)
                suggestionValues.Add(i, "item_" + i.ToString());

            ManageSelection();
        }

        private void ManageSelection()
        {
            
              DataSets.ModelDS.SelectionRow row = base.data.Selection.Where(r => r.Key == SuggestionKey.ToString()).FirstOrDefault();


        }

        protected override bool IsModelInitialized()
        {
            return base.IsModelInitialized() && suggestionValues.Count() > 0;
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
        public int ItemId
        {
            get
            {
                return suggestionValues.Count() > 0 && suggestionValues.ContainsValue(this.InputValue) ? suggestionValues.Keys.Where(k=> suggestionValues[k].Equals(this.InputValue,StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() : 0;
            }
        }
    }
}
