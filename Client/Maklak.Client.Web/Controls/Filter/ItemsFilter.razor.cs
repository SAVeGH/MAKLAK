using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for [Parameter] attribute

namespace Maklak.Client.Web.Controls.Filter
{
	public partial class ItemsFilter
	{
		[Parameter]
		public string FilterType { get; set; }

		private void Search_OnInput() 
		{

			List<string> list = this.SearchList.Where(s => s == this.SearchText).ToList(); //new List<string>() { "3", "4", "5" };

			this.SearchList = list;

			//this.SearchText = "123";
		}
	}
}
