// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.LedBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System.Collections.Generic;

namespace ApexSpace.data
{
    public class LedBean
    {
        private int header;
        private int mode;
        private int peroid;
        private int light;
        private List<int> rgbColor0 = new List<int>();
        private List<int> rgbColor1 = new List<int>();
        private int leftMotor;
        private int rightMotor;

        public int Header
        {
            get => this.header;
            set => this.header = value;
        }

        public int Mode
        {
            get => this.mode;
            set => this.mode = value;
        }

        public int Peroid
        {
            get => this.peroid;
            set => this.peroid = value;
        }

        public int Light
        {
            get => this.light;
            set => this.light = value;
        }

        public List<int> RgbColor0
        {
            get => this.rgbColor0;
            set => this.rgbColor0 = value;
        }

        public List<int> RgbColor1
        {
            get => this.rgbColor1;
            set => this.rgbColor1 = value;
        }

        public int LeftMotor
        {
            get => this.leftMotor;
            set => this.leftMotor = value;
        }

        public int RightMotor
        {
            get => this.rightMotor;
            set => this.rightMotor = value;
        }

        public LedBean Clone()
        {
            LedBean ledBean = new LedBean();
            ledBean.Header = this.Header;
            ledBean.Mode = this.Mode;
            ledBean.Peroid = this.Peroid;
            ledBean.Light = this.Light;
            ledBean.RgbColor0.AddRange((IEnumerable<int>)this.RgbColor0);
            ledBean.RgbColor1.AddRange((IEnumerable<int>)this.RgbColor1);
            ledBean.LeftMotor = this.LeftMotor;
            ledBean.RightMotor = this.RightMotor;
            return ledBean;
        }
    }
}
