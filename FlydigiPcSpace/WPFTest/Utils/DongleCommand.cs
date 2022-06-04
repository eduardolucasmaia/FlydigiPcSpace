// Decompiled with JetBrains decompiler
// Type: WPFTest.Utils.DongleCommand
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using SharpDX.XInput;
using System.Threading;

namespace WPFTest.Utils
{
  internal class DongleCommand
  {
    public static byte[] getUpdatePartlyStartCommand(int partlyID)
    {
      byte[] partlyStartCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      partlyStartCommand[0] = (byte) 5;
      partlyStartCommand[1] = (byte) 239;
      switch (partlyID)
      {
        case 0:
          partlyStartCommand[2] = (byte) 1;
          partlyStartCommand[3] = (byte) 2;
          break;
        case 1:
          partlyStartCommand[2] = (byte) 5;
          partlyStartCommand[3] = (byte) 3;
          break;
        case 2:
          partlyStartCommand[2] = (byte) 7;
          partlyStartCommand[3] = (byte) 2;
          break;
        case 3:
          partlyStartCommand[2] = (byte) 7;
          partlyStartCommand[3] = (byte) 2;
          break;
      }
      return partlyStartCommand;
    }

    public static byte[] getStartCommand(int length)
    {
      byte[] startCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      startCommand[0] = (byte) 5;
      startCommand[1] = (byte) 234;
      startCommand[2] = (byte) length;
      return startCommand;
    }

    public static byte[] getUpdateCommand(int length, int partlyID, int parcelCount)
    {
      byte[] updateCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      updateCommand[0] = (byte) 5;
      updateCommand[1] = (byte) 239;
      switch (partlyID)
      {
        case 1:
          updateCommand[2] = (byte) 3;
          break;
        case 2:
          updateCommand[2] = (byte) 4;
          break;
        case 3:
          updateCommand[2] = (byte) 5;
          break;
        case 4:
          updateCommand[2] = (byte) 6;
          break;
      }
      updateCommand[3] = (byte) parcelCount;
      return updateCommand;
    }

    public static byte[] getReadConfigCommand(int pageId)
    {
      byte[] readConfigCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      readConfigCommand[0] = (byte) 5;
      readConfigCommand[1] = (byte) 235;
      readConfigCommand[2] = (byte) pageId;
      return readConfigCommand;
    }

    public static byte[] getSwitchOriginData(bool state)
    {
      byte[] switchOriginData = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      switchOriginData[0] = (byte) 5;
      switchOriginData[1] = (byte) 238;
      switchOriginData[2] = state ? (byte) 0 : (byte) 1;
      return switchOriginData;
    }

    public static byte[] getSwitchAndroid2XInput()
    {
      byte[] switchAndroid2Xinput = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      switchAndroid2Xinput[0] = (byte) 5;
      switchAndroid2Xinput[1] = (byte) 237;
      return switchAndroid2Xinput;
    }

    public static byte[] getDeviceInfoInAndroid()
    {
      byte[] deviceInfoInAndroid = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      deviceInfoInAndroid[0] = (byte) 5;
      deviceInfoInAndroid[1] = (byte) 236;
      return deviceInfoInAndroid;
    }

    public static byte[] getTestRGBCommand(int r, int g, int b)
    {
      byte[] testRgbCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      testRgbCommand[0] = (byte) 5;
      testRgbCommand[1] = (byte) 224;
      testRgbCommand[2] = (byte) r;
      testRgbCommand[3] = (byte) g;
      testRgbCommand[4] = (byte) b;
      return testRgbCommand;
    }

    public static byte[] getTestLedCommand(int led_id)
    {
      byte[] testLedCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      testLedCommand[0] = (byte) 5;
      testLedCommand[1] = (byte) 227;
      testLedCommand[2] = (byte) led_id;
      testLedCommand[3] = (byte) 2;
      testLedCommand[4] = (byte) 1;
      return testLedCommand;
    }

    public static byte[] getTestMotorCommand(int left, int right)
    {
      byte[] testMotorCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      testMotorCommand[0] = (byte) 5;
      testMotorCommand[1] = (byte) 15;
      testMotorCommand[2] = (byte) left;
      testMotorCommand[3] = (byte) right;
      return testMotorCommand;
    }

    public static byte[] getTestMotorCommand()
    {
      byte[] testMotorCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      testMotorCommand[0] = (byte) 5;
      testMotorCommand[1] = (byte) 15;
      testMotorCommand[2] = (byte) 112;
      testMotorCommand[3] = (byte) 112;
      return testMotorCommand;
    }

    public static byte[] getDongleVersionCommand()
    {
      byte[] dongleVersionCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      dongleVersionCommand[0] = (byte) 5;
      dongleVersionCommand[1] = (byte) 17;
      return dongleVersionCommand;
    }

    public static byte[] getTestJoystickResetCommand()
    {
      byte[] joystickResetCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      joystickResetCommand[0] = (byte) 5;
      joystickResetCommand[1] = (byte) 226;
      return joystickResetCommand;
    }

    public static byte[] getTestNewJoystickResetCommand(byte d)
    {
      byte[] joystickResetCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      joystickResetCommand[0] = (byte) 5;
      joystickResetCommand[1] = (byte) 226;
      joystickResetCommand[2] = d;
      return joystickResetCommand;
    }

    public static byte[] getStopMacroActionCommand()
    {
      byte[] macroActionCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      macroActionCommand[0] = (byte) 5;
      macroActionCommand[1] = (byte) 233;
      return macroActionCommand;
    }

    public static byte[] getSTDControl(bool state)
    {
      byte[] stdControl = new byte[12];
      stdControl[0] = (byte) 5;
      stdControl[1] = (byte) 16;
      stdControl[2] = state ? (byte) 0 : (byte) 1;
      stdControl[3] = state ? (byte) 0 : (byte) 1;
      stdControl[4] = state ? (byte) 0 : (byte) 1;
      return stdControl;
    }

    public static void SwitchXInput2Android()
    {
      Controller[] controllerArray = new Controller[4]
      {
        new Controller((UserIndex) 0),
        new Controller((UserIndex) 1),
        new Controller((UserIndex) 2),
        new Controller((UserIndex) 3)
      };
      Controller controller1 = (Controller) null;
      int num1 = 3;
      while (num1 > 0)
      {
        foreach (Controller controller2 in controllerArray)
        {
          if (controller2.IsConnected)
          {
            FLog.d("XInputConnected: " + controller2.UserIndex.ToString());
            controller1 = controller2;
            break;
          }
        }
        --num1;
        if (controller1 == null)
        {
          FLog.d("SwitchXInput2Android wait");
          Thread.Sleep(100);
        }
        else
          break;
      }
      if (controller1 == null)
        FLog.d("SwitchXInput2Android controller == null");
      else if (!controller1.IsConnected)
        FLog.d("SwitchXInput2Android controller.IsConnected = false");
      if (controller1 == null || !controller1.IsConnected)
        return;
      FLog.d("controller.IsConnected !!!!!!!!!!!!!!!!!!");
      try
      {
        int num2 = 43520;
        int num3 = 47872;
        Vibration vibration = new Vibration();
        vibration.LeftMotorSpeed = (ushort) num2;
        vibration.RightMotorSpeed = (ushort) num3;
        controller1.SetVibration(vibration);
        Thread.Sleep(100);
        vibration.LeftMotorSpeed = (ushort) num3;
        vibration.RightMotorSpeed = (ushort) num2;
        controller1.SetVibration(vibration);
        Thread.Sleep(100);
        vibration.LeftMotorSpeed = (ushort) num2;
        vibration.RightMotorSpeed = (ushort) num3;
        controller1.SetVibration(vibration);
        Thread.Sleep(100);
        vibration.LeftMotorSpeed = (ushort) 0;
        vibration.RightMotorSpeed = (ushort) 0;
        controller1.SetVibration(vibration);
      }
      catch
      {
      }
      DataManager.getInstance().HidDisconnect();
    }

    public static byte[] getCurrentConfigIdCommand()
    {
      byte[] currentConfigIdCommand = new byte[ConfigUtils.MaxLengthEveryParcel + 2];
      currentConfigIdCommand[0] = (byte) 5;
      currentConfigIdCommand[1] = (byte) 235;
      currentConfigIdCommand[2] = (byte) 160;
      return currentConfigIdCommand;
    }
  }
}
