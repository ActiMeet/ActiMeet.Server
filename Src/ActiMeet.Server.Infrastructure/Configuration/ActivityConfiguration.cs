using ActiMeet.Server.Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActiMeet.Server.Infrastructure.Configuration;
public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
	public void Configure(EntityTypeBuilder<Activity> builder)
	{
		// Activity -> Category (Bire Çok)
		builder
			.HasOne(a => a.Category)
			.WithMany(c => c.Activities)
			.HasForeignKey(a => a.CategoryId)
			.OnDelete(DeleteBehavior.Restrict);

		// Activity -> Location (Bire Çok)
		builder
			.HasOne(a => a.Location)
			.WithMany(l => l.Activities)
			.HasForeignKey(a => a.LocationId)
			.OnDelete(DeleteBehavior.Restrict);

		// Activity -> CreatedByUser (Bire Çok)
		builder
			.HasOne(a => a.CreatedByUser)
			.WithMany()
			.HasForeignKey(a => a.CreatedByUserId)
			.OnDelete(DeleteBehavior.Restrict);

		// Çoka Çok (Activity <-> AppUser) (ActivityParticipants)
		//builder
		//	.HasKey(ap => new { ap.Id, ap.CreatedByUserId });
	}
}
