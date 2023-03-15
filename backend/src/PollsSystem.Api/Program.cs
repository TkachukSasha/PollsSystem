using PollsSystem.Application;
using PollsSystem.Persistence;
using PollsSystem.Presentation;
using PollsSystem.Shared;

var builder = WebApplication
    .CreateBuilder(args)
    .AddShared();

builder.Services.AddApplication();
builder.Services.AddPersistence();
builder.Services.AddPresentation();

var app = builder.Build();

app.UsePresentation();

app.UseShared();

app.Run();
