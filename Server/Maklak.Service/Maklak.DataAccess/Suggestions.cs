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
            


            SuggestionDS.SuggestionsRow inputRow = inputDS.Suggestions.Where(r => r.IsCurrent == 1).FirstOrDefault();

            SuggestionDS ds = GetRows(inputRow.Key);

            string inputValue = inputRow.ItemValue;

            SuggestionDS.SuggestionsRow idRow  = ds.Suggestions.Where(r => r.ItemValue == inputValue).FirstOrDefault();

            if (idRow != null)
            {
                // найден id
                inputRow.Id = idRow.Id;
                inputRow.ItemValue = idRow.ItemValue;
                inputRow.Key = idRow.Key;
                inputDS.AcceptChanges();
                return inputDS;
            }

            ds.Suggestions.Where(r => r.ItemValue.Contains(inputValue)).ToList().ForEach(r => inputDS.Suggestions.ImportRow(r));
            inputDS.AcceptChanges();
            return inputDS;
        }

        static SuggestionDS GetRows(string key)
        {
            SuggestionDS ds = new SuggestionDS();

            for (int i = 1; i < 6; i++)
            {
                SuggestionDS.SuggestionsRow row = ds.Suggestions.NewSuggestionsRow();
                row.Id = i;
                row.Key = key;
                row.ItemValue = "item_" + i.ToString();
                ds.Suggestions.AddSuggestionsRow(row);

            }

            ds.AcceptChanges();

            return ds;
        }

    }
}
