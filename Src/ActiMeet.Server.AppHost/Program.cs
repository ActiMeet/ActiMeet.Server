var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ActiMeet_Server_WebAPI>("actimeet-server-webapi");

builder.Build().Run();
