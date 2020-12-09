using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maklak.Client.Web.Models
{
	public class PopUpStateModel
	{
		private bool popUpState = false;

		public event Action OnRefresh;


		public object InputParameters { get; set; }
		public object OutputParameters { get; set; }
		public bool IsVisible 
		{ 
			get 
			{ 
				return popUpState; 
			}
			set 
			{
				popUpState = value;
				this.OnRefresh?.Invoke();
			} 
		}
	}

	public class PopUpOutput
	{
		public int Id;
		public int? ParentId;
		public string Value;
		public string FilterType;
	}

	public class PopUpInput
	{
		public int Id;
		public int? ParentId;
		public string Value;
		public string FilterType;
	}

}
