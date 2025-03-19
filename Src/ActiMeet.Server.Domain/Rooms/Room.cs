using ActiMeet.Server.Domain.Abstractions;
using ActiMeet.Server.Domain.Categories;
using ActiMeet.Server.Domain.RoomParticipants;
using ActiMeet.Server.Domain.Users;

namespace ActiMeet.Server.Domain.Rooms;
public sealed class Room : BaseEntity
{
	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;
	public Guid CreatedByUserId { get; set; }

	// Navigation Props
	public AppUser CreatedByUser { get; set; } = default!;
	public ICollection<RoomParticipant> RoomParticipants { get; set; } = new List<RoomParticipant>();
}
