// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.MotorBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

namespace ApexSpace.data
{
    public class MotorBean
    {
        private int left_level;
        private int right_level;
        private int level;

        public int Level
        {
            get => this.level;
            set => this.level = value;
        }

        public int LeftLevel
        {
            get => this.left_level;
            set => this.left_level = value;
        }

        public int RightLevel
        {
            get => this.right_level;
            set => this.right_level = value;
        }

        public MotorBean Clone() => new MotorBean()
        {
            Level = this.Level,
            LeftLevel = this.LeftLevel,
            RightLevel = this.RightLevel
        };
    }
}
