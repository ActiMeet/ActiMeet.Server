using ActiMeet.Server.Domain.Abstractions;
using ActiMeet.Server.Domain.Activities;
using ActiMeet.Server.Domain.Users;

namespace ActiMeet.Server.Domain.ActivityParticipants;
public sealed class ActivityParticipant : BaseEntity
{
	public Guid UserId { get; set; }
	public Guid ActivityId { get; set; }
	public DateTimeOffset JoinedAt { get; set; } = DateTimeOffset.UtcNow;

	// Navigation Props
	public AppUser User { get; set; } = default!;
	public Activity Activity { get; set; } = default!;
}
