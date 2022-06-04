// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlKeyMapping
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace.data;
using ApexSpace.Utils;
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
using System.Windows.Media.Imaging;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public partial class UserControlKeyMapping : UserControl, IComponentConnector
    {
        public const int FRONT = 0;
        public const int AFTER = 1;
        public const int TOP = 2;
        private int mCurrentKeyPosition;
        private int mCurrentKeyId;
        private object mTargetObj;
        private IDelegateCallbackValueThreeObject mIDelegateCallbackValueThreeObject;
        private bool mFocusEnable = true;
        //internal Label mLabelCurrent;
        //internal Image mImageCurrent;
        //internal Label mLabelTarget;
        //internal Image mImageTarget;
        //internal Image mArrow;
        private bool _contentLoaded;

        public UserControlKeyMapping() => this.InitializeComponent();

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!this.mFocusEnable)
                return;
            this.BorderThickness = new Thickness(1.0, 1.0, 1.0, 1.0);
            this.BorderBrush = (Brush)new BrushConverter().ConvertFrom((object)"#0074FF");
            if (this.mIDelegateCallbackValueThreeObject == null)
                return;
            this.mIDelegateCallbackValueThreeObject(1, this.mCurrentKeyId, this.mTargetObj);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.mFocusEnable)
                return;
            this.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
            if (this.mIDelegateCallbackValueThreeObject == null)
                return;
            this.mIDelegateCallbackValueThreeObject(0, this.mCurrentKeyId, this.mTargetObj);
        }

        public void setFocusEnable(bool focused) => this.mFocusEnable = focused;

        public void setCurrentKeyPosition(int postion) => this.mCurrentKeyPosition = postion;

        public void setCurrentKeyId(int keyId)
        {
            this.mCurrentKeyId = keyId;
            if (keyId == 3 || keyId == 1 || keyId == 0 || keyId == 2)
            {
                this.mLabelCurrent.Visibility = Visibility.Collapsed;
                this.mImageCurrent.Visibility = Visibility.Visible;
                this.mImageCurrent.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(keyId, false)));
            }
            else
            {
                this.mLabelCurrent.Visibility = Visibility.Visible;
                this.mImageCurrent.Visibility = Visibility.Collapsed;
                this.mLabelCurrent.Content = (object)DeviceUtils.getKeyNameByID(keyId);
            }
        }

        public void setTargetObj(object obj)
        {
            this.mTargetObj = obj;
            switch (obj)
            {
                case int id:
                    if (id == 3 || id == 1 || id == 0 || id == 2)
                    {
                        this.mLabelTarget.Visibility = Visibility.Collapsed;
                        this.mImageTarget.Visibility = Visibility.Visible;
                        this.mImageCurrent.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(id, false)));
                    }
                    else
                    {
                        this.mLabelTarget.Visibility = Visibility.Visible;
                        this.mImageTarget.Visibility = Visibility.Collapsed;
                        this.mLabelTarget.Content = (object)DeviceUtils.getKeyNameByID(id);
                    }
                    if (this.mCurrentKeyId == id)
                    {
                        this.mLabelCurrent.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
                        this.mLabelTarget.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
                        this.mImageCurrent.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(id, false)));
                        this.mImageTarget.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(id, false)));
                        this.mArrow.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_arrow_right_white.png"));
                        break;
                    }
                    this.mLabelCurrent.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
                    this.mLabelTarget.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
                    this.mImageCurrent.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(this.mCurrentKeyId, true)));
                    this.mImageTarget.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(id, true)));
                    this.mArrow.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_arrow_right_blue.png"));
                    break;
                case OneMacroBean _:
                    if (this.mCurrentKeyId == 3 || this.mCurrentKeyId == 1 || this.mCurrentKeyId == 0 || this.mCurrentKeyId == 2)
                    {
                        this.mLabelCurrent.Visibility = Visibility.Collapsed;
                        this.mImageCurrent.Visibility = Visibility.Visible;
                        this.mImageCurrent.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(this.mCurrentKeyId, true)));
                    }
                    this.mLabelTarget.Visibility = Visibility.Visible;
                    this.mImageTarget.Visibility = Visibility.Collapsed;
                    FLog.d("设置宏文字：" + JsonConvert.SerializeObject(obj));
                    this.mLabelTarget.Content = (object)Application.Current.FindResource((object)"macro").ToString();
                    this.mLabelCurrent.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
                    this.mLabelTarget.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
                    this.mArrow.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_arrow_right_blue.png"));
                    break;
            }
        }

        public int getCurrentKeyPosition() => this.mCurrentKeyPosition;

        public int getCurrentKeyId() => this.mCurrentKeyId;

        public object getTargetObj() => this.mTargetObj;

        public void setDalegate(
          IDelegateCallbackValueThreeObject delegateCallbackValueThreeObject)
        {
            this.mIDelegateCallbackValueThreeObject = delegateCallbackValueThreeObject;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.mIDelegateCallbackValueThreeObject == null)
                return;
            this.mIDelegateCallbackValueThreeObject(2, this.mCurrentKeyId, this.mTargetObj);
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolkeymapping.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            ((UIElement)target).MouseEnter += new MouseEventHandler(this.UserControl_MouseEnter);
        //            ((UIElement)target).MouseLeave += new MouseEventHandler(this.UserControl_MouseLeave);
        //            break;
        //        case 2:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Click);
        //            break;
        //        case 3:
        //            this.mLabelCurrent = (Label)target;
        //            break;
        //        case 4:
        //            this.mImageCurrent = (Image)target;
        //            break;
        //        case 5:
        //            this.mLabelTarget = (Label)target;
        //            break;
        //        case 6:
        //            this.mImageTarget = (Image)target;
        //            break;
        //        case 7:
        //            this.mArrow = (Image)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
