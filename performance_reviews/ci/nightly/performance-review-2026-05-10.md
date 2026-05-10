# Performance Review Results

**Date**: 2026-05-10 22:49:24 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: 132101630646d29ae6ff33de987aac54bd014f8e

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 1
- **Improvements**: 1
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 945.600 ns | -2.9% | ➡️  |
| Create_Command | 636.900 ns | 628.300 ns | -1.4% | ➡️  |
| Create_Notification | 633.400 ns | 628.300 ns | -0.8% | ➡️  |
| Create_Query | 633.000 ns | 628.500 ns | -0.7% | ➡️  |
| Publish_Notification | 943.200 ns | 965.500 ns | +2.4% | ➡️  |
| Send_Command | 928.600 ns | 944.300 ns | +1.7% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 232,000 B | +0.0% | 88.5/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 3,342,570 B | +1166.1% | 53.8/1.7 | ⚠️ CRITICAL |
| Bulk_Send_Commands | 263,997 B | 264,000 B | +0.0% | 99.7/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 0 B | -100.0% | 75.0/3.1 | ✅  |
| Create_And_Send_Commands | 359,915 B | 359,920 B | +0.0% | 67.7/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 280,128 B | +0.0% | 105.9/5.2 | ➡️  |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 46.9/0.0 | ➡️  |

## Regressions

### Bulk_Publish_Notifications - CRITICAL

- **Baseline**: 900800.000 ns (263,997 B allocated)
- **Current**: 883900.000 ns (3,342,570 B allocated)
- **Change**: +1166.1%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **1 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
