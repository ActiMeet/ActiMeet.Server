namespace ActiMeet.Server.Domain.Abstractions;
public interface IBaseEntity
{
	Guid Id { get; set; }
	DateTimeOffset CreateAt { get; set; }
	Guid CreateUserId { get; set; }
	string CreateUserName { get; }
	DateTimeOffset? UpdateAt { get; set; }
	Guid? UpdateUserId { get; set; }
	string? UpdateUserName { get; }
	DateTimeOffset? DeleteAt { get; set; }
	Guid? DeleteUserId { get; set; }
	string? DeleteUserName { get; }
	bool IsDeleted { get; set; }
	bool IsActive { get; set; }

	string GetCreateUserName();
	string? GetUpdateUserName();
	string? GetDeleteUserName();
}
