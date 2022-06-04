// Decompiled with JetBrains decompiler
// Type: WPFTest.Bean.FDGEventInfo
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

namespace WPFTest.Bean
{
  internal class FDGEventInfo
  {
    private string uid;
    private string appVersion;
    private string osVersion;
    private string osBit;
    private string lang;
    private string action;
    private string cpu_type;
    private string device_type;

    public string Uid
    {
      get => this.uid;
      set => this.uid = value;
    }

    public string AppVersion
    {
      get => this.appVersion;
      set => this.appVersion = value;
    }

    public string OsVersion
    {
      get => this.osVersion;
      set => this.osVersion = value;
    }

    public string OsBit
    {
      get => this.osBit;
      set => this.osBit = value;
    }

    public string Action
    {
      get => this.action;
      set => this.action = value;
    }

    public string Lang
    {
      get => this.lang;
      set => this.lang = value;
    }

    public string cpuType
    {
      get => this.cpu_type;
      set => this.cpu_type = value;
    }

    public string deviceType
    {
      get => this.device_type;
      set => this.device_type = value;
    }
  }
}
