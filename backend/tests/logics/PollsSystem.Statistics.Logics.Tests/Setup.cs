using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PollsSystem.Application.Common.Services;

namespace PollsSystem.Statistics.Logics.Tests;

public class Setup
{
    private readonly IHostBuilder _defaultBuilder;
    private IServiceProvider _services;
    private bool _built = false;

    public Setup()
    {
        _defaultBuilder = Host.CreateDefaultBuilder();
    }

    public IServiceProvider Services => _services ?? Build();

    private IServiceProvider Build()
    {
        if (_built)
            throw new InvalidOperationException("Build can only be called once.");

        _built = true;

        _defaultBuilder.ConfigureServices((context, services) =>
        {
            services.AddScoped<IStatisticCalculationService, StatisticCalculationService>();
        });

        _services = _defaultBuilder.Build().Services;

        return _services;
    }
}