using PollsSystem.Api.DependencyInjection;
using PollsSystem.Shared;

var builder = WebApplication
    .CreateBuilder(args)
    .AddShared();

builder.AddServices();

var app = builder.Build();

app.AddApplication();

