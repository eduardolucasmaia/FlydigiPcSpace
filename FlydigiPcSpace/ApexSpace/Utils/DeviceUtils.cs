// Decompiled with JetBrains decompiler
// Type: ApexSpace.Utils.DeviceUtils
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System.Collections.Generic;

namespace ApexSpace.Utils
{
  internal class DeviceUtils
  {
    public static int getByteIndexByID(int id)
    {
      switch (id)
      {
        case 0:
          return 0;
        case 1:
          return 1;
        case 2:
          return 2;
        case 3:
          return 3;
        case 4:
          return 4;
        case 5:
          return 5;
        case 6:
          return 6;
        case 7:
          return 7;
        case 8:
          return 8;
        case 9:
          return 9;
        case 10:
          return 10;
        case 11:
          return 11;
        case 12:
          return 12;
        case 13:
          return 13;
        case 14:
          return 14;
        case 15:
          return 15;
        case 16:
          return 16;
        case 17:
          return 17;
        case 18:
          return 18;
        case 19:
          return 19;
        case 20:
          return 20;
        case 21:
          return 21;
        case 22:
          return 22;
        case 23:
          return 23;
        case 24:
          return 24;
        case 27:
          return 27;
        case 28:
          return 28;
        default:
          return -1;
      }
    }

    public static string getSpecialKeyIconByID(int id, bool state)
    {
      switch (id)
      {
        case 0:
          return !state ? "img_special_key_up_white.png" : "img_special_key_up_blue.png";
        case 1:
          return !state ? "img_special_key_right_white.png" : "img_special_key_right_blue.png";
        case 2:
          return !state ? "img_special_key_down_white.png" : "img_special_key_down_blue.png";
        case 3:
          return !state ? "img_special_key_left_white.png" : "img_special_key_left_blue.png";
        default:
          return "img_key_circle.png";
      }
    }

    public static string getKeyNameByID(int id)
    {
      switch (id)
      {
        case 0:
          return "UP";
        case 1:
          return "RIGHT";
        case 2:
          return "DOWN";
        case 3:
          return "LEFT";
        case 4:
          return "A";
        case 5:
          return "B";
        case 6:
          return "SELECT";
        case 7:
          return "X";
        case 8:
          return "Y";
        case 9:
          return "START";
        case 10:
          return "LB";
        case 11:
          return "RB";
        case 12:
          return "LT";
        case 13:
          return "RT";
        case 14:
          return "L3";
        case 15:
          return "R3";
        case 16:
          return "C";
        case 17:
          return "Z";
        case 18:
          return "M1";
        case 19:
          return "M2";
        case 20:
          return "M3";
        case 21:
          return "M4";
        case 22:
          return "M5";
        case 23:
          return "M6";
        case 24:
          return "MENU";
        case 27:
          return "HOME";
        case 28:
          return "BACK";
        default:
          return "";
      }
    }

    public static string getKeyImageByID(int id, bool state)
    {
      switch (id)
      {
        case 0:
          return !state ? "img_key_circle_up_gray.png" : "img_key_circle_up_blue.png";
        case 1:
          return !state ? "img_key_circle_right_gray.png" : "img_key_circle_right_blue.png";
        case 2:
          return !state ? "img_key_circle_down_gray.png" : "img_key_circle_down_blue.png";
        case 3:
          return !state ? "img_key_circle_left_gray.png" : "img_key_circle_left_blue.png";
        case 4:
          return !state ? "img_key_circle_a_gray.png" : "img_key_circle_a_blue.png";
        case 5:
          return !state ? "img_key_circle_b_gray.png" : "img_key_circle_b_blue.png";
        case 6:
          return !state ? "img_key_circle_select_gray.png" : "img_key_circle_select_blue.png";
        case 7:
          return !state ? "img_key_circle_x_gray.png" : "img_key_circle_x_blue.png";
        case 8:
          return !state ? "img_key_circle_y_gray.png" : "img_key_circle_y_blue.png";
        case 9:
          return !state ? "img_key_circle_start_gray.png" : "img_key_circle_start_blue.png";
        case 10:
          return !state ? "img_key_circle_lb_gray.png" : "img_key_circle_lb_blue.png";
        case 11:
          return !state ? "img_key_circle_rb_gray.png" : "img_key_circle_rb_blue.png";
        case 12:
          return !state ? "img_key_circle_lt_gray.png" : "img_key_circle_lt_blue.png";
        case 13:
          return !state ? "img_key_circle_rt_gray.png" : "img_key_circle_rt_blue.png";
        case 14:
          return !state ? "img_key_circle_l3_gray.png" : "img_key_circle_l3_blue.png";
        case 15:
          return !state ? "img_key_circle_r3_gray.png" : "img_key_circle_r3_blue.png";
        case 16:
          return !state ? "img_key_circle_c_gray.png" : "img_key_circle_c_blue.png";
        case 17:
          return !state ? "img_key_circle_z_gray.png" : "img_key_circle_z_blue.png";
        case 18:
          return !state ? "img_key_circle_m1_gray.png" : "img_key_circle_m1_blue.png";
        case 19:
          return !state ? "img_key_circle_m2_gray.png" : "img_key_circle_m2_blue.png";
        case 20:
          return !state ? "img_key_circle_m3_gray.png" : "img_key_circle_m3_blue.png";
        case 21:
          return !state ? "img_key_circle_m4_gray.png" : "img_key_circle_m4_blue.png";
        case 22:
          return !state ? "img_key_circle_m5_gray.png" : "img_key_circle_m5_blue.png";
        case 23:
          return !state ? "img_key_circle_m6_gray.png" : "img_key_circle_m6_blue.png";
        default:
          return "img_key_circle.png";
      }
    }

    public static List<int> getCurrentListenKeyListFromAndroid(byte[] data)
    {
      List<int> keyListFromAndroid = new List<int>();
      int num1 = (int) data[4] & (int) byte.MaxValue;
      int num2 = (int) data[5] & (int) byte.MaxValue;
      int num3 = (int) data[8];
      int num4 = (int) data[9];
      if ((num1 & 1) != 0)
        keyListFromAndroid.Add(0);
      if ((num1 & 2) != 0)
        keyListFromAndroid.Add(1);
      if ((num1 & 4) != 0)
        keyListFromAndroid.Add(2);
      if ((num1 & 8) != 0)
        keyListFromAndroid.Add(3);
      if ((num1 & 16) != 0)
        keyListFromAndroid.Add(4);
      if ((num1 & 32) != 0)
        keyListFromAndroid.Add(5);
      if ((num1 & 128) != 0)
        keyListFromAndroid.Add(7);
      if ((num2 & 1) != 0)
        keyListFromAndroid.Add(8);
      if ((num1 & 64) != 0)
        keyListFromAndroid.Add(6);
      if ((num2 & 2) != 0)
        keyListFromAndroid.Add(9);
      if ((num2 & 4) != 0)
        keyListFromAndroid.Add(10);
      if ((num2 & 8) != 0)
        keyListFromAndroid.Add(11);
      if ((num2 & 16) != 0)
        keyListFromAndroid.Add(12);
      if ((num2 & 32) != 0)
        keyListFromAndroid.Add(13);
      if ((num2 & 64) != 0)
        keyListFromAndroid.Add(14);
      if ((num2 & 128) != 0)
        keyListFromAndroid.Add(15);
      return keyListFromAndroid;
    }

    public static List<int> getCurrentListenKeyListFromXinput(byte[] data)
    {
      List<int> keyListFromXinput = new List<int>();
      int num1 = (int) data[4] & (int) byte.MaxValue;
      int num2 = (int) data[5] & (int) byte.MaxValue;
      int num3 = (int) data[6] & (int) byte.MaxValue;
      int num4 = (int) data[7] & (int) byte.MaxValue;
      if ((num1 & (int) byte.MaxValue) == 4)
        keyListFromXinput.Add(0);
      if ((num1 & (int) byte.MaxValue) == 12)
        keyListFromXinput.Add(1);
      if ((num1 & (int) byte.MaxValue) == 20)
        keyListFromXinput.Add(2);
      if ((num1 & (int) byte.MaxValue) == 28)
        keyListFromXinput.Add(3);
      if ((num1 & 1) != 0)
        keyListFromXinput.Add(14);
      if ((num1 & 2) != 0)
        keyListFromXinput.Add(15);
      if ((num2 & 1) != 0)
        keyListFromXinput.Add(4);
      if ((num2 & 2) != 0)
        keyListFromXinput.Add(5);
      if ((num2 & 4) != 0)
        keyListFromXinput.Add(7);
      if ((num2 & 8) != 0)
        keyListFromXinput.Add(8);
      if ((num2 & 16) != 0)
        keyListFromXinput.Add(10);
      if ((num2 & 32) != 0)
        keyListFromXinput.Add(11);
      if ((num2 & 64) != 0)
        keyListFromXinput.Add(6);
      if ((num2 & 128) != 0)
        keyListFromXinput.Add(9);
      if ((num3 & (int) byte.MaxValue) > 128)
        keyListFromXinput.Add(12);
      if ((num4 & (int) byte.MaxValue) < 128)
        keyListFromXinput.Add(13);
      return keyListFromXinput;
    }
  }
}
