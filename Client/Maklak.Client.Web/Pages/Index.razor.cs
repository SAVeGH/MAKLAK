using Maklak.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace Maklak.Client.Web.Pages
{
	public partial class Index
	{
		[Inject]
		public StateModel StateStorage { get; set; }


		//protected override void OnInitialized()
		//{
		//	base.OnInitialized();

		//	//if (string.IsNullOrEmpty(SID))
		//	//{
		//	//	System.Guid.NewGuid().ToString();
		//	//	StateStorage.SID = System.Guid.NewGuid().ToString();
		//	//	SID = StateStorage.SID;
		//	//}
		//}

		//protected override void OnParametersSet()
		//{
		//	base.OnParametersSet();

		//	if (string.IsNullOrEmpty(SID))
		//	{
		//		System.Guid.NewGuid().ToString();
		//		StateStorage.SID = System.Guid.NewGuid().ToString();
		//		SID = StateStorage.SID;
		//	}
		//}

		////protected override Task OnAfterRenderAsync(bool firstRender)
		////{
		////	return base.OnAfterRenderAsync(firstRender);
		////}
		//[Parameter]
		//public string SID 
		//{
		//	get; 			
		//	set;
		//}


	}
}
