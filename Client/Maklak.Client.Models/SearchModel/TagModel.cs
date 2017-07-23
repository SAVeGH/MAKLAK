using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Client.Models
{
    public class TagModel : SuggestionModel
    {
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
            //DataSets.ModelDS.SuggestionRow sRow = base.data.Suggestion.Where(r => r.Key == "TAG").FirstOrDefault();            

            //if (sRow == null)
            //    return;

            //DataSets.ModelDS.TagsRow row = base.data.Tags.NewTagsRow();
            //row.Tag_Id = sRow.Item_Id;
            //row.TagName = sRow.Key;
            //base.data.Tags.Rows.Add(row);
            //base.data.Tags.AcceptChanges();

            //sRow.Delete();
            //base.data.Suggestion.AcceptChanges();

        }

        

        public DataSets.ModelDS.SuggestionDataTable Tags
        {
            get
            {
                DataSets.ModelDS.SuggestionDataTable tagsTable = new DataSets.ModelDS.SuggestionDataTable();
                //tagsTable.ImportRow()
                //base.data.Suggestion.Where(r => r.Key == "TAG").ToList().ForEach(r => tagsTable.ImportRow(r));
                this.SuggestionData.ToList().ToList().ForEach(r => tagsTable.ImportRow(r));
                return tagsTable;
            }
        }
    }
}
