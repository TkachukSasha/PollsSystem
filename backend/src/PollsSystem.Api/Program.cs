using PollsSystem.Application;
using PollsSystem.Persistence;
using PollsSystem.Presentation;
using PollsSystem.Shared;

var builder = WebApplication
    .CreateBuilder(args)
    .AddShared();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddPersistence();
builder.Services.AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePresentation();

app.UseShared();

app.Run();
