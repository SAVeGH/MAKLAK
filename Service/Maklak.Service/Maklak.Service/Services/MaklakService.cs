using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
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
	}
}
