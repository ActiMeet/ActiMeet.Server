using ActiMeet.Server.Domain.ActivityParticipants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActiMeet.Server.Infrastructure.Configuration;
public class ActivityParticipantConfiguration : IEntityTypeConfiguration<ActivityParticipant>
{
	public void Configure(EntityTypeBuilder<ActivityParticipant> builder)
	{
		// Çoka Çok (Activity <-> AppUser) (ActivityParticipants)
		//builder
		//	.HasKey(ap => new { ap.ActivityId, ap.UserId });

		builder
			.HasOne(ap => ap.Activity)
			.WithMany(a => a.ActivityParticipants)
			.HasForeignKey(ap => ap.ActivityId)
			.OnDelete(DeleteBehavior.Restrict);

		builder
			.HasOne(ap => ap.User)
			.WithMany(a => a.ActivityParticipants)
			.HasForeignKey(ap => ap.UserId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
