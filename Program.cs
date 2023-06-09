using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using webapi.Data;
using webapi.Constants;
using webapi.Services.UserService;
using webapi.Services.Authorization;
using webapi.Services.ContactService;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure the DataContext with the provided connection string
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

// Add services to the container.
builder.Services.AddControllers();

// Register the AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Register the UserService implementation
// For IUserService interface as scoped service.
builder.Services.AddScoped<IUserService, UserService>();

// Register the ContactService implementation
// For IContactService interface as scoped service.
builder.Services.AddScoped<IContactService, ContactService>();

// Register the AuthService implementation
// For IAuthService interface as scoped service.
builder.Services.AddScoped<IAuthService, AuthService>();

//// Registers the authentication services using JWT Bearer authentication scheme.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Enable validation of the issuer signing key
            ValidateIssuerSigningKey = true,
            // Set the issuer signing key from the app settings
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection(IAuthConstants.JwtSecretKey).Value)),
            // Disable issuer validation
            ValidateIssuer = false,
            // Disable issuer validation
            ValidateAudience = false
        };
    });

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

// Add middleware thats enable authentication capabilities
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
