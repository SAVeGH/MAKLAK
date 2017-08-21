using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SqlClient;

using Maklak.DataAccess.DataSets;

namespace Maklak.DataAccess
{
    public static class Suggestions
    {
        public static SuggestionDS Suggestion(SuggestionDS inputDS)
        {
            int result = DBTest();

            SuggestionDS.SuggestionInputRow inputRow = inputDS.SuggestionInput.FirstOrDefault();
            string key = inputRow.Key;
            string inputValue = inputRow.ItemValue;

            bool emptyInput = string.IsNullOrEmpty(inputValue) || string.IsNullOrWhiteSpace(inputValue);

            inputDS.Suggestion.Clear();
            inputDS.AcceptChanges();

            if (emptyInput)
                return inputDS;

            SuggestionDS ds = GetRows(key, inputDS);
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

        static SuggestionDS GetRows(string key, SuggestionDS inputDS)
        {
            SuggestionDS ds = new SuggestionDS();

            for (int i = 1; i < 6; i++)
            {
                string itemValue = "item_" + i.ToString();

                if (inputDS.SuggestionFilter.Where(r => r.Key == key && r.ItemValue.Equals(itemValue, StringComparison.InvariantCultureIgnoreCase)).Any())
                    continue;

                SuggestionDS.SuggestionRow row = ds.Suggestion.NewSuggestionRow();
                row.Id = i;
                row.Key = key;
                row.ItemValue = itemValue;
                ds.Suggestion.AddSuggestionRow(row);

            }

            ds.AcceptChanges();

            return ds;
        }

        public static int DBTest()
        {

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["srv"];

            using (SqlConnection conn = new SqlConnection(settings.ConnectionString))
            {
                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = "sp_DBTest";
                    conn.Open();
                    int result = (int)comm.ExecuteScalar();

                    return result;
                }
            }
        }

    }
}
