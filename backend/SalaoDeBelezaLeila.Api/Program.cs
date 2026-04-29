using Microsoft.EntityFrameworkCore;
using SalaoDeBelezaLeila.Application.Services;
using SalaoDeBelezaLeila.Infra.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=salao.db"));

// DI
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.UseHttpsRedirection();

app.Run();