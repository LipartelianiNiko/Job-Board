using JobBoard.Data;
using JobBoard.Helpers;
using JobBoard.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);//create builder, configure before start

builder.Services.AddControllers();

//register appdbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtKey = builder.Configuration["Jwt:Key"];//read jwt key from appsettings.json
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

//register modules.
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JobsService>();
builder.Services.AddScoped<ApplicationService>();
builder.Services.AddScoped<SavedJobsService>();



var app = builder.Build();//config is done

app.UseAuthentication();//adds jwt middleware, ceck token on each request
app.UseAuthorization();
app.MapControllers();


app.Run();//start the server
