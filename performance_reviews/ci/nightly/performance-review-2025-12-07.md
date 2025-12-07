# Performance Review Results

**Date**: 2025-12-07 22:39:09 UTC
**Baseline**: 2025-11-30T22:40:15.999287
**Commit**: f55be520746366cdf26a77972c1faff5def2095c

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 0
- **Improvements**: 0
- **Status**: ✅ PASS

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 972.500 ns | 974.000 ns | +0.2% | ➡️  |
| Create_Command | 630.300 ns | 636.900 ns | +1.0% | ➡️  |
| Create_Notification | 632.700 ns | 633.400 ns | +0.1% | ➡️  |
| Create_Query | 631.300 ns | 633.000 ns | +0.3% | ➡️  |
| Publish_Notification | 939.200 ns | 943.200 ns | +0.4% | ➡️  |
| Send_Command | 927.000 ns | 928.600 ns | +0.2% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 232,000 B | 231,997 B | -0.0% | 88.5/0.0 | ➡️  |
| Bulk_Publish_Notifications | 264,002 B | 263,997 B | -0.0% | 100.0/0.0 | ➡️  |
| Bulk_Send_Commands | 0 B | 263,997 B | 0.0% | 100.7/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,000 B | 368,005 B | +0.0% | 68.2/0.0 | ➡️  |
| Create_And_Send_Commands | 359,920 B | 359,915 B | -0.0% | 67.7/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,128 B | 280,125 B | -0.0% | 53.8/1.7 | ➡️  |
| Store_Query_Results_In_List | 0 B | 240,128 B | 0.0% | 46.9/0.0 | ➡️  |

## Action Items

- [x] No regressions detected
- [x] Baseline will be automatically updated

## Conclusion

✅ **All benchmarks passed.** Performance is within acceptable range of baseline.
