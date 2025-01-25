using Core.Event;
using Core.Event.Interfaces;
using Core.Event.Models;
using Core.Generator;
using Core.User;
using Core.User.Interfaces;
using EntryPoint;
using EntryPoint.Services;
using Infrastructure;
using Infrastructure.Repositories.ApplicationScope;
using Infrastructure.Repositories.UserScope;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddTransient<ApplicationContext>();
builder.Services.AddTransient<IApplicationScope, ApplicationScope>();
builder.Services.AddTransient<IUserScope, UserScope>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<EventRepository>();
builder.Services.AddTransient<RepeatRepository>();
builder.Services.AddTransient<JwtProvider>();
builder.Services.AddTransient<AuthenticationService>();
builder.Services.AddAuth();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IGenerator<EventDto>, EventGenerator>();
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