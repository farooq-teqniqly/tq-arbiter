# Performance Review Results

**Date**: 2026-05-03 22:49:01 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: 5ca378f41c0e47cceb4760bfa7a5306eda689ae0

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 1
- **Improvements**: 1
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 940.800 ns | -3.4% | ➡️  |
| Create_Command | 636.900 ns | 630.800 ns | -1.0% | ➡️  |
| Create_Notification | 633.400 ns | 632.300 ns | -0.2% | ➡️  |
| Create_Query | 633.000 ns | 635.800 ns | +0.4% | ➡️  |
| Publish_Notification | 943.200 ns | 964.100 ns | +2.2% | ➡️  |
| Send_Command | 928.600 ns | 931.800 ns | +0.3% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 232,000 B | +0.0% | 87.5/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 264,000 B | +0.0% | 100.0/0.0 | ➡️  |
| Bulk_Send_Commands | 263,997 B | 2,795,818 B | +959.0% | 54.1/1.7 | ⚠️ CRITICAL |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,000 B | -0.0% | 140.6/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,920 B | +0.0% | 67.7/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 0 B | -100.0% | 57.3/3.5 | ✅  |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 46.4/0.0 | ➡️  |

## Regressions

### Bulk_Send_Commands - CRITICAL

- **Baseline**: 875200.000 ns (263,997 B allocated)
- **Current**: 861400.000 ns (2,795,818 B allocated)
- **Change**: +959.0%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **1 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
