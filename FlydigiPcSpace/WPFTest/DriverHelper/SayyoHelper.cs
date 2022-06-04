// Decompiled with JetBrains decompiler
// Type: WPFTest.DriverHelper.SayyoHelper
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.Runtime.InteropServices;
using System.Threading;
using WPFTest.Utils;

namespace WPFTest.DriverHelper
{
  internal class SayyoHelper
  {
    private const int keyB = 102;
    public static string sysPath = "Sayyo_x64.sys";
    public static string dllPath = "SayyoDll.dll";
    public static IntPtr FLyService;
    public static IntPtr VMouseDevice;
    public static IntPtr VKeyboardDevice;

    [DllImport("SayyoDll.dll")]
    public static extern int KmdDriverSetup(string sPath, IntPtr phSysService);

    [DllImport("SayyoDll.dll")]
    public static extern int KmdDriverRemove(ref IntPtr phSysService);

    [DllImport("SayyoDll.dll")]
    public static extern int DriverDeviceOpen([MarshalAs(UnmanagedType.LPStr)] string sDeviceName, ref IntPtr phDevice);

    [DllImport("SayyoDll.dll")]
    public static extern int DriverDeviceClose(IntPtr hDevice);

    [DllImport("SayyoDll.dll")]
    public static extern int VKSendMsVirtualKeySingle_KB(
      IntPtr hKeyboard,
      byte VirtualKey,
      bool bUp);

    [DllImport("SayyoDll.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int VKSendKeyAction_MU(
      IntPtr hMouse,
      ref SayyoHelper.MOUSE_ACTION pMouAction);

    [DllImport("SayyoDll.dll", EntryPoint = "VKSendKeyAction_MU", CallingConvention = CallingConvention.Cdecl)]
    public static extern int VKSendKeyAction_MUIntPtr(IntPtr hMouse, IntPtr pMouAction);

    public static void Initialize()
    {
      string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      string str = baseDirectory + SayyoHelper.dllPath;
      string sFullPath = baseDirectory + SayyoHelper.sysPath;
      string sDeviceName = "\\\\.\\SayyoCMU";
      IntPtr dllModule = FDllWrapper.LoadSDK(SayyoHelper.dllPath);
      FLog.d("driver load success");
      Type t = typeof (SayyoHelper.Delegate_KmdDriverSetup);
      if (((SayyoHelper.Delegate_KmdDriverSetup) FDllWrapper.GetFunctionAddress(dllModule, "KmdDriverSetup", t))(sFullPath, ref SayyoHelper.FLyService) == 1)
        FLog.d("driver init success");
      else
        FLog.d("driver init failed");
      if (SayyoHelper.DriverDeviceOpen("\\\\.\\SayyoCKB", ref SayyoHelper.VKeyboardDevice) == 1)
        FLog.d("Visual keyboard device open success");
      else
        FLog.d("Visual keyboard device open faield");
      if (SayyoHelper.DriverDeviceOpen(sDeviceName, ref SayyoHelper.VMouseDevice) == 1)
        FLog.d("Visual mouse device open success");
      else
        FLog.d("Visual mouse device open failed");
      SayyoHelper.MOUSE_ACTION structure = new SayyoHelper.MOUSE_ACTION();
      IntPtr hglobal = Marshal.AllocHGlobal(Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure));
      IntPtr ptr1 = Marshal.AllocHGlobal(Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure));
      IntPtr ptr2 = Marshal.AllocHGlobal(Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure));
      IntPtr ptr3 = Marshal.AllocHGlobal(Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure));
      structure.Type = (ushort) 0;
      structure.Reserved = (ushort) 0;
      structure.mMove.SimulateType = (ushort) 2;
      structure.mMove.AxisFlag = (ushort) 0;
      structure.mMove.x = 100;
      structure.mMove.y = 200;
      Marshal.StructureToPtr<SayyoHelper.MOUSE_ACTION>(structure, ptr1, false);
      structure.Type = (ushort) 1;
      Marshal.StructureToPtr<SayyoHelper.MOUSE_ACTION>(structure, ptr2, false);
      structure.Type = (ushort) 2;
      structure.Reserved = (ushort) 1;
      Marshal.StructureToPtr<SayyoHelper.MOUSE_ACTION>(structure, ptr3, false);
      FLog.d("MOUSE_ACTION size:" + Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure).ToString());
      Marshal.FreeHGlobal(hglobal);
      Thread.Sleep(20);
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate int Delegate_KmdDriverSetup([MarshalAs(UnmanagedType.LPStr)] string sFullPath, ref IntPtr FService);

    public struct MOUSE_ACTION_Move
    {
      public ushort SimulateType;
      public ushort AxisFlag;
      public int x;
      public int y;
    }

    public struct MOUSE_ACTION_Button
    {
      public ushort ButtonType;
      public ushort ButtonFlags;
    }

    public struct MOUSE_ACTION_Wheel
    {
      public short Count;
      public short Reserved;
    }

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct MOUSE_ACTION
    {
      [FieldOffset(0)]
      public ushort Type;
      [FieldOffset(2)]
      public ushort Reserved;
      [FieldOffset(4)]
      public SayyoHelper.MOUSE_ACTION_Move mMove;
      [FieldOffset(4)]
      public SayyoHelper.MOUSE_ACTION_Button mButton;
      [FieldOffset(4)]
      public SayyoHelper.MOUSE_ACTION_Wheel mWheel;
    }
  }
}
