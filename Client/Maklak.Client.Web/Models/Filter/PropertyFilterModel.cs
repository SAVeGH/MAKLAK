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
			popUpInput.PopUpAction = PopUpInput.ActionType.Add;

			popUpState.Show();
		}

		//public override string ItemsFilterType 
		//{ 
		//	get => base.ItemsFilterType; 
		//	set => base.ItemsFilterType = value; 
		//}

		//protected override void LoadItems(int? itemId = null)
		//{
		//	// синхронная загрузка
		//	base.serviceProxy.Search(ItemsFilterType, itemId, searchText, itemsDS, (itemId != null));

		//	//StateHasChanged();
		//	OnStateHasChanged?.Invoke();

		//}
	}
}
