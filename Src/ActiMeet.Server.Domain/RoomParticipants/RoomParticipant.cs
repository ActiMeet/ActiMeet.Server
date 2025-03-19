using ActiMeet.Server.Domain.Abstractions;
using ActiMeet.Server.Domain.Rooms;
using ActiMeet.Server.Domain.Users;

namespace ActiMeet.Server.Domain.RoomParticipants;
public sealed class RoomParticipant : BaseEntity
{
	public Guid UserId { get; set; }
	public Guid RoomId { get; set; }
	public bool IsAdmin { get; set; } = false;

	// Navigation Props
	public AppUser User { get; set; } = default!;
	public Room Room { get; set; } = default!;
}
