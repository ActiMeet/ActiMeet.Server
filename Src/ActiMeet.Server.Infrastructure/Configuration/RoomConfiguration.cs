using ActiMeet.Server.Domain.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActiMeet.Server.Infrastructure.Configuration;
public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
	public void Configure(EntityTypeBuilder<Room> builder)
	{
		// Room -> CreatedByUser (Bire Çok)
		builder
			.HasOne(r => r.CreatedByUser)
			.WithMany()
			.HasForeignKey(r => r.CreatedByUserId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
