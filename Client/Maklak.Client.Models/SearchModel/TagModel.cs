using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Client.DataSets;

namespace Maklak.Client.Models
{
    public class TagModel : SuggestionModel
    {
        int? filterId;

        public TagModel()
        {
            base.OnModelReady += TagModel_OnModelReady;
        }

        private void TagModel_OnModelReady()
        {
            //UpdateTags();
        }

        private void UpdateTags()
        {
            if (FILTERID == null)
                return;

            if (FILTERID == 0)
                AddFilter();
            else
                DeleteFilter();

           
        }

        private void DeleteFilter()
        {
            int filterIdValue = this.FILTERID ?? 0;

            ModelDS.SuggestionFilterRow filterRow = this.data.SuggestionFilter.Where(r => r.Key == "TAG" && r.Id == filterIdValue).FirstOrDefault();

            if (filterRow == null)
                return;

            this.data.SuggestionFilter.RemoveSuggestionFilterRow(filterRow);
            this.data.SuggestionFilter.AcceptChanges();
        }

        private void AddFilter()
        {
            ModelDS.SuggestionInputRow row = this.data.SuggestionInput.Where(r => r.Key == "TAG" && !r.IsIdNull()).FirstOrDefault();

            if (row == null)
                return;

            ModelDS.SuggestionFilterRow filterRow = this.data.SuggestionFilter.Where(r => r.Key == "TAG" && r.Id == row.Id).FirstOrDefault();

            if (filterRow != null)
                return;

            filterRow = this.data.SuggestionFilter.NewSuggestionFilterRow();

            filterRow.Id = row.Id;
            filterRow.Key = row.Key;
            filterRow.ItemValue = row.ItemValue;

            this.data.SuggestionFilter.AddSuggestionFilterRow(filterRow);

            this.data.SuggestionFilter.AcceptChanges();

            this.data.SuggestionInput.RemoveSuggestionInputRow(row);
            this.data.SuggestionInput.AcceptChanges();
        }

        public int? FILTERID
        {
            get { return filterId; }
            set
            {
                filterId = value;

                UpdateTags();
            }
        }

        public DataSets.ModelDS.SuggestionFilterDataTable Tags
        {
            get
            {

                ModelDS.SuggestionFilterDataTable tagsTable = new ModelDS.SuggestionFilterDataTable();
                this.data.SuggestionFilter.Where(r => r.Key == "TAG").ToList().ForEach(r=> tagsTable.ImportRow(r));
                tagsTable.AcceptChanges();
                
                return tagsTable;
            }
        }
    }
}
