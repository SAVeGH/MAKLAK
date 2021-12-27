﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;
using Maklak.Client.Web.Models.PopUp;

namespace Maklak.Client.Web.Models.Filter
{
	public class ItemsFilterModel : ComponentBase
	{

		// так как разметка унаследована от этого класса, а обработка Add,Edit,Del своя для каждого лукапа - сделан класс с виртуальными методами куда 
		// передаётся обработка событий. Иначе нужно дублировать разметку для лукапов 
		// Например можно сделать модель PropertiesFilterModel : ItemsFilterModel, но придется делать отдельную разметку (копию ItemsFilter.razor) для PropertiesFilter что бы от неё унаследоваться
		ItemsFilterBase iFilter;		

		//[Inject]
		//public StateModel StateStorage { get; set; }

		[Inject]
		private grpcProxy serviceProxy { get; set; }

		[Inject]
		public PopUpStateModel popUpState { get; set; }


		public ItemsFilterModel() 
		{
			this.IsEditable = true; // по умолчанию - true
		}


		[Parameter]
		public string SearchText
		{
			get { return iFilter == null ? null : iFilter.SearchText; }
			set // устанавливается на oninput
			{
				if (iFilter == null)
					return;

				iFilter.SearchText = value;
				
			}
		}	
		
		// устанавливается 1 раз после инициализации (после вызова конструктора)
		[Parameter]
		public string ItemsFilterType
		{
			get;
			set;
		}		

		// устанавливается 1 раз после инициализации (после вызова конструктора)
		[Parameter]
		public bool IsEditable
		{
			get;
			set;
		}		

		public ItemsTreeDS.ItemsDataTable Items
		{
			get
			{				
				return iFilter == null ? null : iFilter.Items;
			}
		}

		[Parameter]
		public ItemsTreeDS.ItemsDataTable CheckedItems
		{
			get; set;
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			CreateManagementInstance();

			iFilter.ItemsFilterType = this.ItemsFilterType;
			iFilter.IsEditable = this.IsEditable;

			iFilter.OnInitialized(); // загрузка данных

			iFilter.OnStateHasChanged += this.InvokeStateHasChangedAsync; // объект управления должен вызвать обновление асинхронно
			
		}

		private void InvokeStateHasChangedAsync() 
		{
			this.InvokeAsync(this.StateHasChanged); // нужно вызывать только асинхроно
		}


		private void CreateManagementInstance() 
		{
			switch (this.ItemsFilterType) 
			{
				case "Property":
					iFilter = new PropertyFilterModel(popUpState, serviceProxy);
					break;
				case "Category":
					iFilter = new CategoryFilterModel(popUpState, serviceProxy);
					break;
				default:
					iFilter = new ItemsFilterBase(popUpState, serviceProxy);
					break;

			}
		}

		public void OnAdd()
		{
			iFilter.AddItem();			
		}		
		public void OnEdit()
		{
			iFilter.EditItem();			
		}		
		public void OnDelete() 
		{
			iFilter.DeleteItem();			
		}

		// вызов обновления Items по клику expand/close на узле
		public void OnNodeToggled(ItemsTreeDS.ItemsRow row)
		{
			iFilter.Toggle(row);
		}
	}
}
