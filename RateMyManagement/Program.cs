using RateMyManagement;

var builder = WebApplication.CreateBuilder(args);
string connectionString;
if (builder.Environment.EnvironmentName == "Development")
{
    connectionString = builder.Configuration.GetConnectionString("Development");
}
else if (builder.Environment.EnvironmentName == "Azure")
{
    connectionString = builder.Configuration.GetConnectionString("Azure");
}
else
{
    throw new Exception("Neither development nor azure");
}
StartUp.ConfigureService(builder.Services, connectionString);
var app = builder.Build();
StartUp.ConfigureApplication(app);
app.Run();
