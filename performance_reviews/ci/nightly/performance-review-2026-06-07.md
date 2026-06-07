# Performance Review Results

**Date**: 2026-06-07 22:52:56 UTC
**Baseline**: 2026-05-24T22:50:49.690761
**Commit**: e0f56d27cc88d708f94cb9b5d816706585b1ba5e

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 0
- **Improvements**: 2
- **Status**: ✅ PASS

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 956.100 ns | 951.700 ns | -0.5% | ➡️  |
| Create_Command | 631.800 ns | 626.100 ns | -0.9% | ➡️  |
| Create_Notification | 628.100 ns | 627.400 ns | -0.1% | ➡️  |
| Create_Query | 623.600 ns | 626.900 ns | +0.5% | ➡️  |
| Publish_Notification | 947.700 ns | 941.600 ns | -0.6% | ➡️  |
| Send_Command | 928.900 ns | 927.400 ns | -0.2% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 232,000 B | 232,000 B | 0.0% | 88.5/0.0 | ➡️  |
| Bulk_Publish_Notifications | 264,000 B | 0 B | -100.0% | 62.5/1.8 | ✅  |
| Bulk_Send_Commands | 264,000 B | 264,000 B | 0.0% | 99.7/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,000 B | 0 B | -100.0% | 90.9/5.7 | ✅  |
| Create_And_Send_Commands | 0 B | 359,920 B | 0.0% | 67.7/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,128 B | 280,128 B | 0.0% | 53.8/1.7 | ➡️  |
| Store_Query_Results_In_List | 0 B | 240,128 B | 0.0% | 46.9/0.0 | ➡️  |

## Action Items

- [x] No regressions detected
- [x] Baseline will be automatically updated

## Conclusion

✅ **All benchmarks passed.** Performance is within acceptable range of baseline.
