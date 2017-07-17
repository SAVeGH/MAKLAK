using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Maklak.Client.Proxy;
using Maklak.Client.DataSets;

namespace Maklak.Client.Data
{
    public class DataSource
    {
        Proxy.DataSource dataSource;

        public DataSource()
        {
            dataSource = new Proxy.DataSource();
        }

        public void DoWork()
        {
            dataSource.DoWork();
        }

        public void MakeSuggestion(ModelDS modelDS)
        {
            Proxy.DataSourceServiceReference.SuggestionDS inputDS = new Proxy.DataSourceServiceReference.SuggestionDS();

            modelDS.Suggestion.Where(r=> (r.IsSuggestedNull() ? 0 : r.Suggested) != 1).ToList().ForEach(row => inputDS.Suggestions.ImportRow(row));            

            Proxy.DataSourceServiceReference.SuggestionDS  outputDS = dataSource.MakeSuggestion(inputDS);

            modelDS.Suggestion.Clear();

            outputDS.Suggestions.AsEnumerable().ToList().ForEach(r => modelDS.Suggestion.ImportRow(r));

            modelDS.Suggestion.AcceptChanges();

        }
    }
}
