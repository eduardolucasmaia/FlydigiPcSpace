// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigLed
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using ApexSpace.data;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public partial class UserControlSettingConfigLed : UserControl, IComponentConnector
    {
        private ConfigBean mFlashConfigBean;
        private LedBean mLed;
        private MotorBean motor;
        private LunpanBean lunpan;
        private string mLastLedJson;
        private Grid mGrid;
        private bool mLive = true;
        private int Led_Continue_Time = 100;
        private int[] MotorLevel = new int[4]
        {
      242,
      0,
      243,
      241
        };
        private string[] MotorLevelText = new string[4]
        {
      "motor_level_242",
      "motor_level_4",
      "motor_level_1",
      "motor_level_3"
        };
        private int[] LunpanMode = new int[3] { 0, 1, 2 };
        private string[] LunpanModeText = new string[3]
        {
      "right_joystick",
      "left_joystick",
      "motion_close"
        };
        private int updatePatrlyType;
        //internal UserControlTitle mTitle1;
        //internal UserControlSelectMenu mLeftMotorLevel;
        //internal UserControlTitle mTitle3;
        //internal UserControlSelectMenu mLedMode;
        //internal StackPanel mLayoutColor0;
        //internal UserControlSelectMenuColor mColor0;
        //internal StackPanel mLayoutColor1;
        //internal UserControlSelectMenuColor mColor1;
        //internal Grid mLayoutLight;
        //internal Label mLightValue;
        //internal Slider mSliderLight;
        //internal Grid mLayoutPeroid;
        //internal Label mPeroidValue;
        //internal Slider mSliderPeroid;
        //internal StackPanel mLuanpanBox;
        //internal UserControlTitle mTitle2;
        //internal StackPanel mTitle2_content;
        //internal UserControlSelectMenu mMotionTarget;
        //internal Border mSelectMenuMotionRemoteTarget;
        //internal Border mSelectMenuLedMode;
        //internal Border mSelectLeftMotorLevel;
        //internal Border mSelectRightMotorLevel;
        //internal UserControlDialogColor mSelectMenuColor;
       //private bool _contentLoaded;

        public UserControlSettingConfigLed()
        {
            this.InitializeComponent();
            this.mTitle1.setTitle(Application.Current.FindResource((object)"basic_vibration_intensity").ToString());
            this.mTitle2.setTitle(Application.Current.FindResource((object)"basic_roulette_mapping").ToString());
            this.mTitle3.setTitle(Application.Current.FindResource((object)"basic_adjusting_colour").ToString());
            this.mLuanpanBox.Visibility = Visibility.Collapsed;
            if (DataManager.getInstance().getDeviceInfo().DeviceId != Constant.DEVICE.APEX_2)
                return;
            this.mLuanpanBox.Visibility = Visibility.Visible;
        }

        public void setData(
          ConfigBean config,
          LedBean led,
          Grid grid,
          MotorBean motorObj,
          LunpanBean lunpanObj)
        {
            FLog.d("LED--config1:" + JsonConvert.SerializeObject((object)config));
            FLog.d("led:" + JsonConvert.SerializeObject((object)led));
            this.mFlashConfigBean = config;
            this.mLed = led;
            this.mLastLedJson = JsonConvert.SerializeObject((object)this.mLed);
            this.mGrid = grid;
            this.motor = motorObj;
            this.lunpan = lunpanObj;
            this.initData();
            new Thread((ThreadStart)(() =>
            {
                while (this.mLive)
                {
                    if (this.mLed.Light == 0 || this.Visibility != Visibility.Visible)
                    {
                        this.setBackgroundColor(this.mLed.Light, 0, 0, 0);
                        Thread.Sleep(this.Led_Continue_Time);
                    }
                    else
                    {
                        int millisecondsTimeout = 3000 / (this.mLed.Light * 2);
                        int light = this.mLed.Light;
                        switch (this.mLed.Mode)
                        {
                            case 0:
                                this.setBackgroundColor(this.mLed.Light, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                                Thread.Sleep(this.Led_Continue_Time);
                                continue;
                            case 1:
                                for (int index = 0; index < 2; ++index)
                                {
                                    List<int> intList = new List<int>();
                                    if (index == 0)
                                        intList.AddRange((IEnumerable<int>)this.mLed.RgbColor0);
                                    else
                                        intList.AddRange((IEnumerable<int>)this.mLed.RgbColor1);
                                    for (int alpha = 0; alpha < this.mLed.Light * 2 && this.mLed.Mode == 1 && this.mLed.Light == light; ++alpha)
                                    {
                                        if (alpha <= this.mLed.Light)
                                            this.setBackgroundColor(alpha, intList[0], intList[1], intList[2]);
                                        else
                                            this.setBackgroundColor(this.mLed.Light * 2 - alpha, intList[0], intList[1], intList[2]);
                                        Thread.Sleep(millisecondsTimeout);
                                    }
                                }
                                continue;
                            case 2:
                                this.setBackgroundColor(0, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                                Thread.Sleep(this.Led_Continue_Time);
                                continue;
                            case 3:
                                this.setBackgroundColor(this.mLed.Light, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                                Thread.Sleep(this.Led_Continue_Time);
                                continue;
                            case 4:
                                for (int index = 0; index < 7; ++index)
                                {
                                    List<int> rgbColorById = CommonUtils.getRGBColorById(index + 1);
                                    for (int alpha = 0; alpha < this.mLed.Light * 2 && this.mLed.Mode == 4 && this.mLed.Light == light && this.Visibility == Visibility.Visible; ++alpha)
                                    {
                                        if (alpha <= this.mLed.Light)
                                            this.setBackgroundColor(alpha, rgbColorById[0], rgbColorById[1], rgbColorById[2]);
                                        else
                                            this.setBackgroundColor(this.mLed.Light * 2 - alpha, rgbColorById[0], rgbColorById[1], rgbColorById[2]);
                                        Thread.Sleep(millisecondsTimeout);
                                    }
                                }
                                continue;
                            default:
                                continue;
                        }
                    }
                }
            })).Start();
        }

        public void setData(ConfigBean config, LedBean led, Grid grid)
        {
            FLog.d("LED--config2:" + JsonConvert.SerializeObject((object)config));
            this.mFlashConfigBean = config;
            this.mLed = led;
            this.mLastLedJson = JsonConvert.SerializeObject((object)this.mLed);
            this.mGrid = grid;
            this.initData();
            new Thread((ThreadStart)(() =>
            {
                while (this.mLive)
                {
                    if (this.mLed.Light == 0 || this.Visibility != Visibility.Visible)
                    {
                        this.setBackgroundColor(this.mLed.Light, 0, 0, 0);
                        Thread.Sleep(this.Led_Continue_Time);
                    }
                    else
                    {
                        int millisecondsTimeout = 3000 / (this.mLed.Light * 2);
                        int light = this.mLed.Light;
                        switch (this.mLed.Mode)
                        {
                            case 0:
                                this.setBackgroundColor(this.mLed.Light, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                                Thread.Sleep(this.Led_Continue_Time);
                                continue;
                            case 1:
                                for (int index = 0; index < 2; ++index)
                                {
                                    List<int> intList = new List<int>();
                                    if (index == 0)
                                        intList.AddRange((IEnumerable<int>)this.mLed.RgbColor0);
                                    else
                                        intList.AddRange((IEnumerable<int>)this.mLed.RgbColor1);
                                    for (int alpha = 0; alpha < this.mLed.Light * 2 && this.mLed.Mode == 1 && this.mLed.Light == light; ++alpha)
                                    {
                                        if (alpha <= this.mLed.Light)
                                            this.setBackgroundColor(alpha, intList[0], intList[1], intList[2]);
                                        else
                                            this.setBackgroundColor(this.mLed.Light * 2 - alpha, intList[0], intList[1], intList[2]);
                                        Thread.Sleep(millisecondsTimeout);
                                    }
                                }
                                continue;
                            case 2:
                                this.setBackgroundColor(0, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                                Thread.Sleep(this.Led_Continue_Time);
                                continue;
                            case 3:
                                this.setBackgroundColor(this.mLed.Light, this.mLed.RgbColor0[0], this.mLed.RgbColor0[1], this.mLed.RgbColor0[2]);
                                Thread.Sleep(this.Led_Continue_Time);
                                continue;
                            case 4:
                                for (int index = 0; index < 7; ++index)
                                {
                                    List<int> rgbColorById = CommonUtils.getRGBColorById(index + 1);
                                    for (int alpha = 0; alpha < this.mLed.Light * 2 && this.mLed.Mode == 4 && this.mLed.Light == light && this.Visibility == Visibility.Visible; ++alpha)
                                    {
                                        if (alpha <= this.mLed.Light)
                                            this.setBackgroundColor(alpha, rgbColorById[0], rgbColorById[1], rgbColorById[2]);
                                        else
                                            this.setBackgroundColor(this.mLed.Light * 2 - alpha, rgbColorById[0], rgbColorById[1], rgbColorById[2]);
                                        Thread.Sleep(millisecondsTimeout);
                                    }
                                }
                                continue;
                            default:
                                continue;
                        }
                    }
                }
            })).Start();
        }

        public void close() => this.mLive = false;

        private void setBackgroundColor(int alpha, int r, int g, int b) => Application.Current.Dispatcher.Invoke((Action)(() => this.mGrid.Background = (Brush)new SolidColorBrush(Color.FromArgb((byte)alpha, (byte)r, (byte)g, (byte)b))));

        public void BackbroundClick()
        {
            this.closeSelectMenuLedMode();
            this.closeSelectMenuColor0();
            this.closeSelectMenuColor1();
            this.closeSelectMenuLeftMotorLevelTarget();
            this.closeSelectMenuMotionTarget();
        }

        private void initData()
        {
            try
            {
                this.mLeftMotorLevel.setName(Application.Current.FindResource((object)("motor_level_" + this.motor.Level.ToString())).ToString());
                this.mMotionTarget.setName(Application.Current.FindResource((object)this.LunpanModeText[this.lunpan.Type]).ToString());
                switch (this.mLed.Mode)
                {
                    case 0:
                        this.setModeUI(0);
                        break;
                    case 1:
                        this.setModeUI(1);
                        break;
                    case 3:
                        this.setModeUI(3);
                        break;
                    case 4:
                        this.setModeUI(4);
                        break;
                }
                this.mColor0.setColor((byte)this.mLed.RgbColor0[0], (byte)this.mLed.RgbColor0[1], (byte)this.mLed.RgbColor0[2]);
                this.mColor1.setColor((byte)this.mLed.RgbColor1[0], (byte)this.mLed.RgbColor1[1], (byte)this.mLed.RgbColor1[2]);
                this.mSliderLight.Value = (double)this.mLed.Light;
                this.setValueLight(this.mLed.Light);
                this.mSliderPeroid.Value = (double)this.mLed.Peroid;
                this.setValuePeroid(this.mLed.Peroid);
                this.setLeftMotorLevelUI(this.mLed.LeftMotor);
                this.setRightMotorLevelUI(this.mLed.RightMotor);
            }
            catch (Exception ex)
            {
                NetworkUtils.FlydigiPostEvent("基础配置页面修改异常" + ex.Message);
            }
        }

        private void setLeftMotorLevelUI(int Level)
        {
            if (Level >= 0 && Level <= this.MotorLevel[0] || Level >= this.MotorLevel[0] && Level <= this.MotorLevel[1] || Level >= this.MotorLevel[1] && Level <= this.MotorLevel[2] || Level >= this.MotorLevel[2] && Level <= this.MotorLevel[3] || Level < this.MotorLevel[3])
                return;
            int num = this.MotorLevel[4];
        }

        private void setRightMotorLevelUI(int Level)
        {
            if (Level >= 0 && Level <= this.MotorLevel[0] || Level >= this.MotorLevel[0] && Level <= this.MotorLevel[1] || Level >= this.MotorLevel[1] && Level <= this.MotorLevel[2] || Level >= this.MotorLevel[2] && Level <= this.MotorLevel[3] || Level < this.MotorLevel[3])
                return;
            int num = this.MotorLevel[4];
        }

        private void setModeUI(int mode)
        {
            this.mLed.Mode = mode;
            this.mSelectMenuLedMode.Visibility = Visibility.Hidden;
            this.mLedMode.setSelected(false);
            switch (mode)
            {
                case 0:
                    this.mLedMode.setName(Application.Current.FindResource((object)"led_mode_alert").ToString());
                    this.mLayoutColor0.Visibility = Visibility.Visible;
                    this.mLayoutColor1.Visibility = Visibility.Collapsed;
                    this.mLayoutLight.Visibility = Visibility.Visible;
                    this.mLayoutPeroid.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    this.mLedMode.setName(Application.Current.FindResource((object)"led_mode_hide").ToString());
                    this.mLayoutColor0.Visibility = Visibility.Visible;
                    this.mLayoutColor1.Visibility = Visibility.Visible;
                    this.mLayoutLight.Visibility = Visibility.Visible;
                    this.mLayoutPeroid.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    this.mLedMode.setName(Application.Current.FindResource((object)"led_mode_close").ToString());
                    this.mLayoutColor0.Visibility = Visibility.Collapsed;
                    this.mLayoutColor1.Visibility = Visibility.Collapsed;
                    this.mLayoutLight.Visibility = Visibility.Collapsed;
                    this.mLayoutPeroid.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    this.mLedMode.setName(Application.Current.FindResource((object)"led_mode_hunt").ToString());
                    this.mLayoutColor0.Visibility = Visibility.Visible;
                    this.mLayoutColor1.Visibility = Visibility.Collapsed;
                    this.mLayoutLight.Visibility = Visibility.Visible;
                    this.mLayoutPeroid.Visibility = Visibility.Visible;
                    break;
                case 4:
                    this.mLedMode.setName(Application.Current.FindResource((object)"led_mode_float").ToString());
                    this.mLayoutColor0.Visibility = Visibility.Collapsed;
                    this.mLayoutColor1.Visibility = Visibility.Collapsed;
                    this.mLayoutLight.Visibility = Visibility.Visible;
                    this.mLayoutPeroid.Visibility = Visibility.Collapsed;
                    break;
            }
            this.updatePatrlyType = 0;
            this.updatePartly();
        }

        private void LedModeSelect_Click(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuColor0();
            this.closeSelectMenuColor1();
            this.closeSelectMenuLeftMotorLevelTarget();
            this.closeSelectMenuMotionTarget();
            if (this.mSelectMenuLedMode.Visibility == Visibility.Visible)
            {
                this.closeSelectMenuLedMode();
            }
            else
            {
                this.mSelectMenuLedMode.Visibility = Visibility.Visible;
                this.mLedMode.setSelected(true);
            }
        }

        private void Color0Select_Click(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuLedMode();
            this.closeSelectMenuLeftMotorLevelTarget();
            this.closeSelectMenuMotionTarget();
            if (this.mSelectMenuColor.Visibility == Visibility.Visible)
            {
                this.closeSelectMenuColor0();
            }
            else
            {
                this.mSelectMenuColor.Visibility = Visibility.Visible;
                this.mSelectMenuColor.Margin = new Thickness(0.0, 310.0, 0.0, 0.0);
                this.mColor0.setSelected(true);
                this.mSelectMenuColor.setData(this.mColor0, 0, this.mLed, (IDelegateCallback)(result =>
                {
                    this.updatePatrlyType = 0;
                    this.updatePartly();
                }));
            }
        }

        private void Color1Select_Click(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuLedMode();
            this.closeSelectMenuLeftMotorLevelTarget();
            this.closeSelectMenuMotionTarget();
            if (this.mSelectMenuColor.Visibility == Visibility.Visible)
            {
                this.closeSelectMenuColor1();
            }
            else
            {
                this.mSelectMenuColor.Visibility = Visibility.Visible;
                this.mSelectMenuColor.Margin = new Thickness(0.0, 383.0, 0.0, 0.0);
                this.mColor1.setSelected(true);
                this.mSelectMenuColor.setData(this.mColor1, 1, this.mLed, (IDelegateCallback)(result =>
                {
                    this.updatePatrlyType = 0;
                    this.updatePartly();
                }));
            }
        }

        private void LedMode_Click_1(object sender, RoutedEventArgs e) => this.setModeUI(0);

        private void LedMode_Click_2(object sender, RoutedEventArgs e) => this.setModeUI(3);

        private void LedMode_Click_3(object sender, RoutedEventArgs e) => this.setModeUI(4);

        private void LedMode_Click_4(object sender, RoutedEventArgs e) => this.setModeUI(1);

        private void mSliderLight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.setValueLight((int)this.mSliderLight.Value);

        private void mSliderPeroid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.setValuePeroid((int)this.mSliderPeroid.Value);

        private void SliderLight_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.updatePatrlyType = 0;
            this.updatePartly();
        }

        private void SliderPeroid_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.updatePatrlyType = 0;
            this.updatePartly();
        }

        private void setValueLight(int value)
        {
            if (this.mLed == null)
                return;
            this.mLed.Light = value;
            this.mLightValue.Content = (object)value;
        }

        private void setValuePeroid(int value)
        {
            if (this.mLed == null)
                return;
            this.mLed.Peroid = value;
            this.mPeroidValue.Content = (object)value;
        }

        private void closeSelectMenuLedMode()
        {
            this.mSelectMenuLedMode.Visibility = Visibility.Hidden;
            this.mLedMode.setSelected(false);
        }

        private void closeSelectMenuColor0()
        {
            this.mSelectMenuColor.Visibility = Visibility.Hidden;
            this.mColor0.setSelected(false);
        }

        private void closeSelectMenuColor1()
        {
            this.mSelectMenuColor.Visibility = Visibility.Hidden;
            this.mColor1.setSelected(false);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(true, 44, "灯光调节:\n警戒：常亮\n猎杀：按下即亮\n浮游：自由切换\n潜伏：呼吸");

        private void Button_MouseLeave(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(false, 44, "");

        private void updatePartly()
        {
            if ((JsonConvert.SerializeObject((object)this.mLed) + JsonConvert.SerializeObject((object)this.motor) + JsonConvert.SerializeObject((object)this.lunpan)).Equals(this.mLastLedJson) || DataManager.getInstance().getUpdatePartlyWritingState())
                return;
            DataManager.getInstance().setUpdatePartlyWritingState(true);
            ConfigBean config = this.mFlashConfigBean.Clone();
            config.Led = this.mLed.Clone();
            config.Motor = this.motor.Clone();
            config.LunpanMapping = this.lunpan.Clone();
            FLog.d("修改后：：Led" + JsonConvert.SerializeObject((object)config));
            List<List<byte>> doubleListByConfig = ConfigUtils.getDoubleListByConfig(config);
            List<List<byte>> partlyDoubleList = new List<List<byte>>();
            List<byte> byteList = new List<byte>();
            byteList.AddRange((IEnumerable<byte>)((IEnumerable<byte>)DongleCommand.getUpdatePartlyStartCommand(this.updatePatrlyType)).ToList<byte>());
            partlyDoubleList.Add(byteList);
            int num1 = (int)byteList[2];
            int num2 = (int)byteList[3];
            for (int index = num1 + 1; index < num1 + num2 + 1; ++index)
                partlyDoubleList.Add(doubleListByConfig[index]);
            for (int index = 0; index < partlyDoubleList.Count; ++index)
                FLog.d("局部更新待发送配置:" + CommonUtils.byteToHexString(partlyDoubleList[index].ToArray()));
            new Thread((ThreadStart)(() =>
            {
                DataManager.getInstance().writeConfig(partlyDoubleList, (IDelegateCallback)(result =>
                {
                    if (result != 100)
                        return;
                    this.mFlashConfigBean.Led = this.mLed;
                    this.mLastLedJson = JsonConvert.SerializeObject((object)this.mLed) + JsonConvert.SerializeObject((object)this.motor) + JsonConvert.SerializeObject((object)this.lunpan);
                    FLog.d("配置局部更新完成");
                    DataManager.getInstance().setUpdatePartlyWritingState(false);
                }));
                for (int index = 0; index < 60 && DataManager.getInstance().getUpdatePartlyWritingState(); ++index)
                    Thread.Sleep(100);
                DataManager.getInstance().setUpdatePartlyWritingState(false);
            })).Start();
        }

        private void MotionRemoteTargetSelect_Click(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuLedMode();
            this.closeSelectMenuColor0();
            this.closeSelectMenuColor1();
            this.closeSelectMenuLeftMotorLevelTarget();
            if (this.mSelectMenuMotionRemoteTarget.Visibility == Visibility.Visible)
                this.closeSelectMenuMotionTarget();
            else
                this.openSelectMenuMotionTarget();
        }

        private void closeSelectMenuMotionTarget()
        {
            this.mSelectMenuMotionRemoteTarget.Visibility = Visibility.Hidden;
            this.mMotionTarget.setSelected(false);
        }

        private void openSelectMenuMotionTarget()
        {
            this.mSelectMenuMotionRemoteTarget.Visibility = Visibility.Visible;
            this.mMotionTarget.setSelected(true);
        }

        private void MotionTargetItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.mMotionTarget.setName(Application.Current.FindResource((object)"left_joystick").ToString());
            this.closeSelectMenuMotionTarget();
            this.lunpan.Type = this.LunpanMode[1];
            this.updatePatrlyType = 3;
            this.updatePartly();
        }

        private void MotionTargetItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.mMotionTarget.setName(Application.Current.FindResource((object)"right_joystick").ToString());
            this.closeSelectMenuMotionTarget();
            this.lunpan.Type = this.LunpanMode[0];
            this.updatePatrlyType = 3;
            this.updatePartly();
        }

        private void MotionTargetItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.mMotionTarget.setName(Application.Current.FindResource((object)"motion_close").ToString());
            this.closeSelectMenuMotionTarget();
            this.lunpan.Type = this.LunpanMode[2];
            this.updatePatrlyType = 3;
            this.updatePartly();
        }

        private void MotionActiveItem_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void MotionActiveItem_Click_2(object sender, RoutedEventArgs e)
        {
        }

        private void LeftMotorLevelTargetSelect_Click(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuLedMode();
            this.closeSelectMenuColor0();
            this.closeSelectMenuColor1();
            this.closeSelectMenuMotionTarget();
            if (this.mSelectLeftMotorLevel.Visibility == Visibility.Visible)
                this.closeSelectMenuLeftMotorLevelTarget();
            else
                this.openSelectMenuLeftMotorLevelTarget();
        }

        private void RightMotorLevelTargetSelect_Click(object sender, RoutedEventArgs e)
        {
            if (this.mSelectRightMotorLevel.Visibility == Visibility.Visible)
                this.closeSelectMenuRightMotorLevelTarget();
            else
                this.openSelectMenuRightMotorLevelTarget();
        }

        private void closeSelectMenuLeftMotorLevelTarget()
        {
            this.mSelectLeftMotorLevel.Visibility = Visibility.Hidden;
            this.mLeftMotorLevel.setSelected(false);
        }

        private void openSelectMenuLeftMotorLevelTarget()
        {
            this.mSelectLeftMotorLevel.Visibility = Visibility.Visible;
            this.mLeftMotorLevel.setSelected(true);
        }

        private void closeSelectMenuRightMotorLevelTarget() => this.mSelectRightMotorLevel.Visibility = Visibility.Hidden;

        private void openSelectMenuRightMotorLevelTarget() => this.mSelectRightMotorLevel.Visibility = Visibility.Visible;

        private void LeftMotorLevelItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.mLeftMotorLevel.setName(Application.Current.FindResource((object)"motor_level_242").ToString());
            this.closeSelectMenuLeftMotorLevelTarget();
            FLog.d("meng set motor 1");
            this.motor.Level = this.MotorLevel[0];
            this.updatePatrlyType = 3;
            this.updatePartly();
        }

        private void LeftMotorLevelItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.mLeftMotorLevel.setName(Application.Current.FindResource((object)"motor_level_0").ToString());
            this.closeSelectMenuLeftMotorLevelTarget();
            FLog.d("meng set motor 1");
            this.motor.Level = this.MotorLevel[1];
            this.updatePatrlyType = 3;
            this.updatePartly();
        }

        private void LeftMotorLevelItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.mLeftMotorLevel.setName(Application.Current.FindResource((object)"motor_level_243").ToString());
            this.closeSelectMenuLeftMotorLevelTarget();
            FLog.d("meng set motor 1");
            this.motor.Level = this.MotorLevel[2];
            this.updatePatrlyType = 3;
            this.updatePartly();
        }

        private void LeftMotorLevelItem_Click_4(object sender, RoutedEventArgs e)
        {
            this.mLeftMotorLevel.setName(Application.Current.FindResource((object)"motor_level_241").ToString());
            this.closeSelectMenuLeftMotorLevelTarget();
            FLog.d("meng set motor 1");
            this.motor.Level = this.MotorLevel[3];
            this.updatePatrlyType = 3;
            this.updatePartly();
        }

        private void LeftMotorLevelItem_Click_5(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuLeftMotorLevelTarget();
            this.updatePatrlyType = 3;
            this.updatePartly();
        }

        private void RightMotorLevelItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuRightMotorLevelTarget();
            this.mLed.RightMotor = this.MotorLevel[0];
            this.updatePartly();
        }

        private void RightMotorLevelItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuRightMotorLevelTarget();
            this.mLed.RightMotor = this.MotorLevel[1];
            this.updatePartly();
        }

        private void RightMotorLevelItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuRightMotorLevelTarget();
            this.mLed.RightMotor = this.MotorLevel[2];
            this.updatePartly();
        }

        private void RightMotorLevelItem_Click_4(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuRightMotorLevelTarget();
            this.mLed.RightMotor = this.MotorLevel[3];
            this.updatePartly();
        }

        private void RightMotorLevelItem_Click_5(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuRightMotorLevelTarget();
            this.mLed.RightMotor = this.MotorLevel[4];
            this.updatePartly();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_test_Click(object sender, RoutedEventArgs e) => new Thread((ParameterizedThreadStart)(o =>
        {
            DataManager.getInstance().SendByteArray(DongleCommand.getTestMotorCommand());
            Thread.Sleep(2000);
            DataManager.getInstance().SendByteArray(DongleCommand.getTestMotorCommand(0, 0));
        })).Start();

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigled.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mTitle1 = (UserControlTitle)target;
        //            break;
        //        case 2:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LeftMotorLevelTargetSelect_Click);
        //            break;
        //        case 3:
        //            this.mLeftMotorLevel = (UserControlSelectMenu)target;
        //            break;
        //        case 4:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_test_Click);
        //            break;
        //        case 5:
        //            this.mTitle3 = (UserControlTitle)target;
        //            break;
        //        case 6:
        //            ((UIElement)target).MouseEnter += new MouseEventHandler(this.Button_MouseEnter);
        //            ((UIElement)target).MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
        //            break;
        //        case 7:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LedModeSelect_Click);
        //            break;
        //        case 8:
        //            this.mLedMode = (UserControlSelectMenu)target;
        //            break;
        //        case 9:
        //            this.mLayoutColor0 = (StackPanel)target;
        //            break;
        //        case 10:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.Color0Select_Click);
        //            break;
        //        case 11:
        //            this.mColor0 = (UserControlSelectMenuColor)target;
        //            break;
        //        case 12:
        //            this.mLayoutColor1 = (StackPanel)target;
        //            break;
        //        case 13:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.Color1Select_Click);
        //            break;
        //        case 14:
        //            this.mColor1 = (UserControlSelectMenuColor)target;
        //            break;
        //        case 15:
        //            this.mLayoutLight = (Grid)target;
        //            break;
        //        case 16:
        //            this.mLightValue = (Label)target;
        //            break;
        //        case 17:
        //            this.mSliderLight = (Slider)target;
        //            this.mSliderLight.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.mSliderLight_ValueChanged);
        //            this.mSliderLight.AddHandler(Thumb.DragCompletedEvent, (Delegate)new DragCompletedEventHandler(this.SliderLight_DragCompleted));
        //            break;
        //        case 18:
        //            this.mLayoutPeroid = (Grid)target;
        //            break;
        //        case 19:
        //            this.mPeroidValue = (Label)target;
        //            break;
        //        case 20:
        //            this.mSliderPeroid = (Slider)target;
        //            this.mSliderPeroid.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.mSliderPeroid_ValueChanged);
        //            this.mSliderPeroid.AddHandler(Thumb.DragCompletedEvent, (Delegate)new DragCompletedEventHandler(this.SliderPeroid_DragCompleted));
        //            break;
        //        case 21:
        //            this.mLuanpanBox = (StackPanel)target;
        //            break;
        //        case 22:
        //            this.mTitle2 = (UserControlTitle)target;
        //            break;
        //        case 23:
        //            this.mTitle2_content = (StackPanel)target;
        //            break;
        //        case 24:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionRemoteTargetSelect_Click);
        //            break;
        //        case 25:
        //            this.mMotionTarget = (UserControlSelectMenu)target;
        //            break;
        //        case 26:
        //            this.mSelectMenuMotionRemoteTarget = (Border)target;
        //            break;
        //        case 27:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetItem_Click_1);
        //            break;
        //        case 28:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetItem_Click_2);
        //            break;
        //        case 29:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetItem_Click_3);
        //            break;
        //        case 30:
        //            this.mSelectMenuLedMode = (Border)target;
        //            break;
        //        case 31:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LedMode_Click_1);
        //            break;
        //        case 32:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LedMode_Click_2);
        //            break;
        //        case 33:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LedMode_Click_3);
        //            break;
        //        case 34:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LedMode_Click_4);
        //            break;
        //        case 35:
        //            this.mSelectLeftMotorLevel = (Border)target;
        //            break;
        //        case 36:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LeftMotorLevelItem_Click_1);
        //            break;
        //        case 37:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LeftMotorLevelItem_Click_2);
        //            break;
        //        case 38:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LeftMotorLevelItem_Click_3);
        //            break;
        //        case 39:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.LeftMotorLevelItem_Click_4);
        //            break;
        //        case 40:
        //            this.mSelectRightMotorLevel = (Border)target;
        //            break;
        //        case 41:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.RightMotorLevelItem_Click_1);
        //            break;
        //        case 42:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.RightMotorLevelItem_Click_2);
        //            break;
        //        case 43:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.RightMotorLevelItem_Click_3);
        //            break;
        //        case 44:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.RightMotorLevelItem_Click_5);
        //            break;
        //        case 45:
        //            this.mSelectMenuColor = (UserControlDialogColor)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
