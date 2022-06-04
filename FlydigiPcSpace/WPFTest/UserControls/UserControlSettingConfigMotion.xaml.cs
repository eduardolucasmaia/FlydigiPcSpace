// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigMotion
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using ApexSpace.data;
using ApexSpace.Utils;
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
    public partial class UserControlSettingConfigMotion : UserControl, IComponentConnector
    {
        private ConfigBean mFlashConfigBean;
        private string mLastMotionJson;
        private MotionMappingBean mMotionMappingBean;
        //internal Button mButtonMotorDefault;
        //internal Button mButtonMotorPOut;
        //internal Button modeTypeHeleBox;
        //internal Grid tipBox;
        //internal TextBlock tipBoxtextBlock;
        //internal Grid ModeBox;
        //internal Label mLineLeft;
        //internal Label mLineRight;
        //internal Grid mappingSetBox;
        //internal UserControlSelectMenu mMotionTarget;
        //internal Grid mLayoutTime;
        //internal Label mSensityValue;
        //internal Slider mSliderSensity;
        //internal Grid deadBox;
        //internal Label mZeroValue;
        //internal Slider mSliderZero;
        //internal Label startTypeBox1;
        //internal Label startTypeBox2;
        //internal Button startTypeBox3;
        //internal UserControlSelectMenu mMotionActive;
        //internal StackPanel startKeyBox;
        //internal Label mStartKeyTipLabel;
        //internal UserControlSelectMenu mTargetKey;
        //internal Button mKeySelectTwoButton;
        //internal UserControlSelectMenu mTargetKeyTwo;
        //internal Border mSelectMenuMotionTarget;
        //internal Border mSelectMenuMotionActive;
        //internal UserControlDialogKey mSelectMenuKey;
        //internal UserControlDialogKey mSelectMenuKeyTwo;
        //internal Grid mEnableShadow;
       //private bool _contentLoaded;

        public UserControlSettingConfigMotion()
        {
            this.InitializeComponent();
            this.mSelectMenuKey.setDelegate((IDelegateCallback)(id =>
            {
                this.closeSelectMenu();
                this.setTargetKeyId(id);
            }));
            this.mSelectMenuKeyTwo.setDelegate((IDelegateCallback)(id =>
            {
                this.closeSelectMenuTwo();
                this.setTargetExtKeyId(id);
            }));
        }

        public void setData(ConfigBean config, MotionMappingBean motionMappingBean)
        {
            this.mFlashConfigBean = config;
            this.mMotionMappingBean = motionMappingBean;
            this.mLastMotionJson = JsonConvert.SerializeObject((object)this.mMotionMappingBean);
            this.initData();
            if (DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1_WIRED)
                this.mEnableShadow.Visibility = Visibility.Visible;
            else
                this.mEnableShadow.Visibility = Visibility.Collapsed;
            if (((WindowMain)WindowMain.getInstance()).mTopBar.mCurrentModeType != 2)
                return;
            this.ModeBox.Visibility = Visibility.Collapsed;
            this.mButtonMotorPOut.Visibility = Visibility.Collapsed;
            this.mButtonMotorDefault.Visibility = Visibility.Collapsed;
            this.modeTypeHeleBox.Visibility = Visibility.Collapsed;
            this.mappingSetBox.Visibility = Visibility.Collapsed;
            this.deadBox.Visibility = Visibility.Collapsed;
            this.startTypeBox1.Visibility = Visibility.Collapsed;
            this.startTypeBox2.Visibility = Visibility.Collapsed;
            this.startTypeBox3.Visibility = Visibility.Collapsed;
            this.startKeyBox.Visibility = Visibility.Collapsed;
            this.tipBox.Visibility = Visibility.Visible;
            this.tipBoxtextBlock.Text = Application.Current.FindResource((object)"tipinfo4").ToString();
        }

        private void initData()
        {
            this.updateMotorModeUI();
            FLog.d("体感数据：" + JsonConvert.SerializeObject((object)this.mMotionMappingBean));
            if (this.mMotionMappingBean.Type == 1)
                this.MotionTargetItem_Click_1((object)null, (RoutedEventArgs)null);
            else if (this.mMotionMappingBean.Type == 2)
                this.MotionTargetItem_Click_2((object)null, (RoutedEventArgs)null);
            else
                this.MotionTargetItem_Click_3((object)null, (RoutedEventArgs)null);
            this.setTargetKeyId(this.mMotionMappingBean.KeyId);
            this.setTargetExtKeyId(this.mMotionMappingBean.KeyidExt);
            if (this.mMotionMappingBean.Method == 0)
                this.MotionActiveItem_Click_1((object)null, (RoutedEventArgs)null);
            else if (this.mMotionMappingBean.Method == 1)
                this.MotionActiveItem_Click_2((object)null, (RoutedEventArgs)null);
            this.mSliderSensity.Value = (double)this.mMotionMappingBean.Sensity;
            this.setValueSensity(this.mMotionMappingBean.Sensity);
            this.mSliderZero.Value = (double)this.mMotionMappingBean.Zero;
            this.setValueZero(this.mMotionMappingBean.Zero);
        }

        private void setTargetKeyId(int id)
        {
            this.mMotionMappingBean.KeyId = id;
            this.mTargetKey.setName(DeviceUtils.getKeyNameByID(this.mMotionMappingBean.KeyId));
            this.updatePartly();
        }

        private void setTargetExtKeyId(int id)
        {
            this.mMotionMappingBean.KeyidExt = id;
            this.mTargetKeyTwo.setName(DeviceUtils.getKeyNameByID(this.mMotionMappingBean.KeyidExt));
            this.updatePartly();
        }

        private void KeySelect_Click(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuMotionTarget();
            this.closeSelectMenuMotionActive();
            this.closeSelectMenuKeyTwo();
            if (this.mSelectMenuKey.Visibility == Visibility.Visible)
                this.closeSelectMenu();
            else
                this.openSelectMenu();
        }

        private void closeSelectMenu()
        {
            this.mSelectMenuKey.Visibility = Visibility.Hidden;
            this.mTargetKey.setSelected(false);
        }

        private void closeSelectTwoMenu()
        {
            this.mSelectMenuKeyTwo.Visibility = Visibility.Hidden;
            this.mTargetKeyTwo.setSelected(false);
        }

        private void openSelectMenu()
        {
            this.mSelectMenuKey.Visibility = Visibility.Visible;
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (gameHadleName == "f1p")
                        this.mSelectMenuKey.setListKey(new List<int>(), 0);
                }
                else
                    this.mSelectMenuKey.setListKey(new List<int>(), 1);
            }
            else
                this.mSelectMenuKey.setListKey(new List<int>(), 2);
            this.mTargetKey.setSelected(true);
        }

        public void BackbroundClick()
        {
            this.closeSelectMenuMotionTarget();
            this.closeSelectMenuMotionActive();
            this.closeSelectMenuKey();
            this.closeSelectMenuKeyTwo();
        }

        private void MotionTargetSelect_Click(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuMotionActive();
            this.closeSelectMenuKey();
            this.closeSelectMenuKeyTwo();
            if (this.mSelectMenuMotionTarget.Visibility == Visibility.Visible)
                this.closeSelectMenuMotionTarget();
            else
                this.openSelectMenuMotionTarget();
        }

        private void MotionActiveSelect_Click(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuMotionTarget();
            this.closeSelectMenuKey();
            this.closeSelectMenuKeyTwo();
            if (this.mSelectMenuMotionActive.Visibility == Visibility.Visible)
                this.closeSelectMenuMotionActive();
            else
                this.openSelectMenuMotionActive();
        }

        private void MotionTargetItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.mMotionTarget.setName(Application.Current.FindResource((object)"left_joystick").ToString());
            this.closeSelectMenuMotionTarget();
            this.mMotionMappingBean.Type = 1;
            this.updatePartly();
        }

        private void MotionTargetItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.mMotionTarget.setName(Application.Current.FindResource((object)"right_joystick").ToString());
            this.closeSelectMenuMotionTarget();
            this.mMotionMappingBean.Type = 2;
            this.updatePartly();
        }

        private void MotionTargetItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.mMotionTarget.setName(Application.Current.FindResource((object)"motion_close").ToString());
            this.closeSelectMenuMotionTarget();
            this.mMotionMappingBean.Type = 0;
            this.updatePartly();
        }

        private void MotionActiveItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.mMotionActive.setName(Application.Current.FindResource((object)"press_open_or_close").ToString());
            this.closeSelectMenuMotionActive();
            this.mMotionMappingBean.Method = 0;
            this.mStartKeyTipLabel.Visibility = Visibility.Hidden;
            this.mKeySelectTwoButton.Visibility = Visibility.Hidden;
            this.updatePartly();
        }

        private void MotionActiveItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.mMotionActive.setName(Application.Current.FindResource((object)"hold_press_release_close").ToString());
            this.closeSelectMenuMotionActive();
            this.mMotionMappingBean.Method = 1;
            this.mStartKeyTipLabel.Visibility = Visibility.Visible;
            this.mKeySelectTwoButton.Visibility = Visibility.Visible;
            this.updatePartly();
        }

        private void closeSelectMenuKey()
        {
            this.mSelectMenuKey.Visibility = Visibility.Hidden;
            this.mTargetKey.setSelected(false);
        }

        private void closeSelectMenuKeyTwo()
        {
            this.mSelectMenuKeyTwo.Visibility = Visibility.Hidden;
            this.mTargetKeyTwo.setSelected(false);
        }

        private void closeSelectMenuMotionTarget()
        {
            this.mSelectMenuMotionTarget.Visibility = Visibility.Hidden;
            this.mMotionTarget.setSelected(false);
        }

        private void closeSelectMenuMotionActive()
        {
            this.mSelectMenuMotionActive.Visibility = Visibility.Hidden;
            this.mMotionActive.setSelected(false);
        }

        private void openSelectMenuMotionTarget()
        {
            this.mSelectMenuMotionTarget.Visibility = Visibility.Visible;
            this.mMotionTarget.setSelected(true);
        }

        private void openSelectMenuMotionActive()
        {
            this.mSelectMenuMotionActive.Visibility = Visibility.Visible;
            this.mMotionActive.setSelected(true);
        }

        private void mSliderSensity_ValueChanged(
          object sender,
          RoutedPropertyChangedEventArgs<double> e)
        {
            this.setValueSensity((int)this.mSliderSensity.Value);
        }

        private void mSliderZero_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.setValueZero((int)this.mSliderZero.Value);

        private void SliderZero_DragCompleted(object sender, DragCompletedEventArgs e) => this.updatePartly();

        private void SliderSensity_DragCompleted(object sender, DragCompletedEventArgs e) => this.updatePartly();

        private void setValueSensity(int value)
        {
            if (this.mMotionMappingBean == null)
                return;
            this.mMotionMappingBean.Sensity = value;
            this.mSensityValue.Content = (object)value;
        }

        private void setValueZero(int value)
        {
            if (this.mMotionMappingBean == null)
                return;
            this.mMotionMappingBean.Zero = value;
            this.mZeroValue.Content = (object)(value.ToString() + "%");
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(true, 44, Application.Current.FindResource((object)"sensing_setting_desc").ToString());

        private void Button_MouseLeave(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(false, 44, "");

        private void updatePartly()
        {
            if (JsonConvert.SerializeObject((object)this.mMotionMappingBean).Equals(this.mLastMotionJson) || DataManager.getInstance().getUpdatePartlyWritingState())
                return;
            DataManager.getInstance().setUpdatePartlyWritingState(true);
            ConfigBean config = this.mFlashConfigBean.Clone();
            config.MotionMapping = this.mMotionMappingBean.Clone();
            List<List<byte>> doubleListByConfig = ConfigUtils.getDoubleListByConfig(config);
            List<List<byte>> partlyDoubleList = new List<List<byte>>();
            List<byte> byteList = new List<byte>();
            byteList.AddRange((IEnumerable<byte>)((IEnumerable<byte>)DongleCommand.getUpdatePartlyStartCommand(2)).ToList<byte>());
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
                    this.mFlashConfigBean.MotionMapping = this.mMotionMappingBean;
                    this.mLastMotionJson = JsonConvert.SerializeObject((object)this.mMotionMappingBean);
                    FLog.d("配置局部更新完成");
                    DataManager.getInstance().setUpdatePartlyWritingState(false);
                }));
                for (int index = 0; index < 60 && DataManager.getInstance().getUpdatePartlyWritingState(); ++index)
                    Thread.Sleep(100);
                DataManager.getInstance().setUpdatePartlyWritingState(false);
            })).Start();
        }

        private void Button_Motor_default(object sender, RoutedEventArgs e)
        {
            this.mButtonMotorDefault.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mButtonMotorDefault.FontWeight = FontWeights.Bold;
            this.mLineLeft.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            this.mButtonMotorPOut.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonMotorPOut.FontWeight = FontWeights.Normal;
            this.mLineRight.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
            this.mMotionMappingBean.Mode = 0;
            this.updatePartly();
        }

        private void Button_Motor_pout(object sender, RoutedEventArgs e)
        {
            this.mButtonMotorDefault.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonMotorDefault.FontWeight = FontWeights.Normal;
            this.mLineLeft.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
            this.mButtonMotorPOut.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mButtonMotorPOut.FontWeight = FontWeights.Bold;
            this.mLineRight.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            this.mMotionMappingBean.Mode = 1;
            this.updatePartly();
        }

        private void updateMotorModeUI()
        {
            if (this.mMotionMappingBean.Mode == 0)
            {
                this.mButtonMotorDefault.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
                this.mButtonMotorDefault.FontWeight = FontWeights.Bold;
                this.mLineLeft.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
                this.mButtonMotorPOut.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
                this.mButtonMotorPOut.FontWeight = FontWeights.Normal;
                this.mLineRight.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
            }
            else
            {
                if (this.mMotionMappingBean.Mode != 1)
                    return;
                this.mButtonMotorDefault.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
                this.mButtonMotorDefault.FontWeight = FontWeights.Normal;
                this.mLineLeft.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
                this.mButtonMotorPOut.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
                this.mButtonMotorPOut.FontWeight = FontWeights.Bold;
                this.mLineRight.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            }
        }

        private void Button_MouseEnter_mode(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(true, 44, Application.Current.FindResource((object)"sensing_mode_setting_desc").ToString());

        private void Button_MouseLeave_mode(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(false, 44, "");

        private void Button_MouseEnter_nav(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(true, 44, Application.Current.FindResource((object)"sensing_setting_desc").ToString());

        private void Button_MouseLeave_nav(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(false, 44, "");

        private void UserControlSettingConfigMotion_IsVisibleChanged(
          object sender,
          DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                FLog.d("Motion_show");
            else
                FLog.d("Motion_hidden");
        }

        private void KeyNewSelect_Click(object sender, RoutedEventArgs e)
        {
            this.closeSelectMenuMotionTarget();
            this.closeSelectMenuMotionActive();
            this.closeSelectMenuKey();
            if (this.mSelectMenuKeyTwo.Visibility == Visibility.Visible)
                this.closeSelectMenuTwo();
            else
                this.openSelectMenuTwo();
        }

        private void closeSelectMenuTwo()
        {
            this.mSelectMenuKeyTwo.Visibility = Visibility.Hidden;
            this.mTargetKeyTwo.setSelected(false);
        }

        private void openSelectMenuTwo()
        {
            this.mSelectMenuKeyTwo.Visibility = Visibility.Visible;
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (gameHadleName == "f1p")
                        this.mSelectMenuKeyTwo.setListKey(new List<int>(), 0);
                }
                else
                    this.mSelectMenuKeyTwo.setListKey(new List<int>(), 1);
            }
            else
                this.mSelectMenuKeyTwo.setListKey(new List<int>(), 2);
            this.mTargetKeyTwo.setSelected(true);
        }

        private void mMotionActive_Loaded(object sender, RoutedEventArgs e)
        {
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigmotion.xaml", UriKind.Relative));
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
        //            this.mButtonMotorDefault = (Button)target;
        //            this.mButtonMotorDefault.Click += new RoutedEventHandler(this.Button_Motor_default);
        //            break;
        //        case 2:
        //            this.mButtonMotorPOut = (Button)target;
        //            this.mButtonMotorPOut.Click += new RoutedEventHandler(this.Button_Motor_pout);
        //            break;
        //        case 3:
        //            this.modeTypeHeleBox = (Button)target;
        //            this.modeTypeHeleBox.MouseEnter += new MouseEventHandler(this.Button_MouseEnter_mode);
        //            this.modeTypeHeleBox.MouseLeave += new MouseEventHandler(this.Button_MouseLeave_mode);
        //            break;
        //        case 4:
        //            this.tipBox = (Grid)target;
        //            break;
        //        case 5:
        //            this.tipBoxtextBlock = (TextBlock)target;
        //            break;
        //        case 6:
        //            this.ModeBox = (Grid)target;
        //            break;
        //        case 7:
        //            this.mLineLeft = (Label)target;
        //            break;
        //        case 8:
        //            this.mLineRight = (Label)target;
        //            break;
        //        case 9:
        //            this.mappingSetBox = (Grid)target;
        //            break;
        //        case 10:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetSelect_Click);
        //            break;
        //        case 11:
        //            this.mMotionTarget = (UserControlSelectMenu)target;
        //            break;
        //        case 12:
        //            ((UIElement)target).MouseEnter += new MouseEventHandler(this.Button_MouseEnter);
        //            ((UIElement)target).MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
        //            break;
        //        case 13:
        //            this.mLayoutTime = (Grid)target;
        //            break;
        //        case 14:
        //            this.mSensityValue = (Label)target;
        //            break;
        //        case 15:
        //            this.mSliderSensity = (Slider)target;
        //            this.mSliderSensity.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.mSliderSensity_ValueChanged);
        //            this.mSliderSensity.AddHandler(Thumb.DragCompletedEvent, (Delegate)new DragCompletedEventHandler(this.SliderSensity_DragCompleted));
        //            break;
        //        case 16:
        //            this.deadBox = (Grid)target;
        //            break;
        //        case 17:
        //            this.mZeroValue = (Label)target;
        //            break;
        //        case 18:
        //            this.mSliderZero = (Slider)target;
        //            this.mSliderZero.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.mSliderZero_ValueChanged);
        //            this.mSliderZero.AddHandler(Thumb.DragCompletedEvent, (Delegate)new DragCompletedEventHandler(this.SliderZero_DragCompleted));
        //            break;
        //        case 19:
        //            this.startTypeBox1 = (Label)target;
        //            break;
        //        case 20:
        //            this.startTypeBox2 = (Label)target;
        //            break;
        //        case 21:
        //            this.startTypeBox3 = (Button)target;
        //            this.startTypeBox3.Click += new RoutedEventHandler(this.MotionActiveSelect_Click);
        //            break;
        //        case 22:
        //            this.mMotionActive = (UserControlSelectMenu)target;
        //            break;
        //        case 23:
        //            this.startKeyBox = (StackPanel)target;
        //            break;
        //        case 24:
        //            this.mStartKeyTipLabel = (Label)target;
        //            break;
        //        case 25:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.KeySelect_Click);
        //            break;
        //        case 26:
        //            this.mTargetKey = (UserControlSelectMenu)target;
        //            break;
        //        case 27:
        //            this.mKeySelectTwoButton = (Button)target;
        //            this.mKeySelectTwoButton.Click += new RoutedEventHandler(this.KeyNewSelect_Click);
        //            break;
        //        case 28:
        //            this.mTargetKeyTwo = (UserControlSelectMenu)target;
        //            break;
        //        case 29:
        //            this.mSelectMenuMotionTarget = (Border)target;
        //            break;
        //        case 30:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetItem_Click_1);
        //            break;
        //        case 31:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetItem_Click_2);
        //            break;
        //        case 32:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionTargetItem_Click_3);
        //            break;
        //        case 33:
        //            this.mSelectMenuMotionActive = (Border)target;
        //            break;
        //        case 34:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionActiveItem_Click_1);
        //            break;
        //        case 35:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.MotionActiveItem_Click_2);
        //            break;
        //        case 36:
        //            this.mSelectMenuKey = (UserControlDialogKey)target;
        //            break;
        //        case 37:
        //            this.mSelectMenuKeyTwo = (UserControlDialogKey)target;
        //            break;
        //        case 38:
        //            this.mEnableShadow = (Grid)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
