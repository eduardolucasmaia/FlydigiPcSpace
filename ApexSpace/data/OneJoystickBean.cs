// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.OneJoystickBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

namespace ApexSpace.data
{
    public class OneJoystickBean
    {
        private int zero;
        private int point0_x;
        private int point0_y;
        private int point1_x;
        private int point1_y;

        public int Zero
        {
            get => this.zero;
            set => this.zero = value;
        }

        public int Point0_X
        {
            get => this.point0_x;
            set => this.point0_x = value;
        }

        public int Point0_Y
        {
            get => this.point0_y;
            set => this.point0_y = value;
        }

        public int Point1_X
        {
            get => this.point1_x;
            set => this.point1_x = value;
        }

        public int Point1_Y
        {
            get => this.point1_y;
            set => this.point1_y = value;
        }

        public OneJoystickBean Clone() => new OneJoystickBean()
        {
            Zero = this.Zero,
            Point0_X = this.Point0_X,
            Point0_Y = this.Point0_Y,
            Point1_X = this.Point1_X,
            Point1_Y = this.Point1_Y
        };
    }
}
