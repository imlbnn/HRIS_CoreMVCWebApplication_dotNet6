using HRIS.Application;
using HRIS.Application.Common.Interfaces.Services;
using HRIS.Infrastructure;
using HRISBlazorServerApp;
using HRISBlazorServerApp.Data;
using HRISBlazorServerApp.Services;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.Load("HRIS.Application"));
builder.Services.AddMediatR(Assembly.Load("HRIS.Infrastructure"));
builder.Services.AddHttpContextAccessor();

builder.Services.AddUIDependency(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
