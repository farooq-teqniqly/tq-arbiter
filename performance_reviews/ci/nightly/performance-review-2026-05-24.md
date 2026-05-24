# Performance Review Results

**Date**: 2026-05-24 22:50:49 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: 890e07016b75e6e5202f6ce78c01d61c1fbb7e2f

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 0
- **Improvements**: 2
- **Status**: ✅ PASS

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 956.100 ns | -1.8% | ➡️  |
| Create_Command | 636.900 ns | 631.800 ns | -0.8% | ➡️  |
| Create_Notification | 633.400 ns | 628.100 ns | -0.8% | ➡️  |
| Create_Query | 633.000 ns | 623.600 ns | -1.5% | ➡️  |
| Publish_Notification | 943.200 ns | 947.700 ns | +0.5% | ➡️  |
| Send_Command | 928.600 ns | 928.900 ns | +0.0% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 232,000 B | +0.0% | 88.5/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 264,000 B | +0.0% | 50.3/0.0 | ➡️  |
| Bulk_Send_Commands | 263,997 B | 264,000 B | +0.0% | 50.7/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,000 B | -0.0% | 137.5/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 0 B | -100.0% | 78.1/5.2 | ✅  |
| Store_Command_Results_In_List | 280,125 B | 280,128 B | +0.0% | 106.5/4.6 | ➡️  |
| Store_Query_Results_In_List | 240,128 B | 0 B | -100.0% | 55.6/1.7 | ✅  |

## Action Items

- [x] No regressions detected
- [x] Baseline will be automatically updated

## Conclusion

✅ **All benchmarks passed.** Performance is within acceptable range of baseline.
