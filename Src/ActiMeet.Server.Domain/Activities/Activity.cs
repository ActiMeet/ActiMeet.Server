using ActiMeet.Server.Domain.Abstractions;
using ActiMeet.Server.Domain.ActivityParticipants;
using ActiMeet.Server.Domain.Categories;
using ActiMeet.Server.Domain.Locations;
using ActiMeet.Server.Domain.Users;

namespace ActiMeet.Server.Domain.Activities;
public sealed class Activity : BaseEntity
{
	public string Title { get; set; } = default!;
	public string Description { get; set; } = default!;
	public DateTimeOffset ActivityDate { get; set; } = default!;
	public int MaxParticipants { get; set; } // Maksimum katılımcı
	public Guid CategoryId { get; set; }
	public Guid CreatedByUserId { get; set; }
	public Guid LocationId { get; set; }

	// Navigation Props
	public Category Category { get; set; } = default!;
	public AppUser CreatedByUser { get; set; } = default!;
	public Location Location { get; set; } = default!;
	public ICollection<ActivityParticipant> ActivityParticipants { get; set; } = new List<ActivityParticipant>();
}
