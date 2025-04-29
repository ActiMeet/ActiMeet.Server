using ActiMeet.Server.WebAPI.Controllers;
using Microsoft.AspNetCore.OData;
using Scalar.AspNetCore;
using ActiMeet.Server.Infrastructure;
using ActiMeet.Server.Application;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using ActiMeet.Server.WebAPI.Modules;

namespace ActiMeet.Server.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddResponseCompression(opt =>
		{
			opt.EnableForHttps = true; // sorgu sonucu dönen verinin boyutunu sıkıştırır böylelikle daha hızlı sonuç döner. 
		});

		builder.AddServiceDefaults();

		// Registrars
		builder.Services.AddApplication();
		builder.Services.AddInfrastrusture(builder.Configuration);
        
        builder.Services.AddCors();
        builder.Services.AddOpenApi();
		
		// OData
		builder.Services.AddControllers().AddOData(opt => opt // openapi ekstra sorgu
			  .Select()
			  .Filter()
			  .Count()
			  .Expand()
			  .OrderBy()
			  .SetMaxTop(null)
			  .AddRouteComponents("odata", AppODataController.GetEdmModel())
			  );

		builder.Services.AddRateLimiter(x =>
		   x.AddFixedWindowLimiter("fixed", cfg =>
		   {
			   cfg.QueueLimit = 100;
			   cfg.Window = TimeSpan.FromSeconds(1);
			   cfg.PermitLimit = 100;
			   cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		   }));

		builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

		var app = builder.Build();

        app.MapOpenApi(); // openapi
        app.MapScalarApiReference(); // scalar

        app.MapDefaultEndpoints();

        app.UseHttpsRedirection();

        app.UseCors(x => x
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials()
			.SetIsOriginAllowed(t => true)
			.SetPreflightMaxAge(TimeSpan.FromMinutes(10)) // Aynı browser'dan gelen istekler 10dk boyunca ikinci defa CORS politikası kontrolüne girmiyor.
			);

		app.RegisterRoutes();

		app.UseAuthentication();
        app.UseAuthorization();

		app.UseResponseCompression(); // sorgu sonucu dönen verinin boyutunu sıkıştırır böylelikle daha hızlı sonuç döner. 

		app.UseExceptionHandler();

		app.MapControllers().RequireRateLimiting("fixed").RequireAuthorization(); // RequireAuthorization() => tüm controller'larda authorization'u otomatik kontrol eder.

		ExtensionsMiddleware.CreateFirstUser(app); // create first user

		app.Run();
    }
}
