# Performance Review Results

**Date**: 2026-03-01 22:43:37 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: 83c02b2376237c47df7e926ab04621625be462a4

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 8
- **Improvements**: 0
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 1328.300 ns | +36.4% | ⚠️ CRITICAL |
| Create_Command | 636.900 ns | 906.900 ns | +42.4% | ⚠️ CRITICAL |
| Create_Notification | 633.400 ns | 905.800 ns | +43.0% | ⚠️ CRITICAL |
| Create_Query | 633.000 ns | 905.400 ns | +43.0% | ⚠️ CRITICAL |
| Publish_Notification | 943.200 ns | 1329.000 ns | +40.9% | ⚠️ CRITICAL |
| Send_Command | 928.600 ns | 1322.300 ns | +42.4% | ⚠️ CRITICAL |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 231,997 B | 0.0% | 45.0/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 3,370,321 B | +1176.7% | 55.0/2.5 | ⚠️ CRITICAL |
| Bulk_Send_Commands | 263,997 B | 263,997 B | 0.0% | 50.0/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,005 B | 0.0% | 68.8/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,915 B | 0.0% | 68.8/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 3,782,533 B | +1250.3% | 57.5/5.0 | ⚠️ CRITICAL |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 45.0/0.0 | ➡️  |

## Regressions

### Ask_Query - CRITICAL

- **Baseline**: 974.000 ns (304 B allocated)
- **Current**: 1328.300 ns (0 B allocated)
- **Change**: +36.4%
- **Recommendation**: Fix before merge

### Create_Command - CRITICAL

- **Baseline**: 636.900 ns (40 B allocated)
- **Current**: 906.900 ns (40 B allocated)
- **Change**: +42.4%
- **Recommendation**: Fix before merge

### Create_Notification - CRITICAL

- **Baseline**: 633.400 ns (40 B allocated)
- **Current**: 905.800 ns (40 B allocated)
- **Change**: +43.0%
- **Recommendation**: Fix before merge

### Create_Query - CRITICAL

- **Baseline**: 633.000 ns (40 B allocated)
- **Current**: 905.400 ns (0 B allocated)
- **Change**: +43.0%
- **Recommendation**: Fix before merge

### Publish_Notification - CRITICAL

- **Baseline**: 943.200 ns (3,142 B allocated)
- **Current**: 1329.000 ns (0 B allocated)
- **Change**: +40.9%
- **Recommendation**: Fix before merge

### Send_Command - CRITICAL

- **Baseline**: 928.600 ns (344 B allocated)
- **Current**: 1322.300 ns (344 B allocated)
- **Change**: +42.4%
- **Recommendation**: Fix before merge

### Bulk_Publish_Notifications - CRITICAL

- **Baseline**: 900800.000 ns (263,997 B allocated)
- **Current**: 1310000.000 ns (3,370,321 B allocated)
- **Change**: +1176.7%
- **Recommendation**: Fix before merge

### Store_Command_Results_In_List - CRITICAL

- **Baseline**: 865600.000 ns (280,125 B allocated)
- **Current**: 1278000.000 ns (3,782,533 B allocated)
- **Change**: +1250.3%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **8 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
