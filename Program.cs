using DevKickstart.Api.Configuration;
using DevKickstart.Api.Services;
using DevKickstart.Application.Interfaces;
using DevKickstart.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RedisOptions>(
    builder.Configuration.GetSection("Redis"));

builder.Services.AddSingleton<UsuarioService>();

builder.Services.AddSingleton<IUsuarioRepository, RedisUsuarioRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();