using ActiMeet.Server.Domain.Activities;
using ActiMeet.Server.Domain.ActivityParticipants;
using ActiMeet.Server.Domain.RoomParticipants;
using ActiMeet.Server.Domain.Rooms;
using Microsoft.AspNetCore.Identity;

namespace ActiMeet.Server.Domain.Users;
public sealed class AppUser : IdentityUser<Guid>
{
	public AppUser()
	{
		Id = Guid.CreateVersion7();
	}
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public string FullName => $"{FirstName} {LastName}"; // computed property

	// Navigation Props
	public ICollection<Activity> Activities { get; set; } = new List<Activity>();
	public ICollection<ActivityParticipant> ActivityParticipants { get; set; } = new List<ActivityParticipant>();
	public ICollection<RoomParticipant> RoomParticipants { get; set; } = new List<RoomParticipant>();
	public ICollection<Room> Rooms { get; set; } = new List<Room>(); 

	#region Audit Log
	public DateTimeOffset CreateAt { get; set; }
	public Guid CreateUserId { get; set; } = default!;
	public DateTimeOffset? UpdateAt { get; set; }
	public Guid? UpdateUserId { get; set; }
	public DateTimeOffset? DeleteAt { get; set; }
	public Guid? DeleteUserId { get; set; }
	public bool IsDeleted { get; set; }
	#endregion

}
