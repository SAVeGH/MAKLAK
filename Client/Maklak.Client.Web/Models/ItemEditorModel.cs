using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;


namespace Maklak.Client.Web.Models
{
	public class ItemEditorModel : ComponentBase
	{
		[Parameter]
		public string FilterType { get; set; }


		[Parameter]
		public bool IsTreeMode { get; set; }
	}
}
