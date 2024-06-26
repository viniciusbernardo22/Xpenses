using Xpenses.API;
using Xpenses.API.Common.Api;
using Xpenses.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();
builder.AddOpenTelemetry();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseSecurity();

app.MapEndpoints();

app.Run();

