using ActiMeet.Server.Domain.RoomParticipants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ActiMeet.Server.Infrastructure.Configuration;
public class RoomParticipantConfiguration : IEntityTypeConfiguration<RoomParticipant>
{
	public void Configure(EntityTypeBuilder<RoomParticipant> builder)
	{
		// Çoka Çok (Room <-> AppUser) (RoomParticipant)
		builder
			.HasKey(rp => new { rp.RoomId, rp.UserId });

		builder
			.HasOne(rp => rp.Room)
			.WithMany(r => r.RoomParticipants)
			.HasForeignKey(rp => rp.RoomId);

		builder
			.HasOne(rp => rp.User)
			.WithMany()
			.HasForeignKey(rp => rp.UserId);
	}
}
