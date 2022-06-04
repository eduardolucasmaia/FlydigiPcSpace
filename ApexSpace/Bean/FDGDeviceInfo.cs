// Decompiled with JetBrains decompiler
// Type: ApexSpace.Bean.FDGDeviceInfo
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using WPFTest.Utils;

namespace ApexSpace.Bean
{
    public class FDGDeviceInfo
    {
        private string firmwareVersion;
        private int firmwareVersionCode;
        private int batteryPercent = -1;
        private int deviceId = -1;
        private string dongleVersion = "";
        private string deviceMac = "";
        public int connectType = -1;
        public string cpuType;
        public string cpuName;
        public string gameHadleName;
        public string firmwareName;

        public string FirmwareVersion
        {
            get => this.firmwareVersion;
            set => this.firmwareVersion = value;
        }

        public int BatteryPercent
        {
            get => this.batteryPercent;
            set => this.batteryPercent = value;
        }

        public int DeviceId
        {
            get => this.deviceId;
            set => this.deviceId = value;
        }

        public int FirmwareVersionCode
        {
            get => this.firmwareVersionCode;
            set => this.firmwareVersionCode = value;
        }

        public string DongleVersion
        {
            get => this.dongleVersion;
            set => this.dongleVersion = value;
        }

        public string DeviceMac
        {
            get => this.deviceMac;
            set => this.deviceMac = value;
        }

        public int ConnectType
        {
            get => this.connectType;
            set => this.connectType = value;
        }

        public string getGameHandleName()
        {
            if (this.DeviceId > 0 && (this.DeviceId == Constant.DEVICE.F1 || this.DeviceId == Constant.DEVICE.F1_MERGE || this.DeviceId == Constant.DEVICE.F1_WIRED))
            {
                this.gameHadleName = "f1";
                if (this.cpuType == "wch")
                    this.gameHadleName = "f1wch";
            }
            else if (this.DeviceId > 0 && this.DeviceId == Constant.DEVICE.APEX_2)
                this.gameHadleName = "apex2";
            else if (this.DeviceId > 0 && this.DeviceId == Constant.DEVICE.F1_PRO)
                this.gameHadleName = "f1p";
            return this.gameHadleName;
        }

        public string getFirmwareName()
        {
            if (this.DeviceId > 0 && this.DeviceId == Constant.DEVICE.F1)
            {
                this.firmwareName = "f1";
                if (this.cpuType == "wch")
                    this.firmwareName = "f1wch";
            }
            else if (this.DeviceId > 0 && this.DeviceId == Constant.DEVICE.F1_MERGE)
                this.firmwareName = "f1p";
            else if (this.DeviceId > 0 && this.DeviceId == Constant.DEVICE.APEX_2)
                this.firmwareName = "apex2";
            else if (this.DeviceId > 0 && this.DeviceId == Constant.DEVICE.F1_PRO)
                this.firmwareName = "f1p";
            return this.firmwareName;
        }

        public void setGameHandleName()
        {
            if (this.DeviceId > 0 && (this.DeviceId == Constant.DEVICE.F1 || this.DeviceId == Constant.DEVICE.F1_MERGE))
            {
                this.gameHadleName = "f1";
                if (!(this.cpuType == "wch"))
                    return;
                this.gameHadleName = "f1wch";
            }
            else if (this.DeviceId > 0 && this.DeviceId == Constant.DEVICE.APEX_2)
            {
                this.gameHadleName = "apex2";
            }
            else
            {
                if (this.DeviceId <= 0 || this.DeviceId != Constant.DEVICE.F1_PRO)
                    return;
                this.gameHadleName = "f1p";
            }
        }
    }
}
