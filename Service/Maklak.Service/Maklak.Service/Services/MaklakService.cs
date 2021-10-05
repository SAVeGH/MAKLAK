using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.Logging;
using Maklak.Service.Data;
using Maklak.Service.DataSets;

namespace Maklak.Service 
{
	public class MaklakService : Maklak.MaklakBase
	{
		private readonly ILogger<GreeterService> _logger;
		public MaklakService(ILogger<GreeterService> logger)
		{
			_logger = logger;
		}

		public override Task<HelloReplyExt> SayHello(HelloRequestExt request, ServerCallContext context)
		{
			return Task.FromResult(new HelloReplyExt
			{
				Message = "Hello " + request.Name
			});
		}

		public override Task<AuthenticateResponse> AuthenticateUser(AuthenticateRequest request, ServerCallContext context)
		{
			AuthenticateResponse response = new AuthenticateResponse();
			response.IsAuthenticated = false;
			
			response.IsAuthenticated = Auth.GetUser(request.Login, request.Password);

			return Task.FromResult(response);
		}

		public override Task<RegisterResponse> RegisterUser(RegisterRequest request, ServerCallContext context)
		{
			RegisterResponse response = new RegisterResponse();
			response.IsAuthenticated = false;

			response.IsAuthenticated = Auth.RegisterUser(request.Login, request.Password);

			return Task.FromResult(response);
		}

		public override Task<SearchResponse> Search(SearchRequest request, ServerCallContext context)		
		{			

			ItemsTreeDS ds = Items.GetItems(request.InputType, request.ItemId, request.ParentItemId,  request.InputValue);

			SearchResponse response = new SearchResponse();			

			foreach (ItemsTreeDS.ItemsRow row in ds.Items) 
			{
				SearchResponse.Types.ItemsData respData = new SearchResponse.Types.ItemsData();

				respData.ItemId = row.Id;
				respData.ParentId = row.IsParent_IdNull() ? null : (int?)row.Parent_Id;
				respData.ItemType = row.ItemType;
				respData.ItemValue = row.Name;
				respData.MeasureUnitId = row.IsMeasureUnit_IdNull() ? null : (int?)row.MeasureUnit_Id;
				respData.HasChildren = row.IsHasChildrenNull() ? null : (bool?)row.HasChildren;
				
				response.Items.Add(respData);
			}		

			return Task.FromResult(response);
		}

		public override Task<ItemResponse> AddItem(ItemRequest request, ServerCallContext context)
		{
			ItemResponse response = new ItemResponse();
			response.Result = Items.AddItem(request.ItemType, request.ItemId, request.MeasureUnitId, request.ItemValue);

			return Task.FromResult(response);
		}

		public override Task<ItemResponse> EditItem(ItemRequest request, ServerCallContext context)
		{
			ItemResponse response = new ItemResponse();
			response.Result = Items.EditItem(request.ItemType, (int)request.ItemId, request.ItemValue);

			return Task.FromResult(response);
		}

		public override Task<ItemResponse> DeleteItem(ItemRequest request, ServerCallContext context)
		{
			ItemResponse response = new ItemResponse();
			response.Result = Items.DeleteItem(request.ItemType, (int)request.ItemId);

			return Task.FromResult(response);
		}

		public override Task<LookupResponse> GetLookupItems(LookupRequest request, ServerCallContext context)
		{

			LookupDS ds = Items.GetLookupItems(request.LookupType);

			LookupResponse response = new LookupResponse();

			foreach (LookupDS.ItemsRow row in ds.Items)
			{
				LookupResponse.Types.ItemsData respData = new LookupResponse.Types.ItemsData();

				respData.ItemId = row.Id;				
				respData.ItemValue = row.Name;

				response.Items.Add(respData);
			}

			return Task.FromResult(response);
		}

	}
}
