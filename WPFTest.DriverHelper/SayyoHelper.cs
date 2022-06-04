using System;
using System.Runtime.InteropServices;
using System.Threading;
using WPFTest.Utils;

namespace WPFTest.DriverHelper
{
	public class SayyoHelper
	{
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
		public static extern int VKSendMsVirtualKeySingle_KB(IntPtr hKeyboard, byte VirtualKey, bool bUp);

		[DllImport("SayyoDll.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int VKSendKeyAction_MU(IntPtr hMouse, ref SayyoHelper.MOUSE_ACTION pMouAction);

		[DllImport("SayyoDll.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "VKSendKeyAction_MU")]
		public static extern int VKSendKeyAction_MUIntPtr(IntPtr hMouse, IntPtr pMouAction);

		public static void Initialize()
		{
			string expr_0A = AppDomain.CurrentDomain.BaseDirectory;
			expr_0A + SayyoHelper.dllPath;
			string sFullPath = expr_0A + SayyoHelper.sysPath;
			string arg_82_0 = "\\\\.\\SayyoCKB";
			string sDeviceName = "\\\\.\\SayyoCMU";
			IntPtr arg_4F_0 = FDllWrapper.LoadSDK(SayyoHelper.dllPath);
			FLog.d("driver load success");
			if (((SayyoHelper.Delegate_KmdDriverSetup)FDllWrapper.GetFunctionAddress(arg_4F_0, "KmdDriverSetup", typeof(SayyoHelper.Delegate_KmdDriverSetup)))(sFullPath, ref SayyoHelper.FLyService) == 1)
			{
				FLog.d("driver init success");
			}
			else
			{
				FLog.d("driver init failed");
			}
			if (SayyoHelper.DriverDeviceOpen(arg_82_0, ref SayyoHelper.VKeyboardDevice) == 1)
			{
				FLog.d("Visual keyboard device open success");
			}
			else
			{
				FLog.d("Visual keyboard device open faield");
			}
			if (SayyoHelper.DriverDeviceOpen(sDeviceName, ref SayyoHelper.VMouseDevice) == 1)
			{
				FLog.d("Visual mouse device open success");
			}
			else
			{
				FLog.d("Visual mouse device open failed");
			}
			SayyoHelper.MOUSE_ACTION structure = default(SayyoHelper.MOUSE_ACTION);
			IntPtr arg_196_0 = Marshal.AllocHGlobal(Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure));
			IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure));
			IntPtr ptr2 = Marshal.AllocHGlobal(Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure));
			IntPtr ptr3 = Marshal.AllocHGlobal(Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure));
			structure.Type = 0;
			structure.Reserved = 0;
			structure.mMove.SimulateType = 2;
			structure.mMove.AxisFlag = 0;
			structure.mMove.x = 100;
			structure.mMove.y = 200;
			Marshal.StructureToPtr<SayyoHelper.MOUSE_ACTION>(structure, ptr, false);
			structure.Type = 1;
			Marshal.StructureToPtr<SayyoHelper.MOUSE_ACTION>(structure, ptr2, false);
			structure.Type = 2;
			structure.Reserved = 1;
			Marshal.StructureToPtr<SayyoHelper.MOUSE_ACTION>(structure, ptr3, false);
			FLog.d("MOUSE_ACTION size:" + Marshal.SizeOf<SayyoHelper.MOUSE_ACTION>(structure).ToString());
			Marshal.FreeHGlobal(arg_196_0);
			Thread.Sleep(20);
		}
	}
}
