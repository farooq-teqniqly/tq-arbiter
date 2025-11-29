using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Teqniqly.Arbiter.Core.Abstractions;
using Teqniqly.Arbiter.Core.Extensions;

namespace Teqniqly.Arbiter.Core.Benchmarks;

/// <summary>
/// Memory benchmarks for Teqniqly.Arbiter.Core library operations.
/// Measures allocation patterns in realistic bulk operation scenarios.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(
    warmupCount: 3,
    iterationCount: 10,
    runtimeMoniker: RuntimeMoniker.Net90,
    baseline: false
)]
public class ArbiterMemoryBenchmarks
{
    private const int _bulkOperationCount = 1000;

    private List<BenchmarkCommand> _commands = null!;
    private IMediator _mediator = null!;
    private List<BenchmarkNotification> _notifications = null!;
    private List<BenchmarkQuery> _queries = null!;

    /// <summary>
    /// Benchmark bulk query asking operations.
    /// Measures memory allocation when asking 1000 queries.
    /// </summary>
    [Benchmark]
    public async Task Bulk_Ask_Queries()
    {
        for (var i = 0; i < _bulkOperationCount; i++)
        {
            await _mediator.Ask(_queries[i]);
        }
    }

    /// <summary>
    /// Benchmark bulk notification publishing operations.
    /// Measures memory allocation when publishing 1000 notifications.
    /// Each notification has 2 registered handlers.
    /// </summary>
    [Benchmark]
    public async Task Bulk_Publish_Notifications()
    {
        for (var i = 0; i < _bulkOperationCount; i++)
        {
            await _mediator.Publish(_notifications[i]);
        }
    }

    /// <summary>
    /// Benchmark bulk command sending operations.
    /// Measures memory allocation when sending 1000 commands.
    /// </summary>
    [Benchmark]
    public async Task Bulk_Send_Commands()
    {
        for (var i = 0; i < _bulkOperationCount; i++)
        {
            await _mediator.Send(_commands[i]);
        }
    }

    /// <summary>
    /// Benchmark creating and asking queries in a loop.
    /// Measures memory allocation when creating new query instances.
    /// </summary>
    [Benchmark]
    public async Task Create_And_Ask_Queries()
    {
        for (var i = 0; i < _bulkOperationCount; i++)
        {
            var query = new BenchmarkQuery(Guid.NewGuid(), $"New Query {i}");
            await _mediator.Ask(query);
        }
    }

    /// <summary>
    /// Benchmark creating and publishing notifications in a loop.
    /// Measures memory allocation when creating new notification instances.
    /// </summary>
    [Benchmark]
    public async Task Create_And_Publish_Notifications()
    {
        for (var i = 0; i < _bulkOperationCount; i++)
        {
            var notification = new BenchmarkNotification(Guid.NewGuid(), $"New Notification {i}");
            await _mediator.Publish(notification);
        }
    }

    /// <summary>
    /// Benchmark creating and sending commands in a loop.
    /// Measures memory allocation when creating new command instances.
    /// </summary>
    [Benchmark]
    public async Task Create_And_Send_Commands()
    {
        for (var i = 0; i < _bulkOperationCount; i++)
        {
            var command = new BenchmarkCommand(Guid.NewGuid(), $"New Command {i}");
            await _mediator.Send(command);
        }
    }

    /// <summary>
    /// Sets up the service provider, mediator, and test data before running benchmarks.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddArbiter(typeof(ArbiterMemoryBenchmarks).Assembly);

        var serviceProvider = services.BuildServiceProvider();
        _mediator = serviceProvider.GetRequiredService<IMediator>();

        // Pre-create test data
        _commands = new List<BenchmarkCommand>(_bulkOperationCount);
        _queries = new List<BenchmarkQuery>(_bulkOperationCount);
        _notifications = new List<BenchmarkNotification>(_bulkOperationCount);

        for (var i = 0; i < _bulkOperationCount; i++)
        {
            _commands.Add(new BenchmarkCommand(Guid.NewGuid(), $"Command Data {i}"));
            _queries.Add(new BenchmarkQuery(Guid.NewGuid(), $"Query Data {i}"));
            _notifications.Add(new BenchmarkNotification(Guid.NewGuid(), $"Notification Data {i}"));
        }
    }

    /// <summary>
    /// Benchmark storing command results in a list.
    /// Measures memory allocation when collecting command results.
    /// </summary>
    [Benchmark]
    public async Task<List<Guid>> Store_Command_Results_In_List()
    {
        var results = new List<Guid>(_bulkOperationCount);
        for (var i = 0; i < _bulkOperationCount; i++)
        {
            var result = await _mediator.Send(_commands[i]);
            results.Add(result);
        }
        return results;
    }

    /// <summary>
    /// Benchmark storing query results in a list.
    /// Measures memory allocation when collecting query results.
    /// </summary>
    [Benchmark]
    public async Task<List<string>> Store_Query_Results_In_List()
    {
        var results = new List<string>(_bulkOperationCount);
        for (var i = 0; i < _bulkOperationCount; i++)
        {
            var result = await _mediator.Ask(_queries[i]);
            results.Add(result);
        }
        return results;
    }
}
