using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Perfolizer.Horology;

namespace Teqniqly.Arbiter.Core.Benchmarks
{
    /// <summary>
    /// BenchmarkDotNet configuration optimized for CI environments.
    /// Increases warmup and iteration counts to reduce variance from noisy cloud runners.
    /// </summary>
    public class CiConfig : ManualConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CiConfig"/> class.
        /// </summary>
        public CiConfig()
        {
            AddJob(
                Job.Default.WithRuntime(CoreRuntime.Core90)
                    .WithPlatform(Platform.X64)
                    .WithJit(Jit.RyuJit)
                    .WithWarmupCount(5) // More warmup to stabilize tiered JIT
                    .WithIterationTime(TimeInterval.FromMilliseconds(500))
                    .WithIterationCount(20) // More iterations for tighter confidence intervals
                    .WithGcServer(true)
                    .WithGcConcurrent(true)
                    .WithId("CI")
            );

            AddDiagnoser(MemoryDiagnoser.Default);
            WithOptions(ConfigOptions.JoinSummary);
        }
    }
}
