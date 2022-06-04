// Decompiled with JetBrains decompiler
// Type: ApexSpace.UsbDataManager
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using WPFTest.DriverHelper;
using WPFTest.Utils;

namespace ApexSpace
{
  internal class UsbDataManager
  {
    private static UsbDataManager mUsbDataManager;
    private bool checkConnectedStatus = true;
    public int pid = 654;
    public int vid = 1118;
    public static UsbDeviceFinder mengUsbFinder = new UsbDeviceFinder(1118, 654);
    public IntPtr phDevice;
    private UsbDevice usbDevice;
    private UsbEndpointReader epReader;
    private UsbEndpointWriter epWriter;
    private List<byte> recvData;
    private System.Timers.Timer mTimer;
    private int mCurrentCount;

    public static UsbDataManager getInstance()
    {
      if (UsbDataManager.mUsbDataManager == null)
        UsbDataManager.mUsbDataManager = new UsbDataManager();
      return UsbDataManager.mUsbDataManager;
    }

    private UsbDataManager() => this.Initial();

    public void Initial()
    {
      FLog.d("UsbDataManager start");
      this.setTimer();
      this.checkAndConnectFlydigiGamepad();
      SayyoHelper.Initialize();
    }

    private void setTimer()
    {
      this.mTimer = new System.Timers.Timer(1000.0);
      this.mTimer.AutoReset = true;
      this.mTimer.Enabled = false;
      this.mTimer.Elapsed += new ElapsedEventHandler(this.TimerUp);
      this.mTimer.Start();
    }

    private void TimerUp(object sender, ElapsedEventArgs e)
    {
      try
      {
        ++this.mCurrentCount;
        if (this.mCurrentCount <= 2)
          return;
        FLog.d("心跳进程检测到设备断开连接");
        this.Close();
      }
      catch (Exception ex)
      {
        FLog.d("TimerUp ERROR:" + ex.Message);
      }
    }

    public void Open(int vid, int pid)
    {
      UsbDeviceFinder usbDeviceFinder = new UsbDeviceFinder(vid, pid);
      this.usbDevice = UsbDevice.OpenUsbDevice(usbDeviceFinder);
      if (this.usbDevice == null)
      {
        for (int index = 0; index < 10 && this.usbDevice == null; ++index)
        {
          Thread.Sleep(100);
          this.usbDevice = UsbDevice.OpenUsbDevice(usbDeviceFinder);
        }
      }
      if (this.usbDevice == null)
      {
        this.mTimer.Enabled = false;
        Console.WriteLine("打开USB设备失败");
      }
      else
      {
        this.mTimer.Enabled = true;
        Console.WriteLine("打开USB设备成功");
        if (this.usbDevice is IUsbDevice usbDevice)
        {
          usbDevice.SetConfiguration((byte) 1);
          usbDevice.ClaimInterface(0);
        }
        this.epReader = this.usbDevice.OpenEndpointReader((ReadEndpointID) 129);
        this.epReader.ReadBufferSize = 1024;
        this.epReader.DataReceived += new EventHandler<EndpointDataEventArgs>(this.EpReader_DataReceived);
        this.epReader.DataReceivedEnabled = true;
        this.epWriter = this.usbDevice.OpenEndpointWriter((WriteEndpointID) 1);
      }
    }

    private void EpReader_DataReceived(object sender, EndpointDataEventArgs e)
    {
      if (e.Count == 0)
        this.Close();
      byte[] destinationArray = new byte[e.Count];
      Array.Copy((Array) e.Buffer, (Array) destinationArray, e.Count);
      Console.WriteLine("Data  :" + UsbDataManager.showBytes(e.Buffer, (uint) e.Count));
      this.mCurrentCount = 0;
    }

    private void checkAndConnectFlydigiGamepad() => new Thread((ParameterizedThreadStart) (o =>
    {
      FLog.d("检查Usb设备状态中......");
      while (this.checkConnectedStatus)
      {
        if (!this.IsOpen())
          this.usbDevice = (UsbDevice) null;
        if (this.usbDevice == null)
        {
          FLog.d("搜索Usb设备......");
          this.mTimer.Enabled = false;
          this.GetUSBInfo();
          Thread.Sleep(1500);
        }
        Thread.Sleep(1000);
      }
    })).Start();

    public void Close()
    {
      if (this.IsOpen())
      {
        if (this.usbDevice is IUsbDevice usbDevice)
          usbDevice.ReleaseInterface(0);
        this.usbDevice.Close();
        this.usbDevice = (UsbDevice) null;
        this.mTimer.Enabled = false;
      }
      UsbDevice.Exit();
    }

    public bool IsOpen() => this.usbDevice != null && this.usbDevice.IsOpen;

    public void GetUSBInfo()
    {
      UsbRegDeviceList allDevices = UsbDevice.AllDevices;
      FLog.d(string.Format("Found {0} devices", (object) allDevices.Count));
      foreach (UsbRegistry usbRegistry in allDevices)
      {
        Console.WriteLine("--------------");
        Console.WriteLine("Device info:" + usbRegistry.Device.Info.ProductString);
        int num = usbRegistry.Pid;
        string str1 = num.ToString("X2");
        num = usbRegistry.Vid;
        string str2 = num.ToString("X2");
        Console.WriteLine("Pid:" + str1 + ",VID:" + str2);
        if (usbRegistry.Pid == this.pid && usbRegistry.Vid == this.vid)
          this.Open(usbRegistry.Vid, usbRegistry.Pid);
      }
    }

    public static string showBytes(byte[] readBuffer, uint uiTransmitted)
    {
      string str = "";
      for (int index = 0; (long) index < (long) uiTransmitted; ++index)
        str = str + readBuffer[index].ToString("X2") + " ";
      return str + "-----" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }
  }
}
