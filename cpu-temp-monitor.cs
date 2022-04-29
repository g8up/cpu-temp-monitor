/**
https://stackoverflow.com/questions/553143/compiling-executing-a-c-sharp-source-file-in-command-prompt
csc.exe /t:exe /r:OpenHardwareMonitorLib.dll /out:cpu-temp-monitor.exe cpu-temp-monitor.cs
以管理页权限运行
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;
namespace Get_CPU_Temp5
{
  class Program
  {
    public class UpdateVisitor : IVisitor
    {
      public void VisitComputer(IComputer computer)
      {
        computer.Traverse(this);
      }

      public void VisitHardware(IHardware hardware)
      {
        hardware.Update();
        foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
      }

      public void VisitSensor(ISensor sensor) { }

      public void VisitParameter(IParameter parameter) { }
    }

    static void GetSystemInfo()
    {
      UpdateVisitor updateVisitor = new UpdateVisitor();
      Computer computer = new Computer();
      computer.Open();
      computer.CPUEnabled = true;
      computer.Accept(updateVisitor);
      for (int i = 0; i < computer.Hardware.Length; i++)
      {
        if (computer.Hardware[i].HardwareType == HardwareType.CPU)
        {
          for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
          {
            if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature) {
              Console.WriteLine(computer.Hardware[i].Sensors[j].Name + ":" + computer.Hardware[i].Sensors[j].Value.ToString() + "\r");
              // 温度高于70度是 beep 警报
              if( computer.Hardware[i].Sensors[j].Value> 70) {
                Console.Beep();
              }
            }
          }
          Console.WriteLine("\n");
        }
      }
      computer.Close();
    }

    static void Main(string[] args)
    {
      while (true)
      {
        GetSystemInfo();
        System.Threading.Thread.Sleep(10000); // 10秒轮询一次
      }
    }
  }
}