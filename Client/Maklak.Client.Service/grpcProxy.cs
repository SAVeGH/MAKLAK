using System;
using System.Collections.Generic;
using System.Text;
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

		public void Search(Maklak.Client.DataSets.FilterItemsDS filterData)
		{
			//SearchRequest request = new SearchRequest();
			////Dictionary<string, string> inputData = filterData.SearchInput.ToDictionary(keyField => keyField.InputName, valueField => valueField.InputValue);
			//request.SerchInput.AddRange(filterData.Input.Select(r => new SearchRequest.Types.InputData() { InputType = r.InputName, InputValue = r.InputValue }));
			//SearchResponse response = client.Search(request);

			//filterData.Items.Clear();

			//foreach (SearchResponse.Types.OutputData item in response.Items) 
			//{
			//	FilterItemsDS.ItemsRow row = filterData.Items.NewItemsRow();

			//	row.ItemId = item.ItemId;
			//	row.ItemValue = item.ItemValue;
			//	//row.Name = item.Name;

			//	filterData.Items.AddItemsRow(row);
			//}
			//return filterData;
		}

		public void Search(string itemType, string inputValue, Maklak.Client.DataSets.ItemsTreeDS itemsData)
		{
			SearchRequest request = new SearchRequest();
			//Dictionary<string, string> inputData = filterData.SearchInput.ToDictionary(keyField => keyField.InputName, valueField => valueField.InputValue);
			//request.SerchInput.AddRange(filterData.Input.Select(r => new SearchRequest.Types.InputData() { InputType = r.InputName, InputValue = r.InputValue }));

			request.InputType = itemType;
			request.InputValue = string.IsNullOrEmpty(inputValue) ? string.Empty : inputValue;

			SearchResponse response = client.Search(request);

			itemsData.Items.Clear();

			ItemsTreeDS.ItemsRow rootRow = itemsData.Items.NewItemsRow();
			rootRow.Id = int.MaxValue;        // не существующий Id
			rootRow.Parent_Id = int.MinValue; // вместо NULL
			rootRow.Name = "Root";
			itemsData.Items.AddItemsRow(rootRow);

			foreach (SearchResponse.Types.ItemsData item in response.Items)
			{
				ItemsTreeDS.ItemsRow row = itemsData.Items.NewItemsRow();

				row.Id = item.ItemId;
				row.Parent_Id = rootRow.Id;
				row.Name = item.ItemValue;
				//row.Name = item.Name;

				itemsData.Items.AddItemsRow(row);
			}
			//return filterData;
		}

		public int AddItem(string itemType, string itemValue) 
		{
			ItemRequest request = new ItemRequest() { ItemType = itemType, ItemValue = itemValue };
			ItemResponse response = client.AddItem(request);
			return response.Result;
		}
	}
}
