using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.Logging;

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

			if (request.Login == "1")
				response.IsAuthenticated = true;

			return Task.FromResult(response);
		}

		public override Task<RegisterResponse> RegisterUser(RegisterRequest request, ServerCallContext context)
		{
			RegisterResponse response = new RegisterResponse();
			response.IsAuthenticated = false;

			if (request.Login == "1")
				response.IsAuthenticated = true;

			return Task.FromResult(response);
		}
	}
}
