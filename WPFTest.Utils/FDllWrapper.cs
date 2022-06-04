using System;
using System.Runtime.InteropServices;

namespace WPFTest.Utils
{
	public class FDllWrapper
	{
		[DllImport("kernel32.dll")]
		private static extern uint GetLastError();

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern int LoadLibrary(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr handle, string funcname);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern int FreeLibrary(IntPtr handle);

		public static Delegate GetFunctionAddress(IntPtr dllModule, string functionName, Type t)
		{
			IntPtr procAddress = FDllWrapper.GetProcAddress(dllModule, functionName);
			uint lastError = FDllWrapper.GetLastError();
			FLog.d(string.Format("GetFunctionAddress:{0} ErrorCode: {1}", procAddress, lastError));
			return Marshal.GetDelegateForFunctionPointer(procAddress, t);
		}

		public static Delegate GetDelegateFromIntPtr(IntPtr address, Type t)
		{
			if (address == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer(address, t);
		}

		public static Delegate GetDelegateFromIntPtr(int address, Type t)
		{
			if (address == 0)
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
		}

		public static IntPtr LoadSDK(string lpFileName)
		{
			IntPtr zero = IntPtr.Zero;
			LoadLibraryFlags dwFlags = LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH;
			IntPtr intPtr = FDllWrapper.LoadLibraryEx(lpFileName, zero, dwFlags);
			uint lastError = FDllWrapper.GetLastError();
			FLog.d(string.Format("LoadSDK Result:{0},path:{1} ErrorCode: {2},ErrorMsg:", intPtr, lpFileName, lastError));
			return intPtr;
		}

		public static int ReleaseSDK(IntPtr handle)
		{
			int result;
			try
			{
				FLog.d(string.Format("FreeLibrary handle：{0}", handle));
				int num = FDllWrapper.FreeLibrary(handle);
				uint lastError = FDllWrapper.GetLastError();
				FLog.d(string.Format("FreeLibrary Result:{0}, ErrorCode: {1}", num, lastError));
				result = 0;
			}
			catch (Exception arg_41_0)
			{
				Console.WriteLine(arg_41_0);
				result = -1;
			}
			return result;
		}
	}
}
