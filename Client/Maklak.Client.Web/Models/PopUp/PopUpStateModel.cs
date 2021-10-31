using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models.PopUp
{
	public class PopUpStateModel 
	{
		private bool popUpState = false;

		public event Action OnRefresh;
		public event Action OnOpen;
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

			// подписка в PopUpScreenModel
			this.OnOpen?.Invoke();			
		}

		public void Close(bool isCancel = false) 
		{
			popUpState = false;

			if(!isCancel)
				this.OnClose?.Invoke(); // вызов только на Ok			

			CleanSubscriptions();

			InputParameters.Clear();

			// подписки в PopUpScreenModel и PopUpContentModel. Вызывается InvokeAsync(StateHasChanged) при закрытии popUp окна
			// нужно обязательно вызывать иначе popUp "зависает" - всплывает в состоянии предыдущего вызова.
			this.OnRefresh?.Invoke();


		}

		private void CleanSubscriptions() 
		{
			if (this.OnClose == null)
				return;

			// освобождает все подписки на событие
			Delegate[] closeSubscriptions = this.OnClose.GetInvocationList();

			foreach (Delegate closeDelegate in closeSubscriptions)			
				this.OnClose -= (Action)closeDelegate;			
		}
	}	

	public class PopUpInput
	{
		public enum ActionType { None, Add, Edit, Delete};
		
		ItemsTreeRowHelper rowHelper;

		public string FilterType;		
		public Type dialogType;
		public int Height;
		public int Width;
		public string Title;
		public ActionType PopUpAction = ActionType.None;

		public PopUpInput() 
		{			
			rowHelper = new ItemsTreeRowHelper(null);
		}

		public ItemsTreeDS.ItemsRow Row 
		{
			get 
			{
				return rowHelper.Row;
			}
		}

		

		public void SetDataRow(ItemsTreeDS.ItemsRow row) 
		{
			// делается копия строки (иначе Row out of table exception)
			rowHelper.Row = row;
			
		}


		public void Clear() 
		{		
			rowHelper.Clear();			
			FilterType = null;
			dialogType = null;
			
		}
	}

}
