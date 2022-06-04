// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlMacroActionEdit
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using ApexSpace.data;
using ApexSpace.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
    public partial class UserControlMacroActionEdit : UserControl, IComponentConnector
    {
        private IDelegateCallback mDelegateCallback;
        private List<UserControlMacroAction> mListMacroAction = new List<UserControlMacroAction>();
        private List<int> mListContainKey = new List<int>();
        private int mActionKeyId;
        private int mCurrentKeyId;
        private int mWaitTime;
        private int mWorkTime;
        private int MaxTime = 37500;
        private double MaxUIWidth;
        private double ThumbHalf;
        //internal UserControlSelectMenu mCurrentKey;
        //internal Image mWaitTimeDecrease;
        //internal Label mLabelWaitTime;
        //internal Image mWaitTimeIncrease;
        //internal Image mWorkTimeDecrease;
        //internal Label mLabelWorkTime;
        //internal Image mWorkTimeIncrease;
        //internal Canvas mCanvas;
        //internal Label mBgWorkTime;
        //internal Label mBgWaitTime;
        //internal Thumb mThumbWaitTime;
        //internal Thumb mThumbWorkTime;
        //internal UserControlDialogKey mSelectMenuKey;
       //private bool _contentLoaded;

        public UserControlMacroActionEdit()
        {
            this.InitializeComponent();
            this.MaxUIWidth = this.mCanvas.Width;
            this.ThumbHalf = this.mThumbWaitTime.Width / 2.0;
            this.mSelectMenuKey.setDelegate((IDelegateCallback)(id =>
            {
                this.closeSelectMenu();
                this.setActionKeyId(id, true);
            }));
        }

        private void setActionKeyId(int actionKeyId, bool updateCurrentKey)
        {
            this.mActionKeyId = actionKeyId;
            this.mCurrentKey.setName(DeviceUtils.getKeyNameByID(this.mActionKeyId));
            for (int index = 0; index < this.mListMacroAction.Count; ++index)
            {
                if (updateCurrentKey)
                {
                    MacroActionBean macroActionBean = this.mListMacroAction[index].getMacroActionBean();
                    macroActionBean.Btn = this.mActionKeyId;
                    this.mListMacroAction[index].setMacroActionBean(macroActionBean);
                }
            }
        }

        public void setDataAndDelegate(
          int currentKeyId,
          int actionKeyId,
          List<UserControlMacroAction> tempEdit,
          List<int> listContainKey,
          IDelegateCallback delegateCallback)
        {
            this.mCurrentKeyId = currentKeyId;
            this.setActionKeyId(actionKeyId, false);
            this.mListMacroAction.Clear();
            this.mListMacroAction.AddRange((IEnumerable<UserControlMacroAction>)tempEdit);
            this.mListContainKey.Clear();
            this.mListContainKey.AddRange((IEnumerable<int>)listContainKey);
            this.mDelegateCallback = delegateCallback;
            this.closeSelectMenu();
            if (this.mListMacroAction.Count == 1)
            {
                MacroActionBean macroActionBean = this.mListMacroAction[0].getMacroActionBean();
                int num = CommonUtils.getIntTime(macroActionBean.Time_h, macroActionBean.Time_l) / 8;
                this.mWaitTime = num;
                this.mWorkTime = num;
            }
            else if (this.mListMacroAction.Count == 2)
            {
                MacroActionBean macroActionBean1 = this.mListMacroAction[0].getMacroActionBean();
                int num1 = CommonUtils.getIntTime(macroActionBean1.Time_h, macroActionBean1.Time_l) / 8;
                MacroActionBean macroActionBean2 = this.mListMacroAction[1].getMacroActionBean();
                int num2 = CommonUtils.getIntTime(macroActionBean2.Time_h, macroActionBean2.Time_l) / 8;
                this.mWaitTime = num1;
                this.mWorkTime = num2 - num1;
            }
            this.setWaitTime();
            this.setWorkTime();
            this.updateThumb();
        }

        private void KeySelect_Click(object sender, RoutedEventArgs e)
        {
            if (this.mSelectMenuKey.Visibility == Visibility.Visible)
                this.closeSelectMenu();
            else
                this.openSelectMenu();
        }

        private void closeSelectMenu()
        {
            this.mSelectMenuKey.Visibility = Visibility.Hidden;
            this.mCurrentKey.setSelected(false);
        }

        private void openSelectMenu()
        {
            this.mSelectMenuKey.Visibility = Visibility.Visible;
            this.mCurrentKey.setSelected(true);
            this.mListContainKey.Add(this.mCurrentKeyId);
            this.mSelectMenuKey.setListKey(this.mListContainKey, 0);
        }

        private void Button_Close(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Collapsed;

        private void handleMacroList()
        {
            if (this.mListMacroAction.Count == 1)
            {
                MacroActionBean macroActionBean = this.mListMacroAction[0].getMacroActionBean();
                byte[] hexByte = CommonUtils.strToHexByte(this.mWaitTime.ToString("X4"));
                macroActionBean.Time_h = hexByte[0];
                macroActionBean.Time_l = hexByte[1];
                this.mListMacroAction[0].setMacroActionBean(macroActionBean);
            }
            else if (this.mListMacroAction.Count == 2)
            {
                MacroActionBean macroActionBean1 = this.mListMacroAction[0].getMacroActionBean();
                byte[] hexByte1 = CommonUtils.strToHexByte(this.mWaitTime.ToString("X4"));
                macroActionBean1.Time_h = hexByte1[0];
                macroActionBean1.Time_l = hexByte1[1];
                this.mListMacroAction[0].setMacroActionBean(macroActionBean1);
                MacroActionBean macroActionBean2 = this.mListMacroAction[1].getMacroActionBean();
                byte[] hexByte2 = CommonUtils.strToHexByte((this.mWaitTime + this.mWorkTime).ToString("X4"));
                macroActionBean2.Time_h = hexByte2[0];
                macroActionBean2.Time_l = hexByte2[1];
                this.mListMacroAction[1].setMacroActionBean(macroActionBean2);
            }
            if (this.mDelegateCallback == null)
                return;
            this.mDelegateCallback(0);
        }

        private void mWaitTimeDecrease_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mWaitTimeDecrease.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_decrease_blue.png"));
            if (this.mWaitTime - 1 < 0)
                return;
            --this.mWaitTime;
            this.setWaitTime();
            this.updateThumb();
        }

        private void mWaitTimeDecrease_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => this.mWaitTimeDecrease.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_decrease_white.png"));

        private void mWaitTimeIncrease_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mWaitTimeIncrease.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_increase_blue.png"));
            if (this.mWaitTime + 1 > this.MaxTime - this.mWorkTime)
                return;
            ++this.mWaitTime;
            this.setWaitTime();
            this.updateThumb();
        }

        private void mWaitTimeIncrease_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => this.mWaitTimeIncrease.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_increase_white.png"));

        private void mWorkTimeDecrease_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mWorkTimeDecrease.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_decrease_blue.png"));
            if (this.mWorkTime - 1 < 0)
                return;
            --this.mWorkTime;
            this.setWorkTime();
            this.updateThumb();
        }

        private void mWorkTimeDecrease_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => this.mWorkTimeDecrease.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_decrease_white.png"));

        private void mWorkTimeIncrease_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mWorkTimeIncrease.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_increase_blue.png"));
            if (this.mWorkTime + 1 > this.MaxTime - this.mWaitTime)
                return;
            ++this.mWorkTime;
            this.setWorkTime();
            this.updateThumb();
        }

        private void mWorkTimeIncrease_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => this.mWorkTimeIncrease.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/icon_increase_white.png"));

        private void mButtonWaitTime_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double length = Canvas.GetLeft((UIElement)this.mThumbWaitTime) + e.HorizontalChange;
            if (length < -this.ThumbHalf)
                length = -this.ThumbHalf;
            else if (length > Canvas.GetLeft((UIElement)this.mThumbWorkTime))
                length = Canvas.GetLeft((UIElement)this.mThumbWorkTime);
            Canvas.SetLeft((UIElement)this.mThumbWaitTime, length);
            this.mBgWaitTime.Width = length + this.ThumbHalf;
            this.mWaitTime = this.UIWidthToTime(this.mBgWaitTime.Width);
            this.setWaitTime();
            this.mWorkTime = this.UIWidthToTime(this.mBgWorkTime.Width) - this.mWaitTime;
            this.setWorkTime();
        }

        private void mButtonWorkTime_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double length = Canvas.GetLeft((UIElement)this.mThumbWorkTime) + e.HorizontalChange;
            if (length < Canvas.GetLeft((UIElement)this.mThumbWaitTime))
                length = Canvas.GetLeft((UIElement)this.mThumbWaitTime);
            else if (length > this.MaxUIWidth - this.ThumbHalf)
                length = this.MaxUIWidth - this.ThumbHalf;
            Canvas.SetLeft((UIElement)this.mThumbWorkTime, length);
            this.mBgWorkTime.Width = length + this.ThumbHalf;
            this.mWorkTime = this.UIWidthToTime(this.mBgWorkTime.Width) - this.mWaitTime;
            this.setWorkTime();
        }

        private void setWaitTime()
        {
            if (this.mWaitTime < 0)
                this.mWaitTime = 0;
            this.mLabelWaitTime.Content = (object)(Math.Round((double)(this.mWaitTime * 8) / 1000.0, 3).ToString() + "s");
        }

        private void setWorkTime()
        {
            if (this.mWorkTime < 0)
                this.mWorkTime = 0;
            this.mLabelWorkTime.Content = (object)(Math.Round((double)(this.mWorkTime * 8) / 1000.0, 3).ToString() + "s");
        }

        private void updateThumb()
        {
            this.mBgWaitTime.Width = this.TimeToUIWidth((double)this.mWaitTime);
            Canvas.SetLeft((UIElement)this.mThumbWaitTime, this.mBgWaitTime.Width - this.ThumbHalf);
            this.mBgWorkTime.Width = this.TimeToUIWidth((double)(this.mWaitTime + this.mWorkTime));
            Canvas.SetLeft((UIElement)this.mThumbWorkTime, this.mBgWorkTime.Width - this.ThumbHalf);
            this.handleMacroList();
        }

        private int UIWidthToTime(double width) => (int)(width / this.MaxUIWidth * (double)this.MaxTime);

        private double TimeToUIWidth(double time) => (double)(int)(time / (double)this.MaxTime * this.MaxUIWidth);

        private void mThumbWaitTime_DragCompleted(object sender, DragCompletedEventArgs e) => this.handleMacroList();

        private void mThumbWorkTime_DragCompleted(object sender, DragCompletedEventArgs e) => this.handleMacroList();

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolmacroactionedit.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.KeySelect_Click);
        //            break;
        //        case 2:
        //            this.mCurrentKey = (UserControlSelectMenu)target;
        //            break;
        //        case 3:
        //            this.mWaitTimeDecrease = (Image)target;
        //            this.mWaitTimeDecrease.MouseLeftButtonDown += new MouseButtonEventHandler(this.mWaitTimeDecrease_MouseLeftButtonDown);
        //            this.mWaitTimeDecrease.MouseLeftButtonUp += new MouseButtonEventHandler(this.mWaitTimeDecrease_MouseLeftButtonUp);
        //            break;
        //        case 4:
        //            this.mLabelWaitTime = (Label)target;
        //            break;
        //        case 5:
        //            this.mWaitTimeIncrease = (Image)target;
        //            this.mWaitTimeIncrease.MouseLeftButtonDown += new MouseButtonEventHandler(this.mWaitTimeIncrease_MouseLeftButtonDown);
        //            this.mWaitTimeIncrease.MouseLeftButtonUp += new MouseButtonEventHandler(this.mWaitTimeIncrease_MouseLeftButtonUp);
        //            break;
        //        case 6:
        //            this.mWorkTimeDecrease = (Image)target;
        //            this.mWorkTimeDecrease.MouseLeftButtonDown += new MouseButtonEventHandler(this.mWorkTimeDecrease_MouseLeftButtonDown);
        //            this.mWorkTimeDecrease.MouseLeftButtonUp += new MouseButtonEventHandler(this.mWorkTimeDecrease_MouseLeftButtonUp);
        //            break;
        //        case 7:
        //            this.mLabelWorkTime = (Label)target;
        //            break;
        //        case 8:
        //            this.mWorkTimeIncrease = (Image)target;
        //            this.mWorkTimeIncrease.MouseLeftButtonDown += new MouseButtonEventHandler(this.mWorkTimeIncrease_MouseLeftButtonDown);
        //            this.mWorkTimeIncrease.MouseLeftButtonUp += new MouseButtonEventHandler(this.mWorkTimeIncrease_MouseLeftButtonUp);
        //            break;
        //        case 9:
        //            this.mCanvas = (Canvas)target;
        //            break;
        //        case 10:
        //            this.mBgWorkTime = (Label)target;
        //            break;
        //        case 11:
        //            this.mBgWaitTime = (Label)target;
        //            break;
        //        case 12:
        //            this.mThumbWaitTime = (Thumb)target;
        //            this.mThumbWaitTime.DragDelta += new DragDeltaEventHandler(this.mButtonWaitTime_DragDelta);
        //            this.mThumbWaitTime.DragCompleted += new DragCompletedEventHandler(this.mThumbWaitTime_DragCompleted);
        //            break;
        //        case 13:
        //            this.mThumbWorkTime = (Thumb)target;
        //            this.mThumbWorkTime.DragDelta += new DragDeltaEventHandler(this.mButtonWorkTime_DragDelta);
        //            this.mThumbWorkTime.DragCompleted += new DragCompletedEventHandler(this.mThumbWorkTime_DragCompleted);
        //            break;
        //        case 14:
        //            this.mSelectMenuKey = (UserControlDialogKey)target;
        //            break;
        //        case 15:
        //            ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Close);
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
