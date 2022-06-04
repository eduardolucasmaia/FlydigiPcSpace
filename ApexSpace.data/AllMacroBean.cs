// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.AllMacroBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System.Collections.Generic;

namespace ApexSpace.data
{
    public class AllMacroBean
    {
        private int nums;
        private List<int> listOffset = new List<int>();
        private List<OneMacroBean> listMacro = new List<OneMacroBean>();

        public int Nums
        {
            get => this.nums;
            set => this.nums = value;
        }

        public List<int> ListOffset
        {
            get => this.listOffset;
            set => this.listOffset = value;
        }

        public List<OneMacroBean> ListMacro
        {
            get => this.listMacro;
            set => this.listMacro = value;
        }

        public AllMacroBean Clone()
        {
            AllMacroBean allMacroBean = new AllMacroBean();
            allMacroBean.Nums = this.Nums;
            allMacroBean.listOffset.AddRange((IEnumerable<int>)this.listOffset);
            for (int index = 0; index < this.ListMacro.Count; ++index)
                allMacroBean.ListMacro.Add(this.ListMacro[index].Clone());
            return allMacroBean;
        }
    }
}
