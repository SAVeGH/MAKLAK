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
		public event Action OnClose;

		public PopUpStateModel() 
		{
			InputParameters = new PopUpInput();
			OutputParameters = new PopUpOutput();
		}
		public PopUpInput InputParameters { get; private set; }
		public PopUpOutput OutputParameters { get; private set; }
		public bool IsVisible 
		{ 
			get 
			{ 
				return popUpState; 
			}
			set 
			{
				popUpState = value;

				if (!popUpState)
				{
					this.OnClose?.Invoke();
					InputParameters.Clear();

				}

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

	public class PopUpInput//<T> where T: ComponentBase
	{
		public int Id;
		public int? ParentId;
		public string Value;
		public string FilterType;
		//public Microsoft.AspNetCore.Components.IComponent dialogType;
		public Type dialogType;

		public void Clear() 
		{
			Id = int.MinValue;
			ParentId = null;
			Value = null;
			FilterType = null;
			dialogType = null;
		}
	}

}
