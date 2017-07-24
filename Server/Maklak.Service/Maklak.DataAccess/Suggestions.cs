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
            // строка фильтра для заданного ключа
            SuggestionDS.SuggestionFilterRow fRow = inputDS.SuggestionFilter.Where(r => r.Key == key).FirstOrDefault();

            if (emptyInput)
            {
                if (fRow != null)
                    inputDS.SuggestionFilter.RemoveSuggestionFilterRow(fRow);  // удалить фильтр              
            }
            else
            {
                // ввод не пустой
                if (fRow == null)
                {
                    // добавить ключ если его ещё не было
                    fRow = inputDS.SuggestionFilter.NewSuggestionFilterRow();
                    inputDS.SuggestionFilter.AddSuggestionFilterRow(fRow);
                }
                // обновить данные фильтра
                fRow.Key = key;
                fRow.ItemValue = inputValue;                
            }

            inputDS.SuggestionFilter.AcceptChanges();

            SuggestionDS ds = GetRows(key);
            // найти Id для inputValue
            int filterId = ds.Suggestion.Where(r => r.ItemValue == inputValue).Select(r => r.Id).FirstOrDefault();

            if (!emptyInput && fRow!= null && filterId > 0)
            {
                fRow.Id = filterId; // установить Id
                inputDS.SuggestionFilter.AcceptChanges();

            }

            ds.Suggestion.Where(r => !emptyInput && r.ItemValue.Contains(inputValue) && !r.ItemValue.Equals(inputValue, StringComparison.InvariantCultureIgnoreCase)).ToList().ForEach(r => inputDS.Suggestion.ImportRow(r));
            
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
