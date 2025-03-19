using ActiMeet.Server.Domain.Abstractions;
using ActiMeet.Server.Domain.Activities;

namespace ActiMeet.Server.Domain.Locations;
public sealed class Location : BaseEntity
{
	public double Latitude { get; set; }
	public double Longitude { get; set; }
	public string City { get; set; } = default!;
	public string Country { get; set; } = default!;

	// Navigation Props
	public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
