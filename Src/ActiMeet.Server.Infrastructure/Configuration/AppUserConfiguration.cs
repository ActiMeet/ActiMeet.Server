﻿using ActiMeet.Server.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActiMeet.Server.Infrastructure.Configuration;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
	public void Configure(EntityTypeBuilder<AppUser> builder)
	{
		builder.HasIndex(i => i.UserName).IsUnique();
		builder.Property(p => p.FirstName).HasColumnType("varchar(50)");
		builder.Property(p => p.LastName).HasColumnType("varchar(50)");
		builder.Property(p => p.UserName).HasColumnType("varchar(15)");
		builder.Property(p => p.Email).HasColumnType("varchar(MAX)");
	}
}
