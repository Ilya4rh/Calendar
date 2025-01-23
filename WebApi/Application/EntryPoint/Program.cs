using Core.Event;
using Core.Event.Models;
using Core.Generator;
using Core.Repeat;
using Core.Task;
using Core.User;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<EventRepository>();
builder.Services.AddSingleton<RepeatRepository>();
builder.Services.AddSingleton<TaskRepository>();
builder.Services.AddTransient<ApplicationContext>();
builder.Services.AddScoped<JwtProvider>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddAuth();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<EventService>();
builder.Services.AddSingleton<RepeatService>();
builder.Services.AddSingleton<TaskService>();
builder.Services.AddSingleton<IGenerator<EventDto>, EventGenerator>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(build =>
{
    build.WithOrigins("http://localhost:3000");
    build.AllowAnyMethod();
    build.AllowAnyHeader();
    build.AllowCredentials();
});
app.MapControllers();

app.Run();