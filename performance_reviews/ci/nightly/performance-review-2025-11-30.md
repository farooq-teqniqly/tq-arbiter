# Performance Review Results - Initial Baseline

**Date**: 2025-11-30 18:56:46 UTC
**Baseline**: Initial Run
**Commit**: 2ebbce98962810b772de32f21b6a646c8d4769fe

## Summary

This is the **initial benchmark run**. No baseline exists for comparison.

- **Total Benchmarks**: 14
- **Status**: ✅ INITIAL BASELINE ESTABLISHED

## Benchmarks Recorded

The following benchmarks will serve as the baseline for future comparisons:


### CPU Benchmarks

- **Ask_Query**: 944.400 ns (0 B)
- **Create_Command**: 640.400 ns (40 B)
- **Create_Notification**: 636.000 ns (40 B)
- **Create_Query**: 636.700 ns (40 B)
- **Publish_Notification**: 931.000 ns (264 B)
- **Send_Command**: 942.900 ns (344 B)

### Memory Benchmarks

- **Bulk_Ask_Queries**: 908000.000 ns (232,000 B, Gen0/1: 44.6/0.0)
- **Bulk_Publish_Notifications**: 913000.000 ns (264,002 B, Gen0/1: 100.0/0.0)
- **Bulk_Send_Commands**: 861700.000 ns (264,000 B, Gen0/1: 50.3/0.0)
- **Create_And_Ask_Queries**: 1597200.000 ns (320,000 B, Gen0/1: 62.5/0.0)
- **Create_And_Publish_Notifications**: 1645800.000 ns (0 B, Gen0/1: 78.1/5.2)
- **Create_And_Send_Commands**: 1575000.000 ns (359,920 B, Gen0/1: 67.7/0.0)
- **Store_Command_Results_In_List**: 870200.000 ns (0 B, Gen0/1: 66.0/3.5)
- **Store_Query_Results_In_List**: 892800.000 ns (240,128 B, Gen0/1: 46.4/0.0)

## Next Steps

- [x] Initial baseline established
- [x] Future runs will compare against this baseline
- [x] Performance regressions will be automatically detected

## Conclusion

✅ **Initial baseline successfully established.** Future benchmark runs will compare against these values.
