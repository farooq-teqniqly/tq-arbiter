# Performance Review Results

**Date**: 2026-01-18 22:40:53 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: 94ee7b1c44524b1acfca74dfd6ede39256b28bf4

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 6
- **Improvements**: 0
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 1329.300 ns | +36.5% | ⚠️ CRITICAL |
| Create_Command | 636.900 ns | 917.200 ns | +44.0% | ⚠️ CRITICAL |
| Create_Notification | 633.400 ns | 917.900 ns | +44.9% | ⚠️ CRITICAL |
| Create_Query | 633.000 ns | 916.500 ns | +44.8% | ⚠️ CRITICAL |
| Publish_Notification | 943.200 ns | 1357.400 ns | +43.9% | ⚠️ CRITICAL |
| Send_Command | 928.600 ns | 1321.100 ns | +42.3% | ⚠️ CRITICAL |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 231,997 B | 0.0% | 45.0/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 263,997 B | 0.0% | 93.8/0.0 | ➡️  |
| Bulk_Send_Commands | 263,997 B | 263,997 B | 0.0% | 50.0/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,005 B | 0.0% | 68.8/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,915 B | 0.0% | 69.4/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 280,125 B | 0.0% | 52.5/0.0 | ➡️  |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 45.0/0.0 | ➡️  |

## Regressions

### Ask_Query - CRITICAL

- **Baseline**: 974.000 ns (304 B allocated)
- **Current**: 1329.300 ns (304 B allocated)
- **Change**: +36.5%
- **Recommendation**: Fix before merge

### Create_Command - CRITICAL

- **Baseline**: 636.900 ns (40 B allocated)
- **Current**: 917.200 ns (40 B allocated)
- **Change**: +44.0%
- **Recommendation**: Fix before merge

### Create_Notification - CRITICAL

- **Baseline**: 633.400 ns (40 B allocated)
- **Current**: 917.900 ns (538 B allocated)
- **Change**: +44.9%
- **Recommendation**: Fix before merge

### Create_Query - CRITICAL

- **Baseline**: 633.000 ns (40 B allocated)
- **Current**: 916.500 ns (40 B allocated)
- **Change**: +44.8%
- **Recommendation**: Fix before merge

### Publish_Notification - CRITICAL

- **Baseline**: 943.200 ns (3,142 B allocated)
- **Current**: 1357.400 ns (264 B allocated)
- **Change**: +43.9%
- **Recommendation**: Fix before merge

### Send_Command - CRITICAL

- **Baseline**: 928.600 ns (344 B allocated)
- **Current**: 1321.100 ns (0 B allocated)
- **Change**: +42.3%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **6 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
