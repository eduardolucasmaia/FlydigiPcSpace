// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlDialogKey
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public partial class UserControlDialogKey : UserControl, IComponentConnector
    {
        private IDelegateCallback mDelegateCallback;
        private int[] mArrayKey = new int[16]
        {
      4,
      5,
      7,
      8,
      0,
      2,
      3,
      1,
      12,
      13,
      10,
      11,
      6,
      9,
      14,
      15
        };
        private int[] mArrayKey_A2 = new int[24]
        {
      4,
      5,
      7,
      8,
      0,
      2,
      3,
      1,
      12,
      13,
      10,
      11,
      6,
      9,
      14,
      15,
      18,
      19,
      20,
      21,
      22,
      23,
      16,
      17
        };
        private int[] mArrayKey_F1 = new int[22]
        {
      4,
      5,
      7,
      8,
      0,
      2,
      3,
      1,
      12,
      13,
      10,
      11,
      6,
      9,
      14,
      15,
      18,
      19,
      20,
      21,
      16,
      17
        };
        private int[] mSpecialKey = new int[8]
        {
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
        };
        //internal Border mSelectMenuKey;
        //internal WrapPanel mWrapPanel;
        private bool _contentLoaded;

        public UserControlDialogKey() => this.InitializeComponent();

        public void setDelegate(IDelegateCallback delegateCallback) => this.mDelegateCallback = delegateCallback;

        public void setListKey(List<int> mList, int type)
        {
            this.mWrapPanel.Children.Clear();
            List<int> intList = new List<int>();
            FLog.d("当前选择的类型是：" + type.ToString());
            switch (type)
            {
                case 1:
                    for (int index = 0; index < this.mArrayKey_A2.Length; ++index)
                        intList.Add(this.mArrayKey_A2[index]);
                    break;
                case 2:
                    for (int index = 0; index < this.mArrayKey_F1.Length; ++index)
                        intList.Add(this.mArrayKey_F1[index]);
                    break;
                default:
                    for (int index = 0; index < this.mArrayKey.Length; ++index)
                        intList.Add(this.mArrayKey[index]);
                    break;
            }
            for (int index = 0; index < intList.Count; ++index)
            {
                if (!mList.Contains(intList[index]))
                {
                    Button element = new Button();
                    element.Style = (Style)this.FindResource((object)"StyleDialogKeyItem");
                    element.Tag = (object)intList[index];
                    element.Click += new RoutedEventHandler(this.Button_Click);
                    if (intList[index] == 3 || intList[index] == 1 || intList[index] == 0 || intList[index] == 2)
                    {
                        Image image = new Image();
                        image.Width = 10.0;
                        image.Height = 10.0;
                        string specialKeyIconById = DeviceUtils.getSpecialKeyIconByID(intList[index], false);
                        image.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + specialKeyIconById));
                        element.Content = (object)image;
                    }
                    else
                        element.Content = (object)DeviceUtils.getKeyNameByID(intList[index]);
                    element.MouseEnter += new MouseEventHandler(this.Button_MouseEnter);
                    element.MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
                    this.mWrapPanel.Children.Add((UIElement)element);
                }
            }
        }

        public void setListKey(List<int> mList, int type, int currentSelectId)
        {
            this.mWrapPanel.Children.Clear();
            List<int> intList = new List<int>();
            switch (type)
            {
                case 1:
                    for (int index = 0; index < this.mArrayKey_A2.Length; ++index)
                        intList.Add(this.mArrayKey_A2[index]);
                    break;
                case 2:
                    for (int index = 0; index < this.mArrayKey_F1.Length; ++index)
                        intList.Add(this.mArrayKey_F1[index]);
                    break;
                default:
                    for (int index = 0; index < this.mArrayKey.Length; ++index)
                        intList.Add(this.mArrayKey[index]);
                    if (currentSelectId >= 16 && currentSelectId <= 23)
                    {
                        intList.Add(this.mSpecialKey[currentSelectId - 16]);
                        break;
                    }
                    break;
            }
            for (int index = 0; index < intList.Count; ++index)
            {
                if (!mList.Contains(intList[index]))
                {
                    Button element = new Button();
                    element.Style = (Style)this.FindResource((object)"StyleDialogKeyItem");
                    element.Tag = (object)intList[index];
                    element.Click += new RoutedEventHandler(this.Button_Click);
                    if (intList[index] == 3 || intList[index] == 1 || intList[index] == 0 || intList[index] == 2)
                    {
                        Image image = new Image();
                        image.Width = 10.0;
                        image.Height = 10.0;
                        string specialKeyIconById = DeviceUtils.getSpecialKeyIconByID(intList[index], false);
                        image.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + specialKeyIconById));
                        element.Content = (object)image;
                    }
                    else
                        element.Content = (object)DeviceUtils.getKeyNameByID(intList[index]);
                    element.MouseEnter += new MouseEventHandler(this.Button_MouseEnter);
                    element.MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
                    this.mWrapPanel.Children.Add((UIElement)element);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) => this.selected((int)(sender as Button).Tag);

        private void selected(int id)
        {
            if (this.mDelegateCallback == null)
                return;
            this.mDelegateCallback(id);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            button.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            int tag = (int)button.Tag;
            switch (tag)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    (button.Content as Image).Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(tag, true)));
                    break;
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            button.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            int tag = (int)button.Tag;
            switch (tag)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    (button.Content as Image).Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(tag, false)));
                    break;
            }
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontroldialogkey.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    if (connectionId != 1)
        //    {
        //        if (connectionId == 2)
        //            this.mWrapPanel = (WrapPanel)target;
        //        else
        //            this._contentLoaded = true;
        //    }
        //    else
        //        this.mSelectMenuKey = (Border)target;
        //}
    }
}
