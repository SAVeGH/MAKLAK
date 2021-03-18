using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Maklak.Client.Service;
using Maklak.Client.DataSets;
using Maklak.Client.Web.Models.PopUp;

namespace Maklak.Client.Web.Models.Filter
{
	public class PropertyFilterModel : ItemsFilterBase
	{
		public PropertyFilterModel(PopUpStateModel popUpStateModel, grpcProxy srvProxy) : base(popUpStateModel, srvProxy) 
		{		

		}

		public override void AddItem()
		{
			PopUpInput popUpInput = popUpState.InputParameters;
			popUpInput.FilterType = this.ItemsFilterType;
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.PropertyEditor);
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Add";
			popUpInput.SetDataRow(base.CurrentItemRow);
			//popUpInput.Row = base.CurrentItemRow;
			//popUpInput.Id = base.CurrentItemRow == null ? null : (int?)base.CurrentItemRow.Id;
			//popUpInput.ParentId = base.CurrentItemRow == null ? null : (base.CurrentItemRow.IsParent_IdNull() ? null : (base.CurrentItemRow.Parent_Id == int.MaxValue ? null : (int?)base.CurrentItemRow.Parent_Id));

			popUpState.Show();
		}
	}
}
