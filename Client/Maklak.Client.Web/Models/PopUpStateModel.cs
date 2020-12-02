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


		//public void Update() 
		//{
		//	this.OnUpdate?.Invoke();
		//}
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
}
