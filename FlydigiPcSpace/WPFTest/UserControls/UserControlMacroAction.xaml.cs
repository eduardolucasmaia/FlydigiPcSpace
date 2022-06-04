using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTest.UserControls
{
    /// <summary>
    /// Interação lógica para UserControlMacroAction.xam
    /// </summary>
    public partial class UserControlMacroAction : UserControl
    {
        public UserControlMacroAction()
        {
            InitializeCompo// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlMacroAction
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

            using ApexSpace;
            using ApexSpace.data;
            using ApexSpace.Utils;
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
        public class UserControlMacroAction : UserControl, IComponentConnector
        {
            private IDelegateCallback mDelegateCallback;
            private MacroActionBean mMacroActionBean;
            private int mIndex;
            internal StackPanel mLayout;
            internal Label mLabelTime;
            internal Image mImageKey;
            internal Image mImageActionState;
            internal Label mLabelActionDesc;
            private bool _contentLoaded;

            public UserControlMacroAction() => this.InitializeComponent();

            public void setAction(
              MacroActionBean macroActionBean,
              int index,
              IDelegateCallback delegateCallback)
            {
                this.mDelegateCallback = delegateCallback;
                this.mMacroActionBean = macroActionBean;
                this.mIndex = index;
                string keyNameById = DeviceUtils.getKeyNameByID(macroActionBean.Btn);
                this.mImageKey.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getKeyImageByID(macroActionBean.Btn, false)));
                if (macroActionBean.State == 0)
                {
                    this.mLabelActionDesc.Content = (object)(keyNameById + Application.Current.FindResource((object)"key_released").ToString());
                    this.mImageActionState.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_key_released_gray.png"));
                }
                else
                {
                    this.mLabelActionDesc.Content = (object)(keyNameById + Application.Current.FindResource((object)"key_pressed").ToString());
                    this.mImageActionState.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_key_pressed_gray.png"));
                }
                this.mLabelTime.Content = (object)(Math.Round((double)CommonUtils.getIntTime(macroActionBean.Time_h, macroActionBean.Time_l) / 1000.0, 3).ToString() + "s");
            }

            public MacroActionBean getMacroActionBean() => this.mMacroActionBean;

            public int getTime() => CommonUtils.getIntTime(this.mMacroActionBean.Time_h, this.mMacroActionBean.Time_l);

            public void setMacroActionBean(MacroActionBean macroActionBean)
            {
                this.mMacroActionBean = macroActionBean;
                string keyNameById = DeviceUtils.getKeyNameByID(macroActionBean.Btn);
                this.mImageKey.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getKeyImageByID(macroActionBean.Btn, true)));
                if (macroActionBean.State == 0)
                    this.mLabelActionDesc.Content = (object)(keyNameById + Application.Current.FindResource((object)"key_released").ToString());
                else
                    this.mLabelActionDesc.Content = (object)(keyNameById + Application.Current.FindResource((object)"key_pressed").ToString());
                this.mLabelTime.Content = (object)(Math.Round((double)CommonUtils.getIntTime(macroActionBean.Time_h, macroActionBean.Time_l) / 1000.0, 3).ToString() + "s");
            }

            public void setSelected(bool state)
            {
                if (state)
                {
                    this.mLayout.Background = (Brush)new BrushConverter().ConvertFrom((object)"#190074ff");
                    this.mLabelTime.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
                    this.mLabelActionDesc.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
                    this.mImageKey.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getKeyImageByID(this.mMacroActionBean.Btn, true)));
                    if (this.mMacroActionBean.State == 0)
                        this.mImageActionState.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_key_released_blue.png"));
                    else
                        this.mImageActionState.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_key_pressed_blue.png"));
                }
                else
                {
                    this.mLayout.Background = (Brush)new BrushConverter().ConvertFrom((object)"#181818");
                    this.mLabelTime.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
                    this.mLabelActionDesc.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
                    this.mImageKey.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getKeyImageByID(this.mMacroActionBean.Btn, false)));
                    if (this.mMacroActionBean.State == 0)
                        this.mImageActionState.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_key_released_gray.png"));
                    else
                        this.mImageActionState.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_key_pressed_gray.png"));
                }
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                if (this.mDelegateCallback == null)
                    return;
                this.mDelegateCallback(this.mIndex);
            }

            [DebuggerNonUserCode]
            [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
            public void InitializeComponent()
            {
                if (this._contentLoaded)
                    return;
                this._contentLoaded = true;
                Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolmacroaction.xaml", UriKind.Relative));
            }

            [DebuggerNonUserCode]
            [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
            [EditorBrowsable(EditorBrowsableState.Never)]
            void IComponentConnector.Connect(int connectionId, object target)
            {
                switch (connectionId)
                {
                    case 1:
                        ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Click);
                        break;
                    case 2:
                        this.mLayout = (StackPanel)target;
                        break;
                    case 3:
                        this.mLabelTime = (Label)target;
                        break;
                    case 4:
                        this.mImageKey = (Image)target;
                        break;
                    case 5:
                        this.mImageActionState = (Image)target;
                        break;
                    case 6:
                        this.mLabelActionDesc = (Label)target;
                        break;
                    default:
                        this._contentLoaded = true;
                        break;
                }
            }
        }
    }
    nent();
        }
    }
}
