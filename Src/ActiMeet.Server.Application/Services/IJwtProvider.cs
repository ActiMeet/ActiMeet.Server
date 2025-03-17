using ActiMeet.Server.Domain.Users;

namespace ActiMeet.Server.Application.Services;
public interface IJwtProvider
{
	public Task<string> CreateTokenAsync(AppUser user, CancellationToken cancellationToken = default);
}
