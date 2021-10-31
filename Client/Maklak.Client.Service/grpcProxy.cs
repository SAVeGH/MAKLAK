using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maklak.Service; // from proto file
using Grpc.Net.Client;
using System.Net.Http;

using Maklak.Client.DataSets;
using System.Linq;

namespace Maklak.Client.Service
{
	public class grpcProxy
	{
		Maklak.Service.Greeter.GreeterClient greetClient;

		Maklak.Service.Maklak.MaklakClient client;
		public grpcProxy() 
		{
			// See solution here: https://docs.microsoft.com/ru-ru/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.1
			var httpClientHandler = new HttpClientHandler();
			// Return `true` to allow certificates that are untrusted/invalid
			httpClientHandler.ServerCertificateCustomValidationCallback =
				HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var httpClient = new HttpClient(httpClientHandler);

			var channel = GrpcChannel.ForAddress("https://localhost:5001",
				new GrpcChannelOptions { HttpClient = httpClient });

			

			greetClient = new Maklak.Service.Greeter.GreeterClient(channel);

			client = new Maklak.Service.Maklak.MaklakClient(channel);

			//HelloRequest request = new HelloRequest();
			//request.Name = "Bob";
			//HelloReply reply = client.SayHello(request);

		}		

		public string SayHello(string name) 
		{
			HelloRequest request = new HelloRequest();
			request.Name = name;
			HelloReply reply = greetClient.SayHello(request);

			return reply.Message;
		}

		public string SayHelloExt(string name)
		{
			HelloRequestExt request = new HelloRequestExt();
			request.Name = name;
			HelloReplyExt reply = client.SayHello(request);

			return reply.Message;
		}

		public bool AuthenticateUser(string login, string password) 
		{
			AuthenticateRequest request = new AuthenticateRequest() { Login = login, Password = (password??string.Empty) };
			AuthenticateResponse response = client.AuthenticateUser(request);
			return response.IsAuthenticated;
		}

		public bool RegisterUser(string login, string password)
		{
			RegisterRequest request = new RegisterRequest() { Login = login, Password = (password ?? string.Empty) };
			RegisterResponse response = client.RegisterUser(request);
			return response.IsAuthenticated;
		}		

		public int DeleteItem(string itemType, int itemId)
		{
			ItemRequest request = new ItemRequest() { ItemType = itemType, ItemId = itemId };
			ItemResponse response = client.DeleteItem(request);
			return response.Result;
		}

		//public void CleanUp(int? itemId, ItemsTreeDS itemsData) 
		//{
		//	CleanUpNodes(itemId, itemsData);
		//}

		public Maklak.Client.DataSets.ItemsTreeDS Search(string itemType, ItemsTreeDS.ItemsRow row, string inputValue)
		{
			SearchRequest request = new SearchRequest();
			Maklak.Client.DataSets.ItemsTreeDS searchData = new ItemsTreeDS();

			request.ItemId = row == null || row.IsIdNull() ? null : (int?)row.Id; // ?? int.MaxValue;
			request.ParentItemId = row == null ? null : (row.IsParent_IdNull() ? null : (row.Parent_Id == int.MaxValue ? null : (int?)row.Parent_Id));
			request.InputType = itemType;
			request.InputValue = string.IsNullOrEmpty(inputValue) ? string.Empty : inputValue;

			SearchResponse response = client.Search(request);

			FillDataSet(response, searchData);

			return searchData;
		}

		private void FillDataSet(SearchResponse response, ItemsTreeDS itemsData)
		{
			//int rootRowId = (itemId ?? int.MaxValue);

			foreach (SearchResponse.Types.ItemsData item in response.Items)
			{


				ItemsTreeDS.ItemsRow row = itemsData.Items.NewItemsRow();

				row.Id = item.ItemId;// + (itemId == null ? 0 : 300) ;
				if (item.ParentId.HasValue)
					row.Parent_Id = (int)item.ParentId;
				row.Name = item.ItemValue;
				row.ItemType = item.ItemType;
				if (item.MeasureUnitId.HasValue)
					row.MeasureUnit_Id = (int)item.MeasureUnitId;
				if (item.HasChildren.HasValue)
					row.HasChildren = (bool)item.HasChildren;

				itemsData.Items.AddItemsRow(row);
			}
		}

		//public void Search(string itemType, int? itemId, string inputValue, Maklak.Client.DataSets.ItemsTreeDS itemsData)
		//{
		//	SearchRequest request = new SearchRequest();

		//	request.ItemId = itemId; // ?? int.MaxValue;
		//	request.InputType = itemType;
		//	request.InputValue = string.IsNullOrEmpty(inputValue) ? string.Empty : inputValue;

		//	SearchResponse response = client.Search(request);

		//	CleanUpNodes(itemId, itemsData);

		//	AddRootRow(itemsData);			

		//	AddChildNodes(itemId, response, itemsData);
		//}

		//private void AddChildNodes(int? itemId, SearchResponse response, ItemsTreeDS itemsData) 
		//{
		//	int rootRowId = (itemId ?? int.MaxValue);

		//	foreach (SearchResponse.Types.ItemsData item in response.Items)
		//	{
				

		//		ItemsTreeDS.ItemsRow row = itemsData.Items.NewItemsRow();

		//		row.Id = item.ItemId;// + (itemId == null ? 0 : 300) ;
		//		row.Parent_Id = rootRowId;
		//		row.Name = item.ItemValue;
		//		row.ItemType = item.ItemType;
		//		if (item.MeasureUnitId.HasValue)
		//			row.MeasureUnit_Id = (int)item.MeasureUnitId;
		//		if (item.HasChildren.HasValue)
		//			row.HasChildren = (bool)item.HasChildren;				

		//		itemsData.Items.AddItemsRow(row);
		//	}
		//}

		//private void AddRootRow(ItemsTreeDS itemsData) 
		//{
		//	ItemsTreeDS.ItemsRow rootRow = itemsData.Items.FirstOrDefault(item => item.Id == int.MaxValue);

		//	if (rootRow != null)
		//		return;

		//	rootRow = itemsData.Items.NewItemsRow();
		//	rootRow.Id = int.MaxValue;        // не существующий Id
										   
		//	rootRow.SetParent_IdNull();			
		//	rootRow.Name = "Root";
		//	rootRow.ItemType = "Root";
		//	itemsData.Items.AddItemsRow(rootRow);
		//}

		//private void CleanUpNodes(int? itemId, ItemsTreeDS itemsData) 
		//{
		//	if (itemId == null)
		//	{
		//		itemsData.Items.Clear();
		//		return; // перезапись всех узлов
		//	}

		//	ItemsTreeDS.ItemsRow rootRow = itemsData.Items.FirstOrDefault(item => item.Id == itemId);
		//	// удаление всех дочерних узлов
		//	foreach (var row in itemsData.Items.Where(item => !item.IsParent_IdNull() && item.Parent_Id == rootRow.Id))
		//		CleanUpChildNodes(row.Id, itemsData);			

		//}

		//private void CleanUpChildNodes(int? itemId, ItemsTreeDS itemsData) 
		//{
		//	if (itemId == null)
		//		return;

		//	ItemsTreeDS.ItemsRow rootRow = itemsData.Items.FirstOrDefault(item => item.Id == itemId);			

		//	foreach (var row in itemsData.Items.Where(item => !item.IsParent_IdNull() && item.Parent_Id == rootRow.Id))			
		//		CleanUpChildNodes(row.Id, itemsData);			

		//	itemsData.Items.RemoveItemsRow(rootRow);
		//}


		public async Task SearchAsync(string itemType, int? itemId, string inputValue, Maklak.Client.DataSets.ItemsTreeDS itemsData)
		{
			SearchRequest request = new SearchRequest();
			//Dictionary<string, string> inputData = filterData.SearchInput.ToDictionary(keyField => keyField.InputName, valueField => valueField.InputValue);
			//request.SerchInput.AddRange(filterData.Input.Select(r => new SearchRequest.Types.InputData() { InputType = r.InputName, InputValue = r.InputValue }));

			request.ItemId = itemId; //?? int.MaxValue;
			request.InputType = itemType;			
			request.InputValue = string.IsNullOrEmpty(inputValue) ? string.Empty : inputValue;

			SearchResponse response = await client.SearchAsync(request);

			itemsData.Items.Clear();

			ItemsTreeDS.ItemsRow rootRow = itemsData.Items.NewItemsRow();
			rootRow.Id = int.MaxValue;        // не существующий Id для самого верхнего узла	
			rootRow.SetParent_IdNull();
			rootRow.Name = "Root";
			itemsData.Items.AddItemsRow(rootRow);

			foreach (SearchResponse.Types.ItemsData item in response.Items)
			{
				ItemsTreeDS.ItemsRow row = itemsData.Items.NewItemsRow();

				row.Id = item.ItemId;
				row.Parent_Id = rootRow.Id;
				row.Name = item.ItemValue;				

				itemsData.Items.AddItemsRow(row);
			}
			
		}

		public int EditItem(string itemType, ItemsTreeDS.ItemsRow row)
		{
			ItemRequest request = new ItemRequest() { ItemType = itemType, ItemId = row.Id , ItemValue = row.Name };
			ItemResponse response = client.EditItem(request);
			return response.Result;
		}

		public int AddItem(string itemType, ItemsTreeDS.ItemsRow row) 
		{
			ItemRequest request = new ItemRequest() { ItemType = itemType, ItemValue = row.Name };
			ItemResponse response = client.AddItem(request);
			return response.Result;
		}

		public int AddPropertyItem(ItemsTreeDS.ItemsRow row, int measureUnitId, string itemValue)
		{
			ItemRequest request = new ItemRequest() { ItemType = row.ItemType, ItemId = row.Id, MeasureUnitId = measureUnitId, ItemValue = itemValue };
			ItemResponse response = client.AddItem(request);
			return response.Result;			
		}

		public int EditPropertyItem(ItemsTreeDS.ItemsRow row, string itemValue)
		{
			ItemRequest request = new ItemRequest() { ItemType = row.ItemType, ItemId = row.Id , ItemValue = itemValue };
			ItemResponse response = client.EditItem(request);
			return response.Result;
		}

		public void GetLookupItems(string lookupType, Maklak.Client.DataSets.LookupDS itemsData)
		{
			LookupRequest request = new LookupRequest();

			request.LookupType = lookupType;

			LookupResponse response = client.GetLookupItems(request);

			itemsData.Items.Clear();			

			foreach (LookupResponse.Types.ItemsData item in response.Items)
			{
				LookupDS.ItemsRow row = itemsData.Items.NewItemsRow();

				row.Id = item.ItemId;				
				row.Name = item.ItemValue;				

				itemsData.Items.AddItemsRow(row);
			}
			
		}
	}
}
