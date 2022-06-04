// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlDialogConnect
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public class UserControlDialogConnect : UserControl, IComponentConnector
    {
        internal Image mTypeWireImg;
        internal Label mLabelConnectMethod1;
        internal Label mLabelCableDesce;
        internal Image mType24GImg;
        internal Label mLabelConnectMethod2;
        internal TextBlock mTextBlockDesc;
        internal Label mLabelConnectSwitchMode;
        internal Label mLabelConnectLed2Desc;
        internal Grid mHelpCenter;
        internal UserControlSelectMenu mDeviceType;
        internal Label mLabelConnectNotice;
        internal Border mSelectMenuDeviceType;
        private bool _contentLoaded;

        public UserControlDialogConnect()
        {
            this.InitializeComponent();
            this.initData();
        }

        private void initData()
        {
            this.mDeviceType.setName(Application.Current.FindResource((object)"f1p_connect_tutorial").ToString());
            this.mDeviceType.mLabel.Foreground = (Brush)new SolidColorBrush(Color.FromRgb((byte)0, (byte)116, byte.MaxValue));
            this.mDeviceType.mLabel.FontSize = 18.0;
            this.mDeviceType.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00ffffff");
            this.mDeviceType.mBorder.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
            try
            {
                this.mDeviceType.mImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/resources/icon_arrow_down_blue.png"));
            }
            catch
            {
            }
            this.mDeviceType.mBorder.BorderBrush = (Brush)new BrushConverter().ConvertFrom((object)"#00ffffff");
            this.mDeviceType.mBorder.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00ffffff");
            this.updateUI();
        }

        public void updateUI()
        {
            FLog.d("current device type:" + Constant.CURRENT_DEVICE_TYPE.ToString());
            switch (Constant.CURRENT_DEVICE_TYPE)
            {
                case 0:
                    this.mDeviceType.setName(Application.Current.FindResource((object)"f1_connect_tutorial").ToString());
                    try
                    {
                        this.mTypeWireImg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_connect_mode_0_f1.png"));
                        this.mType24GImg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_connect_mode_1_f1.png"));
                    }
                    catch
                    {
                    }
                    this.mLabelConnectNotice.Content = (object)Application.Current.FindResource((object)"connect_notice").ToString();
                    this.mLabelConnectMethod1.Content = (object)Application.Current.FindResource((object)"connect_method_1").ToString();
                    this.mLabelCableDesce.Content = (object)Application.Current.FindResource((object)"connect_method_data_cable_desc").ToString();
                    this.mLabelConnectSwitchMode.Content = (object)Application.Current.FindResource((object)"connect_switch_mode_f1").ToString();
                    this.mLabelConnectLed2Desc.Content = (object)Application.Current.FindResource((object)"connect_led2_desc_f1").ToString();
                    this.mLabelConnectMethod2.Content = (object)Application.Current.FindResource((object)"connect_method_2").ToString();
                    this.mTextBlockDesc.Text = Application.Current.FindResource((object)"connect_method_receiver_desc_f1").ToString();
                    break;
                case 1:
                    this.mDeviceType.setName(Application.Current.FindResource((object)"a2_connect_tutorial").ToString());
                    try
                    {
                        this.mTypeWireImg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_connect_mode_0.png"));
                        this.mType24GImg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_connect_mode_1.png"));
                    }
                    catch
                    {
                    }
                    this.mLabelConnectNotice.Content = (object)Application.Current.FindResource((object)"connect_notice").ToString();
                    this.mLabelConnectMethod1.Content = (object)Application.Current.FindResource((object)"connect_method_1").ToString();
                    this.mLabelCableDesce.Content = (object)Application.Current.FindResource((object)"connect_method_data_cable_desc").ToString();
                    this.mLabelConnectSwitchMode.Content = (object)Application.Current.FindResource((object)"connect_switch_mode").ToString();
                    this.mLabelConnectLed2Desc.Content = (object)Application.Current.FindResource((object)"connect_led2_desc").ToString();
                    this.mLabelConnectMethod2.Content = (object)Application.Current.FindResource((object)"connect_method_2").ToString();
                    this.mTextBlockDesc.Text = Application.Current.FindResource((object)"connect_method_receiver_desc").ToString();
                    break;
                case 2:
                    this.mDeviceType.setName(Application.Current.FindResource((object)"f1p_connect_tutorial").ToString());
                    try
                    {
                        this.mTypeWireImg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_connect_mode_0_f1p.png"));
                        this.mType24GImg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_connect_mode_1_f1p.png"));
                    }
                    catch
                    {
                    }
                    this.mLabelConnectNotice.Content = (object)Application.Current.FindResource((object)"connect_notice").ToString();
                    this.mLabelConnectMethod1.Content = (object)Application.Current.FindResource((object)"connect_method_1").ToString();
                    this.mLabelCableDesce.Content = (object)Application.Current.FindResource((object)"connect_method_data_cable_desc").ToString();
                    this.mLabelConnectSwitchMode.Content = (object)Application.Current.FindResource((object)"connect_switch_mode_f1").ToString();
                    this.mLabelConnectLed2Desc.Content = (object)Application.Current.FindResource((object)"connect_led2_desc_f1p").ToString();
                    this.mLabelConnectMethod2.Content = (object)Application.Current.FindResource((object)"connect_method_2").ToString();
                    this.mTextBlockDesc.Text = Application.Current.FindResource((object)"connect_method_receiver_desc_f1p").ToString();
                    break;
                default:
                    this.mDeviceType.setName(Application.Current.FindResource((object)"f1p_connect_tutorial").ToString());
                    try
                    {
                        this.mTypeWireImg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_connect_mode_0_f1.png"));
                        this.mType24GImg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_connect_mode_1_f1.png"));
                    }
                    catch
                    {
                    }
                    this.mLabelConnectNotice.Content = (object)Application.Current.FindResource((object)"connect_notice").ToString();
                    this.mLabelConnectMethod1.Content = (object)Application.Current.FindResource((object)"connect_method_1").ToString();
                    this.mLabelCableDesce.Content = (object)Application.Current.FindResource((object)"connect_method_data_cable_desc").ToString();
                    this.mLabelConnectSwitchMode.Content = (object)Application.Current.FindResource((object)"connect_switch_mode_f1").ToString();
                    this.mLabelConnectLed2Desc.Content = (object)Application.Current.FindResource((object)"connect_led2_desc_f1p").ToString();
                    this.mLabelConnectMethod2.Content = (object)Application.Current.FindResource((object)"connect_method_2").ToString();
                    this.mTextBlockDesc.Text = Application.Current.FindResource((object)"connect_method_receiver_desc_f1p").ToString();
                    break;
            }
        }

        private void DeviceTypeSelect_Click(object sender, RoutedEventArgs e)
        {
            if (this.mSelectMenuDeviceType.Visibility == Visibility.Visible)
                this.closeSelectMenuDeviceType();
            else
                this.openSelectMenuDeviceType();
        }

        private void DeviceTypeItem_Click_0(object sender, RoutedEventArgs e)
        {
            this.mDeviceType.setName(Application.Current.FindResource((object)"f1_connect_tutorial").ToString());
            this.closeSelectMenuDeviceType();
            Constant.CURRENT_DEVICE_TYPE = 0;
            this.updateUI();
        }

        private void DeviceTypeItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.mDeviceType.setName(Application.Current.FindResource((object)"a2_connect_tutorial").ToString());
            this.closeSelectMenuDeviceType();
            Constant.CURRENT_DEVICE_TYPE = 1;
            this.updateUI();
        }

        private void DeviceTypeItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.mDeviceType.setName(Application.Current.FindResource((object)"f1p_connect_tutorial").ToString());
            this.closeSelectMenuDeviceType();
            Constant.CURRENT_DEVICE_TYPE = 2;
            this.updateUI();
        }

        private void closeSelectMenuDeviceType()
        {
            this.mSelectMenuDeviceType.Visibility = Visibility.Hidden;
            this.mDeviceType.setSelected(false);
            this.mDeviceType.mImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_arrow_down_blue.png"));
        }

        private void openSelectMenuDeviceType()
        {
            this.mSelectMenuDeviceType.Visibility = Visibility.Visible;
            this.mDeviceType.setSelected(true);
        }

        private void Connect_Help_Click(object sender, RoutedEventArgs e)
        {
            if (NetworkUtils.getCurrentLanguage().Equals("zh"))
                Process.Start("http://bbs.flydigi.com/detail/15346");
            else
                Process.Start("http://bbs.flydigi.com/en/detail/80");
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontroldialogconnect.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.mTypeWireImg = (Image)target;
                    break;
                case 2:
                    this.mLabelConnectMethod1 = (Label)target;
                    break;
                case 3:
                    this.mLabelCableDesce = (Label)target;
                    break;
                case 4:
                    this.mType24GImg = (Image)target;
                    break;
                case 5:
                    this.mLabelConnectMethod2 = (Label)target;
                    break;
                case 6:
                    this.mTextBlockDesc = (TextBlock)target;
                    break;
                case 7:
                    this.mLabelConnectSwitchMode = (Label)target;
                    break;
                case 8:
                    this.mLabelConnectLed2Desc = (Label)target;
                    break;
                case 9:
                    this.mHelpCenter = (Grid)target;
                    break;
                case 10:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.Connect_Help_Click);
                    break;
                case 11:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.DeviceTypeSelect_Click);
                    break;
                case 12:
                    this.mDeviceType = (UserControlSelectMenu)target;
                    break;
                case 13:
                    this.mLabelConnectNotice = (Label)target;
                    break;
                case 14:
                    this.mSelectMenuDeviceType = (Border)target;
                    break;
                case 15:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.DeviceTypeItem_Click_2);
                    break;
                case 16:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.DeviceTypeItem_Click_0);
                    break;
                case 17:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.DeviceTypeItem_Click_1);
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }
    }
}
