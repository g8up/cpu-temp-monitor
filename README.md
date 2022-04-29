# CPU temp
> 编程实时获取 CPU 温度

# 编译
> csc.exe /t:exe /r:OpenHardwareMonitorLib.dll /out:dist/cpu-temp.exe cpu-temp.cs

注：以管理页权限运行

# 依赖
- `OpenHardwareMonitorLib.dll`: 取自 `openhardwaremonitor-v0.9.6`