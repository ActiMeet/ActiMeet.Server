using ActiMeet.Server.Application.Interfaces.UnitOfWorks;
using ActiMeet.Server.Domain.Employees;
using FluentValidation;
using Mapster;
using MediatR;
using TS.Result;

namespace ActiMeet.Server.Application.Features.Employees;
public sealed record EmployeeUpdateCommand(
	Guid Id,
	string FirstName,
	string LastName,
	DateOnly BirthOfDate,
	decimal Salary,
	PersonnelInformation PersonnelInformation,
	Address? Address,
	bool IsActive) : IRequest<Result<string>>;

public sealed class EmployeeUpdateCommandValidator : AbstractValidator<EmployeeUpdateCommand>
{
	public EmployeeUpdateCommandValidator()
	{
		RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır!");
		RuleFor(x => x.LastName).MinimumLength(3).WithMessage("Soyad alanı en az 3 karakter olmalıdır!");
		RuleFor(x => x.PersonnelInformation.IdentityNumber)
			.MinimumLength(11).WithMessage("Geçerli bir TC Numarası girin!")
			.MaximumLength(11).WithMessage("Geçerli bir TC Numarası girin!");
	}
}

internal sealed class EmployeeUpdateCommandHandler(
	IUnitOfWork unitOfWork) : IRequestHandler<EmployeeUpdateCommand, Result<string>>
{
	public async Task<Result<string>> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
	{
		Employee? employee = await unitOfWork.GetReadRepository<Employee>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

		if (employee is null)
			return
				Result<string>.Failure("Çalışan bulunamadı");

		var isEmployeeExists = await unitOfWork.GetReadRepository<Employee>().AnyAsync(p => p.PersonnelInformation.IdentityNumber == request.PersonnelInformation.IdentityNumber, cancellationToken);

		if (isEmployeeExists)
			return 
				Result<string>.Failure("Bu TC numarası daha önce kaydedilmiş");


		request.Adapt(employee);

		unitOfWork.GetWriteRepository<Employee>().Update(employee);

		await unitOfWork.SaveChangesAsync(cancellationToken);

		// todo: mail veya sms gönderme işlemleri yapılacak.
		// todo: notification içinde domain event kullanabilirim.

		return "Çalışan bilgileri başarıyla güncellendi!";
	}
}
