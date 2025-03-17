using ActiMeet.Server.Domain.Abstractions;

namespace ActiMeet.Server.Domain.Employees;
public sealed class Employee : BaseEntity
{
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public string FullName => string.Join(" ", FirstName, LastName);
	public DateOnly BirthOfDate { get; set; }
	public decimal Salary { get; set; }
	public PersonnelInformation PersonnelInformation { get; set; } = default!;
	public Address Address { get; set; } = default!;
}
