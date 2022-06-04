// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSelectMenu
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace.Utils;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFTest.UserControls
{
    public class UserControlSelectMenu : UserControl, IComponentConnector
    {
        internal Border mBorder;
        internal Label mLabel;
        internal Image mKeyIcon;
        internal Image mImage;
        private bool _contentLoaded;

        public UserControlSelectMenu() => this.InitializeComponent();

        public void setSelected(bool selected)
        {
            if (selected)
            {
                this.mBorder.BorderBrush = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
                this.mImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_arrow_up_blue.png"));
            }
            else
            {
                this.mBorder.BorderBrush = (Brush)new BrushConverter().ConvertFrom((object)"#26ffffff");
                this.mImage.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_arrow_down_white.png"));
            }
        }

        public void setName(string name)
        {
            if (name.Equals(DeviceUtils.getKeyNameByID(3)))
            {
                this.mKeyIcon.Visibility = Visibility.Visible;
                this.mLabel.Visibility = Visibility.Hidden;
                this.mKeyIcon.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(3, false)));
            }
            else if (name.Equals(DeviceUtils.getKeyNameByID(1)))
            {
                this.mKeyIcon.Visibility = Visibility.Visible;
                this.mLabel.Visibility = Visibility.Hidden;
                this.mKeyIcon.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(1, false)));
            }
            else if (name.Equals(DeviceUtils.getKeyNameByID(0)))
            {
                this.mKeyIcon.Visibility = Visibility.Visible;
                this.mLabel.Visibility = Visibility.Hidden;
                this.mKeyIcon.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(0, false)));
            }
            else if (name.Equals(DeviceUtils.getKeyNameByID(2)))
            {
                this.mKeyIcon.Visibility = Visibility.Visible;
                this.mLabel.Visibility = Visibility.Hidden;
                this.mKeyIcon.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(2, false)));
            }
            else
            {
                this.mKeyIcon.Visibility = Visibility.Hidden;
                this.mLabel.Visibility = Visibility.Visible;
                this.mLabel.Content = (object)name;
            }
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolselectmenu.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.mBorder = (Border)target;
                    break;
                case 2:
                    this.mLabel = (Label)target;
                    break;
                case 3:
                    this.mKeyIcon = (Image)target;
                    break;
                case 4:
                    this.mImage = (Image)target;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }
    }
}
