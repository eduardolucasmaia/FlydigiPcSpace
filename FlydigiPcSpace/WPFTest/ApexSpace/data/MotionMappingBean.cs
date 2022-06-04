// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.MotionMappingBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

namespace ApexSpace.data
{
  public class MotionMappingBean
  {
    private int type;
    private int keyId;
    private int method;
    private int zero;
    private int sensity = 25;
    private int mode;
    private int keyid_ext;
    private int tempZero;

    public int Type
    {
      get => this.type;
      set => this.type = value;
    }

    public int KeyId
    {
      get => this.keyId;
      set => this.keyId = value;
    }

    public int Method
    {
      get => this.method;
      set => this.method = value;
    }

    public int Zero
    {
      get => this.zero;
      set => this.zero = value;
    }

    public int Sensity
    {
      get => this.sensity;
      set => this.sensity = value;
    }

    public int Mode
    {
      get => this.mode;
      set => this.mode = value;
    }

    public int KeyidExt
    {
      get => this.keyid_ext;
      set => this.keyid_ext = value;
    }

    public MotionMappingBean Clone() => new MotionMappingBean()
    {
      Type = this.Type,
      KeyId = this.KeyId,
      Method = this.Method,
      Zero = this.Zero,
      Sensity = this.Sensity,
      Mode = this.Mode,
      KeyidExt = this.KeyidExt
    };
  }
}
