// Decompiled with JetBrains decompiler
// Type: WPFTest.Utils.FDllWrapper
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.Runtime.InteropServices;

namespace WPFTest.Utils
{
  internal class FDllWrapper
  {
    [DllImport("kernel32.dll")]
    private static extern uint GetLastError();

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr LoadLibraryEx(
      string lpFileName,
      IntPtr hReservedNull,
      LoadLibraryFlags dwFlags);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern int LoadLibrary(
      string lpFileName,
      IntPtr hReservedNull,
      LoadLibraryFlags dwFlags);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr handle, string funcname);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern int FreeLibrary(IntPtr handle);

    public static Delegate GetFunctionAddress(
      IntPtr dllModule,
      string functionName,
      Type t)
    {
      IntPtr procAddress = FDllWrapper.GetProcAddress(dllModule, functionName);
      uint lastError = FDllWrapper.GetLastError();
      FLog.d(string.Format("GetFunctionAddress:{0} ErrorCode: {1}", (object) procAddress, (object) lastError));
      return Marshal.GetDelegateForFunctionPointer(procAddress, t);
    }

    public static Delegate GetDelegateFromIntPtr(IntPtr address, Type t) => address == IntPtr.Zero ? (Delegate) null : Marshal.GetDelegateForFunctionPointer(address, t);

    public static Delegate GetDelegateFromIntPtr(int address, Type t) => address == 0 ? (Delegate) null : Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);

    public static IntPtr LoadSDK(string lpFileName)
    {
      IntPtr zero = IntPtr.Zero;
      LoadLibraryFlags dwFlags = LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH;
      IntPtr num = FDllWrapper.LoadLibraryEx(lpFileName, zero, dwFlags);
      uint lastError = FDllWrapper.GetLastError();
      FLog.d(string.Format("LoadSDK Result:{0},path:{1} ErrorCode: {2},ErrorMsg:", (object) num, (object) lpFileName, (object) lastError));
      return num;
    }

    public static int ReleaseSDK(IntPtr handle)
    {
      try
      {
        FLog.d(string.Format("FreeLibrary handle：{0}", (object) handle));
        FLog.d(string.Format("FreeLibrary Result:{0}, ErrorCode: {1}", (object) FDllWrapper.FreeLibrary(handle), (object) FDllWrapper.GetLastError()));
        return 0;
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
        return -1;
      }
    }
  }
}
