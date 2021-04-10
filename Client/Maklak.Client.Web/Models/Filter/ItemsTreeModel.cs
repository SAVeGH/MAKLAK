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
		[Parameter]
		public Action<int> OnNodeToggled { get; set; } // сюда передается функция обънкта который требует уведомления о клике expand/close (функция конретного фильтра)

		//[Inject]
		//private grpcProxy serviceProxy { get; set; }
		

		[Parameter]
		public ItemsTreeDS.ItemsDataTable Items 
		{
			get; set;			
		}

		// эта функция предаётся в делегат верхнего узла ItemsTreeNode (и далее в дочерние узлы) и вызывается на клик expand/close
		public void OnNodeToggle(int itemId) 
		{
			OnNodeToggled?.Invoke(itemId); // вызов уведомления внешнего объекта по клику expand/close на узле
		}
	}
}
