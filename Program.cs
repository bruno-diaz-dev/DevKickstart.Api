using DevKickstart.Api.Configuration;
using DevKickstart.Api.Services;
using DevKickstart.Application.Interfaces;
using DevKickstart.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using DevKickstart.Api.Middleware;
using DevKickstart.Api.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Unicode;
using Microsoft.AspNetCore.Cors.Infrastructure;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RedisOptions>(
    builder.Configuration.GetSection("Redis"));

builder.Services.AddSingleton<UsuarioService>();

builder.Services.AddSingleton<
    DevKickstart.Api.Services.Auth.TokenService>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddSingleton<IUsuarioRepository, RedisUsuarioRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var key = Encoding.UTF8.GetBytes("ESTA_ES_UNA_SECRET_KEY_SUPER_LARGA");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
builder.Services.AddSingleton<
    INotaRepository,
    RedisNotaRepository
>();
builder.Services.AddSingleton<NotaService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();