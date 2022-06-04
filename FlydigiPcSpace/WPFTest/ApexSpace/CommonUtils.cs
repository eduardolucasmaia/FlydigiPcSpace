// Decompiled with JetBrains decompiler
// Type: ApexSpace.CommonUtils
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using WPFTest;
using WPFTest.Utils;
using WPFTest.Windows;

namespace ApexSpace
{
  internal class CommonUtils
  {
    private static Timer aTimer;
    private static WindowMain mWindowMain;
    private static bool mUpdateFirmwareShowing;

    public static string byteToHexString(byte[] bytes)
    {
      string hexString = "";
      if (bytes != null)
      {
        for (int index = 0; index < bytes.Length; ++index)
          hexString = hexString + bytes[index].ToString("X2") + ":";
      }
      return hexString;
    }

    public static byte[] strToHexByte(string hexString)
    {
      hexString = hexString.Replace(" ", "");
      if (hexString.Length % 2 != 0)
        hexString += " ";
      byte[] hexByte = new byte[hexString.Length / 2];
      for (int index = 0; index < hexByte.Length; ++index)
        hexByte[index] = Convert.ToByte(hexString.Substring(index * 2, 2), 16);
      return hexByte;
    }

    public static List<List<byte>> splitList(List<byte> list, int len)
    {
      List<List<byte>> byteListList = new List<List<byte>>();
      if (list == null || list.Count == 0 || len < 1)
        return byteListList;
      int num = (int) Math.Ceiling((double) list.Count / (double) len);
      for (int index = 0; index < num; ++index)
      {
        List<byte> range = list.GetRange(index * len, len);
        byteListList.Add(range);
      }
      return byteListList;
    }

    public static List<List<byte>> getDoubleList(byte[] byteArray)
    {
      int length = (int) Math.Ceiling((double) byteArray.Length / (double) ConfigUtils.MaxLengthEveryParcel);
      List<byte> list = new List<byte>();
      FLog.d("parcelCount:" + length.ToString());
      FLog.d("parcelCountX2:" + length.ToString("X2"));
      FLog.d("byteArray.Length:" + byteArray.Length.ToString());
      list.AddRange((IEnumerable<byte>) ((IEnumerable<byte>) DongleCommand.getStartCommand(length)).ToList<byte>());
      for (int index1 = 0; index1 < length; ++index1)
      {
        list.Add((byte) 5);
        list.Add((byte) 34);
        for (int index2 = 0; index2 < 10; ++index2)
        {
          if (index1 < length - 1)
            list.Add(byteArray[index1 * 10 + index2]);
          else
            list.Add(index1 * 10 + index2 < byteArray.Length ? byteArray[index1 * 10 + index2] : (byte) 0);
        }
      }
      return CommonUtils.splitList(list, ConfigUtils.MaxLengthEveryParcel + 2);
    }

    public static List<List<byte>> getDoubleListForUpdate(byte[] byteArray, int partlyID)
    {
      int parcelCount = (int) Math.Ceiling((double) byteArray.Length / (double) ConfigUtils.MaxLengthEveryParcel);
      List<byte> list = new List<byte>();
      list.AddRange((IEnumerable<byte>) ((IEnumerable<byte>) DongleCommand.getUpdateCommand(byteArray.Length, partlyID, parcelCount)).ToList<byte>());
      for (int index1 = 0; index1 < parcelCount; ++index1)
      {
        list.Add((byte) 5);
        list.Add((byte) 34);
        for (int index2 = 0; index2 < 10; ++index2)
        {
          if (index1 < parcelCount - 1)
            list.Add(byteArray[index1 * 10 + index2]);
          else
            list.Add(index1 * 10 + index2 < byteArray.Length ? byteArray[index1 * 10 + index2] : (byte) 0);
        }
      }
      return CommonUtils.splitList(list, ConfigUtils.MaxLengthEveryParcel + 2);
    }

    public static byte[] getByteArrayByLength(byte[] value, int length)
    {
      byte[] byteArrayByLength = new byte[length];
      for (int index = 0; index < value.Length; ++index)
        byteArrayByLength[index] = value[index];
      return byteArrayByLength;
    }

    public static List<byte> getListByte(List<int> list)
    {
      List<byte> listByte = new List<byte>();
      for (int index = 0; index < list.Count; ++index)
        listByte.Add((byte) list[index]);
      return listByte;
    }

    public static void showDialog(string title, string content)
    {
      WindowDialogCommon windowDialogCommon = new WindowDialogCommon(0, title, content, (string) null, (string) null, (IDelegateCallback) (result => FLog.d("Dialog返回结果：" + result.ToString())));
      windowDialogCommon.Owner = WindowMain.getInstance();
      windowDialogCommon.ShowDialog();
    }

    public static void showCommonTip(
      WindowMain mainWin,
      string content,
      string TipType,
      int showTime = 2000)
    {
      CommonUtils.mWindowMain = mainWin;
      CommonUtils.SetTimer(showTime);
      if (TipType == "success")
      {
        CommonUtils.mWindowMain.mSuccessIcon.Visibility = Visibility.Visible;
        CommonUtils.mWindowMain.mTipsLabel.Foreground = (System.Windows.Media.Brush) new BrushConverter().ConvertFrom((object) "#fff");
      }
      else
      {
        CommonUtils.mWindowMain.mFailIcon.Visibility = Visibility.Visible;
        CommonUtils.mWindowMain.mTipsLabel.Foreground = (System.Windows.Media.Brush) new BrushConverter().ConvertFrom((object) "#d9534f");
      }
      CommonUtils.mWindowMain.showTip(content);
    }

    public static void showCommonLangTip(WindowMain mainWin, string content, int showTime = 2000)
    {
      CommonUtils.mWindowMain = mainWin;
      CommonUtils.SetTimer(showTime);
      CommonUtils.mWindowMain.mSuccessIcon.Visibility = Visibility.Visible;
      CommonUtils.mWindowMain.showLongTip(content);
    }

    private static void SetTimer(int showTime)
    {
      CommonUtils.aTimer = new Timer((double) showTime);
      CommonUtils.aTimer.Elapsed += new ElapsedEventHandler(CommonUtils.OnTimedEvent);
      CommonUtils.aTimer.AutoReset = false;
      CommonUtils.aTimer.Enabled = true;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e) => CommonUtils.mWindowMain.Dispatcher.Invoke((Action) (() =>
    {
      CommonUtils.mWindowMain.hideTip();
      CommonUtils.mWindowMain.hideLongTip();
    }));

    public static void showDialogUpdateFirmware()
    {
      if (CommonUtils.mUpdateFirmwareShowing)
        return;
      CommonUtils.mUpdateFirmwareShowing = true;
      WindowDialogCommon windowDialogCommon = new WindowDialogCommon(0, Application.Current.FindResource((object) "notice").ToString(), Application.Current.FindResource((object) "your_current_fw_lower").ToString(), Application.Current.FindResource((object) "tutorial").ToString(), (string) null, (IDelegateCallback) (result =>
      {
        CommonUtils.mUpdateFirmwareShowing = false;
        if (result != 1)
          return;
        NetworkUtils.FlydigiPostEvent("低版本固件点击查看教程");
        if (NetworkUtils.getCurrentLanguage().Equals("zh"))
          Process.Start("http://bbs.flydigi.com/detail/11816");
        else
          Process.Start("http://bbs.flydigi.com/en/detail/74");
      }));
      windowDialogCommon.Owner = WindowMain.getInstance();
      windowDialogCommon.ShowDialog();
    }

    public static int getFirmwareCodeByVersion(string verison)
    {
      string[] strArray = verison.Split('.');
      string s = "";
      for (int index = 0; index < strArray.Length; ++index)
        s += strArray[index];
      FLog.d("转换后版本号：" + int.Parse(s).ToString());
      return int.Parse(s);
    }

    public static List<float> colorRGBtoHSB(int red, int green, int blue)
    {
      List<float> floatList = new List<float>();
      System.Drawing.Color color = System.Drawing.Color.FromArgb(red, green, blue);
      floatList.Add(color.GetHue());
      floatList.Add(color.GetSaturation());
      floatList.Add(color.GetBrightness());
      return floatList;
    }

    public static List<int> colorHSBtoRGB(float hue, float saturation, float brightness)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      if ((double) saturation == 0.0)
      {
        int num4;
        num3 = num4 = (int) ((double) brightness * (double) byte.MaxValue + 0.5);
        num2 = num4;
        num1 = num4;
      }
      else
      {
        double num5 =0;
        float num6 = (float) num5 - (float) Math.Floor(num5 = ((double) hue - Math.Floor((double) hue)) * 6.0);
        float num7 = brightness * (1f - saturation);
        float num8 = brightness * (float) (1.0 - (double) saturation * (double) num6);
        float num9 = brightness * (float) (1.0 - (double) saturation * (1.0 - (double) num6));
        switch ((int) num5)
        {
          case 0:
            num1 = (int) ((double) brightness * (double) byte.MaxValue + 0.5);
            num2 = (int) ((double) num9 * (double) byte.MaxValue + 0.5);
            num3 = (int) ((double) num7 * (double) byte.MaxValue + 0.5);
            break;
          case 1:
            num1 = (int) ((double) num8 * (double) byte.MaxValue + 0.5);
            num2 = (int) ((double) brightness * (double) byte.MaxValue + 0.5);
            num3 = (int) ((double) num7 * (double) byte.MaxValue + 0.5);
            break;
          case 2:
            num1 = (int) ((double) num7 * (double) byte.MaxValue + 0.5);
            num2 = (int) ((double) brightness * (double) byte.MaxValue + 0.5);
            num3 = (int) ((double) num9 * (double) byte.MaxValue + 0.5);
            break;
          case 3:
            num1 = (int) ((double) num7 * (double) byte.MaxValue + 0.5);
            num2 = (int) ((double) num8 * (double) byte.MaxValue + 0.5);
            num3 = (int) ((double) brightness * (double) byte.MaxValue + 0.5);
            break;
          case 4:
            num1 = (int) ((double) num9 * (double) byte.MaxValue + 0.5);
            num2 = (int) ((double) num7 * (double) byte.MaxValue + 0.5);
            num3 = (int) ((double) brightness * (double) byte.MaxValue + 0.5);
            break;
          case 5:
            num1 = (int) ((double) brightness * (double) byte.MaxValue + 0.5);
            num2 = (int) ((double) num7 * (double) byte.MaxValue + 0.5);
            num3 = (int) ((double) num8 * (double) byte.MaxValue + 0.5);
            break;
        }
      }
      return new List<int>() { num1, num2, num3 };
    }

    public static List<int> getAddedList(List<int> last, List<int> current)
    {
      List<int> addedList = new List<int>();
      for (int index = 0; index < current.Count; ++index)
      {
        if (!last.Contains(current[index]))
          addedList.Add(current[index]);
      }
      return addedList;
    }

    public static List<int> getRemovedList(List<int> last, List<int> current)
    {
      List<int> removedList = new List<int>();
      for (int index = 0; index < last.Count; ++index)
      {
        if (!current.Contains(last[index]))
          removedList.Add(last[index]);
      }
      return removedList;
    }

    public static int getIntTime(byte timeH, byte timeL) => Convert.ToInt32(CommonUtils.byteToHexString(new byte[2]
    {
      timeH,
      timeL
    }).Replace(":", ""), 16) * 8;

    public static void ApplicationExit()
    {
      DataManager.getInstance().Close();
      Process.GetCurrentProcess().Kill();
    }

    public static int getLimitMaxValue(int value, int max) => value <= max ? value : max;

    public static List<int> getRGBColorById(int colorID)
    {
      List<int> rgbColorById = new List<int>();
      switch (colorID)
      {
        case 1:
          rgbColorById.Add((int) byte.MaxValue);
          rgbColorById.Add(0);
          rgbColorById.Add(0);
          break;
        case 2:
          rgbColorById.Add((int) byte.MaxValue);
          rgbColorById.Add(128);
          rgbColorById.Add(0);
          break;
        case 3:
          rgbColorById.Add(204);
          rgbColorById.Add((int) byte.MaxValue);
          rgbColorById.Add(0);
          break;
        case 4:
          rgbColorById.Add(17);
          rgbColorById.Add((int) byte.MaxValue);
          rgbColorById.Add(0);
          break;
        case 5:
          rgbColorById.Add(0);
          rgbColorById.Add(208);
          rgbColorById.Add((int) byte.MaxValue);
          break;
        case 6:
          rgbColorById.Add(0);
          rgbColorById.Add(106);
          rgbColorById.Add((int) byte.MaxValue);
          break;
        case 7:
          rgbColorById.Add(222);
          rgbColorById.Add(125);
          rgbColorById.Add((int) byte.MaxValue);
          break;
      }
      return rgbColorById;
    }

    public static void LoadResourceDll() => AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CommonUtils.CurrentDomain_AssemblyResolve);

    private static Assembly CurrentDomain_AssemblyResolve(
      object sender,
      ResolveEventArgs args)
    {
      string name = (args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "")).Replace(".", "_");
      return name.EndsWith("_resources") ? (Assembly) null : Assembly.Load((byte[]) new ResourceManager(Assembly.GetEntryAssembly().GetTypes()[0].Namespace + ".Properties.Resources", Assembly.GetExecutingAssembly()).GetObject(name));
    }

    public static int compareVersion(string v1, string v2)
    {
      if (v1.Equals(v2))
        return 0;
      char[] chArray = new char[1]{ '.' };
      string[] strArray1 = v1.Split(chArray);
      string[] strArray2 = v2.Split(chArray);
      int index1 = 0;
      int num1 = Math.Min(strArray1.Length, strArray2.Length);
      long num2 = 0;
      while (index1 < num1 && (num2 = long.Parse(strArray1[index1]) - long.Parse(strArray2[index1])) == 0L)
        ++index1;
      if (num2 == 0L)
      {
        for (int index2 = index1; index2 < strArray1.Length; ++index2)
        {
          if (long.Parse(strArray1[index2]) > 0L)
            return 1;
        }
        for (int index3 = index1; index3 < strArray2.Length; ++index3)
        {
          if (long.Parse(strArray2[index3]) > 0L)
            return -1;
        }
        return 0;
      }
      return num2 <= 0L ? -1 : 1;
    }

    public static long getCurrentTime()
    {
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.ToUniversalTime();
      return (dateTime.Ticks - 621355968000000000L) / 10000L;
    }

    public byte set_bit(byte data, int index, bool flag)
    {
      if (index > 8 || index < 1)
        throw new ArgumentOutOfRangeException();
      int num = index < 2 ? index : 2 << index - 2;
      return !flag ? (byte) ((uint) data & (uint) ~num) : (byte) ((uint) data | (uint) num);
    }

    public bool get_bit(byte data, int index)
    {
      if (index > 8 || index < 1)
        throw new ArgumentOutOfRangeException();
      BitConverter.GetBytes(Math.Pow(2.0, (double) index));
      return false;
    }
  }
}
