using BenchmarkDotNet.Running;
using Teqniqly.Arbiter.Core.Benchmarks;

BenchmarkRunner.Run<ArbiterCpuBenchmarks>();
BenchmarkRunner.Run<ArbiterMemoryBenchmarks>();
