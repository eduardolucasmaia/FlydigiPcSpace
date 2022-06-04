// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSelectMenuColor
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFTest.UserControls
{
    public class UserControlSelectMenuColor : UserControl, IComponentConnector
    {
        internal Border mBorder;
        internal Label mLabelColor;
        internal Label mLabelRGB;
        internal Image mImage;
        private bool _contentLoaded;

        public UserControlSelectMenuColor() => this.InitializeComponent();

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

        public void setColor(byte r, byte g, byte b)
        {
            this.mLabelColor.Background = (Brush)new SolidColorBrush(Color.FromRgb(r, g, b));
            this.mLabelRGB.Content = (object)("#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2"));
        }

        public List<int> getColor()
        {
            List<int> color = new List<int>();
            byte r = ((SolidColorBrush)this.mLabelColor.Background).Color.R;
            byte g = ((SolidColorBrush)this.mLabelColor.Background).Color.G;
            byte b = ((SolidColorBrush)this.mLabelColor.Background).Color.B;
            color.Add((int)r);
            color.Add((int)g);
            color.Add((int)b);
            return color;
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolselectmenucolor.xaml", UriKind.Relative));
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
                    this.mLabelColor = (Label)target;
                    break;
                case 3:
                    this.mLabelRGB = (Label)target;
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
