// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigKey
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace;
using ApexSpace.data;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WPFTest.Utils;

namespace WPFTest.UserControls
{
    public class UserControlSettingConfigKey : UserControl, IComponentConnector
    {
        private List<int> mListKeyMapping = new List<int>();
        private List<OneMacroBean> mListMacro = new List<OneMacroBean>();
        private int[] mCommonListKeyFront = new int[14]
        {
      4,
      5,
      7,
      8,
      3,
      1,
      0,
      2,
      6,
      9,
      14,
      15,
      16,
      17
        };
        private int[] mCommonListKeyTop = new int[6]
        {
      12,
      13,
      10,
      11,
      22,
      23
        };
        private int[] mCommonListKeyAfter = new int[4]
        {
      18,
      19,
      20,
      21
        };
        private Dictionary<int, UserControlKeyMapping> mDictionary = new Dictionary<int, UserControlKeyMapping>();
        internal UserControlSettingConfigKeyDisplay mLayoutKeyDisplay;
        internal UserControlSettingConfigKeySetting mLayoutKeySetting;
        internal Grid mLayoutMappingKey;
        internal UserControlTitle mTitle1;
        internal WrapPanel mWrapPanel_1;
        internal UserControlTitle mTitle2;
        internal WrapPanel mWrapPanel_2;
        internal UserControlTitle mTitle3;
        internal WrapPanel mWrapPanel_3;
        private bool _contentLoaded;

        public UserControlSettingConfigKey()
        {
            this.InitializeComponent();
            this.mTitle1.setTitle(Application.Current.FindResource((object)"front_key").ToString());
            this.mTitle2.setTitle(Application.Current.FindResource((object)"side_key").ToString());
            this.mTitle3.setTitle(Application.Current.FindResource((object)"back_key").ToString());
        }

        private void initData()
        {
            if (DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1 || DataManager.getInstance().getDeviceInfo().DeviceId == Constant.DEVICE.F1_WIRED)
                return;
            int deviceId = DataManager.getInstance().getDeviceInfo().DeviceId;
            int apex2 = Constant.DEVICE.APEX_2;
        }

        public void setData(List<int> list, List<OneMacroBean> listMacro)
        {
            int num1 = 0;
            this.mListKeyMapping = list;
            this.mListMacro = listMacro;
            this.mWrapPanel_1.Children.Clear();
            this.mWrapPanel_2.Children.Clear();
            this.mWrapPanel_3.Children.Clear();
            this.mDictionary.Clear();
            for (int index = 0; index < this.mCommonListKeyFront.Length; ++index)
            {
                int num2 = this.mCommonListKeyFront[index];
                UserControlKeyMapping keyMapping = new UserControlKeyMapping();
                keyMapping.setCurrentKeyPosition(0);
                keyMapping.setCurrentKeyId(num2);
                keyMapping.setTargetObj((object)this.mListKeyMapping[num2]);
                this.addKeyMapping(this.mWrapPanel_1, keyMapping);
            }
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch") && !(gameHadleName == "f1p"))
            {
                if (gameHadleName == "apex2")
                    num1 = this.mCommonListKeyTop.Length;
            }
            else
                num1 = this.mCommonListKeyTop.Length - 2;
            for (int index = 0; index < num1; ++index)
            {
                int num3 = this.mCommonListKeyTop[index];
                UserControlKeyMapping keyMapping = new UserControlKeyMapping();
                keyMapping.setCurrentKeyPosition(2);
                keyMapping.setCurrentKeyId(num3);
                keyMapping.setTargetObj((object)this.mListKeyMapping[num3]);
                this.addKeyMapping(this.mWrapPanel_2, keyMapping);
            }
            for (int index = 0; index < this.mCommonListKeyAfter.Length; ++index)
            {
                int num4 = this.mCommonListKeyAfter[index];
                UserControlKeyMapping keyMapping = new UserControlKeyMapping();
                keyMapping.setCurrentKeyPosition(1);
                keyMapping.setCurrentKeyId(num4);
                keyMapping.setTargetObj((object)this.mListKeyMapping[num4]);
                this.addKeyMapping(this.mWrapPanel_3, keyMapping);
            }
            for (int index = 0; index < listMacro.Count; ++index)
            {
                if (this.mDictionary.ContainsKey(listMacro[index].Btn))
                    this.mDictionary[listMacro[index].Btn].setTargetObj((object)listMacro[index]);
            }
        }

        private void addKeyMapping(WrapPanel wrapPanel, UserControlKeyMapping keyMapping)
        {
            if (wrapPanel.Children.Count % 2 == 0)
                keyMapping.Margin = new Thickness(0.0, 9.0, 8.0, 9.0);
            else
                keyMapping.Margin = new Thickness(8.0, 9.0, 0.0, 9.0);
            keyMapping.setDalegate((IDelegateCallbackValueThreeObject)((type, curKeyId, targetObj) =>
            {
                if (type == 0)
                    this.mLayoutKeyDisplay.setOutsideFocusKeyId(this.mDictionary[curKeyId].getCurrentKeyPosition(), this.mDictionary[curKeyId].getCurrentKeyId(), this.mDictionary[curKeyId].getTargetObj(), false);
                else if (type == 1)
                {
                    this.mLayoutKeyDisplay.setOutsideFocusKeyId(this.mDictionary[curKeyId].getCurrentKeyPosition(), this.mDictionary[curKeyId].getCurrentKeyId(), this.mDictionary[curKeyId].getTargetObj(), true);
                }
                else
                {
                    if (!this.mDictionary.ContainsKey(curKeyId))
                        return;
                    this.mLayoutMappingKey.Visibility = Visibility.Hidden;
                    this.mLayoutKeySetting.Visibility = Visibility.Visible;
                    this.mLayoutKeyDisplay.updateDisplay(false);
                    this.mLayoutKeyDisplay.setInsideFocusKeyId(this.mDictionary[curKeyId].getCurrentKeyId(), this.mDictionary[curKeyId].getTargetObj());
                    this.mLayoutKeySetting.setDataAndDelegate(this.mDictionary[curKeyId].getCurrentKeyId(), this.mDictionary[curKeyId].getTargetObj(), this.mListMacro.Count, (IDelegateCallbackValueThreeObject)((updateType, keyId, tObj) =>
                    {
                        if (updateType == 1)
                        {
                            DataManager.getInstance().SendByteArray(DongleCommand.getSwitchOriginData(false));
                            this.mLayoutKeyDisplay.updateDisplay(true);
                            this.mLayoutMappingKey.Visibility = Visibility.Visible;
                            this.mLayoutKeySetting.Visibility = Visibility.Hidden;
                            this.mDictionary[keyId].setTargetObj(tObj);
                        }
                        switch (tObj)
                        {
                            case int targetId2:
                                ConfigUtils.updateKeyMappingList(this.mListKeyMapping, keyId, targetId2);
                                ConfigUtils.checkMacroListRemoveByKeyId(this.mListMacro, keyId);
                                break;
                            case OneMacroBean _:
                                OneMacroBean oneMacro = tObj as OneMacroBean;
                                FLog.d("宏设置完成：" + JsonConvert.SerializeObject((object)oneMacro));
                                ConfigUtils.updateListMacro(this.mListMacro, oneMacro);
                                ConfigUtils.checkKeyMappingListRemoveByKeyId(this.mListKeyMapping, keyId);
                                break;
                        }
                    }));
                }
            }));
            wrapPanel.Children.Add((UIElement)keyMapping);
            this.mDictionary.Add(keyMapping.getCurrentKeyId(), keyMapping);
        }

        public bool getKeySettingVisible() => this.mLayoutKeySetting.Visibility == Visibility.Visible;

        public bool ApplyKeySetting() => this.mLayoutKeySetting.ApplyKeySetting();

        public void setKeyListen(bool switchOriginData) => this.mLayoutKeySetting.setKeyListen(switchOriginData);

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigkey.xaml", UriKind.Relative));
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
                    this.mLayoutKeyDisplay = (UserControlSettingConfigKeyDisplay)target;
                    break;
                case 2:
                    this.mLayoutKeySetting = (UserControlSettingConfigKeySetting)target;
                    break;
                case 3:
                    this.mLayoutMappingKey = (Grid)target;
                    break;
                case 4:
                    this.mTitle1 = (UserControlTitle)target;
                    break;
                case 5:
                    this.mWrapPanel_1 = (WrapPanel)target;
                    break;
                case 6:
                    this.mTitle2 = (UserControlTitle)target;
                    break;
                case 7:
                    this.mWrapPanel_2 = (WrapPanel)target;
                    break;
                case 8:
                    this.mTitle3 = (UserControlTitle)target;
                    break;
                case 9:
                    this.mWrapPanel_3 = (WrapPanel)target;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }
    }
}
