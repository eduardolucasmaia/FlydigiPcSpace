// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.JoyMappingBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

namespace ApexSpace.data
{
    public class JoyMappingBean
    {
        private OneJoystickBean leftJoyStick;
        private OneJoystickBean rightJoyStick;

        public OneJoystickBean LeftJoyStick
        {
            get => this.leftJoyStick;
            set => this.leftJoyStick = value;
        }

        public OneJoystickBean RightJoyStick
        {
            get => this.rightJoyStick;
            set => this.rightJoyStick = value;
        }

        public JoyMappingBean Clone() => new JoyMappingBean()
        {
            LeftJoyStick = this.LeftJoyStick.Clone(),
            RightJoyStick = this.RightJoyStick.Clone()
        };
    }
}
