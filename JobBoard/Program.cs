using JobBoard.Data;
using JobBoard.Helpers;
using JobBoard.Middleware;
using JobBoard.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);//create builder, configure before start

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });;

//register appdbcontext
//register appdbcontext
var databaseUrl = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                  ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (databaseUrl != null && databaseUrl.StartsWith("postgresql://"))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    databaseUrl = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(databaseUrl));
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

//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins("http://localhost:3000", "https://job-board-dun-ten.vercel.app")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();//config is done

app.UseCors("AllowReact");

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();//adds jwt middleware, ceck token on each request
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();

app.Run();//start the server
