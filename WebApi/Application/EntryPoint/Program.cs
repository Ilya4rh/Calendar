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
builder.Services.AddTransient<ApplicationContext>();
builder.Services.AddScoped<JwtProvider>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddAuth();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserService>();
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