using ActiMeet.Server.Application.Auth;
using MediatR;
using TS.Result;

namespace ActiMeet.Server.WebAPI.Modules;

public static class AuthModule
{
	public static void RegisterAuthRoutes(this IEndpointRouteBuilder app)
	{
		RouteGroupBuilder group = app.MapGroup("/auth").WithTags("Auth");

		group.MapPost("login",
			async (ISender sender, LoginCommand request, CancellationToken cancellationToken) =>
			{
				var response = await sender.Send(request, cancellationToken);

				if (response.IsSuccessful)
					return Results.Ok(response);

				return Results.Json(response, statusCode: StatusCodes.Status401Unauthorized);

			})
			.Produces<Result<LoginCommandResponse>>();
	}
}
