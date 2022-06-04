// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.Arc
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

namespace WPFTest.UserControls
{
    public partial class Arc : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Geometry), typeof(Arc), new PropertyMetadata((object)Geometry.Parse(""), (PropertyChangedCallback)((s, e) =>
        {
            if (!(s is Arc arc2) || !(e.NewValue.ToString() != e.OldValue.ToString()))
                return;
            arc2.InvalidateVisual();
        })));
        public static readonly DependencyProperty RectProperty = DependencyProperty.Register(nameof(Rect), typeof(Rect), typeof(Arc), new PropertyMetadata((object)new Rect(), new PropertyChangedCallback(Arc.propertyChangedCallback)));
        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(Arc), new PropertyMetadata((object)0.0, new PropertyChangedCallback(Arc.propertyChangedCallback)));
        public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(Arc), new PropertyMetadata((object)0.0, new PropertyChangedCallback(Arc.propertyChangedCallback)));
        //private bool _contentLoaded;

        private Geometry Data
        {
            get => (Geometry)this.GetValue(Arc.DataProperty);
            set => this.SetValue(Arc.DataProperty, (object)value);
        }

        public Rect Rect
        {
            get => (Rect)this.GetValue(Arc.RectProperty);
            set => this.SetValue(Arc.RectProperty, (object)value);
        }

        public double StartAngle
        {
            get => (double)this.GetValue(Arc.StartAngleProperty);
            set => this.SetValue(Arc.StartAngleProperty, (object)value);
        }

        public double EndAngle
        {
            get => (double)this.GetValue(Arc.EndAngleProperty);
            set => this.SetValue(Arc.EndAngleProperty, (object)value);
        }

        private static void propertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Arc arc = d as Arc;
            if (arc != null)
            {
                if (arc.Rect.Width > 0.0 && arc.Rect.Height > 0.0 && arc.StartAngle != arc.EndAngle)
                {
                    double num = arc.Rect.Width / 2.0;
                    double num2 = arc.Rect.Height / 2.0;
                    if (Math.Abs(arc.StartAngle - arc.EndAngle) >= 360.0)
                    {
                        string source = string.Format("M{0},{1} A{2},{3} 0,1,1 {4},{5}z", new object[]
                        {
                            arc.Rect.X + num * 2.0,
                            arc.Rect.Y + num2 + 0.001,
                            num,
                            num2,
                            arc.Rect.X + num * 2.0,
                            arc.Rect.Y + num2
                        });
                        arc.Data = Geometry.Parse(source);
                        return;
                    }
                    bool flag = arc.EndAngle > arc.StartAngle;
                    bool flag2 = Math.Abs(arc.StartAngle - arc.EndAngle) % 360.0 >= 180.0;
                    Point point = default(Point);
                    if ((90.0 - arc.StartAngle) % 360.0 == 0.0)
                    {
                        point.X = 0.0;
                        point.Y = num2;
                    }
                    else if ((270.0 - arc.StartAngle) % 360.0 == 0.0)
                    {
                        point.X = 0.0;
                        point.Y = -num2;
                    }
                    else
                    {
                        double radian = Arc.GetRadian(arc.StartAngle);
                        point.X = num * num2 / Math.Sqrt(Math.Pow(num2, 2.0) + Math.Pow(num * Math.Tan(radian), 2.0));
                        point.X *= (double)((Math.Cos(radian) > 0.0) ? 1 : -1);
                        point.Y = point.X * Math.Tan(radian);
                    }
                    Point point2 = default(Point);
                    if ((90.0 - arc.EndAngle) % 360.0 == 0.0)
                    {
                        point2.X = 0.0;
                        point2.Y = num2;
                    }
                    else if ((270.0 - arc.EndAngle) % 360.0 == 0.0)
                    {
                        point2.X = 0.0;
                        point2.Y = -num2;
                    }
                    else
                    {
                        double radian = Arc.GetRadian(arc.EndAngle);
                        point2.X = num * num2 / Math.Sqrt(Math.Pow(num2, 2.0) + Math.Pow(num * Math.Tan(radian), 2.0));
                        point2.X *= (double)((Math.Cos(radian) > 0.0) ? 1 : -1);
                        point2.Y = point2.X * Math.Tan(radian);
                    }
                    string text = string.Format("M{0},{1} ", point.X + num + arc.Rect.X, point.Y + num2 + arc.Rect.Y);
                    text += string.Format("A{0},{1} ", num, num2);
                    text = string.Concat(new string[]
                    {
                        text,
                        "0,",
                        flag2 ? "1" : "0",
                        ",",
                        flag ? "1" : "0",
                        " "
                    });
                    text += string.Format("{0},{1}", point2.X + num + arc.Rect.X, point2.Y + num2 + arc.Rect.Y);
                    try
                    {
                        arc.Data = Geometry.Parse(text);
                        return;
                    }
                    catch
                    {
                        arc.Data = Geometry.Parse("");
                        return;
                    }
                }
                arc.Data = Geometry.Parse("");
            }
        }

        //private static void propertyChangedCallbackOld(
        //  DependencyObject d,
        //  DependencyPropertyChangedEventArgs e)
        //{
        //    if (!(d is Arc arc))
        //        return;
        //    if (arc.Rect.Width > 0.0 && arc.Rect.Height > 0.0 && arc.StartAngle != arc.EndAngle)
        //    {
        //        double num1 = arc.Rect.Width / 2.0;
        //        Rect rect = arc.Rect;
        //        double x1 = rect.Height / 2.0;
        //        if (Math.Abs(arc.StartAngle - arc.EndAngle) >= 360.0)
        //        {
        //            object[] objArray = new object[6];
        //            rect = arc.Rect;
        //            objArray[0] = (object)(rect.X + num1 * 2.0);
        //            rect = arc.Rect;
        //            objArray[1] = (object)(rect.Y + x1 + 0.001);
        //            objArray[2] = (object)num1;
        //            objArray[3] = (object)x1;
        //            rect = arc.Rect;
        //            objArray[4] = (object)(rect.X + num1 * 2.0);
        //            rect = arc.Rect;
        //            objArray[5] = (object)(rect.Y + x1);
        //            string source = string.Format("M{0},{1} A{2},{3} 0,1,1 {4},{5}z", objArray);
        //            arc.Data = Geometry.Parse(source);
        //        }
        //        else
        //        {
        //            bool flag1 = arc.EndAngle > arc.StartAngle;
        //            bool flag2 = Math.Abs(arc.StartAngle - arc.EndAngle) % 360.0 >= 180.0;
        //            Point point1 = new Point();
        //            if ((90.0 - arc.StartAngle) % 360.0 == 0.0)
        //            {
        //                point1.X = 0.0;
        //                point1.Y = x1;
        //            }
        //            else if ((270.0 - arc.StartAngle) % 360.0 == 0.0)
        //            {
        //                point1.X = 0.0;
        //                point1.Y = -x1;
        //            }
        //            else
        //            {
        //                double radian = Arc.GetRadian(arc.StartAngle);
        //                point1.X = num1 * x1 / Math.Sqrt(Math.Pow(x1, 2.0) + Math.Pow(num1 * Math.Tan(radian), 2.0));
        //                point1.X *= Math.Cos(radian) > 0.0 ? 1.0 : -1.0;
        //                point1.Y = point1.X * Math.Tan(radian);
        //            }
        //            Point point2 = new Point();
        //            if ((90.0 - arc.EndAngle) % 360.0 == 0.0)
        //            {
        //                point2.X = 0.0;
        //                point2.Y = x1;
        //            }
        //            else if ((270.0 - arc.EndAngle) % 360.0 == 0.0)
        //            {
        //                point2.X = 0.0;
        //                point2.Y = -x1;
        //            }
        //            else
        //            {
        //                double radian = Arc.GetRadian(arc.EndAngle);
        //                point2.X = num1 * x1 / Math.Sqrt(Math.Pow(x1, 2.0) + Math.Pow(num1 * Math.Tan(radian), 2.0));
        //                point2.X *= Math.Cos(radian) > 0.0 ? 1.0 : -1.0;
        //                point2.Y = point2.X * Math.Tan(radian);
        //            }
        //            double num2 = point1.X + num1;
        //            rect = arc.Rect;
        //            double x2 = rect.X;
        //            // ISSUE: variable of a boxed type
        //            __Boxed<double> local1 = (ValueType)(num2 + x2);
        //            double num3 = point1.Y + x1;
        //            rect = arc.Rect;
        //            double y1 = rect.Y;
        //            // ISSUE: variable of a boxed type
        //            __Boxed<double> local2 = (ValueType)(num3 + y1);
        //            string str1 = string.Format("M{0},{1} ", (object)local1, (object)local2) + string.Format("A{0},{1} ", (object)num1, (object)x1) + "0," + (flag2 ? "1" : "0") + "," + (flag1 ? "1" : "0") + " ";
        //            double num4 = point2.X + num1;
        //            rect = arc.Rect;
        //            double x3 = rect.X;
        //            // ISSUE: variable of a boxed type
        //            __Boxed<double> local3 = (ValueType)(num4 + x3);
        //            double num5 = point2.Y + x1;
        //            rect = arc.Rect;
        //            double y2 = rect.Y;
        //            // ISSUE: variable of a boxed type
        //            __Boxed<double> local4 = (ValueType)(num5 + y2);
        //            string str2 = string.Format("{0},{1}", (object)local3, (object)local4);
        //            string source = str1 + str2;
        //            try
        //            {
        //                arc.Data = Geometry.Parse(source);
        //            }
        //            catch
        //            {
        //                arc.Data = Geometry.Parse("");
        //            }
        //        }
        //    }
        //    else
        //        arc.Data = Geometry.Parse("");
        //}

        private static double GetRadian(double angle) => angle / 180.0 * Math.PI;

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/arc.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target) => this._contentLoaded = true;
    }
}
