using ActiMeet.Server.Application.Interfaces.Repositories;
using ActiMeet.Server.Application.Interfaces.UnitOfWorks;
using ActiMeet.Server.Application.Services;
using ActiMeet.Server.Domain.Users;
using ActiMeet.Server.Infrastructure.Context;
using ActiMeet.Server.Infrastructure.Options;
using ActiMeet.Server.Infrastructure.Repositories;
using ActiMeet.Server.Infrastructure.Services;
using ActiMeet.Server.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ActiMeet.Server.Infrastructure;
public static class InfrastructureRegistrar
{
	public static IServiceCollection AddInfrastrusture(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(opt =>
		{
			string connectionString = configuration.GetConnectionString("SqlServer")!;
			opt.UseSqlServer(connectionString);
		});

		services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
		services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IJwtProvider, JwtProvider>();

		// UserManager kullanabilmek için yazılır
		services
			.AddIdentity<AppUser, IdentityRole<Guid>>(opt =>
			{
				opt.Password.RequiredLength = 1;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireDigit = false;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireUppercase = false;
				opt.Lockout.MaxFailedAccessAttempts = 5; // kullanıcı şifreyi 5 denemeden sonra da hatalı girmişse
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // 5 dakika kitler
				opt.SignIn.RequireConfirmedEmail = true; // kitlendikten sonra email onayı gerektirsin
			}) // Identity kütüphanesini tanımlar.
			.AddEntityFrameworkStores<ApplicationDbContext>() // ApplicationDbContext ile bağlantısını yapar.
			.AddDefaultTokenProviders(); // Şifremi unuttum, şifremi yenile gibi işlemler için UserManager class'ının token üretme metodunun çalışabilmesini sağlar.

		services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
		services.ConfigureOptions<JwtOptionsSetup>();

		services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer();
		services.AddAuthorization();

		return services;
	}
}
