// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigKeySetting
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
using System.Threading;
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
    public class UserControlSettingConfigKeySetting :
      UserControl,
      DataManager.IDeviceOperateData,
      IComponentConnector
    {
        private const int MODE_KEY = 0;
        private const int MODE_MACRO = 1;
        private const int MODE_UNSET = -1;
        private int mCurrentMode;
        private bool specialFlag;
        private int mCurrentKeyId;
        private object mTargetObj;
        private IDelegateCallbackValueThreeObject mIDelegateCallbackValueThreeObject;
        private bool isEdit;
        private bool isStartRecord;
        private int mTargetKeyId;
        private List<int> mLastPressedKeyList = new List<int>();
        private List<MacroActionBean> mListMacroAction = new List<MacroActionBean>();
        private long mMacroZeroTime;
        private int mMacroType = 1;
        private int mMacroNum;
        private bool mFlagKeyListen;
        internal Image mTitleKeyIcon;
        internal Label mTitleKey;
        internal Button mButtonMappingKey;
        internal Button mButtonMappingMacro;
        internal Label mLineLeft;
        internal Label mLineRight;
        internal StackPanel mLayoutKey;
        internal UserControlSelectMenu mTargetKey;
        internal UserControlDialogKey mSelectMenuKey;
        internal StackPanel mLayoutMacro;
        internal Label mLabelSingleTouch;
        internal Label mLabelLongPress;
        internal Label mLabelTime;
        internal Label mNotice;
        internal ScrollViewer mScrollViewer;
        internal StackPanel mStackPanelMacro;
        internal Button mButtonStart;
        internal Label mButtonStartText;
        internal Button mButtonPause;
        internal Button mButtonClean;
        internal UserControlMacroActionEdit mMacroActionEdit;
        private bool _contentLoaded;

        public UserControlSettingConfigKeySetting()
        {
            this.InitializeComponent();
            this.mSelectMenuKey.setDelegate((IDelegateCallback)(id =>
            {
                this.closeSelectMenu();
                this.setTargetKeyId(id);
            }));
        }

        private void setTargetKeyId(int id)
        {
            this.mTargetKeyId = id;
            this.mTargetKey.setName(DeviceUtils.getKeyNameByID(this.mTargetKeyId));
        }

        public void setDataAndDelegate(
          int currentKeyId,
          object targetObject,
          int MacroCount,
          IDelegateCallbackValueThreeObject delegateCallbackValueThreeObject)
        {
            this.mCurrentKeyId = currentKeyId;
            this.mTargetObj = targetObject;
            this.mMacroNum = MacroCount;
            this.mIDelegateCallbackValueThreeObject = delegateCallbackValueThreeObject;
            this.setKeyListen(true);
            this.initData();
        }

        private void initData()
        {
            this.isEdit = true;
            this.resetMacroUI();
            if (this.mCurrentKeyId == 3 || this.mCurrentKeyId == 1 || this.mCurrentKeyId == 0 || this.mCurrentKeyId == 2)
            {
                this.mTitleKeyIcon.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(this.mCurrentKeyId, false)));
                this.mTitleKeyIcon.Visibility = Visibility.Visible;
                this.mTitleKey.Content = (object)Application.Current.FindResource((object)"key_attributes").ToString();
            }
            else
            {
                this.mTitleKeyIcon.Visibility = Visibility.Collapsed;
                this.mTitleKey.Content = (object)(DeviceUtils.getKeyNameByID(this.mCurrentKeyId) + Application.Current.FindResource((object)"key_attributes").ToString());
            }
            if (this.mTargetObj is int)
            {
                this.Button_Mapping_Key((object)null, (RoutedEventArgs)null);
                this.setTargetKeyId((int)this.mTargetObj);
            }
            else
            {
                if (!(this.mTargetObj is OneMacroBean))
                    return;
                --this.mMacroNum;
                this.Button_Mapping_Mocro((object)null, (RoutedEventArgs)null);
                this.mNotice.Visibility = Visibility.Collapsed;
                this.mScrollViewer.Visibility = Visibility.Visible;
                this.mButtonStartText.Content = (object)Application.Current.FindResource((object)"re_record").ToString();
                OneMacroBean mTargetObj = this.mTargetObj as OneMacroBean;
                this.mMacroType = mTargetObj.Type;
                if (this.mMacroType == 1)
                    this.LabelSingleTouch_MouseLeftButtonDown((object)null, (MouseButtonEventArgs)null);
                else
                    this.LabelLongPress_MouseLeftButtonDown((object)null, (MouseButtonEventArgs)null);
                this.updateMacroAction(mTargetObj.ListStep);
                this.setTargetKeyId(this.mCurrentKeyId);
            }
        }

        private void resetMacroUI()
        {
            this.mNotice.Visibility = Visibility.Visible;
            this.mScrollViewer.Visibility = Visibility.Collapsed;
            this.mButtonStart.Visibility = Visibility.Visible;
            this.mButtonStartText.Content = (object)Application.Current.FindResource((object)"record").ToString();
            this.mButtonPause.Visibility = Visibility.Hidden;
            this.mLastPressedKeyList.Clear();
            this.mListMacroAction.Clear();
            this.cleanMacroAction();
            this.mLabelTime.Content = (object)(Application.Current.FindResource((object)"total_time").ToString() + "：0ms");
        }

        private void LabelSingleTouch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mMacroType = 1;
            this.mLabelSingleTouch.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mLabelSingleTouch.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            this.mLabelLongPress.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mLabelLongPress.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
        }

        private void LabelLongPress_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mMacroType = 2;
            this.mLabelSingleTouch.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mLabelSingleTouch.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
            this.mLabelLongPress.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mLabelLongPress.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
        }

        private void Button_Mapping_Key(object sender, RoutedEventArgs e)
        {
            this.mCurrentMode = 0;
            this.Button_Pause((object)null, (RoutedEventArgs)null);
            if (this.mListMacroAction.Count == 0)
                this.mButtonStartText.Content = (object)Application.Current.FindResource((object)"record").ToString();
            this.closeMacroActionEdit();
            this.mLayoutKey.Visibility = Visibility.Visible;
            this.mButtonMappingKey.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mButtonMappingKey.FontWeight = FontWeights.Bold;
            this.mLineLeft.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            this.mLayoutMacro.Visibility = Visibility.Collapsed;
            this.mButtonMappingMacro.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
            this.mButtonMappingMacro.FontWeight = FontWeights.Normal;
            this.mLineRight.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
        }

        private void Button_Mapping_Mocro(object sender, RoutedEventArgs e)
        {
            if (this.mMacroNum >= 5)
            {
                CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"maximum_number_of_macro").ToString() + 5.ToString());
            }
            else
            {
                this.mCurrentMode = 1;
                this.closeSelectMenu();
                this.mLayoutKey.Visibility = Visibility.Collapsed;
                this.mButtonMappingKey.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#868788");
                this.mButtonMappingKey.FontWeight = FontWeights.Normal;
                this.mLineLeft.Background = (Brush)new BrushConverter().ConvertFrom((object)"#00000000");
                this.mLayoutMacro.Visibility = Visibility.Visible;
                this.mButtonMappingMacro.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
                this.mButtonMappingMacro.FontWeight = FontWeights.Bold;
                this.mLineRight.Background = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            }
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
            this.mTargetKey.setSelected(false);
        }

        private void openSelectMenu()
        {
            this.mSelectMenuKey.Visibility = Visibility.Visible;
            this.mSelectMenuKey.setListKey(new List<int>(), 0, this.mCurrentKeyId);
            FLog.d("mTargetKey" + this.mTargetKey?.ToString());
            this.mTargetKey.setSelected(true);
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            this.isEdit = true;
            this.isStartRecord = true;
            this.mNotice.Visibility = Visibility.Collapsed;
            this.mScrollViewer.Visibility = Visibility.Visible;
            this.mButtonStart.Visibility = Visibility.Hidden;
            this.mButtonPause.Visibility = Visibility.Visible;
            this.mLastPressedKeyList.Clear();
            this.mListMacroAction.Clear();
            this.cleanMacroAction();
            this.mFlagKeyListen = true;
            this.closeMacroActionEdit();
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            this.mButtonStart.Visibility = Visibility.Visible;
            this.mButtonStartText.Content = (object)Application.Current.FindResource((object)"re_record").ToString();
            this.mButtonPause.Visibility = Visibility.Hidden;
            this.mFlagKeyListen = false;
            this.closeMacroActionEdit();
            this.isEdit = false;
            this.isStartRecord = false;
        }

        private void Button_Clean(object sender, RoutedEventArgs e)
        {
            this.resetMacroUI();
            this.closeMacroActionEdit();
            this.isEdit = false;
        }

        public void onDeviceOperateData(byte[] data)
        {
            List<int> intList = new List<int>();
            intList.AddRange((IEnumerable<int>)DeviceUtils.getCurrentListenKeyListFromAndroid(data));
            if (this.mCurrentMode == 1 && this.isEdit && this.isStartRecord)
            {
                int num1 = (int)data[0];
                int num2 = (int)data[1];
                int num3 = (int)data[2];
                int num4 = (int)data[3];
                int num5 = (int)data[4];
                int num6 = (int)data[5];
                int num7 = (int)data[6];
                int num8 = (int)data[7];
                int num9 = (int)data[8];
                int num10 = (int)data[9] & (int)byte.MaxValue;
                if (!this.specialFlag)
                {
                    if (intList.Contains(this.mCurrentKeyId))
                    {
                        this.specialFlag = true;
                        new Thread((ThreadStart)(() => Application.Current.Dispatcher.Invoke((Action)(() => CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"special_key_self_button_is_forbidden").ToString()))))).Start();
                        return;
                    }
                    if ((num10 & 1) != 0 || (num10 & 2) != 0 || (num10 & 4) != 0 || (num10 & 8) != 0 || (num10 & 16) != 0 || (num10 & 32) != 0 || (num10 & 64) != 0 || (num10 & 128) != 0)
                    {
                        this.specialFlag = true;
                        new Thread((ThreadStart)(() => Application.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            FLog.d("mCurrentMode:" + this.mCurrentMode.ToString());
                            FLog.d("MODE_MACRO:" + 1.ToString());
                            FLog.d("specialFlag:" + this.specialFlag.ToString());
                            CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"special_key_button_is_forbidden").ToString());
                        })))).Start();
                        return;
                    }
                }
                else if ((num10 & 1) == 0 && (num10 & 2) == 0 && (num10 & 4) == 0 && (num10 & 8) == 0 && (num10 & 16) == 0 && (num10 & 32) == 0 && (num10 & 64) == 0 && (num10 & 128) == 0 && !intList.Contains(this.mCurrentKeyId))
                    this.specialFlag = false;
            }
            if (DataManager.getInstance().getDeviceConnectState() != 0 || !this.mFlagKeyListen)
                return;
            if (intList.Contains(this.mCurrentKeyId))
            {
                FLog.d("mCurrentKeyId:" + this.mCurrentKeyId.ToString());
                intList.Remove(this.mCurrentKeyId);
            }
            if (this.mCurrentMode == 0)
            {
                if (intList.Count <= 0)
                    return;
                FLog.d("监听到的手柄按键：" + CommonUtils.byteToHexString(data));
                FLog.d("监听到的手柄按键：" + DeviceUtils.getKeyNameByID(intList[0]));
            }
            else
            {
                if (this.mCurrentMode != 1)
                    return;
                List<int> removedList = CommonUtils.getRemovedList(this.mLastPressedKeyList, intList);
                List<int> addedList = CommonUtils.getAddedList(this.mLastPressedKeyList, intList);
                List<MacroActionBean> actionList = new List<MacroActionBean>();
                for (int index = 0; index < removedList.Count; ++index)
                    actionList.Add(new MacroActionBean()
                    {
                        Btn = removedList[index],
                        State = 0
                    });
                for (int index = 0; index < addedList.Count; ++index)
                    actionList.Add(new MacroActionBean()
                    {
                        Btn = addedList[index],
                        State = 1
                    });
                if (actionList.Count > 0)
                {
                    long currentTime = CommonUtils.getCurrentTime();
                    byte num11 = 0;
                    byte num12 = 0;
                    if (this.mListMacroAction.Count == 0)
                    {
                        this.mMacroZeroTime = currentTime;
                    }
                    else
                    {
                        int num13 = (int)(currentTime - this.mMacroZeroTime);
                        if (num13 >= 300000)
                        {
                            Application.Current.Dispatcher.Invoke((Action)(() => this.Button_Pause((object)null, (RoutedEventArgs)null)));
                            return;
                        }
                        byte[] hexByte = CommonUtils.strToHexByte((num13 / 8).ToString("X4"));
                        num12 = hexByte[0];
                        num11 = hexByte[1];
                    }
                    for (int index = 0; index < actionList.Count; ++index)
                    {
                        actionList[index].Time_h = num12;
                        actionList[index].Time_l = num11;
                    }
                }
                if (this.mListMacroAction.Count + actionList.Count >= 64)
                {
                    while (this.mListMacroAction.Count + actionList.Count > 64)
                        actionList.RemoveAt(actionList.Count - 1);
                    this.updateMacroAction(actionList);
                    FLog.d("最终动作数量：" + this.mListMacroAction.Count.ToString());
                    new Thread((ThreadStart)(() => Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        this.Button_Pause((object)null, (RoutedEventArgs)null);
                        CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"the_maximum_macro_actions").ToString() + 64.ToString());
                    })))).Start();
                }
                else
                    this.updateMacroAction(actionList);
                this.mLastPressedKeyList.Clear();
                this.mLastPressedKeyList.AddRange((IEnumerable<int>)intList);
            }
        }

        private void updateMacroAction(List<MacroActionBean> actionList)
        {
            this.mListMacroAction.AddRange((IEnumerable<MacroActionBean>)actionList);
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                for (int index1 = 0; index1 < actionList.Count; ++index1)
                {
                    UserControlMacroAction userControlMacroAction = new UserControlMacroAction();
                    userControlMacroAction.setAction(actionList[index1], index1, (IDelegateCallback)(result =>
                    {
                        if (this.mFlagKeyListen)
                        {
                            this.Button_Pause((object)null, (RoutedEventArgs)null);
                            CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_stop_recording").ToString());
                        }
                        else
                        {
                            List<UserControlMacroAction> tempList = new List<UserControlMacroAction>();
                            List<UserControlMacroAction> tempEdit = new List<UserControlMacroAction>();
                            List<int> listContainKey = new List<int>();
                            foreach (UserControlMacroAction child in this.mStackPanelMacro.Children)
                            {
                                child.setSelected(false);
                                tempList.Add(child);
                                int btn = child.getMacroActionBean().Btn;
                                if (btn == userControlMacroAction.getMacroActionBean().Btn)
                                    listContainKey.Add(btn);
                            }
                            userControlMacroAction.setSelected(true);
                            int num = this.mStackPanelMacro.Children.IndexOf((UIElement)userControlMacroAction);
                            MacroActionBean macroActionBean4 = userControlMacroAction.getMacroActionBean();
                            int btn2 = macroActionBean4.Btn;
                            int state = macroActionBean4.State;
                            if (state == 1)
                            {
                                for (int index2 = num; index2 < tempList.Count; ++index2)
                                {
                                    MacroActionBean macroActionBean5 = tempList[index2].getMacroActionBean();
                                    if (macroActionBean5.Btn == btn2 && macroActionBean5.State != state)
                                    {
                                        tempList[index2].setSelected(true);
                                        tempEdit.Add(userControlMacroAction);
                                        tempEdit.Add(tempList[index2]);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int index3 = num; index3 > -1; --index3)
                                {
                                    MacroActionBean macroActionBean6 = tempList[index3].getMacroActionBean();
                                    if (macroActionBean6.Btn == btn2 && macroActionBean6.State != state)
                                    {
                                        tempList[index3].setSelected(true);
                                        tempEdit.Add(tempList[index3]);
                                        tempEdit.Add(userControlMacroAction);
                                        break;
                                    }
                                }
                            }
                            this.mMacroActionEdit.Visibility = Visibility.Visible;
                            this.mMacroActionEdit.setDataAndDelegate(this.mCurrentKeyId, btn2, tempEdit, listContainKey, (IDelegateCallback)(r =>
                            {
                                tempList.Sort((Comparison<UserControlMacroAction>)((a, b) => a.getTime() - b.getTime()));
                                this.mStackPanelMacro.Children.Clear();
                                foreach (UIElement element in tempList)
                                    this.mStackPanelMacro.Children.Add(element);
                            }));
                        }
                    }));
                    this.mStackPanelMacro.Children.Add((UIElement)userControlMacroAction);
                    if (index1 == actionList.Count - 1)
                    {
                        int intTime = CommonUtils.getIntTime(actionList[index1].Time_h, actionList[index1].Time_l);
                        this.mLabelTime.Content = (object)(Application.Current.FindResource((object)"total_time").ToString() + "：" + Math.Round((double)intTime / 1000.0, 3).ToString() + "s");
                    }
                }
            }));
        }

        private void cleanMacroAction() => this.mStackPanelMacro.Children.Clear();

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            this.closeMacroActionEdit();
            this.closeSelectMenu();
            if (!this.handleKeySetting(1))
                return;
            DataManager.getInstance().setIDeviceOperateData((DataManager.IDeviceOperateData)null);
        }

        public bool ApplyKeySetting()
        {
            this.closeSelectMenu();
            return this.handleKeySetting(0);
        }

        public void setKeyListen(bool switchOriginData)
        {
            DataManager.getInstance().setIDeviceOperateData((DataManager.IDeviceOperateData)this);
            if (!switchOriginData)
                return;
            DataManager.getInstance().SendByteArray(DongleCommand.getSwitchOriginData(true));
        }

        private bool handleKeySetting(int updateType)
        {
            FLog.d("mCurrentMode:" + this.mCurrentMode.ToString());
            FLog.d("mTargetObj:" + this.mTargetObj?.ToString());
            FLog.d("mTargetKeyId:" + this.mTargetKeyId.ToString());
            FLog.d("updateType:" + updateType.ToString());
            if (this.mCurrentMode == 0)
                this.mTargetObj = (object)this.mTargetKeyId;
            else if (this.mCurrentMode == 1)
            {
                if (this.mListMacroAction.Count == 0 && !this.mFlagKeyListen)
                {
                    this.mTargetObj = (object)this.mTargetKeyId;
                    this.mIDelegateCallbackValueThreeObject(updateType, this.mCurrentKeyId, this.mTargetObj);
                    return true;
                }
                if (this.mFlagKeyListen)
                {
                    CommonUtils.showCommonLangTip((WindowMain)WindowMain.getInstance(), Application.Current.FindResource((object)"please_stop_recording").ToString());
                    return false;
                }
                this.mListMacroAction.Clear();
                foreach (UserControlMacroAction child in this.mStackPanelMacro.Children)
                    this.mListMacroAction.Add(child.getMacroActionBean());
                OneMacroBean oneMacroBean = new OneMacroBean();
                oneMacroBean.Btn = this.mCurrentKeyId;
                oneMacroBean.Type = this.mMacroType;
                oneMacroBean.Count_h = 0;
                oneMacroBean.Count_l = this.mListMacroAction.Count;
                oneMacroBean.ListStep.Clear();
                oneMacroBean.ListStep.AddRange((IEnumerable<MacroActionBean>)this.mListMacroAction);
                this.mTargetObj = (object)oneMacroBean;
            }
            this.mIDelegateCallbackValueThreeObject(updateType, this.mCurrentKeyId, this.mTargetObj);
            return true;
        }

        private void closeMacroActionEdit() => this.mMacroActionEdit.Visibility = Visibility.Collapsed;

        private void WindowOnClosed(object sender, EventArgs e) => this.isEdit = false;

        private void Button_MouseEnter(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(true, 74, Application.Current.FindResource((object)"key_setting_desc").ToString());

        private void Button_MouseLeave(object sender, MouseEventArgs e) => ((WindowMain)WindowMain.getInstance()).showLayoutFunctionDesc(false, 74, "");

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigkeysetting.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => Delegate.CreateDelegate(delegateType, (object)this, handler);

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.Button_Back);
                    break;
                case 2:
                    this.mTitleKeyIcon = (Image)target;
                    break;
                case 3:
                    this.mTitleKey = (Label)target;
                    break;
                case 4:
                    this.mButtonMappingKey = (Button)target;
                    this.mButtonMappingKey.Click += new RoutedEventHandler(this.Button_Mapping_Key);
                    break;
                case 5:
                    this.mButtonMappingMacro = (Button)target;
                    this.mButtonMappingMacro.Click += new RoutedEventHandler(this.Button_Mapping_Mocro);
                    break;
                case 6:
                    ((UIElement)target).MouseEnter += new MouseEventHandler(this.Button_MouseEnter);
                    ((UIElement)target).MouseLeave += new MouseEventHandler(this.Button_MouseLeave);
                    break;
                case 7:
                    this.mLineLeft = (Label)target;
                    break;
                case 8:
                    this.mLineRight = (Label)target;
                    break;
                case 9:
                    this.mLayoutKey = (StackPanel)target;
                    break;
                case 10:
                    ((ButtonBase)target).Click += new RoutedEventHandler(this.KeySelect_Click);
                    break;
                case 11:
                    this.mTargetKey = (UserControlSelectMenu)target;
                    break;
                case 12:
                    this.mSelectMenuKey = (UserControlDialogKey)target;
                    break;
                case 13:
                    this.mLayoutMacro = (StackPanel)target;
                    break;
                case 14:
                    this.mLabelSingleTouch = (Label)target;
                    this.mLabelSingleTouch.MouseLeftButtonDown += new MouseButtonEventHandler(this.LabelSingleTouch_MouseLeftButtonDown);
                    break;
                case 15:
                    this.mLabelLongPress = (Label)target;
                    this.mLabelLongPress.MouseLeftButtonDown += new MouseButtonEventHandler(this.LabelLongPress_MouseLeftButtonDown);
                    break;
                case 16:
                    this.mLabelTime = (Label)target;
                    break;
                case 17:
                    this.mNotice = (Label)target;
                    break;
                case 18:
                    this.mScrollViewer = (ScrollViewer)target;
                    break;
                case 19:
                    this.mStackPanelMacro = (StackPanel)target;
                    break;
                case 20:
                    this.mButtonStart = (Button)target;
                    this.mButtonStart.Click += new RoutedEventHandler(this.Button_Start);
                    break;
                case 21:
                    this.mButtonStartText = (Label)target;
                    break;
                case 22:
                    this.mButtonPause = (Button)target;
                    this.mButtonPause.Click += new RoutedEventHandler(this.Button_Pause);
                    break;
                case 23:
                    this.mButtonClean = (Button)target;
                    this.mButtonClean.Click += new RoutedEventHandler(this.Button_Clean);
                    break;
                case 24:
                    this.mMacroActionEdit = (UserControlMacroActionEdit)target;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }
    }
}
