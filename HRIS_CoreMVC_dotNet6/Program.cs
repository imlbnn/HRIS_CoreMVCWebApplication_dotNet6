using HRIS.Application;
using HRIS.Application.Common.Interfaces.Services;
using HRIS.Infrastructure;
using HRIS_CoreMVC_dotNet6.Helpers;
using HRIS_CoreMVC_dotNet6.Interfaces;
using HRIS_CoreMVC_dotNet6.Services;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCoreMVCAuthentication(builder.Configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.Load("HRIS.Application"));
builder.Services.AddMediatR(Assembly.Load("HRIS.Infrastructure"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=login}/{id?}");

app.Run();
