// Decompiled with JetBrains decompiler
// Type: ApexSpace.Bean.KeyMapping
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

namespace ApexSpace.Bean
{
    internal class KeyMapping
    {
        private int currentKeyId;
        private object targetObj;

        public int CurrentKeyId
        {
            get => this.currentKeyId;
            set => this.currentKeyId = value;
        }

        public object TargetObj
        {
            get => this.targetObj;
            set => this.targetObj = value;
        }
    }
}
