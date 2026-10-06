using Kusanagi.API.Filters;
using Kusanagi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

/// :: Dependency Injections.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
