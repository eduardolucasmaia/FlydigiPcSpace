// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.LunpanBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

namespace ApexSpace.data
{
  public class LunpanBean
  {
    private int type = 1;
    private int rev;

    public int Type
    {
      get => this.type;
      set => this.type = value;
    }

    public int Rev
    {
      get => this.rev;
      set => this.rev = value;
    }

    public LunpanBean Clone() => new LunpanBean()
    {
      type = this.type,
      rev = this.rev
    };
  }
}
