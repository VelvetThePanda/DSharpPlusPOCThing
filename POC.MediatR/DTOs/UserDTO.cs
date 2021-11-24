using DSharpPlus.Entities;

namespace POC.MediatR.DTOs
{
	public readonly record struct UserDTO
	{
		public ulong Id { get; init; }
		public string Username { get; init; }
		
		internal UserDTO(DiscordUser user)
        {
	        Id = user.Id;
            Username = user.Username;
        }
	}
}