using ExpenseTracker.BLL;
using ExpenseTracker.WEBAPI;
using ExpenseTracker.WEBAPI.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//This setup enables cross-origin requests from the specified origin with the specified HTTP methods and headers
var SpecificAllowOrigins = "origin_names";

builder.Services.AddCors(options =>
{
    options.AddPolicy(SpecificAllowOrigins, policy =>
    {
        policy.WithOrigins("https://localhost:3000").AllowCredentials();
        policy.WithMethods("POST", "PUT", "DELETE");
        policy.WithHeaders("*");
    });
});

//configures JWT (JSON Web Token) authentication for an ASP.NET Core application, setting up a JWT Bearer token validation with the provided secret key, enabling HTTPS metadata validation, and allowing the saving of tokens. It also configures the authentication schemes and sets the token validation parameters to validate the signing key and disable issuer and audience validation.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwtBearerOptions =>
    {
        jwtBearerOptions.RequireHttpsMetadata = true;
        jwtBearerOptions.SaveToken = true;
        jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:JWTSettings:AccessTokenSecretKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPlayground", Version = "v1" });
    c.AddSecurityDefinition("token", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization,
        Scheme = "Bearer"
    });
    c.OperationFilter<SecureEndpointAuthRequirementFilter>();
});
builder.Services.AddSingleton<AuthController>();
BLLConfig.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(SpecificAllowOrigins);
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
