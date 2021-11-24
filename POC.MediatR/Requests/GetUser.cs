using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using MediatR;
using POC.MediatR.DTOs;

namespace POC.MediatR.Requests
{
	public static class GetUser
	{
		public record Request(ulong UserId) : IRequest<UserDTO?>;
		
		internal class RequestHandler : IRequestHandler<Request, UserDTO?>
		{
			private readonly DiscordRestClient _client;
			
			public RequestHandler(DiscordRestClient client)
            {
                _client = client;
            }
			
			public async Task<UserDTO?> Handle(Request request, CancellationToken cancellationToken)
			{
				UserDTO? returnDTO = default;

				try
				{
					var user = await _client.GetUserAsync(request.UserId);

					returnDTO = new(user);
					return returnDTO;
				}
				catch
				{
					return returnDTO;
				}
			}
		}
	}
}