// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlLineJoystickSetting
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace.data;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public partial class UserControlLineJoystickSetting : UserControl, IComponentConnector
    {
        private int MaxDataValue = (int)sbyte.MaxValue;
        private int MaxUIValue;
        private double ZerotHalf;
        private double PointHalf;
        private OneJoystickBean mOneJoystickBean;
        private bool isFirstRefresh;
        private IDelegateCallback mIDelegateCallback;
        //internal Label mButtonDefault;
        //internal Label mButtonFast;
        //internal Label mButtonDelay;
        //internal Canvas mCanvas;
        //internal Line mLine1;
        //internal Line mLine2;
        //internal Line mLine3;
        //internal Thumb mZero;
        //internal Thumb mPoint1;
        //internal Thumb mPoint2;
       //private bool _contentLoaded;

        public UserControlLineJoystickSetting()
        {
            this.InitializeComponent();
            this.MaxUIValue = (int)this.mCanvas.Width;
            this.ZerotHalf = (double)(int)(this.mZero.Width / 2.0);
            this.PointHalf = (double)(int)(this.mPoint1.Width / 2.0);
        }

        public void setData(OneJoystickBean oneJoystickBean, IDelegateCallback delegateCallback)
        {
            this.mOneJoystickBean = oneJoystickBean;
            this.mIDelegateCallback = delegateCallback;
            FLog.d("setData:" + JsonConvert.SerializeObject((object)this.mOneJoystickBean));
            this.setUI();
        }

        private void setUI()
        {
            if (this.mOneJoystickBean == null)
                return;
            Canvas.SetLeft((UIElement)this.mZero, this.getUIPosition(this.mOneJoystickBean.Zero) - this.ZerotHalf);
            Canvas.SetLeft((UIElement)this.mPoint1, this.getUIPosition(this.mOneJoystickBean.Point0_X) - this.PointHalf);
            Canvas.SetBottom((UIElement)this.mPoint1, this.getUIPosition(this.mOneJoystickBean.Point0_Y) - this.PointHalf);
            Canvas.SetLeft((UIElement)this.mPoint2, this.getUIPosition(this.mOneJoystickBean.Point1_X) - this.PointHalf);
            Canvas.SetBottom((UIElement)this.mPoint2, this.getUIPosition(this.mOneJoystickBean.Point1_Y) - this.PointHalf);
            this.refreshLine();
        }

        private void mZero_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double num1 = Canvas.GetLeft((UIElement)this.mZero) + e.HorizontalChange + this.ZerotHalf;
            double num2 = Canvas.GetLeft((UIElement)this.mPoint1) + this.PointHalf;
            if (num1 < 0.0)
                num1 = 0.0;
            else if (num1 > num2)
                num1 = num2;
            Canvas.SetLeft((UIElement)this.mZero, num1 - this.ZerotHalf);
            this.refreshLine();
        }

        private void mPoint1_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double num1 = Canvas.GetLeft((UIElement)this.mPoint1) + e.HorizontalChange + this.PointHalf;
            double num2 = Canvas.GetBottom((UIElement)this.mPoint1) - e.VerticalChange + this.PointHalf;
            double num3 = Canvas.GetLeft((UIElement)this.mZero) + this.ZerotHalf;
            double num4 = Canvas.GetLeft((UIElement)this.mPoint2) + this.PointHalf;
            double num5 = Canvas.GetBottom((UIElement)this.mPoint2) + this.PointHalf;
            if (num1 < num3)
                num1 = num3;
            else if (num1 > num4)
                num1 = num4;
            if (num2 < 0.0)
                num2 = 0.0;
            else if (num2 > num5)
                num2 = num5;
            Canvas.SetLeft((UIElement)this.mPoint1, num1 - this.PointHalf);
            Canvas.SetBottom((UIElement)this.mPoint1, num2 - this.PointHalf);
            this.refreshLine();
        }

        private void mPoint2_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double num1 = Canvas.GetLeft((UIElement)this.mPoint2) + e.HorizontalChange + this.PointHalf;
            double num2 = Canvas.GetBottom((UIElement)this.mPoint2) - e.VerticalChange + this.PointHalf;
            double num3 = Canvas.GetLeft((UIElement)this.mPoint1) + this.PointHalf;
            double num4 = Canvas.GetBottom((UIElement)this.mPoint1) + this.PointHalf;
            if (num1 < num3)
                num1 = num3;
            else if (num1 > (double)this.MaxUIValue)
                num1 = (double)this.MaxUIValue;
            if (num2 < num4)
                num2 = num4;
            else if (num2 > (double)this.MaxUIValue)
                num2 = (double)this.MaxUIValue;
            Canvas.SetLeft((UIElement)this.mPoint2, num1 - this.PointHalf);
            Canvas.SetBottom((UIElement)this.mPoint2, num2 - this.PointHalf);
            this.refreshLine();
        }

        private void refreshLine()
        {
            this.mLine1.X1 = Canvas.GetLeft((UIElement)this.mZero) + this.ZerotHalf;
            this.mLine1.Y1 = this.convertBottom2Top(0.0);
            this.mLine1.X2 = Canvas.GetLeft((UIElement)this.mPoint1) + this.PointHalf;
            this.mLine1.Y2 = this.convertBottom2Top(Canvas.GetBottom((UIElement)this.mPoint1) + this.PointHalf);
            this.mLine2.X1 = Canvas.GetLeft((UIElement)this.mPoint1) + this.PointHalf;
            this.mLine2.Y1 = this.convertBottom2Top(Canvas.GetBottom((UIElement)this.mPoint1) + this.PointHalf);
            this.mLine2.X2 = Canvas.GetLeft((UIElement)this.mPoint2) + this.PointHalf;
            this.mLine2.Y2 = this.convertBottom2Top(Canvas.GetBottom((UIElement)this.mPoint2) + this.PointHalf);
            this.mLine3.X1 = Canvas.GetLeft((UIElement)this.mPoint2) + this.PointHalf;
            this.mLine3.Y1 = this.convertBottom2Top(Canvas.GetBottom((UIElement)this.mPoint2) + this.PointHalf);
            this.mLine3.X2 = (double)this.MaxUIValue;
            this.mLine3.Y2 = this.convertBottom2Top((double)this.MaxUIValue);
            if (!this.isFirstRefresh)
            {
                this.isFirstRefresh = true;
            }
            else
            {
                this.mOneJoystickBean.Zero = this.getConfigPosition(Canvas.GetLeft((UIElement)this.mZero) + this.ZerotHalf);
                this.mOneJoystickBean.Point0_X = this.getConfigPosition(Canvas.GetLeft((UIElement)this.mPoint1) + this.PointHalf);
                this.mOneJoystickBean.Point0_Y = this.getConfigPosition(Canvas.GetBottom((UIElement)this.mPoint1) + this.PointHalf);
                this.mOneJoystickBean.Point1_X = this.getConfigPosition(Canvas.GetLeft((UIElement)this.mPoint2) + this.PointHalf);
                this.mOneJoystickBean.Point1_Y = this.getConfigPosition(Canvas.GetBottom((UIElement)this.mPoint2) + this.PointHalf);
            }
        }

        private double getUIPosition(int pos) => (double)(int)((double)this.MaxUIValue * ((double)pos / (double)this.MaxDataValue));

        private int getConfigPosition(double pos) => (int)(pos / (double)this.MaxUIValue * (double)this.MaxDataValue);

        private double convertBottom2Top(double bottom) => (double)this.MaxUIValue - bottom;

        private void resetMode()
        {
            this.mButtonDefault.BorderThickness = new Thickness(0.0);
            this.mButtonDefault.Background = (Brush)new BrushConverter().ConvertFrom((object)"#330074ff");
            this.mButtonDefault.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonFast.BorderThickness = new Thickness(0.0);
            this.mButtonFast.Background = (Brush)new BrushConverter().ConvertFrom((object)"#330074ff");
            this.mButtonFast.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonDelay.BorderThickness = new Thickness(0.0);
            this.mButtonDelay.Background = (Brush)new BrushConverter().ConvertFrom((object)"#330074ff");
            this.mButtonDelay.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
        }

        private void Button_Click_Default(object sender, MouseButtonEventArgs e)
        {
            this.resetMode();
            this.mButtonDefault.BorderThickness = new Thickness(1.0);
            this.mButtonDefault.Background = (Brush)new BrushConverter().ConvertFrom((object)"#4c0074ff");
            this.mButtonDefault.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mOneJoystickBean.Zero = 0;
            this.mOneJoystickBean.Point0_X = 63;
            this.mOneJoystickBean.Point0_Y = 63;
            this.mOneJoystickBean.Point1_X = (int)sbyte.MaxValue;
            this.mOneJoystickBean.Point1_Y = (int)sbyte.MaxValue;
            this.setUI();
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        private void Button_Click_Fast(object sender, MouseButtonEventArgs e)
        {
            this.resetMode();
            this.mButtonFast.BorderThickness = new Thickness(1.0);
            this.mButtonFast.Background = (Brush)new BrushConverter().ConvertFrom((object)"#4c0074ff");
            this.mButtonFast.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mOneJoystickBean.Zero = 20;
            this.mOneJoystickBean.Point0_X = 63;
            this.mOneJoystickBean.Point0_Y = 96;
            this.mOneJoystickBean.Point1_X = (int)sbyte.MaxValue;
            this.mOneJoystickBean.Point1_Y = (int)sbyte.MaxValue;
            this.setUI();
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        private void Button_Click_Delay(object sender, MouseButtonEventArgs e)
        {
            this.resetMode();
            this.mButtonDelay.BorderThickness = new Thickness(1.0);
            this.mButtonDelay.Background = (Brush)new BrushConverter().ConvertFrom((object)"#4c0074ff");
            this.mButtonDelay.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mOneJoystickBean.Zero = 20;
            this.mOneJoystickBean.Point0_X = 63;
            this.mOneJoystickBean.Point0_Y = 32;
            this.mOneJoystickBean.Point1_X = (int)sbyte.MaxValue;
            this.mOneJoystickBean.Point1_Y = (int)sbyte.MaxValue;
            this.setUI();
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        private void mZero_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        private void mPoint1_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        private void mPoint2_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrollinejoysticksetting.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mButtonDefault = (Label)target;
        //            this.mButtonDefault.MouseLeftButtonDown += new MouseButtonEventHandler(this.Button_Click_Default);
        //            break;
        //        case 2:
        //            this.mButtonFast = (Label)target;
        //            this.mButtonFast.MouseLeftButtonDown += new MouseButtonEventHandler(this.Button_Click_Fast);
        //            break;
        //        case 3:
        //            this.mButtonDelay = (Label)target;
        //            this.mButtonDelay.MouseLeftButtonDown += new MouseButtonEventHandler(this.Button_Click_Delay);
        //            break;
        //        case 4:
        //            this.mCanvas = (Canvas)target;
        //            break;
        //        case 5:
        //            this.mLine1 = (Line)target;
        //            break;
        //        case 6:
        //            this.mLine2 = (Line)target;
        //            break;
        //        case 7:
        //            this.mLine3 = (Line)target;
        //            break;
        //        case 8:
        //            this.mZero = (Thumb)target;
        //            this.mZero.DragDelta += new DragDeltaEventHandler(this.mZero_DragDelta);
        //            this.mZero.DragCompleted += new DragCompletedEventHandler(this.mZero_DragCompleted);
        //            break;
        //        case 9:
        //            this.mPoint1 = (Thumb)target;
        //            this.mPoint1.DragDelta += new DragDeltaEventHandler(this.mPoint1_DragDelta);
        //            this.mPoint1.DragCompleted += new DragCompletedEventHandler(this.mPoint1_DragCompleted);
        //            break;
        //        case 10:
        //            this.mPoint2 = (Thumb)target;
        //            this.mPoint2.DragDelta += new DragDeltaEventHandler(this.mPoint2_DragDelta);
        //            this.mPoint2.DragCompleted += new DragCompletedEventHandler(this.mPoint2_DragCompleted);
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
