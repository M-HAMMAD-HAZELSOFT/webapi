using Microsoft.EntityFrameworkCore;
using webapi.Services.UserService;
using webapi.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure the DataContext with the provided connection string
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Register the AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Register the UserService implementation
// For IUserService interface as scoped service.
builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
