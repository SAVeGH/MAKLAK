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
            SuggestionDS ds = new SuggestionDS();

            SuggestionDS.SuggestionsRow inputRow = inputDS.Suggestions.Where(r => r.IsSuggestedNull()).FirstOrDefault();
            ds.Suggestions.ImportRow(inputRow);

            ds.Suggestions.Where(r => !r.IsSuggestedNull()).ToList().ForEach(r => ds.Suggestions.RemoveSuggestionsRow(r));

            ds.Suggestions.AcceptChanges();

            for (int i = 1; i < 6; i++)
            {
                SuggestionDS.SuggestionsRow row = ds.Suggestions.NewSuggestionsRow();
                row.Id = i;
                row.Key = inputRow.Key;
                row.ItemValue = "item_" + i.ToString();
                row.Suggested = 1;
                ds.Suggestions.AddSuggestionsRow(row);
                
            }

            ds.AcceptChanges();

            return ds;
        }

    }
}
