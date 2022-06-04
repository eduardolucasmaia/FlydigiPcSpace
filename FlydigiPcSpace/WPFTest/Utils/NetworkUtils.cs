// Decompiled with JetBrains decompiler
// Type: WPFTest.Utils.NetworkUtils
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using WPFTest.Bean;

namespace WPFTest.Utils
{
  internal class NetworkUtils
  {
    public static FDGEventInfo mFDGEventInfo = (FDGEventInfo) null;
    public static string mCurrentLanguage = "zh";
    public static bool enableDataReport = true;

    public static FDGEventInfo GetFDGEventInfo()
    {
      if (NetworkUtils.mFDGEventInfo == null)
      {
        string str1 = "";
        string str2 = "";
        string str3 = "";
        string osModel = NetworkUtils.getOSModel();
        string str4 = NetworkUtils.Is64Bit() ? "64 Bit" : "32 Bit";
        NetworkUtils.mFDGEventInfo = new FDGEventInfo();
        NetworkUtils.mFDGEventInfo.Uid = str1;
        NetworkUtils.mFDGEventInfo.AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        NetworkUtils.mFDGEventInfo.OsVersion = osModel;
        NetworkUtils.mFDGEventInfo.OsBit = str4;
        NetworkUtils.mFDGEventInfo.Lang = NetworkUtils.getCurrentLanguage();
        NetworkUtils.mFDGEventInfo.cpuType = str2;
        NetworkUtils.mFDGEventInfo.deviceType = str3;
      }
      return NetworkUtils.mFDGEventInfo;
    }

    public static void setUid(string uid) => NetworkUtils.GetFDGEventInfo().Uid = uid;

    public static void setCpuType(string cpu_type) => NetworkUtils.GetFDGEventInfo().cpuType = cpu_type;

    public static void setDeviceType(string device_type)
    {
      NetworkUtils.GetFDGEventInfo().deviceType = device_type;
      FLog.d("current device type:::" + NetworkUtils.GetFDGEventInfo().deviceType);
    }

    public static string getCurrentLanguage() => NetworkUtils.mCurrentLanguage;

    public static void setCurrentLanguage(string lang) => NetworkUtils.mCurrentLanguage = lang;

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWow64Process([In] IntPtr hProcess, out bool lpSystemInfo);

    public static bool Is64Bit()
    {
      bool lpSystemInfo;
      NetworkUtils.IsWow64Process(Process.GetCurrentProcess().Handle, out lpSystemInfo);
      return lpSystemInfo;
    }

    public static string getOSModel() => new ComputerInfo().OSFullName;

    public static void FlydigiPostEvent(string action)
    {
      if (Constant.CURRENT_PROJECT_TYPE == 1)
        return;
      new Thread((ThreadStart) (() =>
      {
        NetworkUtils.GetFDGEventInfo().Action = action;
        string postData;
        try
        {
          postData = JsonConvert.SerializeObject((object) NetworkUtils.GetFDGEventInfo());
        }
        catch (Exception ex)
        {
          FLog.d("FlydigiPost：Json异常：" + ex.Message);
          return;
        }
        NetworkUtils.PostEventUrl("http://data.flydigi.com//api/space_station", postData);
      })).Start();
    }

    public static void FlydigiCheckAppVersion(
      string currentAppVerion,
      bool userClick,
      IDelegateCallbackValueTwoObject delegateCallbackValueTwoObject)
    {
      new Thread((ThreadStart) (() =>
      {
        string str = "";
        try
        {
          ServicePointManager.Expect100Continue = false;
          int num = 13;
          if (NetworkUtils.getCurrentLanguage().Equals("zh"))
            num = 13;
          else if (NetworkUtils.getCurrentLanguage().Equals("en"))
            num = 14;
          HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("http://api.flydigi.com//web/update/init?app_class_type=" + num.ToString());
          httpWebRequest.ServicePoint.Expect100Continue = false;
          httpWebRequest.Method = "POST";
          httpWebRequest.ContentType = "application/json";
          using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
            str = streamReader.ReadToEnd();
          FLog.d("app update post result:" + str);
          try
          {
            AppUpdateInfo result_1 = JsonConvert.DeserializeObject<AppUpdateInfo>(str);
            if (result_1.status.Equals("0"))
            {
              FLog.d("apk_url:" + result_1.data.apk_url);
              if (CommonUtils.compareVersion(result_1.data.version_code, currentAppVerion) == 1)
              {
                if (result_1.data.is_force.Equals("0"))
                {
                  FLog.d("软件需升级 选择弹窗");
                  if (delegateCallbackValueTwoObject == null)
                    return;
                  delegateCallbackValueTwoObject(1, (object) result_1);
                  return;
                }
                if (result_1.data.is_force.Equals("1"))
                {
                  FLog.d("软件需升级 强制弹窗");
                  if (delegateCallbackValueTwoObject == null)
                    return;
                  delegateCallbackValueTwoObject(2, (object) result_1);
                  return;
                }
                if (result_1.data.is_force.Equals("2") & userClick)
                {
                  FLog.d("软件需升级 手动更新");
                  if (delegateCallbackValueTwoObject == null)
                    return;
                  delegateCallbackValueTwoObject(1, (object) result_1);
                  return;
                }
              }
              else
                FLog.d("软件无需升级");
            }
            else
              FLog.d("软件无需升级");
          }
          catch (Exception ex)
          {
            FLog.d("app update json error:" + ex.Message);
          }
        }
        catch (Exception ex)
        {
          FLog.d("app update check error:" + ex.Message);
          if (!(delegateCallbackValueTwoObject != null & userClick))
            return;
          delegateCallbackValueTwoObject(3, (object) null);
          return;
        }
        if (!(delegateCallbackValueTwoObject != null & userClick))
          return;
        delegateCallbackValueTwoObject(0, (object) null);
      })).Start();
    }

    public static void FlydigiCheckFmVersion(
      string currentAppVerion,
      bool userClick,
      IDelegateCallbackValueTwoObject delegateCallbackValueTwoObject)
    {
      new Thread((ThreadStart) (() =>
      {
        string str = "";
        try
        {
          ServicePointManager.Expect100Continue = false;
          HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("http://api.flydigi.com//android/firmwares?type=" + DataManager.getInstance().getDeviceInfo().getFirmwareName() + "&cpuName=ch573&lang=" + NetworkUtils.getCurrentLanguage());
          FLog.d("http://api.flydigi.com//android/firmwares?type=" + DataManager.getInstance().getDeviceInfo().getFirmwareName() + "&cpuName=" + DataManager.getInstance().getDeviceInfo().cpuName + "&lang=" + NetworkUtils.getCurrentLanguage());
          httpWebRequest.ServicePoint.Expect100Continue = false;
          httpWebRequest.Method = "POST";
          httpWebRequest.ContentType = "application/json";
          using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
            str = streamReader.ReadToEnd();
          FLog.d("app firmware update post result:" + str);
          FLog.d("current version:" + currentAppVerion);
          try
          {
            AppUpdateInfo result_1 = JsonConvert.DeserializeObject<AppUpdateInfo>(str);
            if (result_1.status.Equals("0"))
            {
              FLog.d("apk_url:" + result_1.data.apk_url);
              if (CommonUtils.compareVersion(result_1.data.version, currentAppVerion) == 1)
              {
                FLog.d("软件需升级 手动更新");
                if (delegateCallbackValueTwoObject == null)
                  return;
                delegateCallbackValueTwoObject(1, (object) result_1);
                return;
              }
              FLog.d("软件无需升级");
            }
            else
              FLog.d("软件无需升级");
          }
          catch (Exception ex)
          {
            FLog.d("app firmware update json error:" + ex.Message);
          }
        }
        catch (Exception ex)
        {
          FLog.d("app update check error:" + ex.Message);
          if (!(delegateCallbackValueTwoObject != null & userClick))
            return;
          delegateCallbackValueTwoObject(3, (object) null);
          return;
        }
        if (!(delegateCallbackValueTwoObject != null & userClick))
          return;
        delegateCallbackValueTwoObject(0, (object) null);
      })).Start();
    }

    public static void FlydigiCheckDongleVersion(
      string currentAppVerion,
      bool userClick,
      IDelegateCallbackValueTwoObject delegateCallbackValueTwoObject)
    {
      new Thread((ThreadStart) (() =>
      {
        string str = "";
        try
        {
          ServicePointManager.Expect100Continue = false;
          HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("http://api.flydigi.com//android/firmwares?type=" + DataManager.getInstance().getDeviceInfo().getFirmwareName() + "&cpuName=ch571&lang=" + NetworkUtils.getCurrentLanguage());
          FLog.d("http://api.flydigi.com//android/firmwares?type=" + DataManager.getInstance().getDeviceInfo().getFirmwareName() + "&cpuName=" + DataManager.getInstance().getDeviceInfo().cpuName + "&lang=" + NetworkUtils.getCurrentLanguage());
          httpWebRequest.ServicePoint.Expect100Continue = false;
          httpWebRequest.Method = "POST";
          httpWebRequest.ContentType = "application/json";
          using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
            str = streamReader.ReadToEnd();
          FLog.d("app dongle update post result:" + str);
          FLog.d("current version:" + currentAppVerion);
          try
          {
            AppUpdateInfo result_1 = JsonConvert.DeserializeObject<AppUpdateInfo>(str);
            if (result_1.status.Equals("0"))
            {
              FLog.d("apk_url:" + result_1.data.apk_url);
              if (CommonUtils.compareVersion(result_1.data.version, currentAppVerion) == 1)
              {
                FLog.d("软件需升级 手动更新");
                if (delegateCallbackValueTwoObject == null)
                  return;
                delegateCallbackValueTwoObject(1, (object) result_1);
                return;
              }
              FLog.d("软件无需升级");
            }
            else
              FLog.d("软件无需升级");
          }
          catch (Exception ex)
          {
            FLog.d("app dongle update json error:" + ex.Message);
          }
        }
        catch (Exception ex)
        {
          FLog.d("app update check error:" + ex.Message);
          if (!(delegateCallbackValueTwoObject != null & userClick))
            return;
          delegateCallbackValueTwoObject(3, (object) null);
          return;
        }
        if (!(delegateCallbackValueTwoObject != null & userClick))
          return;
        delegateCallbackValueTwoObject(0, (object) null);
      })).Start();
    }

    private static void PostEventUrl(string url, string postData)
    {
      if (!NetworkUtils.enableDataReport)
        return;
      FLog.d("事件发送：" + postData);
      string str = "";
      try
      {
        ServicePointManager.Expect100Continue = false;
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.ServicePoint.Expect100Continue = false;
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/json";
        Stream requestStream = httpWebRequest.GetRequestStream();
        byte[] bytes = Encoding.UTF8.GetBytes(postData);
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
        using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
          str = streamReader.ReadToEnd();
        FLog.d("post event result:" + str);
      }
      catch (Exception ex)
      {
        FLog.d("post event error:" + ex.Message);
      }
    }

    public static void DownloadFile(
      string URL,
      string filename,
      IDelegateCallback delegateCallback)
    {
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(URL);
        httpWebRequest.Timeout = 10000;
        HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
        long contentLength = response.ContentLength;
        Stream responseStream = response.GetResponseStream();
        Stream stream = (Stream) new FileStream(filename, FileMode.Create);
        long num = 0;
        byte[] buffer = new byte[1024];
        int count = responseStream.Read(buffer, 0, buffer.Length);
        while (count > 0)
        {
          num = (long) count + num;
          stream.Write(buffer, 0, count);
          count = responseStream.Read(buffer, 0, buffer.Length);
          float result = (float) ((double) num / (double) contentLength * 100.0);
          if (delegateCallback != null)
            delegateCallback((int) result);
        }
        stream.Close();
        responseStream.Close();
        if (delegateCallback == null)
          return;
        FLog.d("软件下载完成");
        delegateCallback(200);
      }
      catch (Exception ex)
      {
        FLog.d("软件下载异常：" + ex.Message);
        if (delegateCallback == null)
          return;
        delegateCallback(-1);
      }
    }

    private static string GetHash(string s) => NetworkUtils.GetHexString(new MD5CryptoServiceProvider().ComputeHash(new ASCIIEncoding().GetBytes(s)));

    private static string GetHexString(byte[] bt)
    {
      string hexString = string.Empty;
      for (int index = 0; index < bt.Length; ++index)
      {
        int num1 = (int) bt[index];
        int num2 = num1 & 15;
        int num3 = num1 >> 4 & 15;
        string str = num3 <= 9 ? hexString + num3.ToString() : hexString + ((char) (num3 - 10 + 65)).ToString();
        hexString = num2 <= 9 ? str + num2.ToString() : str + ((char) (num2 - 10 + 65)).ToString();
        if (index + 1 != bt.Length && (index + 1) % 2 == 0)
          hexString += "-";
      }
      return hexString;
    }
  }
}
