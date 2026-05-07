using DevKickstart.Api.Configuration;
using DevKickstart.Api.Services;
using DevKickstart.Application.Interfaces;
using DevKickstart.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using DevKickstart.Api.Middleware;
using DevKickstart.Api.Services.Auth;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RedisOptions>(
    builder.Configuration.GetSection("Redis"));

builder.Services.AddSingleton<UsuarioService>();

builder.Services.AddSingleton<
    DevKickstart.Api.Services.Auth.TokenService>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddSingleton<IUsuarioRepository, RedisUsuarioRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();