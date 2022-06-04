using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace WPFTest.UserControls
{
	public class Arc : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class ArcClass
		{
			public static readonly Arc.ArcClass ArcProperty = new Arc.ArcClass();

			internal void ArcMethod(DependencyObject s, DependencyPropertyChangedEventArgs e)
			{
				Arc arc = s as Arc;
				if (arc != null && e.NewValue.ToString() != e.OldValue.ToString())
				{
					arc.InvalidateVisual();
				}
			}
		}

		public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(Geometry), typeof(Arc), new PropertyMetadata(Geometry.Parse(""), new PropertyChangedCallback(Arc.ArcClass.ArcProperty.ArcMethod)));

		public static readonly DependencyProperty RectProperty = DependencyProperty.Register("Rect", typeof(Rect), typeof(Arc), new PropertyMetadata(default(Rect), new PropertyChangedCallback(Arc.propertyChangedCallback)));

		public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register("StartAngle", typeof(double), typeof(Arc), new PropertyMetadata(0.0, new PropertyChangedCallback(Arc.propertyChangedCallback)));

		public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register("EndAngle", typeof(double), typeof(Arc), new PropertyMetadata(0.0, new PropertyChangedCallback(Arc.propertyChangedCallback)));

		//private bool _contentLoaded;

		private Geometry Data
		{
			get
			{
				return (Geometry)base.GetValue(Arc.DataProperty);
			}
			set
			{
				base.SetValue(Arc.DataProperty, value);
			}
		}

		public Rect Rect
		{
			get
			{
				return (Rect)base.GetValue(Arc.RectProperty);
			}
			set
			{
				base.SetValue(Arc.RectProperty, value);
			}
		}

		public double StartAngle
		{
			get
			{
				return (double)base.GetValue(Arc.StartAngleProperty);
			}
			set
			{
				base.SetValue(Arc.StartAngleProperty, value);
			}
		}

		public double EndAngle
		{
			get
			{
				return (double)base.GetValue(Arc.EndAngleProperty);
			}
			set
			{
				base.SetValue(Arc.EndAngleProperty, value);
			}
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

		private static double GetRadian(double angle)
		{
			return angle / 180.0 * 3.1415926535897931;
		}
	}
}
