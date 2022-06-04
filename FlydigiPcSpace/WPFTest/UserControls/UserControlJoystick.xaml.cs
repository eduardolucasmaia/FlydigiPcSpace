// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlJoystick
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFTest.UserControls
{
    public class UserControlJoystick : UserControl, IComponentConnector
    {
        private int MaxDataValue = 254;
        private int MaxUIValue = 260;
        private double PointHalf = 10.0;
        internal Image mImageBg;
        internal Line mLine1;
        internal Ellipse mBlueCircle;
        internal Image mPoint;
        private bool _contentLoaded;

        public UserControlJoystick() => this.InitializeComponent();

        public void setData(int x, int y)
        {
            int pos1 = (int)Math.Sqrt((double)(Math.Abs(x - 128) * Math.Abs(x - 128) + Math.Abs(y - 128) * Math.Abs(y - 128)));
            if (pos1 > (int)sbyte.MaxValue)
                pos1 = (int)sbyte.MaxValue;
            else if (pos1 < 10)
                pos1 = 0;
            this.mBlueCircle.Width = this.getUIPosition(pos1) * 2.0;
            this.mBlueCircle.Height = this.getUIPosition(pos1) * 2.0;
            Canvas.SetLeft((UIElement)this.mBlueCircle, ((double)this.MaxUIValue - this.mBlueCircle.Width) / 2.0);
            Canvas.SetTop((UIElement)this.mBlueCircle, ((double)this.MaxUIValue - this.mBlueCircle.Height) / 2.0);
            double num1 = Math.Atan2((double)(128 - y), (double)(x - 128));
            int num2 = (int)((double)pos1 * Math.Cos(num1));
            int num3 = (int)((double)pos1 * Math.Sin(num1));
            int pos2 = (int)sbyte.MaxValue + num2;
            int pos3 = (int)sbyte.MaxValue - num3;
            Canvas.SetLeft((UIElement)this.mPoint, this.getUIPosition(pos2) - this.PointHalf);
            Canvas.SetTop((UIElement)this.mPoint, this.getUIPosition(pos3) - this.PointHalf);
            this.mLine1.X1 = 130.0;
            this.mLine1.Y1 = 130.0;
            this.mLine1.X2 = this.getUIPosition(pos2);
            this.mLine1.Y2 = this.getUIPosition(pos3);
        }

        private double getUIPositionFormCenter(int pos) => (double)(int)((double)this.MaxUIValue * ((double)pos / (double)this.MaxDataValue));

        private double getUIPosition(int pos) => (double)(int)((double)this.MaxUIValue * ((double)pos / (double)this.MaxDataValue));

        public void setBgImgToBlue() => this.mImageBg.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/bg_joystick_blue.png"));

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontroljoystick.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.mImageBg = (Image)target;
                    break;
                case 2:
                    this.mLine1 = (Line)target;
                    break;
                case 3:
                    this.mBlueCircle = (Ellipse)target;
                    break;
                case 4:
                    this.mPoint = (Image)target;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }
    }
}
