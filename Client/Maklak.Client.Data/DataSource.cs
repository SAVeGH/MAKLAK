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

            inputDS.SuggestionInput.ImportRow(modelDS.SuggestionInput.FirstOrDefault());

            modelDS.SuggestionFilter.AsEnumerable().ToList().ForEach(r => inputDS.SuggestionFilter.ImportRow(r));       

            Proxy.DataSourceServiceReference.SuggestionDS  outputDS = dataSource.MakeSuggestion(inputDS);

            modelDS.SuggestionFilter.Clear();
            modelDS.Suggestion.Clear();

            outputDS.SuggestionFilter.AsEnumerable().ToList().ForEach(r=> modelDS.SuggestionFilter.ImportRow(r));
            outputDS.Suggestion.AsEnumerable().ToList().ForEach(r => modelDS.Suggestion.ImportRow(r));

            modelDS.AcceptChanges();

        }
    }
}
