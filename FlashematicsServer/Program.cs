using FlashematicsServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
const string PostgresConnStr = "Host=localhost;Port=5432;User Id=user01;Password=user01;Database=flashematics";

builder.Services.AddControllers();
builder.Services.AddDbContext<DeckContext>(opt =>
{
    opt.UseNpgsql(PostgresConnStr);
});
builder.Services.AddDbContext<UserContext>(opt =>
{
    opt.UseNpgsql(PostgresConnStr);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:9000")
                .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
