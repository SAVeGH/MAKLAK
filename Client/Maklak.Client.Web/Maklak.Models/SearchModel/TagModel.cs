using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    public class TagModel : BaseModel
    {
        public TagModel()
        {
            base.OnModelReady += TagModel_OnModelReady;
        }

        private void TagModel_OnModelReady()
        {
            UpdateTags();
        }

        private void UpdateTags()
        {
            DataSets.ModelDS.SelectionRow sRow = base.data.Selection.Where(r => r.Key == "TAG").FirstOrDefault();            

            if (sRow == null)
                return;

            DataSets.ModelDS.TagsRow row = base.data.Tags.NewTagsRow();
            row.Tag_Id = sRow.Item_Id;
            row.TagName = sRow.Key;
            base.data.Tags.Rows.Add(row);
            base.data.Tags.AcceptChanges();

            sRow.Delete();
            base.data.Selection.AcceptChanges();

        }

        public DataSets.ModelDS.TagsDataTable Tags
        {
            get
            {
                return base.data.Tags;
            }
        }
    }
}
