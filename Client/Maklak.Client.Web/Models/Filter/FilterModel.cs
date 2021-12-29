using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase
using Maklak.Client.Service;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models.Filter
{
	
	public class FilterModel : ComponentBase // component inherits from this class. To do it  - class must be inherited from ComponentBase
	{
		private ItemsTreeDS checkedItemsDS;

		public FilterModel() 
		{
			checkedItemsDS = new ItemsTreeDS();
		}

		public ItemsTreeDS.ItemsDataTable CheckedItems
		{
			get
			{
				return checkedItemsDS.Items;
			}
		}

	}
}
