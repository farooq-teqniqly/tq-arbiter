# Performance Review Results

**Date**: 2026-02-01 22:44:44 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: c8e950626e504d8ffb3891c1af38efd1a30d7573

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 6
- **Improvements**: 1
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 1338.900 ns | +37.5% | ⚠️ CRITICAL |
| Create_Command | 636.900 ns | 917.200 ns | +44.0% | ⚠️ CRITICAL |
| Create_Notification | 633.400 ns | 906.000 ns | +43.0% | ⚠️ CRITICAL |
| Create_Query | 633.000 ns | 924.500 ns | +46.1% | ⚠️ CRITICAL |
| Publish_Notification | 943.200 ns | 1344.900 ns | +42.6% | ⚠️ CRITICAL |
| Send_Command | 928.600 ns | 1326.000 ns | +42.8% | ⚠️ CRITICAL |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 231,997 B | 0.0% | 45.0/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 263,997 B | 0.0% | 99.0/0.0 | ➡️  |
| Bulk_Send_Commands | 263,997 B | 163,665 B | -38.0% | 65.0/5.0 | ✅  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,005 B | 0.0% | 68.8/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,915 B | 0.0% | 68.8/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 280,125 B | 0.0% | 52.5/0.0 | ➡️  |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 45.0/0.0 | ➡️  |

## Regressions

### Ask_Query - CRITICAL

- **Baseline**: 974.000 ns (304 B allocated)
- **Current**: 1338.900 ns (304 B allocated)
- **Change**: +37.5%
- **Recommendation**: Fix before merge

### Create_Command - CRITICAL

- **Baseline**: 636.900 ns (40 B allocated)
- **Current**: 917.200 ns (687 B allocated)
- **Change**: +44.0%
- **Recommendation**: Fix before merge

### Create_Notification - CRITICAL

- **Baseline**: 633.400 ns (40 B allocated)
- **Current**: 906.000 ns (40 B allocated)
- **Change**: +43.0%
- **Recommendation**: Fix before merge

### Create_Query - CRITICAL

- **Baseline**: 633.000 ns (40 B allocated)
- **Current**: 924.500 ns (603 B allocated)
- **Change**: +46.1%
- **Recommendation**: Fix before merge

### Publish_Notification - CRITICAL

- **Baseline**: 943.200 ns (3,142 B allocated)
- **Current**: 1344.900 ns (264 B allocated)
- **Change**: +42.6%
- **Recommendation**: Fix before merge

### Send_Command - CRITICAL

- **Baseline**: 928.600 ns (344 B allocated)
- **Current**: 1326.000 ns (344 B allocated)
- **Change**: +42.8%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **6 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
