using ActiMeet.Server.Application.Features.Employees;
using MediatR;
using TS.Result;

namespace ActiMeet.Server.WebAPI.Modules;

public static class EmployeeModule
{
	public static void RegisterEmployeeRoutes(this IEndpointRouteBuilder app)
	{
		RouteGroupBuilder group = app.MapGroup("/employees").WithTags("Employees").RequireAuthorization();

		group.MapGet("{id}",
			async (Guid id, ISender sender, CancellationToken cancellationToken) =>
			{
				var response = await sender.Send(new EmployeeGetQuery(id), cancellationToken);
				return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
			})
			.Produces<Result<EmployeeGetQueryResponse>>()
			.WithName("EmployeeGet");

		group.MapPost(string.Empty,
			async (ISender sender, EmployeeCreateCommand request, CancellationToken cancellationToken) =>
			{
				var response = await sender.Send(request, cancellationToken);
				return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
			})
			.Produces<Result<string>>()
			.WithName("EmployeeCreate");

		group.MapPut(string.Empty,
			async (ISender sender, EmployeeUpdateCommand request, CancellationToken cancellationToken) =>
			{
				var response = await sender.Send(request, cancellationToken);
				return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
			})
			.Produces<Result<string>>()
			.WithName("EmployeeUpdate");

		group.MapDelete("{id}",
			async (Guid id, ISender sender, CancellationToken cancellationToken) =>
			{
				var response = await sender.Send(new EmployeeDeleteCommand(id), cancellationToken);
				return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
			})
				.Produces<Result<string>>()
				.WithName("EmployeeDelete");
	}
}
