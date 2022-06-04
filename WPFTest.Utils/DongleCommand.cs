using ApexSpace;
using SharpDX.XInput;
using System;
using System.Threading;

namespace WPFTest.Utils
{
	public class DongleCommand
	{
		public static byte[] getUpdatePartlyStartCommand(int partlyID)
		{
			byte[] array = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			array[0] = 5;
			array[1] = 239;
			switch (partlyID)
			{
				case 0:
					array[2] = 1;
					array[3] = 2;
					break;
				case 1:
					array[2] = 5;
					array[3] = 3;
					break;
				case 2:
					array[2] = 7;
					array[3] = 2;
					break;
				case 3:
					array[2] = 7;
					array[3] = 2;
					break;
			}
			return array;
		}

		public static byte[] getStartCommand(int length)
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 234;
			expr_0C[2] = (byte)length;
			return expr_0C;
		}

		public static byte[] getUpdateCommand(int length, int partlyID, int parcelCount)
		{
			byte[] array = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			array[0] = 5;
			array[1] = 239;
			if (partlyID == 1)
			{
				array[2] = 3;
			}
			else if (partlyID == 2)
			{
				array[2] = 4;
			}
			else if (partlyID == 3)
			{
				array[2] = 5;
			}
			else if (partlyID == 4)
			{
				array[2] = 6;
			}
			array[3] = (byte)parcelCount;
			return array;
		}

		public static byte[] getReadConfigCommand(int pageId)
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 235;
			expr_0C[2] = (byte)pageId;
			return expr_0C;
		}

		public static byte[] getSwitchOriginData(bool state)
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 238;
			expr_0C[2] = (state ? 0 : 1);
			return expr_0C;
		}

		public static byte[] getSwitchAndroid2XInput()
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 237;
			return expr_0C;
		}

		public static byte[] getDeviceInfoInAndroid()
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 236;
			return expr_0C;
		}

		public static byte[] getTestRGBCommand(int r, int g, int b)
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 224;
			expr_0C[2] = (byte)r;
			expr_0C[3] = (byte)g;
			expr_0C[4] = (byte)b;
			return expr_0C;
		}

		public static byte[] getTestLedCommand(int led_id)
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 227;
			expr_0C[2] = (byte)led_id;
			expr_0C[3] = 2;
			expr_0C[4] = 1;
			return expr_0C;
		}

		public static byte[] getTestMotorCommand(int left, int right)
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 15;
			expr_0C[2] = (byte)left;
			expr_0C[3] = (byte)right;
			return expr_0C;
		}

		public static byte[] getTestMotorCommand()
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 15;
			expr_0C[2] = 112;
			expr_0C[3] = 112;
			return expr_0C;
		}

		public static byte[] getDongleVersionCommand()
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 17;
			return expr_0C;
		}

		public static byte[] getTestJoystickResetCommand()
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 226;
			return expr_0C;
		}

		public static byte[] getTestNewJoystickResetCommand(byte d)
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 226;
			expr_0C[2] = d;
			return expr_0C;
		}

		public static byte[] getStopMacroActionCommand()
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 233;
			return expr_0C;
		}

		public static byte[] getSTDControl(bool state)
		{
			byte[] expr_07 = new byte[12];
			expr_07[0] = 5;
			expr_07[1] = 16;
			expr_07[2] = (state ? 0 : 1);
			expr_07[3] = (state ? 0 : 1);
			expr_07[4] = (state ? 0 : 1);
			return expr_07;
		}

		public static void SwitchXInput2Android()
		{
			Controller[] array = new Controller[]
			{
				new Controller(0),
				new Controller(1),
				new Controller(2),
				new Controller(3)
			};
			Controller controller = null;
			int i = 3;
			while (i > 0)
			{
				Controller[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					Controller controller2 = array2[j];
					if (controller2.get_IsConnected())
					{
						FLog.d("XInputConnected: " + controller2.get_UserIndex().ToString());
						controller = controller2;
						break;
					}
				}
				i--;
				if (controller != null)
				{
					break;
				}
				FLog.d("SwitchXInput2Android wait");
				Thread.Sleep(100);
			}
			if (controller == null)
			{
				FLog.d("SwitchXInput2Android controller == null");
			}
			else if (!controller.get_IsConnected())
			{
				FLog.d("SwitchXInput2Android controller.IsConnected = false");
			}
			if (controller != null && controller.get_IsConnected())
			{
				FLog.d("controller.IsConnected !!!!!!!!!!!!!!!!!!");
				try
				{
					int num = 43520;
					int num2 = 47872;
					Vibration vibration = default(Vibration);
					vibration.LeftMotorSpeed = (ushort)num;
					vibration.RightMotorSpeed = (ushort)num2;
					controller.SetVibration(vibration);
					Thread.Sleep(100);
					vibration.LeftMotorSpeed = (ushort)num2;
					vibration.RightMotorSpeed = (ushort)num;
					controller.SetVibration(vibration);
					Thread.Sleep(100);
					vibration.LeftMotorSpeed = (ushort)num;
					vibration.RightMotorSpeed = (ushort)num2;
					controller.SetVibration(vibration);
					Thread.Sleep(100);
					vibration.LeftMotorSpeed = 0;
					vibration.RightMotorSpeed = 0;
					controller.SetVibration(vibration);
				}
				catch
				{
				}
				DataManager.getInstance().HidDisconnect();
			}
		}

		public static byte[] getCurrentConfigIdCommand()
		{
			byte[] expr_0C = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
			expr_0C[0] = 5;
			expr_0C[1] = 235;
			expr_0C[2] = 160;
			return expr_0C;
		}
	}
}
