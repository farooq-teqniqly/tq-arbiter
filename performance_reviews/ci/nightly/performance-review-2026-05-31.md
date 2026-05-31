# Performance Review Results

**Date**: 2026-05-31 22:50:55 UTC
**Baseline**: 2026-05-24T22:50:49.690761
**Commit**: 8a3ddc1fde3d965b504fd32f63d5ed94b8cf7037

## Summary

- **Total Benchmarks**: 14
- **Regressions**: 1
- **Improvements**: 2
- **Status**: ⚠️ REGRESSIONS FOUND (CRITICAL)

## CPU Benchmarks

| Benchmark | Baseline | Current | Change | Status |
|-----------|----------|---------|--------|--------|
| Ask_Query | 956.100 ns | 952.800 ns | -0.3% | ➡️  |
| Create_Command | 631.800 ns | 631.000 ns | -0.1% | ➡️  |
| Create_Notification | 628.100 ns | 627.600 ns | -0.1% | ➡️  |
| Create_Query | 623.600 ns | 627.600 ns | +0.6% | ➡️  |
| Publish_Notification | 947.700 ns | 949.800 ns | +0.2% | ➡️  |
| Send_Command | 928.900 ns | 929.600 ns | +0.1% | ➡️  |

## Memory Benchmarks

| Benchmark | Baseline | Current | Alloc Change | Gen0/1 | Status |
|-----------|----------|---------|--------------|--------|--------|
| Bulk_Ask_Queries | 232,000 B | 232,000 B | 0.0% | 45.1/0.0 | ➡️  |
| Bulk_Publish_Notifications | 264,000 B | 0 B | -100.0% | 69.6/1.8 | ✅  |
| Bulk_Send_Commands | 264,000 B | 264,000 B | 0.0% | 99.7/0.0 | ➡️  |
| Create_And_Ask_Queries | 320,000 B | 320,000 B | 0.0% | 62.5/0.0 | ➡️  |
| Create_And_Publish_Notifications | 368,000 B | 0 B | -100.0% | 93.8/5.2 | ✅  |
| Create_And_Send_Commands | 0 B | 359,920 B | 0.0% | 67.7/0.0 | ➡️  |
| Store_Command_Results_In_List | 280,128 B | 2,373,219 B | +747.2% | 55.7/3.4 | ⚠️ CRITICAL |
| Store_Query_Results_In_List | 0 B | 240,128 B | 0.0% | 90.3/1.7 | ➡️  |

## Regressions

### Store_Command_Results_In_List - CRITICAL

- **Baseline**: 879700.000 ns (280,128 B allocated)
- **Current**: 887400.000 ns (2,373,219 B allocated)
- **Change**: +747.2%
- **Recommendation**: Fix before merge


## Action Items

- [ ] Review regression details above
- [ ] Investigate root cause of performance degradation
- [ ] Fix regression or document justification

## Conclusion

⚠️ **1 regression(s) detected with CRITICAL severity.** Please review and address before baseline is updated.
