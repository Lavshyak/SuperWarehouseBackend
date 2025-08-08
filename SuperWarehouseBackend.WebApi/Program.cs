using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SuperWarehouseBackend.WebApi.Db;
using SuperWarehouseBackend.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#if !EF_CORE_MIGRATION
builder.Services.AddDbContext<MainDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("mainDb") ?? throw new InvalidOperationException(),
        options =>
        {
            options.MigrationsAssembly(typeof(MainDbContext).Assembly);
            options.MigrationsHistoryTable("__EFMigrationsHistory", "public");
        }));
#else
builder.Services.AddDbContext<MainDbContext>(optionsBuilder => optionsBuilder.UseNpgsql());
#endif


builder.Services.AddScoped<BalanceRepository>();
builder.Services.AddScoped<InboundDocumentsRepository>();
builder.Services.AddScoped<InboundResourcesRepository>();
builder.Services.AddScoped<MeasureUnitRepository>();
builder.Services.AddScoped<ResourcesRepository>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });
}

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            var scheme = httpReq.Headers["X-Forwarded-Proto"];
            if (string.IsNullOrWhiteSpace(scheme))
                scheme = httpReq.Scheme;

            swagger.Servers = new List<OpenApiServer>
            {
                new OpenApiServer
                    { Url = $"{scheme}://{httpReq.Host.Value}/{httpReq.Headers["X-Forwarded-Prefix"]}" },
                new OpenApiServer
                    { Url = $"http://{httpReq.Host.Value}/{httpReq.Headers["X-Forwarded-Prefix"]}" },
                new OpenApiServer
                    { Url = $"https://{httpReq.Host.Value}/{httpReq.Headers["X-Forwarded-Prefix"]}" }
            };
        });
    });
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("v1/swagger.json", "My API V1"); });
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Map("/ping", () => "pong");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
    dbContext.Database.Migrate();
}

app.Run();