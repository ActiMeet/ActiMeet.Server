using ActiMeet.Server.Domain.Abstractions;
using ActiMeet.Server.Domain.Activities;
using ActiMeet.Server.Domain.Rooms;

namespace ActiMeet.Server.Domain.Categories;
public sealed class Category : BaseEntity
{
	public string CategoryName { get; set; } = default!;

	// Navigation Props
	public ICollection<Activity> Activities { get; set; } = default!;
}
