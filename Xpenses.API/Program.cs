using Xpenses.API;
using Xpenses.API.Common.Api;
using Xpenses.API.Endpoints;
using Xpenses.Core;

var builder = WebApplication.CreateBuilder(args);

builder.ApplyBuilderConfigurations();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(Configuration.CorsPolicyName);
app.UseSecurity();

app.MapEndpoints();

app.Run();

