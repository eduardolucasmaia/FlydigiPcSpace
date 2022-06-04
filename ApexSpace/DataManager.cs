// Decompiled with JetBrains decompiler
// Type: ApexSpace.DataManager
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace.Bean;
using ApexSpace.data;
using HidSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using WPFTest;
using WPFTest.Utils;

namespace ApexSpace
{
    internal class DataManager
    {
        private DeviceStream mDeviceStream;
        private int mReadReportLength = 32;
        private byte[] ReadBuffer = new byte[32];
        private System.Timers.Timer mTimer;
        private int mCurrentCount;
        private bool mUpdatePartlyWriting;
        private int mDeviceBatteryReadCount;
        private int MaxDeviceBatteryReadCount = 10;
        private byte[] mByteArray14 = new byte[15];
        private byte[] mByteArray33 = new byte[33];
        private bool switchMode;
        private bool appActivateStatus = true;
        private WindowMain mWindowMain;
        private static DataManager mDataManager;
        private int mDeviceConnectState = -1;
        private int mDeviceConnectCount;
        private DataManager.IDeviceOperateData mIDeviceOperateData;
        public DataManager.IDeviceState mIDeviceState;
        private DataManager.IDataLog mIDataLog;
        private bool mLive = true;
        private int mCurrentDataMode = -1;
        private int ModeRead;
        private int ModeWrite = 1;
        private List<List<byte>> mConfigList = new List<List<byte>>();
        private int mParcelCount;
        private int mCurrentParcelIndex;
        private bool mWriting;
        private IDelegateCallback mIDelegateCallback;
        private bool mReadConfigList;
        private int mReadConfigId;
        private int mLastConfigId;
        private IDelegateCallback mIDelegateCallbackCurrentConfigId;
        private Dictionary<int, ConfigBean> mDictionaryConfig = new Dictionary<int, ConfigBean>();
        private List<byte> mTempListByte = new List<byte>();
        private FDGDeviceInfo mFDGDeviceInfo = new FDGDeviceInfo();
        private bool DebugLog;

        public bool getUpdatePartlyWritingState() => this.mUpdatePartlyWriting;

        public void setUpdatePartlyWritingState(bool state) => this.mUpdatePartlyWriting = state;

        public void setMainWindow(WindowMain windowMain) => this.mWindowMain = windowMain;

        public bool getSwitchMode() => this.switchMode;

        public void setSwitchMode(bool state) => this.switchMode = state;

        public bool getAppActivateStatus() => this.appActivateStatus;

        public void setAppActivateStatus(bool state) => this.appActivateStatus = state;

        public static DataManager getInstance()
        {
            if (DataManager.mDataManager == null)
                DataManager.mDataManager = new DataManager();
            return DataManager.mDataManager;
        }

        private DataManager() => this.Initial();

        public int getDeviceConnectState() => this.mDeviceConnectState;

        public int getDeviceConnectCount() => this.mDeviceConnectCount;

        public void setIDeviceOperateData(DataManager.IDeviceOperateData iDeviceOperateData) => this.mIDeviceOperateData = iDeviceOperateData;

        public void setIDeviceStateImpl(DataManager.IDeviceState iDeviceState) => this.mIDeviceState = iDeviceState;

        public void setIDataLogImpl(DataManager.IDataLog iDataLog) => this.mIDataLog = iDataLog;

        public void Initial()
        {
            this.mLive = true;
            FLog.d("datamanager start");
            this.setTimer();
            this.checkAndConnectFlydigiGamepad();
        }

        private void setTimer()
        {
            this.mTimer = new System.Timers.Timer(1000.0);
            this.mTimer.AutoReset = true;
            this.mTimer.Enabled = true;
            this.mTimer.Elapsed += new ElapsedEventHandler(this.TimerUp);
            this.mTimer.Start();
        }

        private string getDongleVersionByDeviceInfo(string info)
        {
            int num = info.ToLower().IndexOf("version");
            return info.Substring(num + 8, 3);
        }

        private int getConnectTypeByDeviceInfo(string info)
        {
            if (info.Contains("no serial number"))
                return 1;
            return info.Contains("flydigi 2.4g x360") ? 2 : -1;
        }

        private void checkAndConnectFlydigiGamepad() => new Thread((ParameterizedThreadStart)(o =>
        {
            while (this.mLive)
            {
                FLog.d("检查设备状态中......");
                if (this.mDeviceStream != null)
                    break;
                FLog.d("搜索设备......");
                List<Device> list1 = DeviceList.Local.GetAllDevices().ToList<Device>();
                int num1 = 0;
                bool flag = true;
                for (int index = 0; index < list1.Count; ++index)
                {
                    string lower = list1[index].DevicePath.ToLower();
                    if (list1[index].ToString().ToLower().Contains("flydigi") && (lower.Contains("vid_04b4") && lower.Contains("pid_2410") && lower.Contains("mi_02") || lower.Contains("vid_04b4") && lower.Contains("pid_2411") && lower.Contains("mi_02") || lower.Contains("vid_04b4") && lower.Contains("pid_2412") && lower.Contains("mi_02") || lower.Contains("vid_045e") && lower.Contains("pid_028e")))
                        ++num1;
                }
                this.mDeviceConnectCount = num1;
                for (int index = 0; index < list1.Count; ++index)
                {
                    string lower1 = list1[index].DevicePath.ToLower();
                    string lower2 = list1[index].ToString().ToLower();
                    if (lower2.Contains("flydigi") && (lower1.Contains("vid_04b4") && lower1.Contains("pid_2410") && lower1.Contains("mi_02") || lower1.Contains("vid_04b4") && lower1.Contains("pid_2411") && lower1.Contains("mi_02") || lower1.Contains("vid_04b4") && lower1.Contains("pid_2412") && lower1.Contains("mi_02") || lower1.Contains("vid_045e") && lower1.Contains("pid_028e")))
                    {
                        this.getDeviceInfo().DongleVersion = this.getDongleVersionByDeviceInfo(lower2);
                        this.getDeviceInfo().ConnectType = this.getConnectTypeByDeviceInfo(lower2);
                        string lower3 = list1[index].ToString().ToLower();
                        FLog.d("设备已连接,信息:" + lower3);
                        try
                        {
                            this.mDeviceStream = list1[index].Open();
                        }
                        catch (Exception ex)
                        {
                            FLog.d("DeviceStream打开异常:" + ex.Message);
                        }
                        this.mReadReportLength = !lower3.Contains("controller") ? 32 : 15;
                        this.ReadBuffer = new byte[this.mReadReportLength];
                        break;
                    }
                }
                while (this.mDeviceStream != null)
                {
                    try
                    {
                        List<Device> list2 = DeviceList.Local.GetAllDevices().ToList<Device>();
                        int num2 = 0;
                        for (int index = 0; index < list2.Count; ++index)
                        {
                            string lower = list2[index].DevicePath.ToLower();
                            if (list2[index].ToString().ToLower().Contains("flydigi") && (lower.Contains("vid_04b4") && lower.Contains("pid_2410") && lower.Contains("mi_02") || lower.Contains("vid_04b4") && lower.Contains("pid_2411") && lower.Contains("mi_02") || lower.Contains("vid_04b4") && lower.Contains("pid_2412") && lower.Contains("mi_02") || lower.Contains("vid_045e") && lower.Contains("pid_028e")))
                                ++num2;
                        }
                        this.mDeviceConnectCount = num2;
                        if (this.mDeviceConnectCount > 1 & flag)
                            flag = false;
                        if (this.mDeviceConnectCount <= 1)
                            flag = true;
                        if (this.mDeviceStream != null)
                        {
                            this.mDeviceStream.Read(this.ReadBuffer, 0, this.ReadBuffer.Length);
                            this.DataReceived(this.ReadBuffer);
                        }
                    }
                    catch (Exception ex)
                    {
                        FLog.d("读取数据异常:" + ex.Message);
                        this.HidDisconnect();
                    }
                }
                Thread.Sleep(1000);
            }
        })).Start();

        public void HidDisconnect()
        {
            if (this.mDeviceStream == null)
                return;
            try
            {
                this.mDeviceStream.Close();
            }
            catch (Exception ex)
            {
                FLog.d("DeviceStream关闭异常:" + ex.Message);
            }
            this.mDeviceStream = (DeviceStream)null;
        }

        public DateTime GetCurrentTime() => DateTime.Now;

        public void Close()
        {
            FLog.d("datamanager close");
            this.mLive = false;
            this.HidDisconnect();
            if (this.mTimer == null)
                return;
            this.mTimer.Close();
        }

        public void stopFind() => this.mLive = false;

        public void startFind()
        {
            this.mLive = true;
            this.checkAndConnectFlydigiGamepad();
        }

        public void writeConfig(List<List<byte>> list, IDelegateCallback iDelegateCallback)
        {
            this.mIDelegateCallback = iDelegateCallback;
            this.mWriting = true;
            this.mCurrentDataMode = this.ModeWrite;
            this.mConfigList.Clear();
            this.mConfigList.AddRange((IEnumerable<List<byte>>)list);
            this.mParcelCount = this.mConfigList.Count - 1;
            this.mCurrentParcelIndex = 0;
            FLog.d(CommonUtils.getCurrentTime().ToString() + "开始发送配置！！！！！！");
            if (this.mIDataLog != null)
                this.mIDataLog.onLog("开始发送配置！！！！！！");
            this.SendByteArray(this.mConfigList[0].ToArray());
        }

        public void readConfig(
          byte[] data,
          int configId,
          bool readConfigList,
          IDelegateCallback iDelegateCallback)
        {
            this.mReadConfigList = readConfigList;
            this.mReadConfigId = configId;
            this.mIDelegateCallback = iDelegateCallback;
            this.mWriting = true;
            this.mCurrentDataMode = this.ModeRead;
            if (this.mFDGDeviceInfo.cpuType == "wch")
            {
                this.mTempListByte.Clear();
                for (int index = 0; index < 530; ++index)
                    this.mTempListByte.Add(byte.MaxValue);
            }
            long currentTime = CommonUtils.getCurrentTime();
            FLog.d("准备读取配置" + this.mReadConfigId.ToString() + ":" + currentTime.ToString());
            if (!readConfigList && this.mReadConfigId == this.mLastConfigId)
            {
                FLog.d("无需再次读取");
                if (this.mIDelegateCallback == null)
                    return;
                this.mIDelegateCallback(-1);
            }
            else
                this.SendByteArray(data);
        }

        public void getCurrentConfigId(byte[] data, IDelegateCallback iDelegateCallback)
        {
            this.mIDelegateCallbackCurrentConfigId = iDelegateCallback;
            this.SendByteArray(data);
        }

        public Dictionary<int, ConfigBean> getListConfig() => this.mDictionaryConfig;

        public ConfigBean getConfigByKey(int key) => this.mDictionaryConfig.ContainsKey(key) ? this.mDictionaryConfig[key] : (ConfigBean)null;

        public void updateTempList(int index, List<byte> list)
        {
            FLog.d("index=" + index.ToString());
            for (int index1 = 0; index1 < 10; ++index1)
                this.mTempListByte[index * 10 + index1] = list[index1];
        }

        public void deviceRWCallBack(byte[] data)
        {
            FLog.d("接收器返回：" + CommonUtils.byteToHexString(data));
            if (this.mIDataLog != null)
                this.mIDataLog.onLog(CommonUtils.getCurrentTime().ToString() + "接收器返回：" + CommonUtils.byteToHexString(data));
            if (((int)data[3] & (int)byte.MaxValue) == 170 && ((int)data[4] & (int)byte.MaxValue) == 172)
            {
                FLog.d("接收器返回：配置Id：" + CommonUtils.byteToHexString(data));
                if (this.mIDelegateCallbackCurrentConfigId == null)
                    return;
                this.mIDelegateCallbackCurrentConfigId((int)data[5]);
            }
            else if (((int)data[15] & (int)byte.MaxValue) == 236)
            {
                FLog.d("接收器返回：手柄信息" + CommonUtils.byteToHexString(data));
                this.handerDeviceInfo(data);
            }
            else if (!this.mWriting)
                FLog.d("接收器返回mWriting：" + this.mWriting.ToString());
            else if (this.mCurrentDataMode == this.ModeWrite)
            {
                if (this.mFDGDeviceInfo.cpuType == "wch")
                {
                    ++this.mCurrentParcelIndex;
                    this.SendByteArray(this.mConfigList[this.mCurrentParcelIndex].ToArray());
                    if (this.mCurrentParcelIndex != this.mParcelCount)
                        return;
                    this.mConfigList.Clear();
                    this.mCurrentParcelIndex = 0;
                    this.mParcelCount = 0;
                    this.mWriting = false;
                    FLog.d(CommonUtils.getCurrentTime().ToString() + "数据发送完成！！！！！！");
                    if (this.mIDataLog != null)
                        this.mIDataLog.onLog("数据发送完成！！！！！！");
                    if (this.mIDelegateCallback == null)
                        return;
                    this.mIDelegateCallback(100);
                }
                else
                {
                    if ((int)data[3] != this.mCurrentParcelIndex)
                        return;
                    ++this.mCurrentParcelIndex;
                    this.SendByteArray(this.mConfigList[(int)data[3] + 1].ToArray());
                    if (this.mCurrentParcelIndex != this.mParcelCount)
                        return;
                    this.mConfigList.Clear();
                    this.mCurrentParcelIndex = 0;
                    this.mParcelCount = 0;
                    this.mWriting = false;
                    FLog.d(CommonUtils.getCurrentTime().ToString() + "数据发送完成！！！！！！");
                    if (this.mIDataLog != null)
                        this.mIDataLog.onLog("数据发送完成！！！！！！");
                    if (this.mIDelegateCallback == null)
                        return;
                    this.mIDelegateCallback(100);
                }
            }
            else
            {
                if (this.mCurrentDataMode != this.ModeRead)
                    return;
                if (this.mFDGDeviceInfo.cpuType == "wch")
                {
                    long currentTime = CommonUtils.getCurrentTime();
                    FLog.d("读取配置Callback" + this.mReadConfigId.ToString() + ":" + currentTime.ToString());
                    this.updateTempList((int)data[3], ConfigUtils.getRealByteFromCallback(data));
                    if (data[3] != (byte)52)
                        return;
                    this.mLastConfigId = this.mReadConfigId;
                    if (this.mReadConfigList)
                    {
                        this.mDictionaryConfig.Clear();
                        this.mDictionaryConfig.Add(this.mReadConfigId, ConfigUtils.getConfigByListByte(this.mTempListByte));
                        if (this.mIDelegateCallback != null)
                            this.mIDelegateCallback(this.mReadConfigId);
                    }
                    else if (this.mIDelegateCallback != null)
                        this.mIDelegateCallback(-1);
                    this.mTempListByte.Clear();
                    this.mWriting = false;
                    FLog.d("配置读取完成！！！！！！");
                    if (this.mIDataLog == null)
                        return;
                    this.mIDataLog.onLog("配置读取完成！！！！！！");
                }
                else
                {
                    long currentTime = CommonUtils.getCurrentTime();
                    FLog.d("读取配置Callback" + this.mReadConfigId.ToString() + ":" + currentTime.ToString());
                    this.mTempListByte.AddRange((IEnumerable<byte>)ConfigUtils.getRealByteFromCallback(data));
                    if (data[3] != (byte)52)
                        return;
                    this.mLastConfigId = this.mReadConfigId;
                    if (this.mReadConfigList)
                    {
                        this.mDictionaryConfig.Clear();
                        this.mDictionaryConfig.Add(this.mReadConfigId, ConfigUtils.getConfigByListByte(this.mTempListByte));
                        if (this.mIDelegateCallback != null)
                            this.mIDelegateCallback(this.mReadConfigId);
                    }
                    else if (this.mIDelegateCallback != null)
                        this.mIDelegateCallback(-1);
                    this.mTempListByte.Clear();
                    this.mWriting = false;
                    FLog.d("配置读取完成！！！！！！");
                    if (this.mIDataLog == null)
                        return;
                    this.mIDataLog.onLog("配置读取完成！！！！！！");
                }
            }
        }

        public FDGDeviceInfo getDeviceInfo() => this.mFDGDeviceInfo;

        private void handerDeviceInfo(byte[] data)
        {
            byte num1 = data[3];
            this.mFDGDeviceInfo.DeviceMac = CommonUtils.byteToHexString(new byte[4]
            {
        data[5],
        data[6],
        data[7],
        data[8]
            }).TrimEnd(':');
            int num2 = (int)data[9];
            byte num3 = data[10];
            int num4 = num2 & 15;
            int num5 = num2 >> 4;
            int num6 = (int)num3 & 15;
            int num7 = (int)num3 >> 4;
            int num8 = (int)data[11];
            int num9 = 98;
            int num10 = 114;
            if (num8 < num9)
                num8 = num9;
            else if (num8 > num10)
                num8 = num10;
            int num11 = (int)((double)(num8 - num9) / (double)(num10 - num9) * 100.0);
            FLog.d("手柄固件版本：V" + num7.ToString() + "." + num6.ToString() + "." + num5.ToString() + "." + num4.ToString());
            FLog.d("手柄电量：" + num11.ToString());
            this.mFDGDeviceInfo.FirmwareVersionCode = num7 * 1000 + num6 * 100 + num5 * 10 + num4;
            this.mFDGDeviceInfo.FirmwareVersion = num7.ToString() + "." + num6.ToString() + "." + num5.ToString() + "." + num4.ToString();
            this.mFDGDeviceInfo.BatteryPercent = num11;
            this.mFDGDeviceInfo.DeviceId = (int)num1;
            string gameHadleName = this.mFDGDeviceInfo.gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (gameHadleName == "f1p")
                        Constant.CURRENT_DEVICE_TYPE = 2;
                }
                else
                    Constant.CURRENT_DEVICE_TYPE = 1;
            }
            else
                Constant.CURRENT_DEVICE_TYPE = 0;
            this.mFDGDeviceInfo.setGameHandleName();
            if (((int)data[11] & (int)byte.MaxValue) == 0)
                this.mFDGDeviceInfo.BatteryPercent = -1;
            this.mFDGDeviceInfo.cpuType = ((int)data[12] & (int)byte.MaxValue) <= 0 ? "nordic" : "wch";
            if (num7 >= 6 && num6 >= 1)
                this.mFDGDeviceInfo.cpuType = "wch";
            if (this.mFDGDeviceInfo.cpuType == "wch")
            {
                if (((int)data[13] & (int)byte.MaxValue) == 1)
                {
                    this.mFDGDeviceInfo.connectType = 2;
                    this.mFDGDeviceInfo.cpuName = "ch573";
                }
                else
                {
                    this.mFDGDeviceInfo.connectType = 1;
                    this.mFDGDeviceInfo.cpuName = "ch571";
                }
            }
            NetworkUtils.FlydigiPostEvent("获取到手柄固件版本：V" + this.mFDGDeviceInfo.FirmwareVersion);
            if (this.mIDeviceState != null)
                this.mIDeviceState.onDeviceInfo(this.mFDGDeviceInfo);
            NetworkUtils.setUid(this.mFDGDeviceInfo.DeviceMac);
            NetworkUtils.setDeviceType(DataManager.getInstance().getDeviceInfo().gameHadleName);
            NetworkUtils.setCpuType(this.mFDGDeviceInfo.cpuType);
            NetworkUtils.FlydigiPostEvent("连接");
        }

        public void setDefaultConfig(int configId)
        {
            FLog.d("setDefaultConfig:" + configId.ToString());
            FLog.d("mDictionaryConfig before:" + JsonConvert.SerializeObject((object)this.mDictionaryConfig));
            ConfigBean configBean = configId != 4 ? ConfigUtils.getDefaultConfig() : ConfigUtils.getSwitchModeDefaultConfig();
            this.mDictionaryConfig.Remove(configId);
            FLog.d("mDictionaryConfig remove:" + JsonConvert.SerializeObject((object)this.mDictionaryConfig));
            this.mDictionaryConfig.Add(configId, configBean);
            FLog.d("mDictionaryConfig add:" + JsonConvert.SerializeObject((object)this.mDictionaryConfig));
        }

        public bool SendByteArray(byte[] data)
        {
            FLog.d("\n发往接收器------------>>：" + CommonUtils.byteToHexString(data));
            if (this.mIDataLog != null)
                this.mIDataLog.onLog(CommonUtils.getCurrentTime().ToString() + "发往接收器：" + CommonUtils.byteToHexString(data));
            if (this.mDeviceStream != null)
            {
                try
                {
                    this.mDeviceStream.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                    FLog.d("write error:" + ex.Message);
                }
            }
            return true;
        }

        public void delayCheckDeviceState()
        {
            this.mCurrentCount = -4;
            FLog.d("delayCheckDeviceState mCurrentCount=" + this.mCurrentCount.ToString());
        }

        public void disconnect()
        {
            FLog.d("手柄连接断开！！！");
            this.mCurrentCount = 0;
            this.mTimer.Enabled = false;
            this.mFDGDeviceInfo.BatteryPercent = -1;
            this.mFDGDeviceInfo.FirmwareVersion = "";
            this.mFDGDeviceInfo.FirmwareVersionCode = 0;
            this.mFDGDeviceInfo.DeviceId = -1;
            this.mDeviceBatteryReadCount = 0;
            this.mFDGDeviceInfo.DongleVersion = "";
            this.mFDGDeviceInfo.ConnectType = -1;
            this.mFDGDeviceInfo.cpuType = "";
            this.mFDGDeviceInfo.cpuName = "";
            if (this.mDeviceConnectState == -1)
                return;
            this.mDeviceConnectState = -1;
            NetworkUtils.FlydigiPostEvent("手柄连接断开");
            if (this.mIDeviceState == null)
                return;
            this.mIDeviceState.onDeviceState(this.mDeviceConnectState);
        }

        public void startCheckGamepadStatus() => this.mTimer.Enabled = true;

        private void TimerUp(object sender, ElapsedEventArgs e)
        {
            try
            {
                ++this.mCurrentCount;
                if (this.mCurrentCount <= 2)
                    return;
                FLog.d("error004,timerUp");
                this.disconnect();
            }
            catch (Exception ex)
            {
                FLog.d("TimerUp ERROR:" + ex.Message);
            }
        }

        public void setDebugLog(bool state) => this.DebugLog = state;

        public void DataReceived(byte[] value)
        {
            int num1 = -1;
            if (value.Length != 0)
            {
                if (value[0] == (byte)4 && value[1] == (byte)17)
                {
                    int num2 = (int)value[2];
                    byte num3 = value[3];
                    int num4 = num2 & 15;
                    int num5 = num2 >> 4;
                    int num6 = (int)num3 & 15;
                    int num7 = (int)num3 >> 4;
                    this.mFDGDeviceInfo.DongleVersion = num5.ToString() + "." + num4.ToString() + "." + num7.ToString() + "." + num6.ToString();
                    FLog.d("dongle Version:" + this.mFDGDeviceInfo.DongleVersion);
                }
                if (value.Length == 32)
                {
                    num1 = 0;
                    if (this.mIDeviceOperateData != null && (value[0] != (byte)4 || value[1] != byte.MaxValue || value[2] != (byte)240))
                        this.mIDeviceOperateData.onDeviceOperateData(this.getDeviceOperateEventFrom32(value));
                }
                else if (value.Length == 15)
                {
                    num1 = 1;
                    if (this.mIDeviceOperateData != null)
                        this.mIDeviceOperateData.onDeviceOperateData(this.getDeviceOperateEventFrom15(value));
                }
                if (this.DebugLog)
                    FLog.d("DataManager DataReceived:" + CommonUtils.byteToHexString(value));
                if (value[0] == (byte)4 && value[1] == byte.MaxValue && value[2] == (byte)240)
                    this.deviceRWCallBack(value);
            }
            this.mTimer.Enabled = true;
            this.mCurrentCount = 0;
            if (this.mDeviceConnectState == num1)
                return;
            this.mDeviceConnectState = num1;
            if (this.mIDeviceState != null)
            {
                NetworkUtils.FlydigiPostEvent("手柄连接成功");
                this.mIDeviceState.onDeviceState(this.mDeviceConnectState);
                this.SendByteArray(DongleCommand.getDongleVersionCommand());
            }
            if (this.mDeviceConnectState != 0)
                return;
            FLog.d("DataManager DataReceived--Android模式");
            new Thread((ParameterizedThreadStart)(o =>
            {
                if (Constant.CURRENT_PROJECT_TYPE == 0)
                {
                    Thread.Sleep(1000);
                    this.SendByteArray(DongleCommand.getSTDControl(false));
                }
                while (this.mDeviceConnectState == 0 && this.mFDGDeviceInfo.BatteryPercent < 0 && this.mDeviceBatteryReadCount < this.MaxDeviceBatteryReadCount)
                {
                    Thread.Sleep(1000);
                    if (this.mFDGDeviceInfo.BatteryPercent < 0 && this.mDeviceBatteryReadCount < this.MaxDeviceBatteryReadCount)
                    {
                        ++this.mDeviceBatteryReadCount;
                        this.SendByteArray(DongleCommand.getDeviceInfoInAndroid());
                    }
                }
            })).Start();
        }

        private byte[] getDeviceOperateEventFrom32(byte[] data)
        {
            this.mByteArray33[0] = (byte)65;
            for (int index = 0; index < 32; ++index)
                this.mByteArray33[index + 1] = data[index];
            this.mByteArray14[0] = this.mByteArray33[18];
            this.mByteArray14[1] = this.mByteArray33[20];
            this.mByteArray14[2] = this.mByteArray33[22];
            this.mByteArray14[3] = this.mByteArray33[23];
            this.mByteArray14[4] = this.mByteArray33[10];
            this.mByteArray14[5] = this.mByteArray33[11];
            this.mByteArray14[6] = this.mByteArray33[24];
            this.mByteArray14[7] = this.mByteArray33[25];
            this.mByteArray14[8] = this.mByteArray33[9];
            this.mByteArray14[9] = this.mByteArray33[8];
            this.mByteArray14[11] = this.mByteArray33[5];
            this.mByteArray14[12] = this.mByteArray33[6];
            this.mByteArray14[13] = this.mByteArray33[7];
            this.mByteArray14[14] = this.mByteArray33[4];
            return this.mByteArray14;
        }

        private byte[] getDeviceOperateEventFrom15(byte[] data)
        {
            this.mByteArray14[0] = data[2];
            this.mByteArray14[1] = data[4];
            this.mByteArray14[2] = data[6];
            this.mByteArray14[3] = data[8];
            this.mByteArray14[4] = data[12];
            this.mByteArray14[5] = data[11];
            this.mByteArray14[6] = data[10];
            this.mByteArray14[7] = data[10];
            this.mByteArray14[8] = data[8];
            this.mByteArray14[9] = data[9];
            this.mByteArray14[11] = (byte)0;
            this.mByteArray14[12] = (byte)0;
            this.mByteArray14[13] = (byte)0;
            return this.mByteArray14;
        }

        public interface IDeviceOperateData
        {
            void onDeviceOperateData(byte[] data);
        }

        public interface IDeviceState
        {
            void onDeviceState(int deviceState);

            void onDeviceInfo(FDGDeviceInfo info);
        }

        public interface IDataLog
        {
            void onLog(string info);
        }
    }
}
