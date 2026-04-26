# Performance Review Results

**Date**: 2026-04-26 22:48:08 UTC
**Baseline**: 2025-12-07T22:39:09.802928
**Commit**: 4985fa9bb0196cd7205de2f8e662a85235649b32

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 1
- **Improvements**: 1
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 974.000 ns | 930.600 ns | -4.5% | ➡️  |
| Create_Command | 636.900 ns | 632.800 ns | -0.6% | ➡️  |
| Create_Notification | 633.400 ns | 635.400 ns | +0.3% | ➡️  |
| Create_Query | 633.000 ns | 632.400 ns | -0.1% | ➡️  |
| Publish_Notification | 943.200 ns | 964.900 ns | +2.3% | ➡️  |
| Send_Command | 928.600 ns | 934.400 ns | +0.6% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 231,997 B | 232,000 B | +0.0% | 44.6/0.0 | ➡️  |
| Bulk_Publish_Notifications | 263,997 B | 264,002 B | +0.0% | 100.0/0.0 | ➡️  |
| Bulk_Send_Commands | 263,997 B | 0 B | -100.0% | 54.1/1.7 | ✅  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,005 B | 368,000 B | -0.0% | 67.7/0.0 | ➡️  |
| Create_And_Send_Commands | 359,915 B | 359,920 B | +0.0% | 130.2/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,125 B | 280,128 B | +0.0% | 105.9/5.2 | ➡️  |
| Store_Query_Results_In_List | 240,128 B | 3,004,590 B | +1151.2% | 48.6/1.7 | ⚠️ CRITICAL |

## Regressions

### Store_Query_Results_In_List - CRITICAL

- **Baseline**: 879700.000 ns (240,128 B allocated)
- **Current**: 889700.000 ns (3,004,590 B allocated)
- **Change**: +1151.2%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **1 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
