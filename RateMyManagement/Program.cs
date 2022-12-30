using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using RateMyManagement;
using RateMyManagement.Areas.Identity;
using RateMyManagement.Core.EntityFramework;
using RateMyManagement.Core.EntityFramework.Authorization;
using RateMyManagement.IServices;
using RateMyManagement.Services;

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
