# Performance Review Results

**Date**: 2025-11-30 22:40:15 UTC
**Baseline**: 2025-11-30T18:56:46.325543
**Commit**: 676dcba7ec4f7342450ca04b87cf7af9ee3bd2fa

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 0
- **Improvements**: 2
- **Status**: ✅ PASS

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 944.400 ns | 972.500 ns | +3.0% | ➡️  |
| Create_Command | 640.400 ns | 630.300 ns | -1.6% | ➡️  |
| Create_Notification | 636.000 ns | 632.700 ns | -0.5% | ➡️  |
| Create_Query | 636.700 ns | 631.300 ns | -0.8% | ➡️  |
| Publish_Notification | 931.000 ns | 939.200 ns | +0.9% | ➡️  |
| Send_Command | 942.900 ns | 927.000 ns | -1.7% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 232,000 B | 232,000 B | 0.0% | 87.5/0.0 | ➡️  |
| Bulk_Publish_Notifications | 264,002 B | 264,002 B | 0.0% | 100.0/0.0 | ➡️  |
| Bulk_Send_Commands | 264,000 B | 0 B | -100.0% | 60.8/1.7 | ✅  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 0 B | 368,000 B | 0.0% | 71.9/0.0 | ➡️  |
| Create_And_Send_Commands | 359,920 B | 359,920 B | 0.0% | 67.7/0.0 | ➡️  |
| Store_Command_Results_In_List | 0 B | 280,128 B | 0.0% | 54.1/1.7 | ➡️  |
| Store_Query_Results_In_List | 240,128 B | 0 B | -100.0% | 48.6/1.7 | ✅  |

## Action Items

- [x] No regressions detected
- [x] Baseline will be automatically updated

## Conclusion

✅ **All benchmarks passed.** Performance is within acceptable range of baseline.
