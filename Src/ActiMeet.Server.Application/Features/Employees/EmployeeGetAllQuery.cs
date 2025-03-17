using ActiMeet.Server.Application.Interfaces.Repositories;
using ActiMeet.Server.Application.Interfaces.UnitOfWorks;
using ActiMeet.Server.Domain.Abstractions;
using ActiMeet.Server.Domain.Employees;
using ActiMeet.Server.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ActiMeet.Server.Application.Features.Employees;

public sealed class EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

public sealed class EmployeeGetAllQueryResponse : BaseEntityDto
{
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public DateOnly BirthOfDate { get; set; }
	public decimal Salary { get; set; }
	public string IdentityNumber { get; set; } = default!;
}

internal sealed class EmployeeGetAllQueryHandler(
	IUnitOfWork unitOfWork,
	UserManager<AppUser> userManager) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
{
	public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
	{
		var response = (from employee in unitOfWork.GetReadRepository<Employee>().GetAll()
						join create_user in userManager.Users.AsQueryable() on employee.CreateUserId equals create_user.Id
						join update_user in userManager.Users.AsQueryable() on employee.UpdateUserId equals update_user.Id
						into update_user
						from update_users in update_user.DefaultIfEmpty()
						join delete_user in userManager.Users.AsQueryable() on employee.DeleteUserId equals delete_user.Id
						into delete_user
						from delete_users in delete_user.DefaultIfEmpty()
						select new EmployeeGetAllQueryResponse
						{
							Id = employee.Id,
							FirstName = employee.FirstName,
							LastName = employee.LastName,
							BirthOfDate = employee.BirthOfDate,
							Salary = employee.Salary,
							IdentityNumber = employee.PersonnelInformation.IdentityNumber,
							CreateAt = employee.CreateAt,
							DeleteAt = employee.DeleteAt,
							UpdateAt = employee.UpdateAt,
							IsDeleted = employee.IsDeleted,
							CreateUserId = employee.CreateUserId,
							CreateUserName = create_user.FirstName + " " + create_user.LastName + " (" + create_user.Email + ")",
							UpdateUserId = employee.UpdateUserId,
							UpdateUserName = employee.UpdateUserId == null ? null : update_users.FirstName + " " + update_users.LastName + " (" + update_users.Email + ")",
							DeleteUserId = employee.DeleteUserId,
							DeleteUserName = employee.DeleteUserId == null ? null : delete_users.FirstName + " " + delete_users.LastName + " (" + delete_users.Email + ")",
						});


		return Task.FromResult(response);
	}
}