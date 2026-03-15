# Performance Review Results

**Date**: 2026-03-15 22:46:04 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: d3471aeacb3baadc945370dfd56e349ef03bad6c

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 6
- **Improvements**: 0
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 1338.400 ns | +37.4% | ⚠️ CRITICAL |
| Create_Command | 636.900 ns | 910.200 ns | +42.9% | ⚠️ CRITICAL |
| Create_Notification | 633.400 ns | 931.900 ns | +47.1% | ⚠️ CRITICAL |
| Create_Query | 633.000 ns | 919.100 ns | +45.2% | ⚠️ CRITICAL |
| Publish_Notification | 943.200 ns | 1286.900 ns | +36.4% | ⚠️ CRITICAL |
| Send_Command | 928.600 ns | 1340.300 ns | +44.3% | ⚠️ CRITICAL |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 231,997 B | 0.0% | 42.6/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 263,997 B | 0.0% | 50.0/0.0 | ➡️  |
| Bulk_Send_Commands | 263,997 B | 263,997 B | 0.0% | 50.0/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,005 B | 0.0% | 69.4/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,915 B | 0.0% | 67.3/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 280,125 B | 0.0% | 52.5/0.0 | ➡️  |
| Store_Query_Results_In_List | 240,128 B | 240,128 B | 0.0% | 45.0/0.0 | ➡️  |

## Regressions

### Ask_Query - CRITICAL

- **Baseline**: 974.000 ns (304 B allocated)
- **Current**: 1338.400 ns (304 B allocated)
- **Change**: +37.4%
- **Recommendation**: Fix before merge

### Create_Command - CRITICAL

- **Baseline**: 636.900 ns (40 B allocated)
- **Current**: 910.200 ns (40 B allocated)
- **Change**: +42.9%
- **Recommendation**: Fix before merge

### Create_Notification - CRITICAL

- **Baseline**: 633.400 ns (40 B allocated)
- **Current**: 931.900 ns (40 B allocated)
- **Change**: +47.1%
- **Recommendation**: Fix before merge

### Create_Query - CRITICAL

- **Baseline**: 633.000 ns (40 B allocated)
- **Current**: 919.100 ns (40 B allocated)
- **Change**: +45.2%
- **Recommendation**: Fix before merge

### Publish_Notification - CRITICAL

- **Baseline**: 943.200 ns (3,142 B allocated)
- **Current**: 1286.900 ns (264 B allocated)
- **Change**: +36.4%
- **Recommendation**: Fix before merge

### Send_Command - CRITICAL

- **Baseline**: 928.600 ns (344 B allocated)
- **Current**: 1340.300 ns (344 B allocated)
- **Change**: +44.3%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **6 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
