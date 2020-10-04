using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.Logging;
using Maklak.Service.Data;

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
			//return base.Search(request, context);

			SearchResponse response = new SearchResponse();

			SearchResponse.Types.OutputData respData = new SearchResponse.Types.OutputData() { ItemId = 1, ItemValue = "Value 1", Name = request.SerchInput[0].InputType };

			response.Items.Add(respData);

			return Task.FromResult(response);
		}
	}
}
