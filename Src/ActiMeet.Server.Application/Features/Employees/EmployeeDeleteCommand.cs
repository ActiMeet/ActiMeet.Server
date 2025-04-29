using ActiMeet.Server.Application.Interfaces.UnitOfWorks;
using ActiMeet.Server.Domain.Employees;
using MediatR;
using TS.Result;

namespace ActiMeet.Server.Application.Features.Employees;
public sealed record EmployeeDeleteCommand(
	Guid Id) : IRequest<Result<string>>;

internal sealed class EmployeeDeleteCommandHandler(
	IUnitOfWork unitOfWork) : IRequestHandler<EmployeeDeleteCommand, Result<string>>
{
	public async Task<Result<string>> Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
	{
		Employee? employee = await unitOfWork.GetReadRepository<Employee>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

		if (employee is null)
			return
				Result<string>.Failure("Çalışan bulunamadı");

		employee.IsDeleted = true;

		unitOfWork.GetWriteRepository<Employee>().Update(employee);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		return
			"Çalışan başarıyla silindi.";
	}
}
