using PollsSystem.Application;
using PollsSystem.Persistence;
using PollsSystem.Presentation;
using PollsSystem.Shared;
using System.Text.Json.Serialization;

var builder = WebApplication
    .CreateBuilder(args)
    .AddShared();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

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
