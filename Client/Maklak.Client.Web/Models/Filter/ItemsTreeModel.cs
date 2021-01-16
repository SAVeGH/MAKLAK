using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;


namespace Maklak.Client.Web.Models.Filter
{
	public class ItemsTreeModel : ComponentBase
	{
		

		[Inject]
		private grpcProxy serviceProxy { get; set; }
		

		[Parameter]
		public ItemsTreeDS.ItemsDataTable Items 
		{
			get; set;			
		}		
	}
}
