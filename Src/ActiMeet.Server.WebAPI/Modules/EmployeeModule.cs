﻿using ActiMeet.Server.Application.Features.Employees;
using MediatR;
using TS.Result;

namespace ActiMeet.Server.WebAPI.Modules;

public static class EmployeeModule
{
	public static void RegisterEmployeeRoutes(this IEndpointRouteBuilder app)
	{
		RouteGroupBuilder group = app.MapGroup("/employees").WithTags("Employees").RequireAuthorization();

		group.MapPost(string.Empty,
			async (ISender sender, EmployeeCreateCommand request, CancellationToken cancellationToken) =>
			{
				var response = await sender.Send(request, cancellationToken);
				return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
			})
			.Produces<Result<string>>();
	}
}
