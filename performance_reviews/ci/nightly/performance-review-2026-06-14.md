# Performance Review Results

**Date**: 2026-06-14 22:56:06 UTC
**Baseline**: 2026-06-07T22:52:56.404682
**Commit**: 3a5907508f9e0f2831854d5aa67d63b5c74b0fcf

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 0
- **Improvements**: 2
- **Status**: ✅ PASS

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 951.700 ns | 942.000 ns | -1.0% | ➡️  |
| Create_Command | 626.100 ns | 629.500 ns | +0.5% | ➡️  |
| Create_Notification | 627.400 ns | 627.600 ns | +0.0% | ➡️  |
| Create_Query | 626.900 ns | 629.300 ns | +0.4% | ➡️  |
| Publish_Notification | 941.600 ns | 958.300 ns | +1.8% | ➡️  |
| Send_Command | 927.400 ns | 936.400 ns | +1.0% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 232,000 B | 232,000 B | 0.0% | 88.5/0.0 | ➡️  |
| Bulk_Publish_Notifications | 0 B | 264,000 B | 0.0% | 50.0/0.0 | ➡️  |
| Bulk_Send_Commands | 264,000 B | 264,000 B | 0.0% | 99.7/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 0 B | 368,000 B | 0.0% | 67.7/0.0 | ➡️  |
| Create_And_Send_Commands | 359,920 B | 0 B | -100.0% | 78.1/5.2 | ✅  |
| Store_Command_Results_In_List | 280,128 B | 0 B | -100.0% | 59.0/3.5 | ✅  |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 46.9/0.0 | ➡️  |

## Action Items

- [x] No regressions detected
- [x] Baseline will be automatically updated

## Conclusion

✅ **All benchmarks passed.** Performance is within acceptable range of baseline.
