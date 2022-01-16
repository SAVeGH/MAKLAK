using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase
using Maklak.Client.Service;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models.Content
{
	public class ManageContentModel : ComponentBase
	{
		public void OnAddClicked()
		{
			IsEditorVisible = true;
		}

		public void OnEditClicked()
		{

		}

		public void OnDeleteClicked()
		{

		}

		public bool IsEditorVisible { get; set; }
	}
}
