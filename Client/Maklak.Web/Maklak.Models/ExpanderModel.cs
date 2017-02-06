using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Models.DataSets;

namespace Maklak.Models
{
    public class ExpanderModel : BaseModel
    {
        public ExpanderModel()
        {
            this.OnModelInitialized += ExpanderModel_OnModelInitialized;
        }

        private void ExpanderModel_OnModelInitialized()
        {
            ModelDS.ExpanderDataRow row = this.data.ExpanderData.NewExpanderDataRow();
            row.Name = "Product";
            row.Action = "Product";
            row.Controller = "Search";
            this.data.ExpanderData.AddExpanderDataRow(row);

            row = this.data.ExpanderData.NewExpanderDataRow();
            row.Name = "Properties";
            row.Action = "Properties";
            row.Controller = "Search";
            this.data.ExpanderData.AddExpanderDataRow(row);

            row = this.data.ExpanderData.NewExpanderDataRow();
            row.Name = "Tags";
            row.Action = "Tags";
            row.Controller = "Search";
            this.data.ExpanderData.AddExpanderDataRow(row);

            this.data.ExpanderData.AcceptChanges();


        }

        protected override bool IsModelInitialized()
        {
            return base.IsModelInitialized() && this.data.ExpanderData.Count > 0;
        }

        public Maklak.Models.DataSets.ModelDS.ExpanderDataDataTable ExpanderData {
            get { return this.data.ExpanderData; }
        }
    }
}
