using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maklak.Client.Web.Models.PopUp
{
	public class PopUpStateModel
	{
		private bool popUpState = false;

		public event Action OnRefresh;
		public event Action OnClose;

		public PopUpStateModel() 
		{
			InputParameters = new PopUpInput();			
		}
		public PopUpInput InputParameters { get; private set; }		

		public bool IsVisible
		{
			get
			{
				return popUpState;
			}			
		}

		public void Show() 
		{
			popUpState = true;

			this.OnRefresh?.Invoke();
		}

		public void Close(bool isCancel = false) 
		{
			popUpState = false;

			if(!isCancel)
				this.OnClose?.Invoke();

			CleanSubscriptions();

			InputParameters.Clear();

			this.OnRefresh?.Invoke();
		}

		private void CleanSubscriptions() 
		{
			// освобождает все подписки на событие
			Delegate[] subscriptions = this.OnClose.GetInvocationList();

			foreach (Delegate closeDelegate in subscriptions)			
				this.OnClose -= (Action)closeDelegate;			
		}
	}	

	public class PopUpInput
	{
		public int? Id;		
		public string Value;
		public string FilterType;		
		public Type dialogType;
		public int Height;
		public int Width;
		public string Title;


		public void Clear() 
		{
			Id = null;			
			Value = null;
			FilterType = null;
			dialogType = null;
		}
	}

}
