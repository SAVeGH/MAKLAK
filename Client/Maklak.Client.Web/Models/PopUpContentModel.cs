using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;

using Maklak.Client.Web.Controls.Filter;

namespace Maklak.Client.Web.Models
{
	public class PopUpContentModel : ComponentBase
	{
		[Inject]
		public PopUpStateModel PopUpState { get; set; }

		public RenderFragment Content { get { return new RenderFragment(x => { x.OpenComponent(1, typeof(ItemEditor)); x.CloseComponent(); }); } }

		//public void Show()
		//{
		//	Content = new RenderFragment(x => { x.OpenComponent(1, typeof(ItemEditor)); x.CloseComponent(); });
		//}

	}
}
