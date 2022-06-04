// Decompiled with JetBrains decompiler
// Type: ApexSpace.data.ConfigBean
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System.Collections.Generic;

namespace ApexSpace.data
{
    public class ConfigBean
    {
        private string version;
        private string name;
        private LedBean led;
        private List<int> listKeyMapping = new List<int>();
        private JoyMappingBean joyMapping;
        private MotionMappingBean motionMapping;
        private AllMacroBean macro;
        private MotorBean motor;
        private LunpanBean lunpan;

        public string Version
        {
            get => this.version;
            set => this.version = value;
        }

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        public LedBean Led
        {
            get => this.led;
            set => this.led = value;
        }

        public List<int> ListKeyMapping
        {
            get => this.listKeyMapping;
            set => this.listKeyMapping = value;
        }

        public JoyMappingBean JoyMapping
        {
            get => this.joyMapping;
            set => this.joyMapping = value;
        }

        public MotionMappingBean MotionMapping
        {
            get => this.motionMapping;
            set => this.motionMapping = value;
        }

        public AllMacroBean Macro
        {
            get => this.macro;
            set => this.macro = value;
        }

        public MotorBean Motor
        {
            get => this.motor;
            set => this.motor = value;
        }

        public LunpanBean LunpanMapping
        {
            get => this.lunpan;
            set => this.lunpan = value;
        }

        public ConfigBean Clone()
        {
            ConfigBean configBean = new ConfigBean();
            configBean.Version = this.Version;
            configBean.Name = this.Name;
            configBean.Led = this.Led.Clone();
            configBean.ListKeyMapping.AddRange((IEnumerable<int>)this.ListKeyMapping);
            configBean.JoyMapping = this.JoyMapping.Clone();
            configBean.MotionMapping = this.MotionMapping.Clone();
            configBean.Macro = this.Macro.Clone();
            configBean.Motor = this.Motor.Clone();
            configBean.LunpanMapping = this.LunpanMapping.Clone();
            return configBean;
        }
    }
}
