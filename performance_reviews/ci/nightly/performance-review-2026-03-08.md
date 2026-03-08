# Performance Review Results

**Date**: 2026-03-08 22:43:41 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: cf6f4824b2fdc0dc7f8d462d9eb903445fd2bcc0

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 6
- **Improvements**: 2
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 1336.300 ns | +37.2% | ⚠️ CRITICAL |
| Create_Command | 636.900 ns | 921.700 ns | +44.7% | ⚠️ CRITICAL |
| Create_Notification | 633.400 ns | 914.200 ns | +44.3% | ⚠️ CRITICAL |
| Create_Query | 633.000 ns | 910.400 ns | +43.8% | ⚠️ CRITICAL |
| Publish_Notification | 943.200 ns | 1325.900 ns | +40.6% | ⚠️ CRITICAL |
| Send_Command | 928.600 ns | 1311.500 ns | +41.2% | ⚠️ CRITICAL |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 232,000 B | +0.0% | 82.5/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 0 B | -100.0% | 54.7/2.6 | ✅  |
| Bulk_Send_Commands | 263,997 B | 264,000 B | +0.0% | 50.0/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,000 B | -0.0% | 71.4/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,920 B | +0.0% | 68.8/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 0 B | -100.0% | 62.5/2.5 | ✅  |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 45.0/0.0 | ➡️  |

## Regressions

### Ask_Query - CRITICAL

- **Baseline**: 974.000 ns (304 B allocated)
- **Current**: 1336.300 ns (0 B allocated)
- **Change**: +37.2%
- **Recommendation**: Fix before merge

### Create_Command - CRITICAL

- **Baseline**: 636.900 ns (40 B allocated)
- **Current**: 921.700 ns (0 B allocated)
- **Change**: +44.7%
- **Recommendation**: Fix before merge

### Create_Notification - CRITICAL

- **Baseline**: 633.400 ns (40 B allocated)
- **Current**: 914.200 ns (0 B allocated)
- **Change**: +44.3%
- **Recommendation**: Fix before merge

### Create_Query - CRITICAL

- **Baseline**: 633.000 ns (40 B allocated)
- **Current**: 910.400 ns (0 B allocated)
- **Change**: +43.8%
- **Recommendation**: Fix before merge

### Publish_Notification - CRITICAL

- **Baseline**: 943.200 ns (3,142 B allocated)
- **Current**: 1325.900 ns (264 B allocated)
- **Change**: +40.6%
- **Recommendation**: Fix before merge

### Send_Command - CRITICAL

- **Baseline**: 928.600 ns (344 B allocated)
- **Current**: 1311.500 ns (344 B allocated)
- **Change**: +41.2%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **6 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
