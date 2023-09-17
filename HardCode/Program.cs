using Microsoft.EntityFrameworkCore;
using HardCode.Models.EF;
using HardCode.Controllers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddMvcCore();
builder.Services.AddRazorPages();
builder.Services.AddCors();
builder.Services.AddRouting();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Main}/{action=Index}");

app.UseStaticFiles();
app.UseWebSockets();
app.Run();
