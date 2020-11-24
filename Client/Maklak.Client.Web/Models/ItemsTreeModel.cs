using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;


namespace Maklak.Client.Web.Models
{
	public class ItemsTreeModel : ComponentBase
	{
		private FilterItemsDS itemsDS;

		public ItemsTreeModel()
		{
			itemsDS = new FilterItemsDS();
		}
		public FilterItemsDS.ItemsDataTable Items { get { return itemsDS.Items; } }
	}
}
