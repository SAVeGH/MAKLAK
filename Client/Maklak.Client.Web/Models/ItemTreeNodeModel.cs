using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase
using Maklak.Client.Service;
using Maklak.Client.DataSets;


namespace Maklak.Client.Web.Models
{
	public class ItemTreeNodeModel : ComponentBase
	{
		//[Parameter]
		public IEnumerable<ItemsTreeDS.ItemsRow> Items 
		{ 
			get 
			{
				// что бы не работать с DBNull использую int.MinValue
				int parentId = ParentRow == null ? int.MinValue : ParentRow.Id;
				return ItemsSource.Where(i => i.Parent_Id == parentId);
			}
			//set { }
		}

		[Parameter]
		public ItemsTreeDS.ItemsDataTable ItemsSource { get; set; }

		[Parameter]
		public ItemsTreeDS.ItemsRow ParentRow { get; set; }

		//[Parameter]
		//public bool ShowRoot { get; set; }


	}
}
