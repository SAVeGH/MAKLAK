using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.DataAccess.DataSets;

namespace Maklak.DataAccess
{
    public static class Suggestions
    {
        public static SuggestionDS Suggestion(SuggestionDS inputDS)
        {

            SuggestionDS.SuggestionInputRow inputRow = inputDS.SuggestionInput.FirstOrDefault();
            string key = inputRow.Key;
            string inputValue = inputRow.ItemValue;

            bool emptyInput = string.IsNullOrEmpty(inputValue) || string.IsNullOrWhiteSpace(inputValue);

            inputDS.Suggestion.Clear();
            inputDS.AcceptChanges();

            if (emptyInput)
                return inputDS;

            SuggestionDS ds = GetRows(key);
            // найти Id для inputValue
            int inputId = ds.Suggestion.Where(r => r.ItemValue.Equals(inputValue, StringComparison.InvariantCultureIgnoreCase)).Select(r => r.Id).FirstOrDefault();

            if (inputId > 0)
                inputRow.Id = inputId; 

            // Данные подсказки
            ds.Suggestion
              .Where(r => !emptyInput && r.ItemValue.Contains(inputValue) && !r.ItemValue.Equals(inputValue, StringComparison.InvariantCultureIgnoreCase))
              .ToList()
              .ForEach(r => inputDS.Suggestion.ImportRow(r));
            
            inputDS.AcceptChanges();

            return inputDS;
        }

        static SuggestionDS GetRows(string key)
        {
            SuggestionDS ds = new SuggestionDS();

            for (int i = 1; i < 6; i++)
            {
                SuggestionDS.SuggestionRow row = ds.Suggestion.NewSuggestionRow();
                row.Id = i;
                row.Key = key;
                row.ItemValue = "item_" + i.ToString();
                ds.Suggestion.AddSuggestionRow(row);

            }

            ds.AcceptChanges();

            return ds;
        }

    }
}
