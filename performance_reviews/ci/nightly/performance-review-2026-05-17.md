# Performance Review Results

**Date**: 2026-05-17 22:50:28 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: da0092aed06d17fa68eb9c3033593084fb00cc66

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 1
- **Improvements**: 0
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 951.000 ns | -2.4% | ➡️  |
| Create_Command | 636.900 ns | 627.100 ns | -1.5% | ➡️  |
| Create_Notification | 633.400 ns | 629.300 ns | -0.6% | ➡️  |
| Create_Query | 633.000 ns | 627.500 ns | -0.9% | ➡️  |
| Publish_Notification | 943.200 ns | 944.300 ns | +0.1% | ➡️  |
| Send_Command | 928.600 ns | 922.100 ns | -0.7% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 231,997 B | 0.0% | 87.8/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 263,997 B | 0.0% | 100.7/0.0 | ➡️  |
| Bulk_Send_Commands | 263,997 B | 263,997 B | 0.0% | 99.7/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 116.1/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,005 B | 0.0% | 67.7/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,915 B | 0.0% | 67.7/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 280,125 B | 0.0% | 53.8/1.7 | ➡️  |
| Store_Query_Results_In_List | 240,128 B | 3,079,741 B | +1182.5% | 48.6/1.7 | ⚠️ CRITICAL |

## Regressions

### Store_Query_Results_In_List - CRITICAL

- **Baseline**: 879700.000 ns (240,128 B allocated)
- **Current**: 877100.000 ns (3,079,741 B allocated)
- **Change**: +1182.5%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **1 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
