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

            ModelDS.SuggestionInputRow inputRow = modelDS.SuggestionInput.FirstOrDefault();
            inputRow.SetIdNull();
            inputRow.AcceptChanges();

            inputDS.SuggestionInput.ImportRow(inputRow);

            modelDS.SuggestionFilter.AsEnumerable().ToList().ForEach(r => inputDS.SuggestionFilter.ImportRow(r));              

            Proxy.DataSourceServiceReference.SuggestionDS  outputDS = dataSource.MakeSuggestion(inputDS);

            Proxy.DataSourceServiceReference.SuggestionDS.SuggestionInputRow outRow = outputDS.SuggestionInput.FirstOrDefault();

            if (!outRow.IsIdNull())
                inputRow.Id = outRow.Id;

            modelDS.Suggestion.Clear();
                        
            outputDS.Suggestion.AsEnumerable().ToList().ForEach(r => modelDS.Suggestion.ImportRow(r));

            modelDS.AcceptChanges();

        }
    }
}
