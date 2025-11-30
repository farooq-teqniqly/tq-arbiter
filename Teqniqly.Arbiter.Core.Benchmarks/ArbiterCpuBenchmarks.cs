using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;
using Teqniqly.Arbiter.Core.Extensions;

namespace Teqniqly.Arbiter.Core.Benchmarks;

/// <summary>
/// CPU benchmarks for Teqniqly.Arbiter.Core library operations.
/// Measures execution time of individual mediator operations.
/// </summary>
[Config(typeof(CiConfig))]
public class ArbiterCpuBenchmarks
{
    private const string _testData = "Test Data";
    private BenchmarkCommand _command = null!;
    private IMediator _mediator = null!;
    private BenchmarkNotification _notification = null!;
    private BenchmarkQuery _query = null!;

    /// <summary>
    /// Benchmark asking a query via the mediator.
    /// </summary>
    [Benchmark]
    public async Task<string> Ask_Query()
    {
        return await _mediator.Ask(_query);
    }

    /// <summary>
    /// Benchmark creating a new command instance.
    /// </summary>
    [Benchmark]
    public BenchmarkCommand Create_Command()
    {
        return new BenchmarkCommand(Guid.NewGuid(), _testData);
    }

    /// <summary>
    /// Benchmark creating a new notification instance.
    /// </summary>
    [Benchmark]
    public BenchmarkNotification Create_Notification()
    {
        return new BenchmarkNotification(Guid.NewGuid(), _testData);
    }

    /// <summary>
    /// Benchmark creating a new query instance.
    /// </summary>
    [Benchmark]
    public BenchmarkQuery Create_Query()
    {
        return new BenchmarkQuery(Guid.NewGuid(), _testData);
    }

    /// <summary>
    /// Benchmark publishing a notification via the mediator.
    /// This notification has 2 registered handlers.
    /// </summary>
    [Benchmark]
    public async Task Publish_Notification()
    {
        await _mediator.Publish(_notification);
    }

    /// <summary>
    /// Benchmark sending a command via the mediator.
    /// </summary>
    [Benchmark]
    public async Task<Guid> Send_Command()
    {
        return await _mediator.Send(_command);
    }

    /// <summary>
    /// Set up the service provider and mediator before running benchmarks.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddArbiter(typeof(ArbiterCpuBenchmarks).Assembly);

        var serviceProvider = services.BuildServiceProvider();
        _mediator = serviceProvider.GetRequiredService<IMediator>();

        _command = new BenchmarkCommand(Guid.NewGuid(), _testData);
        _query = new BenchmarkQuery(Guid.NewGuid(), _testData);
        _notification = new BenchmarkNotification(Guid.NewGuid(), _testData);
    }
}
