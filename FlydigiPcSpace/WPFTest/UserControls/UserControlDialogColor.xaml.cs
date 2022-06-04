// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlDialogColor
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using ApexSpace.data;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public partial class UserControlDialogColor : UserControl, IComponentConnector
    {
        private UserControlSelectMenuColor mSelectColor;
        private LedBean mLed;
        private int mCurrentColorIndex;
        private IDelegateCallback mIDelegateCallback;
        //internal Slider mSlider;
        //internal TextBox mLabelRGB_text;
        //internal TextBox mLabelR_text;
        //internal TextBox mLabelG_text;
        //internal TextBox mLabelB_text;
        //internal Button DefaultColor1;
        //internal Button DefaultColor2;
        //internal Button DefaultColor3;
        //internal Button DefaultColor4;
        //internal Button DefaultColor5;
        //internal Button DefaultColor6;
        //internal Button DefaultColor7;
        //private bool _contentLoaded;

        public UserControlDialogColor() => this.InitializeComponent();

        public void setData(
          UserControlSelectMenuColor color,
          int colorIndex,
          LedBean led,
          IDelegateCallback delegateCallback)
        {
            FLog.d("setData:");
            this.mIDelegateCallback = delegateCallback;
            this.mSelectColor = color;
            this.mCurrentColorIndex = colorIndex;
            this.mLed = led;
            List<int> color1 = this.mSelectColor.getColor();
            this.mSlider.Value = (double)CommonUtils.colorRGBtoHSB(color1[0], color1[1], color1[2])[0];
        }

        private void DefaultColor_Click_1(object sender, RoutedEventArgs e) => this.setColor(this.DefaultColor1);

        private void DefaultColor_Click_2(object sender, RoutedEventArgs e) => this.setColor(this.DefaultColor2);

        private void DefaultColor_Click_3(object sender, RoutedEventArgs e) => this.setColor(this.DefaultColor3);

        private void DefaultColor_Click_4(object sender, RoutedEventArgs e) => this.setColor(this.DefaultColor4);

        private void DefaultColor_Click_5(object sender, RoutedEventArgs e) => this.setColor(this.DefaultColor5);

        private void DefaultColor_Click_6(object sender, RoutedEventArgs e) => this.setColor(this.DefaultColor6);

        private void DefaultColor_Click_7(object sender, RoutedEventArgs e) => this.setColor(this.DefaultColor7);

        private void setColor(Button button)
        {
            byte r = ((SolidColorBrush)button.Background).Color.R;
            byte g = ((SolidColorBrush)button.Background).Color.G;
            byte b = ((SolidColorBrush)button.Background).Color.B;
            FLog.d(r.ToString() + "," + g.ToString() + "," + b.ToString());
            this.setSelectColor(r, g, b);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FLog.d("hub::::" + this.mSlider.Value.ToString());
            List<int> intList = CommonUtils.colorHSBtoRGB((float)this.mSlider.Value / 360f, 1f, 1f);
            byte r = (byte)intList[0];
            byte g = (byte)intList[1];
            byte b = (byte)intList[2];
            FLog.d("r1:" + r.ToString());
            FLog.d("g1:" + g.ToString());
            FLog.d("b1:" + b.ToString());
            this.setSelectColor(r, g, b);
            this.mLabelRGB_text.Text = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
            this.mLabelR_text.Text = r.ToString() ?? "";
            this.mLabelG_text.Text = g.ToString() ?? "";
            this.mLabelB_text.Text = b.ToString() ?? "";
        }

        private void setSelectColor(byte r, byte g, byte b)
        {
            this.mSelectColor.setColor(r, g, b);
            if (this.mCurrentColorIndex == 0)
            {
                this.mLed.RgbColor0.Clear();
                this.mLed.RgbColor0.Add((int)r);
                this.mLed.RgbColor0.Add((int)g);
                this.mLed.RgbColor0.Add((int)b);
            }
            else if (this.mCurrentColorIndex == 1)
            {
                this.mLed.RgbColor1.Clear();
                this.mLed.RgbColor1.Add((int)r);
                this.mLed.RgbColor1.Add((int)g);
                this.mLed.RgbColor1.Add((int)b);
            }
            if (this.mIDelegateCallback == null)
                return;
            this.mIDelegateCallback(0);
        }

        private void Text_valueChange(object sender, KeyEventArgs e)
        {
            if (this.mLabelRGB_text.Text.Length >= 8)
                this.mLabelRGB_text.Text = this.mLabelRGB_text.Text.Substring(0, 7);
            this.mLabelRGB_text.Text = Regex.Replace(this.mLabelRGB_text.Text, "[G-Z]", "F");
            if (this.mLabelRGB_text.Text.Length == 0)
                this.mLabelRGB_text.Text = "#";
            this.mLabelRGB_text.SelectionStart = this.mLabelRGB_text.Text.Length;
            if (this.mLabelRGB_text.Text.Length == 1)
                return;
            if (this.mLabelRGB_text.Text.LastIndexOf("#") < 0)
                this.mLabelRGB_text.Text = "#" + this.mLabelRGB_text.Text;
            if (this.mLabelRGB_text.Text.LastIndexOf("#") > 0)
            {
                this.mLabelRGB_text.Text = Regex.Replace(this.mLabelRGB_text.Text, "[#]", "");
                this.mLabelRGB_text.Text = "#" + this.mLabelRGB_text.Text;
            }
            byte r = 0;
            byte g = 0;
            byte b = 0;
            try
            {
                System.Drawing.Color color = ColorTranslator.FromHtml(this.mLabelRGB_text.Text);
                r = Convert.ToByte(color.R);
                g = Convert.ToByte(color.G);
                b = Convert.ToByte(color.B);
            }
            catch
            {
            }
            this.setSelectColor(r, g, b);
            this.mLabelR_text.Text = r.ToString() ?? "";
            this.mLabelG_text.Text = g.ToString() ?? "";
            this.mLabelB_text.Text = b.ToString() ?? "";
        }

        private void Text_r_valueChange(object sender, KeyEventArgs e)
        {
            if (this.mLabelB_text.Text == "")
                this.mLabelB_text.Text = "0";
            short val = 0;
            try
            {
                val = Convert.ToInt16(this.mLabelR_text.Text);
            }
            catch
            {
            }
            this.mLabelR_text.Text = Convert.ToString(this.checkValidate(val));
            this.switchRGBandSetColor(this.mLabelR_text.Text, this.mLabelG_text.Text, this.mLabelB_text.Text);
        }

        private void Text_g_valueChange(object sender, KeyEventArgs e)
        {
            if (this.mLabelB_text.Text == "")
                this.mLabelB_text.Text = "0";
            short val = 0;
            try
            {
                val = Convert.ToInt16(this.mLabelG_text.Text);
            }
            catch
            {
            }
            this.mLabelG_text.Text = Convert.ToString(this.checkValidate(val));
            this.switchRGBandSetColor(this.mLabelR_text.Text, this.mLabelG_text.Text, this.mLabelB_text.Text);
        }

        private void Text_b_valueChange(object sender, KeyEventArgs e)
        {
            if (this.mLabelB_text.Text == "")
                this.mLabelB_text.Text = "0";
            short val = 0;
            try
            {
                val = Convert.ToInt16(this.mLabelB_text.Text);
            }
            catch
            {
            }
            this.mLabelB_text.Text = Convert.ToString(this.checkValidate(val));
            this.switchRGBandSetColor(this.mLabelR_text.Text, this.mLabelG_text.Text, this.mLabelB_text.Text);
        }

        private void TextValidata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
                e.Handled = false;
            else if (e.Key >= Key.A && e.Key <= Key.F)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void TextValidata_rgb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private short checkValidate(short val) => val > (short)byte.MaxValue ? (short)byte.MaxValue : val;

        private void switchRGBandSetColor(string r1, string g1, string b1)
        {
            byte r = Convert.ToByte(r1);
            byte g = Convert.ToByte(g1);
            byte b = Convert.ToByte(b1);
            this.setSelectColor(r, g, b);
            this.mLabelRGB_text.Text = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontroldialogcolor.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            this.mSlider = (Slider)target;
        //            this.mSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
        //            break;
        //        case 2:
        //            this.mLabelRGB_text = (TextBox)target;
        //            this.mLabelRGB_text.KeyUp += new KeyEventHandler(this.Text_valueChange);
        //            this.mLabelRGB_text.KeyDown += new KeyEventHandler(this.TextValidata_KeyDown);
        //            break;
        //        case 3:
        //            this.mLabelR_text = (TextBox)target;
        //            this.mLabelR_text.KeyUp += new KeyEventHandler(this.Text_r_valueChange);
        //            this.mLabelR_text.KeyDown += new KeyEventHandler(this.TextValidata_rgb_KeyDown);
        //            break;
        //        case 4:
        //            this.mLabelG_text = (TextBox)target;
        //            this.mLabelG_text.KeyUp += new KeyEventHandler(this.Text_g_valueChange);
        //            this.mLabelG_text.KeyDown += new KeyEventHandler(this.TextValidata_rgb_KeyDown);
        //            break;
        //        case 5:
        //            this.mLabelB_text = (TextBox)target;
        //            this.mLabelB_text.KeyUp += new KeyEventHandler(this.Text_b_valueChange);
        //            this.mLabelB_text.KeyDown += new KeyEventHandler(this.TextValidata_rgb_KeyDown);
        //            break;
        //        case 6:
        //            this.DefaultColor1 = (Button)target;
        //            this.DefaultColor1.Click += new RoutedEventHandler(this.DefaultColor_Click_1);
        //            break;
        //        case 7:
        //            this.DefaultColor2 = (Button)target;
        //            this.DefaultColor2.Click += new RoutedEventHandler(this.DefaultColor_Click_2);
        //            break;
        //        case 8:
        //            this.DefaultColor3 = (Button)target;
        //            this.DefaultColor3.Click += new RoutedEventHandler(this.DefaultColor_Click_3);
        //            break;
        //        case 9:
        //            this.DefaultColor4 = (Button)target;
        //            this.DefaultColor4.Click += new RoutedEventHandler(this.DefaultColor_Click_4);
        //            break;
        //        case 10:
        //            this.DefaultColor5 = (Button)target;
        //            this.DefaultColor5.Click += new RoutedEventHandler(this.DefaultColor_Click_5);
        //            break;
        //        case 11:
        //            this.DefaultColor6 = (Button)target;
        //            this.DefaultColor6.Click += new RoutedEventHandler(this.DefaultColor_Click_6);
        //            break;
        //        case 12:
        //            this.DefaultColor7 = (Button)target;
        //            this.DefaultColor7.Click += new RoutedEventHandler(this.DefaultColor_Click_7);
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
