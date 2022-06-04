// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.MacroActionBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

namespace ApexSpace.data
{
  public class MacroActionBean
  {
    private byte time_l;
    private byte time_h;
    private int btn;
    private int state;

    public byte Time_l
    {
      get => this.time_l;
      set => this.time_l = value;
    }

    public byte Time_h
    {
      get => this.time_h;
      set => this.time_h = value;
    }

    public int Btn
    {
      get => this.btn;
      set => this.btn = value;
    }

    public int State
    {
      get => this.state;
      set => this.state = value;
    }

    public MacroActionBean Clone() => new MacroActionBean()
    {
      Time_l = this.Time_l,
      Time_h = this.Time_h,
      Btn = this.Btn,
      State = this.State
    };
  }
}
