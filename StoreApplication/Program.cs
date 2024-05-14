using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using StoreApplication.AutherizationExctencion;
using StoreApplication.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionString));

IdentityModelEventSource.ShowPII = true;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                // ��������, ����� �� �������������� �������� ��� ��������� ������
                ValidateIssuer = true,
                // ������, �������������� ��������
                ValidIssuer = AuthOptions.ISSUER,
                // ����� �� �������������� ����������� ������
                ValidateAudience = true,
                // ��������� ����������� ������
                ValidAudience = AuthOptions.AUDIENCE,
                // ����� �� �������������� ����� �������������
                ValidateLifetime = true,
                // ��������� ����� ������������
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                // ��������� ����� ������������
                ValidateIssuerSigningKey = true,
                // ��������� �� ������
                ClockSkew = TimeSpan.FromMinutes(AuthOptions.LIFETIME),
            };
        });
var sp = builder.Services.BuildServiceProvider();
var DbContext = sp.GetService<DataBaseContext>();
builder.Services.AddAuthorization(options => options.AddICTAPIPolicies(DbContext as DataBaseContext));
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
// Configure the HTTP request pipeline.


app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
});

app.Run();