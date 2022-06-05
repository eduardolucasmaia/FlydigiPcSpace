// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlConfig
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace.data;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public partial class UserControlConfig : UserControl, IComponentConnector
    {
        private ConfigBean mConfig;
        private IDelegateCallback mIDelegateCallback;
        private int[] mCommonListKey = new int[8]
        {
      4,
      5,
      7,
      8,
      18,
      19,
      20,
      21
        };
        private Dictionary<int, UserControlKeyMapping> mDictionary = new Dictionary<int, UserControlKeyMapping>();
        private int[] MotorLevel = new int[4]
        {
      242,
      0,
      243,
      241
        };
        private string[] MotorLevelText = new string[4];
        //internal UserControlTitle mTitle1;
        //internal WrapPanel mWrapPanel;
        //internal UserControlTitle mTitle2;
        //internal UserControlLedMode mLedMode;
        //internal UserControlLedColor mLedColor;
        //internal UserControlLedMotorLevel mLedMotorLvele;
        //internal UserControlTitle mTitle3;
        //internal UserControlLineJoystickDisplay mJoystickLeft;
        //internal UserControlLineJoystickDisplay mJoystickRight;
        //private bool _contentLoaded;

        public UserControlConfig()
        {
            this.InitializeComponent();
            this.mTitle1.setTitle(Application.Current.FindResource((object)"frequently_used_keys").ToString());
            this.mTitle2.setTitle(Application.Current.FindResource((object)"lighting_mode").ToString());
            this.mTitle3.setTitle(Application.Current.FindResource((object)"joystick_curve").ToString());
        }

        public void setDelegate(IDelegateCallback iDelegateCallback) => this.mIDelegateCallback = iDelegateCallback;

        public void setData(ConfigBean config)
        {
            FLog.d("setDatasetData");
            this.mConfig = config;
            this.mWrapPanel.Children.Clear();
            this.mDictionary.Clear();
            for (int index = 0; index < this.mConfig.Macro.ListMacro.Count; ++index)
            {
                UserControlKeyMapping keyMapping = new UserControlKeyMapping();
                keyMapping.setCurrentKeyId(this.mConfig.Macro.ListMacro[index].Btn);
                keyMapping.setTargetObj((object)this.mConfig.Macro.ListMacro[index]);
                this.addKeyMapping(keyMapping);
            }
            for (int index = 0; index < this.mConfig.ListKeyMapping.Count; ++index)
            {
                if (!this.mDictionary.ContainsKey(index) && this.mConfig.ListKeyMapping[index] != index)
                {
                    UserControlKeyMapping keyMapping = new UserControlKeyMapping();
                    keyMapping.setCurrentKeyId(index);
                    keyMapping.setTargetObj((object)this.mConfig.ListKeyMapping[index]);
                    this.addKeyMapping(keyMapping);
                }
            }
            for (int index = 0; index < this.mCommonListKey.Length; ++index)
            {
                int num = this.mCommonListKey[index];
                if (!this.mDictionary.ContainsKey(num))
                {
                    UserControlKeyMapping keyMapping = new UserControlKeyMapping();
                    keyMapping.setCurrentKeyId(num);
                    keyMapping.setTargetObj((object)num);
                    this.addKeyMapping(keyMapping);
                }
            }
            switch (this.mConfig.Led.Mode)
            {
                case 0:
                    this.mLedMode.setTitle(Application.Current.FindResource((object)"led_mode_alert").ToString());
                    break;
                case 1:
                    this.mLedMode.setTitle(Application.Current.FindResource((object)"led_mode_hide").ToString());
                    break;
                case 2:
                    this.mLedMode.setTitle(Application.Current.FindResource((object)"led_mode_close").ToString());
                    break;
                case 3:
                    this.mLedMode.setTitle(Application.Current.FindResource((object)"led_mode_hunt").ToString());
                    break;
                case 4:
                    this.mLedMode.setTitle(Application.Current.FindResource((object)"led_mode_float").ToString());
                    break;
            }
            try
            {
                if (((IEnumerable<int>)this.MotorLevel).Contains<int>(this.mConfig.Motor.Level))
                    this.mLedMotorLvele.setTitle(Application.Current.FindResource((object)("motor_level_" + this.mConfig.Motor.Level.ToString())).ToString());
            }
            catch (Exception ex)
            {
                this.mLedMotorLvele.setTitle(Application.Current.FindResource((object)"motor_level_0").ToString());
            }
            if (this.mConfig.Led.Mode == 1)
            {
                this.mLedColor.setColor0((Brush)new SolidColorBrush(Color.FromRgb((byte)this.mConfig.Led.RgbColor0[0], (byte)this.mConfig.Led.RgbColor0[1], (byte)this.mConfig.Led.RgbColor0[2])));
                this.mLedColor.setColor1((Brush)new SolidColorBrush(Color.FromRgb((byte)this.mConfig.Led.RgbColor1[0], (byte)this.mConfig.Led.RgbColor1[1], (byte)this.mConfig.Led.RgbColor1[2])));
            }
            else if (this.mConfig.Led.Mode == 0 || this.mConfig.Led.Mode == 2 || this.mConfig.Led.Mode == 3)
            {
                this.mLedColor.setColor0((Brush)new SolidColorBrush(Color.FromRgb((byte)this.mConfig.Led.RgbColor0[0], (byte)this.mConfig.Led.RgbColor0[1], (byte)this.mConfig.Led.RgbColor0[2])));
                this.mLedColor.setColor1((Brush)new SolidColorBrush(Colors.Transparent));
            }
            else if (this.mConfig.Led.Mode == 4)
            {
                this.mLedColor.setColor0((Brush)new SolidColorBrush(Colors.Transparent));
                this.mLedColor.setColor1((Brush)new SolidColorBrush(Colors.Transparent));
            }
            this.mJoystickLeft.setData(this.mConfig.JoyMapping.LeftJoyStick);
            this.mJoystickRight.setData(this.mConfig.JoyMapping.RightJoyStick);
        }

        private void addKeyMapping(UserControlKeyMapping keyMapping)
        {
            if (this.mWrapPanel.Children.Count >= 8)
                return;
            if (this.mWrapPanel.Children.Count % 2 == 0)
                keyMapping.Margin = new Thickness(0.0, 12.0, 8.0, 12.0);
            else
                keyMapping.Margin = new Thickness(8.0, 12.0, 0.0, 12.0);
            keyMapping.setFocusEnable(false);
            this.mWrapPanel.Children.Add((UIElement)keyMapping);
            this.mDictionary.Add(keyMapping.getCurrentKeyId(), keyMapping);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        private void ExportConfig_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Config";
            dlg.DefaultExt = ".flc";
            dlg.Filter = "Flydigi config files (*.flc)|*.flc|All files (*.*)|*.*";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                File.WriteAllText(dlg.FileName, JsonConvert.SerializeObject(this.mConfig));
            }
        }

        private void ImportConfig_Click(object sender, RoutedEventArgs e)
        {
        }



        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolconfig.xaml", UriKind.Relative));
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
        //            this.mWrapPanel = (WrapPanel)target;
        //            break;
        //        case 3:
        //            this.mTitle2 = (UserControlTitle)target;
        //            break;
        //        case 4:
        //            this.mLedMode = (UserControlLedMode)target;
        //            break;
        //        case 5:
        //            this.mLedColor = (UserControlLedColor)target;
        //            break;
        //        case 6:
        //            this.mLedMotorLvele = (UserControlLedMotorLevel)target;
        //            break;
        //        case 7:
        //            this.mTitle3 = (UserControlTitle)target;
        //            break;
        //        case 8:
        //            this.mJoystickLeft = (UserControlLineJoystickDisplay)target;
        //            break;
        //        case 9:
        //            this.mJoystickRight = (UserControlLineJoystickDisplay)target;
        //            break;
        //        case 10:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Click);
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
