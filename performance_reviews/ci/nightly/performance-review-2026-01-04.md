# Performance Review Results

**Date**: 2026-01-04 22:41:17 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: 534080eb882ccdbec270df27aeb6e401482908bc

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 7
- **Improvements**: 1
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 1322.500 ns | +35.8% | ⚠️ CRITICAL |
| Create_Command | 636.900 ns | 935.600 ns | +46.9% | ⚠️ CRITICAL |
| Create_Notification | 633.400 ns | 946.000 ns | +49.4% | ⚠️ CRITICAL |
| Create_Query | 633.000 ns | 940.900 ns | +48.6% | ⚠️ CRITICAL |
| Publish_Notification | 943.200 ns | 1335.800 ns | +41.6% | ⚠️ CRITICAL |
| Send_Command | 928.600 ns | 1316.200 ns | +41.7% | ⚠️ CRITICAL |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 232,000 B | +0.0% | 45.0/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 2,524,046 B | +856.1% | 54.7/2.6 | ⚠️ CRITICAL |
| Bulk_Send_Commands | 263,997 B | 0 B | -100.0% | 77.5/2.5 | ✅  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,004 B | -0.0% | 71.4/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,920 B | +0.0% | 68.8/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 280,128 B | +0.0% | 52.5/2.5 | ➡️  |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 45.0/0.0 | ➡️  |

## Regressions

### Ask_Query - CRITICAL

- **Baseline**: 974.000 ns (304 B allocated)
- **Current**: 1322.500 ns (0 B allocated)
- **Change**: +35.8%
- **Recommendation**: Fix before merge

### Create_Command - CRITICAL

- **Baseline**: 636.900 ns (40 B allocated)
- **Current**: 935.600 ns (40 B allocated)
- **Change**: +46.9%
- **Recommendation**: Fix before merge

### Create_Notification - CRITICAL

- **Baseline**: 633.400 ns (40 B allocated)
- **Current**: 946.000 ns (40 B allocated)
- **Change**: +49.4%
- **Recommendation**: Fix before merge

### Create_Query - CRITICAL

- **Baseline**: 633.000 ns (40 B allocated)
- **Current**: 940.900 ns (40 B allocated)
- **Change**: +48.6%
- **Recommendation**: Fix before merge

### Publish_Notification - CRITICAL

- **Baseline**: 943.200 ns (3,142 B allocated)
- **Current**: 1335.800 ns (264 B allocated)
- **Change**: +41.6%
- **Recommendation**: Fix before merge

### Send_Command - CRITICAL

- **Baseline**: 928.600 ns (344 B allocated)
- **Current**: 1316.200 ns (0 B allocated)
- **Change**: +41.7%
- **Recommendation**: Fix before merge

### Bulk_Publish_Notifications - CRITICAL

- **Baseline**: 900800.000 ns (263,997 B allocated)
- **Current**: 1308000.000 ns (2,524,046 B allocated)
- **Change**: +856.1%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **7 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
