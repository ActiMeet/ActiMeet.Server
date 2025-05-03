using ActiMeet.Server.Application.Interfaces.UnitOfWorks;
using ActiMeet.Server.Domain.Abstractions;
using ActiMeet.Server.Domain.Employees;
using MediatR;
using TS.Result;

namespace ActiMeet.Server.Application.Features.Employees;
public sealed record EmployeeGetQuery(
	Guid Id) : IRequest<Result<EmployeeGetQueryResponse>>;

public sealed class EmployeeGetQueryResponse : BaseEntityDto
{
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public DateOnly BirthOfDate { get; set; }
	public decimal Salary { get; set; }
	public string IdentityNumber { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string Phone1 { get; set; } = default!;
}

internal sealed class EmployeeGetQueryHandler(
	IUnitOfWork unitOfWork) : IRequestHandler<EmployeeGetQuery, Result<EmployeeGetQueryResponse>>
{
	public async Task<Result<EmployeeGetQueryResponse>> Handle(EmployeeGetQuery request, CancellationToken cancellationToken)
	{
		Employee? employee = await unitOfWork.GetReadRepository<Employee>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

		if (employee is null)
			return Result<EmployeeGetQueryResponse>.Failure("Çalışan bulunamadı.");

		var response = new EmployeeGetQueryResponse
		{
			Id = employee.Id,
			FirstName = employee.FirstName,
			LastName = employee.LastName,
			BirthOfDate = employee.BirthOfDate,
			Salary = employee.Salary,
			IdentityNumber = employee.PersonnelInformation.IdentityNumber,
			Email = employee.PersonnelInformation.Email ?? string.Empty,
			Phone1 = employee.PersonnelInformation.Phone1 ?? string.Empty,
			CreateAt = employee.CreateAt,
			UpdateAt = employee.UpdateAt,
			DeleteAt = employee.DeleteAt,
			IsDeleted = employee.IsDeleted,
			CreateUserId = employee.CreateUserId,
			UpdateUserId = employee.UpdateUserId,
			DeleteUserId = employee.DeleteUserId
		};

		return Result<EmployeeGetQueryResponse>.Succeed(response);
	}
}
