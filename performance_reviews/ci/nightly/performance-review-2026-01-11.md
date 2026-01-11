# Performance Review Results

**Date**: 2026-01-11 22:40:47 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: 919be9357d60f2d8b07e4efd898779f43dfe396b

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 8
- **Improvements**: 0
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 1364.200 ns | +40.1% | ⚠️ CRITICAL |
| Create_Command | 636.900 ns | 919.500 ns | +44.4% | ⚠️ CRITICAL |
| Create_Notification | 633.400 ns | 917.200 ns | +44.8% | ⚠️ CRITICAL |
| Create_Query | 633.000 ns | 915.800 ns | +44.7% | ⚠️ CRITICAL |
| Publish_Notification | 943.200 ns | 1329.600 ns | +41.0% | ⚠️ CRITICAL |
| Send_Command | 928.600 ns | 1302.500 ns | +40.3% | ⚠️ CRITICAL |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 231,997 B | 0.0% | 82.5/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 2,714,511 B | +928.2% | 54.7/2.6 | ⚠️ CRITICAL |
| Bulk_Send_Commands | 263,997 B | 263,997 B | 0.0% | 97.5/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,005 B | 0.0% | 71.4/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,915 B | 0.0% | 67.0/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 3,705,210 B | +1222.7% | 57.5/5.0 | ⚠️ CRITICAL |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 85.0/2.5 | ➡️  |

## Regressions

### Ask_Query - CRITICAL

- **Baseline**: 974.000 ns (304 B allocated)
- **Current**: 1364.200 ns (304 B allocated)
- **Change**: +40.1%
- **Recommendation**: Fix before merge

### Create_Command - CRITICAL

- **Baseline**: 636.900 ns (40 B allocated)
- **Current**: 919.500 ns (40 B allocated)
- **Change**: +44.4%
- **Recommendation**: Fix before merge

### Create_Notification - CRITICAL

- **Baseline**: 633.400 ns (40 B allocated)
- **Current**: 917.200 ns (40 B allocated)
- **Change**: +44.8%
- **Recommendation**: Fix before merge

### Create_Query - CRITICAL

- **Baseline**: 633.000 ns (40 B allocated)
- **Current**: 915.800 ns (583 B allocated)
- **Change**: +44.7%
- **Recommendation**: Fix before merge

### Publish_Notification - CRITICAL

- **Baseline**: 943.200 ns (3,142 B allocated)
- **Current**: 1329.600 ns (264 B allocated)
- **Change**: +41.0%
- **Recommendation**: Fix before merge

### Send_Command - CRITICAL

- **Baseline**: 928.600 ns (344 B allocated)
- **Current**: 1302.500 ns (0 B allocated)
- **Change**: +40.3%
- **Recommendation**: Fix before merge

### Bulk_Publish_Notifications - CRITICAL

- **Baseline**: 900800.000 ns (263,997 B allocated)
- **Current**: 1307000.000 ns (2,714,511 B allocated)
- **Change**: +928.2%
- **Recommendation**: Fix before merge

### Store_Command_Results_In_List - CRITICAL

- **Baseline**: 865600.000 ns (280,125 B allocated)
- **Current**: 1277000.000 ns (3,705,210 B allocated)
- **Change**: +1222.7%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **8 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
