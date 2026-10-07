using Kusanagi.API.Filters;
using Kusanagi.Communication.Enums;
using Kusanagi.Communication.Responses;
using Kusanagi.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

bool ExecuteMigrationsOnStartup = true;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

/// :: Dependency Injections.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo> { new("en"), new("pt-BR"), new("es") };

    options.DefaultRequestCulture = new RequestCulture("en");

    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders = [new AcceptLanguageHeaderRequestCultureProvider()];
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var signingKey = builder.Configuration.GetValue<string>("Jwt:SigningKey")!;

    options.TokenValidationParameters = new()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var subject = context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(subject, out var userId) == false)
            {
                context.Fail("Invalid subject");
                return;
            }
        },

        OnChallenge = async context =>
        {
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new PayloadResponse
            {
                Status = nameof(ResponseStatus.Error),
                Messages = ["Unauthorized"],
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    };

});

var app = builder.Build();

if (ExecuteMigrationsOnStartup)
{
    await ExecuteMigrations();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task ExecuteMigrations()
{

    await using var scope = app.Services.CreateAsyncScope();

    await DatabaseMigration.ExecuteMigrations(scope.ServiceProvider);

}
