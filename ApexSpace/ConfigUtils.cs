// Decompiled with JetBrains decompiler
// Type: ApexSpace.ConfigUtils
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using ApexSpace.data;
using ApexSpace.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WPFTest.Utils;

namespace ApexSpace
{
    internal class ConfigUtils
    {
        public static int MaxLengthEveryParcel = 10;

        public static List<byte> getConfigVersionByteArray(string name)
        {
            List<byte> versionByteArray = new List<byte>();
            char[] chArray = new char[1] { '.' };
            string[] strArray = name.Split(chArray);
            versionByteArray.Add((byte)int.Parse(strArray[0]));
            versionByteArray.Add((byte)int.Parse(strArray[1]));
            return versionByteArray;
        }

        public static string getConfigVersionByListByte(List<byte> data)
        {
            byte num = data[0];
            string str1 = num.ToString();
            num = data[1];
            string str2 = num.ToString();
            return str1 + "." + str2;
        }

        public static List<byte> getConfigNameByteArray(string name) => new List<byte>((IEnumerable<byte>)CommonUtils.getByteArrayByLength(Encoding.GetEncoding("GBK").GetBytes(name), 12));

        public static string getConfigNameByListByte(List<byte> data) => Encoding.GetEncoding("GBK").GetString(data.ToArray());

        public static List<byte> getLedByteArray(LedBean led) => new List<byte>()
    {
      (byte) 32,
      (byte) led.Mode,
      (byte) led.Peroid,
      (byte) led.Light,
      (byte) led.RgbColor0[0],
      (byte) led.RgbColor0[1],
      (byte) led.RgbColor0[2],
      (byte) led.RgbColor1[0],
      (byte) led.RgbColor1[1],
      (byte) led.RgbColor1[2]
    };

        public static LedBean getLedByListByte(List<byte> data) => new LedBean()
        {
            Header = (int)data[0],
            Mode = (int)data[1],
            Peroid = (int)data[2],
            Light = (int)data[3],
            RgbColor0 = {
        (int) data[4],
        (int) data[5],
        (int) data[6]
      },
            RgbColor1 = {
        (int) data[7],
        (int) data[8],
        (int) data[9]
      }
        };

        public static List<byte> getKeyMappingByteArray(List<int> list)
        {
            List<byte> mappingByteArray = new List<byte>();
            for (int index = 0; index < list.Count; ++index)
            {
                if (index == list[index])
                    mappingByteArray.Add(byte.MaxValue);
                else
                    mappingByteArray.Add((byte)list[index]);
            }
            return mappingByteArray;
        }

        public static List<int> getKeyMappingByListByte(List<byte> data)
        {
            List<int> mappingByListByte = new List<int>();
            for (int index = 0; index < data.Count; ++index)
            {
                int num = ConfigUtils.convertKeyId(index, (int)data[index]);
                if (num >= 32)
                    mappingByListByte.Add(index);
                else
                    mappingByListByte.Add(num);
            }
            return mappingByListByte;
        }

        private static int switchModeConvertKeyId(
          int currentKeyId,
          int targetKeyId,
          bool specialKeyConvert = false)
        {
            if (targetKeyId >= 32)
                targetKeyId = currentKeyId;
            if (specialKeyConvert && (targetKeyId == 18 || targetKeyId == 19 || targetKeyId == 20 || targetKeyId == 21 || targetKeyId == 16 || targetKeyId == 17 || targetKeyId == 4 || targetKeyId == 5 || targetKeyId == 7 || targetKeyId == 8))
            {
                switch (currentKeyId)
                {
                    case 4:
                        targetKeyId = 5;
                        break;
                    case 5:
                        targetKeyId = 4;
                        break;
                    case 7:
                        targetKeyId = 8;
                        break;
                    case 8:
                        targetKeyId = 7;
                        break;
                    case 16:
                        targetKeyId = 14;
                        break;
                    case 17:
                        targetKeyId = 15;
                        break;
                    case 18:
                        targetKeyId = 0;
                        break;
                    case 19:
                        targetKeyId = 2;
                        break;
                    case 20:
                        targetKeyId = 1;
                        break;
                    case 21:
                        targetKeyId = 3;
                        break;
                }
            }
            return targetKeyId;
        }

        private static int convertKeyId(int currentKeyId, int targetKeyId, bool specialKeyConvert = false)
        {
            if (targetKeyId >= 32)
                targetKeyId = currentKeyId;
            if (specialKeyConvert && (targetKeyId == 18 || targetKeyId == 19 || targetKeyId == 20 || targetKeyId == 21 || targetKeyId == 16 || targetKeyId == 17))
            {
                switch (currentKeyId)
                {
                    case 16:
                        targetKeyId = 14;
                        break;
                    case 17:
                        targetKeyId = 15;
                        break;
                    case 18:
                        targetKeyId = 0;
                        break;
                    case 19:
                        targetKeyId = 2;
                        break;
                    case 20:
                        targetKeyId = 1;
                        break;
                    case 21:
                        targetKeyId = 3;
                        break;
                }
            }
            return targetKeyId;
        }

        public static List<byte> getJoyMappingByteArray(JoyMappingBean joy) => new List<byte>()
    {
      (byte) joy.LeftJoyStick.Zero,
      (byte) joy.LeftJoyStick.Point0_X,
      (byte) joy.LeftJoyStick.Point0_Y,
      (byte) joy.LeftJoyStick.Point1_X,
      (byte) joy.LeftJoyStick.Point1_Y,
      (byte) joy.RightJoyStick.Zero,
      (byte) joy.RightJoyStick.Point0_X,
      (byte) joy.RightJoyStick.Point0_Y,
      (byte) joy.RightJoyStick.Point1_X,
      (byte) joy.RightJoyStick.Point1_Y
    };

        public static JoyMappingBean getJoyMappingByListByte(List<byte> data)
        {
            for (int index = 0; index < data.Count; ++index)
            {
                if (data[index] > (byte)127)
                    data[index] = (byte)127;
            }
            OneJoystickBean oneJoystickBean1 = new OneJoystickBean()
            {
                Zero = (int)data[0],
                Point0_X = (int)Math.Max(data[0], data[1]),
                Point0_Y = (int)data[2]
            };
            oneJoystickBean1.Point1_X = Math.Max(oneJoystickBean1.Point0_X, (int)data[3]);
            oneJoystickBean1.Point1_Y = Math.Max(oneJoystickBean1.Point0_Y, (int)data[4]);
            OneJoystickBean oneJoystickBean2 = new OneJoystickBean()
            {
                Zero = (int)data[5],
                Point0_X = (int)Math.Max(data[5], data[6]),
                Point0_Y = (int)data[7]
            };
            oneJoystickBean2.Point1_X = Math.Max(oneJoystickBean2.Point0_X, (int)data[8]);
            oneJoystickBean2.Point1_Y = Math.Max(oneJoystickBean2.Point0_Y, (int)data[9]);
            return new JoyMappingBean()
            {
                LeftJoyStick = oneJoystickBean1,
                RightJoyStick = oneJoystickBean2
            };
        }

        public static List<byte> getMotionMappingByteArray(MotionMappingBean motion) => new List<byte>()
    {
      (byte) motion.Type,
      (byte) motion.KeyId,
      (byte) motion.Method,
      (byte) motion.Zero,
      (byte) motion.Sensity
    };

        public static List<byte> getMotionMappingMotorLunpanByteArray(
          MotionMappingBean motion,
          MotorBean motor,
          LunpanBean lunpan)
        {
            return new List<byte>()
      {
        (byte) motion.Type,
        (byte) motion.KeyId,
        (byte) motion.Method,
        (byte) motion.Zero,
        (byte) motion.Sensity,
        (byte) motion.Mode,
        (byte) motion.KeyidExt,
        (byte) motor.Level,
        (byte) lunpan.Type,
        (byte) lunpan.Rev
      };
        }

        public static MotionMappingBean getMotionMappingByListByte(List<byte> data)
        {
            MotionMappingBean mappingByListByte = new MotionMappingBean();
            mappingByListByte.Type = (int)data[0];
            mappingByListByte.KeyId = (int)data[1];
            mappingByListByte.Method = (int)data[2];
            mappingByListByte.Zero = (int)data[3];
            mappingByListByte.Sensity = (int)data[4];
            mappingByListByte.Mode = (int)data[5];
            mappingByListByte.KeyidExt = (int)data[6];
            if (mappingByListByte.Zero > 100)
                mappingByListByte.Zero = 100;
            if (mappingByListByte.Sensity < 1)
                mappingByListByte.Sensity = 1;
            else if (mappingByListByte.Sensity > (int)sbyte.MaxValue)
                mappingByListByte.Sensity = (int)sbyte.MaxValue;
            return mappingByListByte;
        }

        public static List<byte> getMacroByteArray(AllMacroBean macro)
        {
            FLog.d(JsonConvert.SerializeObject((object)macro));
            List<byte> macroByteArray = new List<byte>();
            macroByteArray.Add((byte)macro.ListMacro.Count);
            for (int index = 0; index < 5; ++index)
                macroByteArray.Add((byte)0);
            int num = 0;
            for (int index = 0; index < macro.ListMacro.Count; ++index)
            {
                FLog.d(JsonConvert.SerializeObject((object)macro.ListMacro[index]));
                if (index != macro.ListMacro.Count - 1)
                {
                    num += macro.ListMacro[index].ListStep.Count + 1;
                    FLog.d("index:" + num.ToString() + "i" + index.ToString());
                    macroByteArray[index + 2] = (byte)num;
                }
            }
            FLog.d("MacroByteDataStart:" + JsonConvert.SerializeObject((object)macroByteArray));
            for (int index1 = 0; index1 < macro.ListMacro.Count; ++index1)
            {
                macroByteArray.Add((byte)macro.ListMacro[index1].Btn);
                macroByteArray.Add((byte)macro.ListMacro[index1].Count_l);
                macroByteArray.Add((byte)macro.ListMacro[index1].Count_h);
                macroByteArray.Add((byte)macro.ListMacro[index1].Type);
                for (int index2 = 0; index2 < macro.ListMacro[index1].ListStep.Count; ++index2)
                {
                    macroByteArray.Add(macro.ListMacro[index1].ListStep[index2].Time_l);
                    macroByteArray.Add(macro.ListMacro[index1].ListStep[index2].Time_h);
                    macroByteArray.Add((byte)macro.ListMacro[index1].ListStep[index2].Btn);
                    macroByteArray.Add((byte)macro.ListMacro[index1].ListStep[index2].State);
                }
            }
            FLog.d("MacroByteData:" + JsonConvert.SerializeObject((object)macroByteArray));
            return macroByteArray;
        }

        public static MotorBean getMotorByListByte(List<byte> data) => new MotorBean()
        {
            Level = (int)data[0]
        };

        public static LunpanBean getLunpanMappingByListByte(List<byte> data) => new LunpanBean()
        {
            Type = (int)data[0]
        };

        public static AllMacroBean getMacroByListByte(List<byte> data)
        {
            AllMacroBean macroByListByte = new AllMacroBean();
            int num1 = (int)data[0];
            if (num1 < 1 || num1 > 5)
                return macroByListByte;
            for (int index = 0; index < num1; ++index)
            {
                macroByListByte.ListOffset.Add((int)data[index + 1]);
                int num2 = 6;
                int start = num2 + (int)data[index + 1] * 4;
                int end = num2 + (int)data[index + 2] * 4;
                if (index == num1 - 1)
                    end = data.Count;
                FLog.d("getConfigByListByte nums：" + num1.ToString() + " front：" + num2.ToString() + " start：" + start.ToString() + " end：" + end.ToString());
                List<byte> arrayByStartAndEnd = ConfigUtils.getByteArrayByStartAndEnd(data, start, end);
                FLog.d("getConfigByListByte list：" + JsonConvert.SerializeObject((object)arrayByStartAndEnd));
                OneMacroBean macroByByteArray = ConfigUtils.getOneMacroByByteArray(arrayByStartAndEnd);
                FLog.d("getConfigByListByte OneMacro：" + JsonConvert.SerializeObject((object)macroByByteArray));
                if (macroByByteArray.ListStep.Count > 0)
                    macroByListByte.ListMacro.Add(macroByByteArray);
            }
            macroByListByte.Nums = macroByListByte.ListMacro.Count;
            return macroByListByte;
        }

        public static List<byte> getByteArrayByStartAndEnd(List<byte> data, int start, int end)
        {
            List<byte> arrayByStartAndEnd = new List<byte>();
            for (int index = start; index < end; ++index)
                arrayByStartAndEnd.Add(data[index]);
            return arrayByStartAndEnd;
        }

        public static OneMacroBean getOneMacroByByteArray(List<byte> data)
        {
            OneMacroBean macroByByteArray = new OneMacroBean();
            macroByByteArray.Btn = (int)data[0];
            macroByByteArray.Count_l = (int)data[1];
            macroByByteArray.Count_h = (int)data[2];
            macroByByteArray.Type = (int)data[3];
            for (int index1 = 0; index1 < (int)data[1]; ++index1)
            {
                int index2 = 4 + 4 * index1;
                macroByByteArray.ListStep.Add(new MacroActionBean()
                {
                    Time_l = data[index2],
                    Time_h = data[index2 + 1],
                    Btn = (int)data[index2 + 2],
                    State = (int)data[index2 + 3]
                });
            }
            return macroByByteArray;
        }

        public static ConfigBean getSwitchModeDefaultConfig()
        {
            FLog.d("read default config");
            LedBean ledBean = new LedBean();
            ledBean.Header = 32;
            ledBean.Mode = 0;
            ledBean.Peroid = 30;
            ledBean.Light = 150;
            List<int> collection1 = new List<int>();
            collection1.Add(0);
            collection1.Add(116);
            collection1.Add((int)byte.MaxValue);
            ledBean.RgbColor0.AddRange((IEnumerable<int>)collection1);
            ledBean.RgbColor1.AddRange((IEnumerable<int>)collection1);
            List<int> collection2 = new List<int>();
            for (int index = 0; index < 32; ++index)
            {
                int num = ConfigUtils.switchModeConvertKeyId(index, index, true);
                collection2.Add(num);
            }
            OneJoystickBean oneJoystickBean1 = new OneJoystickBean();
            oneJoystickBean1.Zero = 0;
            oneJoystickBean1.Point0_X = 64;
            oneJoystickBean1.Point0_Y = 64;
            oneJoystickBean1.Point1_X = (int)sbyte.MaxValue;
            oneJoystickBean1.Point1_Y = (int)sbyte.MaxValue;
            OneJoystickBean oneJoystickBean2 = new OneJoystickBean();
            oneJoystickBean2.Zero = 0;
            oneJoystickBean2.Point0_X = 64;
            oneJoystickBean2.Point0_Y = 64;
            oneJoystickBean2.Point1_X = (int)sbyte.MaxValue;
            oneJoystickBean2.Point1_Y = (int)sbyte.MaxValue;
            JoyMappingBean joyMappingBean = new JoyMappingBean();
            joyMappingBean.LeftJoyStick = oneJoystickBean1;
            joyMappingBean.RightJoyStick = oneJoystickBean2;
            MotionMappingBean motionMappingBean = new MotionMappingBean();
            motionMappingBean.Type = 0;
            motionMappingBean.KeyId = 12;
            motionMappingBean.Method = 0;
            motionMappingBean.Zero = 0;
            motionMappingBean.Sensity = 25;
            MacroActionBean macroActionBean1 = new MacroActionBean();
            macroActionBean1.Time_l = (byte)0;
            macroActionBean1.Time_h = (byte)0;
            macroActionBean1.Btn = 5;
            macroActionBean1.State = 1;
            MacroActionBean macroActionBean2 = new MacroActionBean();
            macroActionBean2.Time_l = (byte)10;
            macroActionBean2.Time_h = (byte)0;
            macroActionBean2.Btn = 4;
            macroActionBean2.State = 1;
            MacroActionBean macroActionBean3 = new MacroActionBean();
            macroActionBean3.Time_l = (byte)20;
            macroActionBean3.Time_h = (byte)0;
            macroActionBean3.Btn = 4;
            macroActionBean3.State = 0;
            MacroActionBean macroActionBean4 = new MacroActionBean();
            macroActionBean4.Time_l = (byte)30;
            macroActionBean4.Time_h = (byte)0;
            macroActionBean4.Btn = 5;
            macroActionBean4.State = 0;
            OneMacroBean oneMacroBean1 = new OneMacroBean()
            {
                Btn = 16,
                Count_l = 4,
                Count_h = 0,
                Type = 1,
                ListStep = {
          macroActionBean1,
          macroActionBean2,
          macroActionBean3,
          macroActionBean4
        }
            };
            OneMacroBean oneMacroBean2 = new OneMacroBean()
            {
                Btn = 17,
                Count_l = 4,
                Count_h = 0,
                Type = 1,
                ListStep = {
          macroActionBean1,
          macroActionBean2,
          macroActionBean3,
          macroActionBean4
        }
            };
            AllMacroBean allMacroBean = new AllMacroBean();
            allMacroBean.Nums = 0;
            allMacroBean.ListOffset.Add(0);
            allMacroBean.ListOffset.Add(0);
            allMacroBean.ListOffset.Add(0);
            allMacroBean.ListOffset.Add(0);
            allMacroBean.ListOffset.Add(0);
            MotorBean motorBean = new MotorBean();
            LunpanBean lunpanBean = new LunpanBean();
            ConfigBean modeDefaultConfig = new ConfigBean();
            modeDefaultConfig.Version = "0.1";
            modeDefaultConfig.Name = "我的默认配置";
            modeDefaultConfig.Led = ledBean;
            modeDefaultConfig.ListKeyMapping.AddRange((IEnumerable<int>)collection2);
            modeDefaultConfig.JoyMapping = joyMappingBean;
            modeDefaultConfig.MotionMapping = motionMappingBean;
            modeDefaultConfig.Macro = allMacroBean;
            modeDefaultConfig.Motor = motorBean;
            modeDefaultConfig.LunpanMapping = lunpanBean;
            return modeDefaultConfig;
        }

        public static ConfigBean getDefaultConfig()
        {
            FLog.d("read default config");
            LedBean ledBean = new LedBean();
            ledBean.Header = 32;
            ledBean.Mode = 0;
            ledBean.Peroid = 30;
            ledBean.Light = 150;
            List<int> collection1 = new List<int>();
            collection1.Add(0);
            collection1.Add(116);
            collection1.Add((int)byte.MaxValue);
            ledBean.RgbColor0.AddRange((IEnumerable<int>)collection1);
            ledBean.RgbColor1.AddRange((IEnumerable<int>)collection1);
            List<int> collection2 = new List<int>();
            for (int index = 0; index < 32; ++index)
            {
                int num = ConfigUtils.convertKeyId(index, index, true);
                collection2.Add(num);
            }
            OneJoystickBean oneJoystickBean1 = new OneJoystickBean();
            oneJoystickBean1.Zero = 0;
            oneJoystickBean1.Point0_X = 64;
            oneJoystickBean1.Point0_Y = 64;
            oneJoystickBean1.Point1_X = (int)sbyte.MaxValue;
            oneJoystickBean1.Point1_Y = (int)sbyte.MaxValue;
            OneJoystickBean oneJoystickBean2 = new OneJoystickBean();
            oneJoystickBean2.Zero = 0;
            oneJoystickBean2.Point0_X = 64;
            oneJoystickBean2.Point0_Y = 64;
            oneJoystickBean2.Point1_X = (int)sbyte.MaxValue;
            oneJoystickBean2.Point1_Y = (int)sbyte.MaxValue;
            JoyMappingBean joyMappingBean = new JoyMappingBean();
            joyMappingBean.LeftJoyStick = oneJoystickBean1;
            joyMappingBean.RightJoyStick = oneJoystickBean2;
            MotionMappingBean motionMappingBean = new MotionMappingBean();
            motionMappingBean.Type = 0;
            motionMappingBean.KeyId = 12;
            motionMappingBean.Method = 0;
            motionMappingBean.Zero = 0;
            motionMappingBean.Sensity = 25;
            MacroActionBean macroActionBean1 = new MacroActionBean();
            macroActionBean1.Time_l = (byte)0;
            macroActionBean1.Time_h = (byte)0;
            macroActionBean1.Btn = 5;
            macroActionBean1.State = 1;
            MacroActionBean macroActionBean2 = new MacroActionBean();
            macroActionBean2.Time_l = (byte)10;
            macroActionBean2.Time_h = (byte)0;
            macroActionBean2.Btn = 4;
            macroActionBean2.State = 1;
            MacroActionBean macroActionBean3 = new MacroActionBean();
            macroActionBean3.Time_l = (byte)20;
            macroActionBean3.Time_h = (byte)0;
            macroActionBean3.Btn = 4;
            macroActionBean3.State = 0;
            MacroActionBean macroActionBean4 = new MacroActionBean();
            macroActionBean4.Time_l = (byte)30;
            macroActionBean4.Time_h = (byte)0;
            macroActionBean4.Btn = 5;
            macroActionBean4.State = 0;
            OneMacroBean oneMacroBean1 = new OneMacroBean()
            {
                Btn = 16,
                Count_l = 4,
                Count_h = 0,
                Type = 1,
                ListStep = {
          macroActionBean1,
          macroActionBean2,
          macroActionBean3,
          macroActionBean4
        }
            };
            OneMacroBean oneMacroBean2 = new OneMacroBean()
            {
                Btn = 17,
                Count_l = 4,
                Count_h = 0,
                Type = 1,
                ListStep = {
          macroActionBean1,
          macroActionBean2,
          macroActionBean3,
          macroActionBean4
        }
            };
            AllMacroBean allMacroBean = new AllMacroBean();
            allMacroBean.Nums = 0;
            allMacroBean.ListOffset.Add(0);
            allMacroBean.ListOffset.Add(0);
            allMacroBean.ListOffset.Add(0);
            allMacroBean.ListOffset.Add(0);
            allMacroBean.ListOffset.Add(0);
            MotorBean motorBean = new MotorBean();
            LunpanBean lunpanBean = new LunpanBean();
            ConfigBean defaultConfig = new ConfigBean();
            defaultConfig.Version = "0.1";
            defaultConfig.Name = "我的默认配置";
            defaultConfig.Led = ledBean;
            defaultConfig.ListKeyMapping.AddRange((IEnumerable<int>)collection2);
            defaultConfig.JoyMapping = joyMappingBean;
            defaultConfig.MotionMapping = motionMappingBean;
            defaultConfig.Macro = allMacroBean;
            defaultConfig.Motor = motorBean;
            defaultConfig.LunpanMapping = lunpanBean;
            return defaultConfig;
        }

        public static List<List<byte>> getDoubleListByConfig(ConfigBean config)
        {
            List<byte> byteList = new List<byte>();
            byteList.AddRange((IEnumerable<byte>)ConfigUtils.getConfigVersionByteArray(config.Version));
            FLog.d("AVer:" + CommonUtils.byteToHexString(byteList.ToArray()));
            byteList.AddRange((IEnumerable<byte>)ConfigUtils.getConfigNameByteArray(config.Name));
            FLog.d("ACfg:" + CommonUtils.byteToHexString(byteList.ToArray()));
            byteList.AddRange((IEnumerable<byte>)ConfigUtils.getLedByteArray(config.Led));
            FLog.d("ALed:" + CommonUtils.byteToHexString(byteList.ToArray()));
            byteList.AddRange((IEnumerable<byte>)ConfigUtils.getKeyMappingByteArray(config.ListKeyMapping));
            FLog.d("KMap:" + CommonUtils.byteToHexString(byteList.ToArray()));
            byteList.AddRange((IEnumerable<byte>)ConfigUtils.getJoyMappingByteArray(config.JoyMapping));
            FLog.d("JMap:" + CommonUtils.byteToHexString(byteList.ToArray()));
            byteList.AddRange((IEnumerable<byte>)ConfigUtils.getJoyMappingByteArray(config.JoyMapping));
            FLog.d("JMap:" + CommonUtils.byteToHexString(byteList.ToArray()));
            byteList.AddRange((IEnumerable<byte>)ConfigUtils.getMotionMappingMotorLunpanByteArray(config.MotionMapping, config.Motor, config.LunpanMapping));
            FLog.d("MMap:" + CommonUtils.byteToHexString(byteList.ToArray()));
            byteList.AddRange((IEnumerable<byte>)ConfigUtils.getMacroByteArray(config.Macro));
            return CommonUtils.getDoubleList(byteList.ToArray());
        }

        public static void updateKeyMappingList(List<int> listKeyMapping, int currentId, int targetId) => listKeyMapping[DeviceUtils.getByteIndexByID(currentId)] = targetId;

        public static void checkKeyMappingListRemoveByKeyId(List<int> listKeyMapping, int keyId) => listKeyMapping[DeviceUtils.getByteIndexByID(keyId)] = keyId;

        public static void updateListMacro(List<OneMacroBean> listMacro, OneMacroBean oneMacro)
        {
            for (int index = 0; index < listMacro.Count; ++index)
            {
                if (listMacro[index].Btn == oneMacro.Btn)
                {
                    listMacro[index].Type = oneMacro.Type;
                    listMacro[index].Count_h = oneMacro.Count_h;
                    listMacro[index].Count_l = oneMacro.Count_l;
                    listMacro[index].ListStep.Clear();
                    listMacro[index].ListStep.AddRange((IEnumerable<MacroActionBean>)oneMacro.ListStep);
                    return;
                }
            }
            listMacro.Add(oneMacro);
        }

        public static void checkMacroListRemoveByKeyId(List<OneMacroBean> listMacro, int keyId)
        {
            int index1 = -1;
            for (int index2 = 0; index2 < listMacro.Count; ++index2)
            {
                if (listMacro[index2].Btn == keyId)
                    index1 = index2;
            }
            if (index1 <= -1)
                return;
            listMacro.RemoveAt(index1);
        }

        public static OneMacroBean checkMacroListHasValueKeyId(
          List<OneMacroBean> listMacro,
          int keyId)
        {
            for (int index = 0; index < listMacro.Count; ++index)
            {
                if (listMacro[index].Btn == keyId)
                    return listMacro[index];
            }
            return (OneMacroBean)null;
        }

        public static ConfigBean getConfigByListByte(List<byte> listByte)
        {
            FLog.d("getConfigByListByte_listByte：" + JsonConvert.SerializeObject((object)listByte));
            ConfigBean configByListByte = new ConfigBean();
            if (listByte[0] != (byte)0 || listByte[1] != (byte)1)
            {
                FLog.d("配置版本不为0.1,转为默认配置");
                return ConfigUtils.getDefaultConfig();
            }
            List<byte> data1 = new List<byte>();
            List<byte> data2 = new List<byte>();
            List<byte> data3 = new List<byte>();
            List<byte> data4 = new List<byte>();
            List<byte> data5 = new List<byte>();
            List<byte> data6 = new List<byte>();
            List<byte> data7 = new List<byte>();
            List<byte> data8 = new List<byte>();
            List<byte> data9 = new List<byte>();
            List<byte> byteList = new List<byte>();
            for (int index = 0; index < listByte.Count; ++index)
            {
                if (0 <= index && index <= 1)
                    data1.Add(listByte[index]);
                else if (2 <= index && index <= 13)
                    data2.Add(listByte[index]);
                else if (14 <= index && index <= 23)
                    data3.Add(listByte[index]);
                else if (24 <= index && index <= 55)
                    data4.Add(listByte[index]);
                else if (56 <= index && index <= 65)
                    data5.Add(listByte[index]);
                else if (76 <= index && index <= 80)
                    data6.Add(listByte[index]);
                else if (81 <= index && index <= 82)
                    data6.Add(listByte[index]);
                else if (83 <= index && index <= 83)
                    data8.Add(listByte[index]);
                else if (84 <= index && index <= 85)
                    data9.Add(listByte[index]);
                else if (86 <= index && index <= listByte.Count - 1)
                    data7.Add(listByte[index]);
            }
            configByListByte.Version = ConfigUtils.getConfigVersionByListByte(data1);
            FLog.d("getConfigByListByte Version：" + JsonConvert.SerializeObject((object)configByListByte.Version));
            configByListByte.Name = ConfigUtils.getConfigNameByListByte(data2);
            FLog.d("getConfigByListByte Name：" + JsonConvert.SerializeObject((object)configByListByte.Name));
            configByListByte.Led = ConfigUtils.getLedByListByte(data3);
            FLog.d("getConfigByListByte Led：" + JsonConvert.SerializeObject((object)configByListByte.Led));
            configByListByte.ListKeyMapping.AddRange((IEnumerable<int>)ConfigUtils.getKeyMappingByListByte(data4));
            FLog.d("getConfigByListByte ListKeyMapping：" + JsonConvert.SerializeObject((object)configByListByte.ListKeyMapping));
            configByListByte.JoyMapping = ConfigUtils.getJoyMappingByListByte(data5);
            FLog.d("getConfigByListByte JoyMapping：" + JsonConvert.SerializeObject((object)configByListByte.JoyMapping));
            configByListByte.MotionMapping = ConfigUtils.getMotionMappingByListByte(data6);
            FLog.d("getConfigByListByte MotionMapping：" + JsonConvert.SerializeObject((object)configByListByte.MotionMapping));
            configByListByte.Macro = ConfigUtils.getMacroByListByte(data7);
            FLog.d("getConfigByListByte Macro：" + JsonConvert.SerializeObject((object)configByListByte.Macro));
            configByListByte.Motor = ConfigUtils.getMotorByListByte(data8);
            configByListByte.LunpanMapping = ConfigUtils.getLunpanMappingByListByte(data9);
            return configByListByte;
        }

        public static List<byte> getRealByteFromCallback(byte[] data)
        {
            List<byte> byteFromCallback = new List<byte>();
            for (int index = 0; index < data.Length; ++index)
            {
                if (5 <= index && index <= 14)
                    byteFromCallback.Add(data[index]);
            }
            return byteFromCallback;
        }

        public static void ApplyConvertReview(List<byte> list, int count)
        {
            list[1] = (byte)239;
            list[2] = (byte)0;
            list[3] = (byte)count;
        }
    }
}
