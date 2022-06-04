// Decompiled with JetBrains decompiler
// Type: WPFTest.Utils.FLog
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.IO;
using System.Text;

namespace WPFTest.Utils
{
  internal class FLog
  {
    public static bool log;
    public static StreamWriter logHandle;

    public static void setLog(bool logs) => FLog.log = logs;

    public static void d(string text)
    {
      if (FLog.log)
      {
        FLog.Writelog(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff") + " " + text);
      }
      else
      {
        if (FLog.logHandle == null)
          return;
        FLog.logHandle.Close();
        FLog.logHandle = (StreamWriter) null;
      }
    }

    public static StreamWriter getLogHandleInstance()
    {
      if (FLog.logHandle == null)
      {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        if (!Directory.Exists(baseDirectory))
          Directory.CreateDirectory(baseDirectory);
        try
        {
          FLog.logHandle = new StreamWriter(baseDirectory + "\\flydigi_log.txt", true, Encoding.Default);
        }
        catch (Exception ex)
        {
          FLog.logHandle = new StreamWriter(baseDirectory + "\\flydigi_logs.txt", true, Encoding.Default);
        }
      }
      return FLog.logHandle;
    }

    public static void Writelog(string msg)
    {
      FLog.getLogHandleInstance();
      FLog.logHandle.Write(DateTime.Now.ToString() + ":" + msg);
      FLog.logHandle.Write("\r\n");
      FLog.logHandle.Flush();
    }
  }
}
