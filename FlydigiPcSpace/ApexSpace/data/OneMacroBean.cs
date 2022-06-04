// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.OneMacroBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System.Collections.Generic;

namespace ApexSpace.data
{
  public class OneMacroBean
  {
    private int btn;
    private int count_l;
    private int count_h;
    private int type;
    private List<MacroActionBean> listStep = new List<MacroActionBean>();

    public int Btn
    {
      get => this.btn;
      set => this.btn = value;
    }

    public int Count_l
    {
      get => this.count_l;
      set => this.count_l = value;
    }

    public int Count_h
    {
      get => this.count_h;
      set => this.count_h = value;
    }

    public int Type
    {
      get => this.type;
      set => this.type = value;
    }

    public List<MacroActionBean> ListStep
    {
      get => this.listStep;
      set => this.listStep = value;
    }

    public OneMacroBean Clone()
    {
      OneMacroBean oneMacroBean = new OneMacroBean();
      oneMacroBean.Btn = this.Btn;
      oneMacroBean.Count_l = this.Count_l;
      oneMacroBean.Count_h = this.Count_h;
      oneMacroBean.Type = this.Type;
      for (int index = 0; index < this.ListStep.Count; ++index)
        oneMacroBean.ListStep.Add(this.ListStep[index].Clone());
      return oneMacroBean;
    }
  }
}
