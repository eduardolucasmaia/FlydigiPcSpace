// Decompiled with JetBrains decompiler
// Type: WPFTest.UserControls.UserControlSettingConfigKeyDisplay
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
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFTest.UserControls
{
    public class UserControlSettingConfigKeyDisplay : UserControl, IComponentConnector
    {
        private Dictionary<int, Image> mDictionaryOutside = new Dictionary<int, Image>();
        private Dictionary<int, Image> mDictionaryInside = new Dictionary<int, Image>();
        private const int FRONT_LEFT = 0;
        private const int FRONT_RIGHT = 1;
        private const int AFTER_LEFT = 2;
        private const int AFTER_RIGHT = 3;
        private const int TOP_LEFT = 4;
        private const int TOP_RIGHT = 5;
        internal Grid mDisplayOutside;
        internal Label mLabelTop;
        internal Label mLabelFront;
        internal Label mLabelAfter;
        internal Grid mLayoutFront;
        internal ImageBrush mImgFront;
        internal Image mKeyUP;
        internal Image mKeyRight;
        internal Image mKeyLeft;
        internal Image mKeyDown;
        internal Image mKeyA;
        internal Image mKeyB;
        internal Image mKeyX;
        internal Image mKeyY;
        internal Image mKeyC;
        internal Image mKeyZ;
        internal Image mKeySelect;
        internal Image mKeyStart;
        internal Image mKeyL3;
        internal Image mKeyR3;
        internal Grid mLayoutAfter;
        internal ImageBrush mImgAfter;
        internal Image mKeyM1;
        internal Image mKeyM2;
        internal Image mKeyM3;
        internal Image mKeyM4;
        internal Grid mLayoutTop;
        internal ImageBrush mImgTop;
        internal Image mKeyRB;
        internal Image mKeyRT;
        internal Image mKeyLB;
        internal Image mKeyLT;
        internal Image mKeyM5;
        internal Image mKeyM6;
        internal Grid mBubble;
        internal Label mLabelCurrent;
        internal Image mImageCurrent;
        internal Label mLabelTarget;
        internal Image mImageTarget;
        internal Grid mDisplayInside;
        internal Grid mLayoutFrontLeft;
        internal Image mImgFrontLeft;
        internal Image mInsideKeyUP;
        internal Image mInsideKeyRight;
        internal Image mInsideKeyLeft;
        internal Image mInsideKeyDown;
        internal Image mInsideKeySelect;
        internal Image mInsideKeyL3;
        internal Grid mLayoutFrontRight;
        internal Image mImgFrontRight;
        internal Image mInsideKeyA;
        internal Image mInsideKeyB;
        internal Image mInsideKeyX;
        internal Image mInsideKeyY;
        internal Image mInsideKeyC;
        internal Image mInsideKeyZ;
        internal Image mInsideKeyStart;
        internal Image mInsideKeyR3;
        internal Grid mLayoutAfterLeft;
        internal Image mImgAfterLeft;
        internal Image mInsideKeyM1;
        internal Image mInsideKeyM3;
        internal Grid mLayoutAfterRight;
        internal Image mImgAfterRight;
        internal Image mInsideKeyM2;
        internal Image mInsideKeyM4;
        internal Grid mLayoutTopLeft;
        internal Image mImgTopLeft;
        internal Image mInsideKeyRB;
        internal Image mInsideKeyRT;
        internal Image mInsideKeyM5;
        internal Grid mLayoutTopRight;
        internal Image mImgTopRight;
        internal Image mInsideKeyLB;
        internal Image mInsideKeyLT;
        internal Image mInsideKeyM6;
        private bool _contentLoaded;

        public UserControlSettingConfigKeyDisplay()
        {
            this.InitializeComponent();
            this.mDictionaryOutside.Add(0, this.mKeyUP);
            this.mDictionaryOutside.Add(1, this.mKeyRight);
            this.mDictionaryOutside.Add(3, this.mKeyLeft);
            this.mDictionaryOutside.Add(2, this.mKeyDown);
            this.mDictionaryOutside.Add(4, this.mKeyA);
            this.mDictionaryOutside.Add(5, this.mKeyB);
            this.mDictionaryOutside.Add(7, this.mKeyX);
            this.mDictionaryOutside.Add(8, this.mKeyY);
            this.mDictionaryOutside.Add(16, this.mKeyC);
            this.mDictionaryOutside.Add(17, this.mKeyZ);
            this.mDictionaryOutside.Add(6, this.mKeySelect);
            this.mDictionaryOutside.Add(9, this.mKeyStart);
            this.mDictionaryOutside.Add(14, this.mKeyL3);
            this.mDictionaryOutside.Add(15, this.mKeyR3);
            this.mDictionaryOutside.Add(18, this.mKeyM1);
            this.mDictionaryOutside.Add(19, this.mKeyM2);
            this.mDictionaryOutside.Add(20, this.mKeyM3);
            this.mDictionaryOutside.Add(21, this.mKeyM4);
            this.mDictionaryOutside.Add(11, this.mKeyRB);
            this.mDictionaryOutside.Add(13, this.mKeyRT);
            this.mDictionaryOutside.Add(10, this.mKeyLB);
            this.mDictionaryOutside.Add(12, this.mKeyLT);
            this.mDictionaryOutside.Add(22, this.mKeyM5);
            this.mDictionaryOutside.Add(23, this.mKeyM6);
            foreach (KeyValuePair<int, Image> keyValuePair in this.mDictionaryOutside)
                keyValuePair.Value.Visibility = Visibility.Hidden;
            this.mDictionaryInside.Add(0, this.mInsideKeyUP);
            this.mDictionaryInside.Add(1, this.mInsideKeyRight);
            this.mDictionaryInside.Add(3, this.mInsideKeyLeft);
            this.mDictionaryInside.Add(2, this.mInsideKeyDown);
            this.mDictionaryInside.Add(4, this.mInsideKeyA);
            this.mDictionaryInside.Add(5, this.mInsideKeyB);
            this.mDictionaryInside.Add(7, this.mInsideKeyX);
            this.mDictionaryInside.Add(8, this.mInsideKeyY);
            this.mDictionaryInside.Add(16, this.mInsideKeyC);
            this.mDictionaryInside.Add(17, this.mInsideKeyZ);
            this.mDictionaryInside.Add(6, this.mInsideKeySelect);
            this.mDictionaryInside.Add(9, this.mInsideKeyStart);
            this.mDictionaryInside.Add(14, this.mInsideKeyL3);
            this.mDictionaryInside.Add(15, this.mInsideKeyR3);
            this.mDictionaryInside.Add(18, this.mInsideKeyM1);
            this.mDictionaryInside.Add(19, this.mInsideKeyM2);
            this.mDictionaryInside.Add(20, this.mInsideKeyM3);
            this.mDictionaryInside.Add(21, this.mInsideKeyM4);
            this.mDictionaryInside.Add(11, this.mInsideKeyRB);
            this.mDictionaryInside.Add(13, this.mInsideKeyRT);
            this.mDictionaryInside.Add(10, this.mInsideKeyLB);
            this.mDictionaryInside.Add(12, this.mInsideKeyLT);
            this.mDictionaryInside.Add(22, this.mInsideKeyM5);
            this.mDictionaryInside.Add(23, this.mInsideKeyM6);
            foreach (KeyValuePair<int, Image> keyValuePair in this.mDictionaryInside)
                keyValuePair.Value.Visibility = Visibility.Hidden;
            this.updateUI();
        }

        private void updateUI()
        {
            string gameHadleName = DataManager.getInstance().getDeviceInfo().gameHadleName;
            if (!(gameHadleName == "f1") && !(gameHadleName == "f1wch"))
            {
                if (!(gameHadleName == "apex2"))
                {
                    if (!(gameHadleName == "f1p"))
                        return;
                    this.mImgFront.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_front.png"));
                    this.mImgAfter.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_after.png"));
                    this.mImgTop.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_top.png"));
                    this.mImgFrontLeft.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_front_left.png"));
                    this.mImgFrontRight.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_front_right.png"));
                    this.mImgAfterLeft.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_after_left.png"));
                    this.mImgAfterRight.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_after_right.png"));
                    this.mImgTopLeft.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_top_left.png"));
                    this.mImgTopRight.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1p_top_right.png"));
                    this.mKeySelect.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_select.png"));
                    this.mKeyStart.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_start.png"));
                    this.mInsideKeySelect.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_select.png"));
                    this.mInsideKeyStart.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_start.png"));
                    this.mKeyUP.Margin = new Thickness(179.0, 141.0, 0.0, 0.0);
                    this.mKeyRight.Margin = new Thickness(207.0, 170.0, 0.0, 0.0);
                    this.mKeyLeft.Margin = new Thickness(150.0, 170.0, 0.0, 0.0);
                    this.mKeyDown.Margin = new Thickness(179.0, 199.0, 0.0, 0.0);
                    this.mKeyA.Margin = new Thickness(371.0, 123.0, 0.0, 0.0);
                    this.mKeyB.Margin = new Thickness(404.0, 89.0, 0.0, 0.0);
                    this.mKeyX.Margin = new Thickness(336.0, 89.0, 0.0, 0.0);
                    this.mKeyY.Margin = new Thickness(371.0, 55.0, 0.0, 0.0);
                    this.mKeyC.Margin = new Thickness(392.0, 176.0, 0.0, 0.0);
                    this.mKeyZ.Margin = new Thickness(425.0, 147.0, 0.0, 0.0);
                    this.mKeySelect.Margin = new Thickness(186.0, 51.0, 0.0, 0.0);
                    this.mKeyStart.Margin = new Thickness(298.0, 51.0, 0.0, 0.0);
                    this.mKeyL3.Margin = new Thickness(98.0, 72.0, 0.0, 0.0);
                    this.mKeyR3.Margin = new Thickness(290.0, 152.0, 0.0, 0.0);
                    this.mKeyM1.Margin = new Thickness(158.0, 206.0, 0.0, 0.0);
                    this.mKeyM2.Margin = new Thickness(421.0, 206.0, 0.0, 0.0);
                    this.mKeyM3.Margin = new Thickness(222.0, 200.0, 0.0, 0.0);
                    this.mKeyM4.Margin = new Thickness(355.0, 200.0, 0.0, 0.0);
                    this.mKeyRB.Margin = new Thickness(75.0, 50.0, 0.0, 0.0);
                    this.mKeyRT.Margin = new Thickness(70.0, 104.0, 0.0, 0.0);
                    this.mKeyLB.Margin = new Thickness(379.0, 50.0, 0.0, 0.0);
                    this.mKeyLT.Margin = new Thickness(434.0, 104.0, 0.0, 0.0);
                    this.mKeyM5.Margin = new Thickness(173.0, 122.0, 0.0, 0.0);
                    this.mKeyM6.Margin = new Thickness(366.0, 125.0, 0.0, 0.0);
                    this.mInsideKeyUP.Margin = new Thickness(469.0, 320.0, 196.0, 242.0);
                    this.mInsideKeyRight.Margin = new Thickness(562.0, 399.0, 133.0, 168.0);
                    this.mInsideKeyLeft.Margin = new Thickness(402.0, 387.0, 289.0, 151.0);
                    this.mInsideKeyDown.Margin = new Thickness(461.0, 472.0, 189.0, 89.0);
                    this.mInsideKeySelect.Margin = new Thickness(493.0, 65.0, 160.0, 490.0);
                    this.mInsideKeyL3.Margin = new Thickness(263.0, 129.0, 342.0, 345.0);
                    this.mInsideKeyA.Margin = new Thickness(422.0, 307.0, 233.0, 249.0);
                    this.mInsideKeyB.Margin = new Thickness(533.0, 212.0, 145.0, 357.0);
                    this.mInsideKeyX.Margin = new Thickness(324.0, 210.0, 334.0, 355.0);
                    this.mInsideKeyY.Margin = new Thickness(444.0, 109.0, 254.0, 449.0);
                    this.mInsideKeyC.Margin = new Thickness(500.0, 458.0, 196.0, 89.0);
                    this.mInsideKeyZ.Margin = new Thickness(594.0, 385.0, 96.0, 178.0);
                    this.mInsideKeyStart.Margin = new Thickness(237.0, 87.0, 472.0, 456.0);
                    this.mInsideKeyR3.Margin = new Thickness(200.0, 398.0, 394.0, 73.0);
                    this.mInsideKeyM1.Margin = new Thickness(304.0, 278.0, 366.0, 208.0);
                    this.mInsideKeyM3.Margin = new Thickness(457.0, 263.0, 188.0, 281.0);
                    this.mInsideKeyM2.Margin = new Thickness(529.0, 284.0, 147.0, 202.0);
                    this.mInsideKeyM4.Margin = new Thickness(352.0, 269.0, 304.0, 275.0);
                    this.mInsideKeyRB.Margin = new Thickness(210.0, 213.0, 293.0, 316.0);
                    this.mInsideKeyRT.Margin = new Thickness(211.0, 309.0, 391.0, 140.0);
                    this.mInsideKeyM5.Margin = new Thickness(408.0, 355.0, 252.0, 216.0);
                    this.mInsideKeyLB.Margin = new Thickness(403.0, 217.0, 126.0, 317.0);
                    this.mInsideKeyLT.Margin = new Thickness(505.0, 308.0, 120.0, 143.0);
                    this.mInsideKeyM6.Margin = new Thickness(402.0, 343.0, 281.0, 238.0);
                }
                else
                {
                    this.mImgFront.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_front.png"));
                    this.mImgAfter.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_after.png"));
                    this.mImgTop.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_top.png"));
                    this.mImgFrontLeft.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_front_left.png"));
                    this.mImgFrontRight.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_front_right.png"));
                    this.mImgAfterLeft.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_after_left.png"));
                    this.mImgAfterRight.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_after_right.png"));
                    this.mImgTopLeft.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_top_left.png"));
                    this.mImgTopRight.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_apex2_top_right.png"));
                    this.mKeySelect.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_select.png"));
                    this.mKeyStart.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_start.png"));
                    this.mInsideKeySelect.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_select.png"));
                    this.mInsideKeyStart.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_key_start.png"));
                    this.mKeyUP.Margin = new Thickness(176.0, 141.0, 0.0, 0.0);
                    this.mKeyRight.Margin = new Thickness(205.0, 170.0, 0.0, 0.0);
                    this.mKeyLeft.Margin = new Thickness(146.0, 170.0, 0.0, 0.0);
                    this.mKeyDown.Margin = new Thickness(175.0, 199.0, 0.0, 0.0);
                    this.mKeyA.Margin = new Thickness(377.0, 123.0, 0.0, 0.0);
                    this.mKeyB.Margin = new Thickness(409.0, 79.0, 0.0, 0.0);
                    this.mKeyX.Margin = new Thickness(346.0, 78.0, 0.0, 0.0);
                    this.mKeyY.Margin = new Thickness(377.0, 48.0, 0.0, 0.0);
                    this.mKeyC.Margin = new Thickness(400.0, 175.0, 0.0, 0.0);
                    this.mKeyZ.Margin = new Thickness(435.0, 146.0, 0.0, 0.0);
                    this.mKeySelect.Margin = new Thickness(204.0, 64.0, 0.0, 0.0);
                    this.mKeyStart.Margin = new Thickness(290.0, 64.0, 0.0, 0.0);
                    this.mKeyL3.Margin = new Thickness(88.0, 59.0, 0.0, 0.0);
                    this.mKeyR3.Margin = new Thickness(296.0, 144.0, 0.0, 0.0);
                    this.mKeyM1.Margin = new Thickness(160.0, 188.0, 0.0, 0.0);
                    this.mKeyM2.Margin = new Thickness(420.0, 186.0, 0.0, 0.0);
                    this.mKeyM3.Margin = new Thickness(220.0, 177.0, 0.0, 0.0);
                    this.mKeyM4.Margin = new Thickness(357.0, 177.0, 0.0, 0.0);
                    this.mKeyRB.Margin = new Thickness(84.0, 67.0, 0.0, 0.0);
                    this.mKeyRT.Margin = new Thickness(86.0, 112.0, 0.0, 0.0);
                    this.mKeyLB.Margin = new Thickness(370.0, 67.0, 0.0, 0.0);
                    this.mKeyLT.Margin = new Thickness(418.0, 112.0, 0.0, 0.0);
                    this.mKeyM5.Margin = new Thickness(173.0, 122.0, 0.0, 0.0);
                    this.mKeyM6.Margin = new Thickness(366.0, 125.0, 0.0, 0.0);
                    this.mInsideKeyUP.Margin = new Thickness(473.0, 280.0, 192.0, 282.0);
                    this.mInsideKeyRight.Margin = new Thickness(561.0, 360.0, 134.0, 207.0);
                    this.mInsideKeyLeft.Margin = new Thickness(410.0, 345.0, 281.0, 193.0);
                    this.mInsideKeyDown.Margin = new Thickness(461.0, 431.0, 189.0, 130.0);
                    this.mInsideKeySelect.Margin = new Thickness(535.0, 82.0, 118.0, 473.0);
                    this.mInsideKeyL3.Margin = new Thickness(256.0, 67.0, 349.0, 407.0);
                    this.mInsideKeyA.Margin = new Thickness(455.0, 345.0, 200.0, 211.0);
                    this.mInsideKeyB.Margin = new Thickness(547.0, 243.0, 131.0, 326.0);
                    this.mInsideKeyX.Margin = new Thickness(377.0, 236.0, 281.0, 329.0);
                    this.mInsideKeyY.Margin = new Thickness(478.0, 153.0, 220.0, 405.0);
                    this.mInsideKeyC.Margin = new Thickness(535.0, 471.0, 161.0, 76.0);
                    this.mInsideKeyZ.Margin = new Thickness(618.0, 406.0, 72.0, 157.0);
                    this.mInsideKeyStart.Margin = new Thickness(249.0, 190.0, 460.0, 353.0);
                    this.mInsideKeyR3.Margin = new Thickness(256.0, 398.0, 338.0, 73.0);
                    this.mInsideKeyM1.Margin = new Thickness(328.0, 280.0, 342.0, 206.0);
                    this.mInsideKeyM3.Margin = new Thickness(472.0, 256.0, 173.0, 288.0);
                    this.mInsideKeyM2.Margin = new Thickness(520.0, 276.0, 156.0, 210.0);
                    this.mInsideKeyM4.Margin = new Thickness(358.0, 258.0, 298.0, 286.0);
                    this.mInsideKeyRB.Margin = new Thickness(240.0, 235.0, 263.0, 294.0);
                    this.mInsideKeyRT.Margin = new Thickness(237.0, 329.0, 365.0, 120.0);
                    this.mInsideKeyM5.Margin = new Thickness(408.0, 355.0, 252.0, 216.0);
                    this.mInsideKeyLB.Margin = new Thickness(406.0, 238.0, 123.0, 296.0);
                    this.mInsideKeyLT.Margin = new Thickness(502.0, 315.0, 123.0, 136.0);
                    this.mInsideKeyM6.Margin = new Thickness(402.0, 343.0, 281.0, 238.0);
                }
            }
            else
            {
                this.mImgFront.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_front.png"));
                this.mImgAfter.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_after.png"));
                this.mImgTop.ImageSource = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_top.png"));
                this.mImgFrontLeft.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_front_left.png"));
                this.mImgFrontRight.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_front_right.png"));
                this.mImgAfterLeft.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_after_left.png"));
                this.mImgAfterRight.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_after_right.png"));
                this.mImgTopLeft.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_top_left.png"));
                this.mImgTopRight.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_top_right.png"));
                this.mKeySelect.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_select.png"));
                this.mKeyStart.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_start.png"));
                this.mInsideKeySelect.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_select.png"));
                this.mInsideKeyStart.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/img_f1_key_start.png"));
                this.mKeyUP.Margin = new Thickness(179.0, 141.0, 0.0, 0.0);
                this.mKeyRight.Margin = new Thickness(207.0, 170.0, 0.0, 0.0);
                this.mKeyLeft.Margin = new Thickness(150.0, 170.0, 0.0, 0.0);
                this.mKeyDown.Margin = new Thickness(179.0, 199.0, 0.0, 0.0);
                this.mKeyA.Margin = new Thickness(371.0, 123.0, 0.0, 0.0);
                this.mKeyB.Margin = new Thickness(404.0, 89.0, 0.0, 0.0);
                this.mKeyX.Margin = new Thickness(336.0, 89.0, 0.0, 0.0);
                this.mKeyY.Margin = new Thickness(371.0, 55.0, 0.0, 0.0);
                this.mKeyC.Margin = new Thickness(392.0, 176.0, 0.0, 0.0);
                this.mKeyZ.Margin = new Thickness(425.0, 147.0, 0.0, 0.0);
                this.mKeySelect.Margin = new Thickness(186.0, 51.0, 0.0, 0.0);
                this.mKeyStart.Margin = new Thickness(298.0, 51.0, 0.0, 0.0);
                this.mKeyL3.Margin = new Thickness(98.0, 72.0, 0.0, 0.0);
                this.mKeyR3.Margin = new Thickness(290.0, 152.0, 0.0, 0.0);
                this.mKeyM1.Margin = new Thickness(158.0, 206.0, 0.0, 0.0);
                this.mKeyM2.Margin = new Thickness(421.0, 206.0, 0.0, 0.0);
                this.mKeyM3.Margin = new Thickness(222.0, 200.0, 0.0, 0.0);
                this.mKeyM4.Margin = new Thickness(355.0, 200.0, 0.0, 0.0);
                this.mKeyRB.Margin = new Thickness(75.0, 50.0, 0.0, 0.0);
                this.mKeyRT.Margin = new Thickness(70.0, 104.0, 0.0, 0.0);
                this.mKeyLB.Margin = new Thickness(379.0, 50.0, 0.0, 0.0);
                this.mKeyLT.Margin = new Thickness(434.0, 104.0, 0.0, 0.0);
                this.mKeyM5.Margin = new Thickness(173.0, 122.0, 0.0, 0.0);
                this.mKeyM6.Margin = new Thickness(366.0, 125.0, 0.0, 0.0);
                this.mInsideKeyUP.Margin = new Thickness(469.0, 320.0, 196.0, 242.0);
                this.mInsideKeyRight.Margin = new Thickness(562.0, 399.0, 133.0, 168.0);
                this.mInsideKeyLeft.Margin = new Thickness(402.0, 387.0, 289.0, 151.0);
                this.mInsideKeyDown.Margin = new Thickness(461.0, 472.0, 189.0, 89.0);
                this.mInsideKeySelect.Margin = new Thickness(493.0, 65.0, 160.0, 490.0);
                this.mInsideKeyL3.Margin = new Thickness(263.0, 129.0, 342.0, 345.0);
                this.mInsideKeyA.Margin = new Thickness(422.0, 307.0, 233.0, 249.0);
                this.mInsideKeyB.Margin = new Thickness(533.0, 212.0, 145.0, 357.0);
                this.mInsideKeyX.Margin = new Thickness(324.0, 210.0, 334.0, 355.0);
                this.mInsideKeyY.Margin = new Thickness(444.0, 109.0, 254.0, 449.0);
                this.mInsideKeyC.Margin = new Thickness(500.0, 458.0, 196.0, 89.0);
                this.mInsideKeyZ.Margin = new Thickness(594.0, 385.0, 96.0, 178.0);
                this.mInsideKeyStart.Margin = new Thickness(237.0, 87.0, 472.0, 456.0);
                this.mInsideKeyR3.Margin = new Thickness(200.0, 398.0, 394.0, 73.0);
                this.mInsideKeyM1.Margin = new Thickness(304.0, 278.0, 366.0, 208.0);
                this.mInsideKeyM3.Margin = new Thickness(457.0, 263.0, 188.0, 281.0);
                this.mInsideKeyM2.Margin = new Thickness(529.0, 284.0, 147.0, 202.0);
                this.mInsideKeyM4.Margin = new Thickness(352.0, 269.0, 304.0, 275.0);
                this.mInsideKeyRB.Margin = new Thickness(210.0, 213.0, 293.0, 316.0);
                this.mInsideKeyRT.Margin = new Thickness(211.0, 309.0, 391.0, 140.0);
                this.mInsideKeyM5.Margin = new Thickness(408.0, 355.0, 252.0, 216.0);
                this.mInsideKeyLB.Margin = new Thickness(403.0, 217.0, 126.0, 317.0);
                this.mInsideKeyLT.Margin = new Thickness(505.0, 308.0, 120.0, 143.0);
                this.mInsideKeyM6.Margin = new Thickness(402.0, 343.0, 281.0, 238.0);
            }
        }

        public void updateDisplay(bool outside)
        {
            if (outside)
            {
                this.mDisplayOutside.Visibility = Visibility.Visible;
                this.mDisplayInside.Visibility = Visibility.Hidden;
            }
            else
            {
                this.mDisplayOutside.Visibility = Visibility.Hidden;
                this.mDisplayInside.Visibility = Visibility.Visible;
            }
        }

        private void mLabelFront_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.resetUI();
            this.mLabelFront.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            this.mLayoutFront.Visibility = Visibility.Visible;
        }

        private void mLabelAfter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.resetUI();
            this.mLabelAfter.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            this.mLayoutAfter.Visibility = Visibility.Visible;
        }

        private void mLabelTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.resetUI();
            this.mLabelTop.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#0074ff");
            this.mLayoutTop.Visibility = Visibility.Visible;
        }

        private void resetUI()
        {
            this.mLabelFront.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mLabelAfter.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mLabelTop.Foreground = (Brush)new BrushConverter().ConvertFrom((object)"#ffffff");
            this.mLayoutFront.Visibility = Visibility.Hidden;
            this.mLayoutAfter.Visibility = Visibility.Hidden;
            this.mLayoutTop.Visibility = Visibility.Hidden;
        }

        public void setOutsideFocusKeyId(int position, int keyId, object targetObj, bool focused)
        {
            double num1 = 0.0;
            double num2 = 0.0;
            switch (position)
            {
                case 0:
                    this.mLabelFront_MouseLeftButtonDown((object)null, (MouseButtonEventArgs)null);
                    num1 = (this.Width - this.mLayoutFront.Width) / 2.0;
                    num2 = (this.Height - this.mLayoutFront.Height) / 2.0;
                    break;
                case 1:
                    this.mLabelAfter_MouseLeftButtonDown((object)null, (MouseButtonEventArgs)null);
                    num1 = (this.Width - this.mLayoutAfter.Width) / 2.0;
                    num2 = (this.Height - this.mLayoutAfter.Height) / 2.0;
                    break;
                case 2:
                    this.mLabelTop_MouseLeftButtonDown((object)null, (MouseButtonEventArgs)null);
                    num1 = (this.Width - this.mLayoutTop.Width) / 2.0;
                    num2 = (this.Height - this.mLayoutTop.Height) / 2.0;
                    break;
            }
            if (this.mDictionaryOutside.ContainsKey(keyId))
            {
                this.mDictionaryOutside[keyId].Visibility = focused ? Visibility.Visible : Visibility.Hidden;
                if (focused)
                {
                    this.mBubble.Visibility = Visibility.Visible;
                    Thickness margin = this.mDictionaryOutside[keyId].Margin;
                    double left = margin.Left + this.mDictionaryOutside[keyId].Width / 2.0 + num1 - this.mBubble.Width / 2.0;
                    margin = this.mDictionaryOutside[keyId].Margin;
                    double top = margin.Top + num2 - this.mBubble.Height;
                    this.mBubble.Margin = new Thickness(left, top, 0.0, 0.0);
                }
                else
                    this.mBubble.Visibility = Visibility.Hidden;
            }
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
            switch (targetObj)
            {
                case int id:
                    if (id == 3 || id == 1 || id == 0 || id == 2)
                    {
                        this.mLabelTarget.Visibility = Visibility.Collapsed;
                        this.mImageTarget.Visibility = Visibility.Visible;
                        this.mImageTarget.Source = (ImageSource)new BitmapImage(new Uri("pack://application:,,,/Resources/" + DeviceUtils.getSpecialKeyIconByID(id, false)));
                        break;
                    }
                    this.mLabelTarget.Visibility = Visibility.Visible;
                    this.mImageTarget.Visibility = Visibility.Collapsed;
                    this.mLabelTarget.Content = (object)DeviceUtils.getKeyNameByID(id);
                    break;
                case OneMacroBean _:
                    this.mLabelTarget.Visibility = Visibility.Visible;
                    this.mImageTarget.Visibility = Visibility.Collapsed;
                    this.mLabelTarget.Content = (object)Application.Current.FindResource((object)"macro").ToString();
                    break;
            }
        }

        public void setInsideFocusKeyId(int keyId, object targetObj)
        {
            foreach (KeyValuePair<int, Image> keyValuePair in this.mDictionaryInside)
                keyValuePair.Value.Visibility = Visibility.Hidden;
            switch (keyId)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 6:
                case 14:
                    this.setInsideType(0);
                    break;
                case 4:
                case 5:
                case 7:
                case 8:
                case 9:
                case 15:
                case 16:
                case 17:
                    this.setInsideType(1);
                    break;
                case 10:
                case 12:
                case 23:
                    this.setInsideType(5);
                    break;
                case 11:
                case 13:
                case 22:
                    this.setInsideType(4);
                    break;
                case 18:
                case 20:
                    this.setInsideType(2);
                    break;
                case 19:
                case 21:
                    this.setInsideType(3);
                    break;
            }
            if (!this.mDictionaryInside.ContainsKey(keyId))
                return;
            this.mDictionaryInside[keyId].Visibility = Visibility.Visible;
        }

        private void setInsideType(int type)
        {
            this.mLayoutFrontLeft.Visibility = Visibility.Hidden;
            this.mLayoutFrontRight.Visibility = Visibility.Hidden;
            this.mLayoutAfterLeft.Visibility = Visibility.Hidden;
            this.mLayoutAfterRight.Visibility = Visibility.Hidden;
            this.mLayoutTopLeft.Visibility = Visibility.Hidden;
            this.mLayoutTopRight.Visibility = Visibility.Hidden;
            switch (type)
            {
                case 0:
                    this.mLayoutFrontLeft.Visibility = Visibility.Visible;
                    break;
                case 1:
                    this.mLayoutFrontRight.Visibility = Visibility.Visible;
                    break;
                case 2:
                    this.mLayoutAfterLeft.Visibility = Visibility.Visible;
                    break;
                case 3:
                    this.mLayoutAfterRight.Visibility = Visibility.Visible;
                    break;
                case 4:
                    this.mLayoutTopLeft.Visibility = Visibility.Visible;
                    break;
                case 5:
                    this.mLayoutTopRight.Visibility = Visibility.Visible;
                    break;
            }
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/WPFTest;component/usercontrols/usercontrolsettingconfigkeydisplay.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.mDisplayOutside = (Grid)target;
                    break;
                case 2:
                    this.mLabelTop = (Label)target;
                    this.mLabelTop.MouseLeftButtonDown += new MouseButtonEventHandler(this.mLabelTop_MouseLeftButtonDown);
                    break;
                case 3:
                    this.mLabelFront = (Label)target;
                    this.mLabelFront.MouseLeftButtonDown += new MouseButtonEventHandler(this.mLabelFront_MouseLeftButtonDown);
                    break;
                case 4:
                    this.mLabelAfter = (Label)target;
                    this.mLabelAfter.MouseLeftButtonDown += new MouseButtonEventHandler(this.mLabelAfter_MouseLeftButtonDown);
                    break;
                case 5:
                    this.mLayoutFront = (Grid)target;
                    break;
                case 6:
                    this.mImgFront = (ImageBrush)target;
                    break;
                case 7:
                    this.mKeyUP = (Image)target;
                    break;
                case 8:
                    this.mKeyRight = (Image)target;
                    break;
                case 9:
                    this.mKeyLeft = (Image)target;
                    break;
                case 10:
                    this.mKeyDown = (Image)target;
                    break;
                case 11:
                    this.mKeyA = (Image)target;
                    break;
                case 12:
                    this.mKeyB = (Image)target;
                    break;
                case 13:
                    this.mKeyX = (Image)target;
                    break;
                case 14:
                    this.mKeyY = (Image)target;
                    break;
                case 15:
                    this.mKeyC = (Image)target;
                    break;
                case 16:
                    this.mKeyZ = (Image)target;
                    break;
                case 17:
                    this.mKeySelect = (Image)target;
                    break;
                case 18:
                    this.mKeyStart = (Image)target;
                    break;
                case 19:
                    this.mKeyL3 = (Image)target;
                    break;
                case 20:
                    this.mKeyR3 = (Image)target;
                    break;
                case 21:
                    this.mLayoutAfter = (Grid)target;
                    break;
                case 22:
                    this.mImgAfter = (ImageBrush)target;
                    break;
                case 23:
                    this.mKeyM1 = (Image)target;
                    break;
                case 24:
                    this.mKeyM2 = (Image)target;
                    break;
                case 25:
                    this.mKeyM3 = (Image)target;
                    break;
                case 26:
                    this.mKeyM4 = (Image)target;
                    break;
                case 27:
                    this.mLayoutTop = (Grid)target;
                    break;
                case 28:
                    this.mImgTop = (ImageBrush)target;
                    break;
                case 29:
                    this.mKeyRB = (Image)target;
                    break;
                case 30:
                    this.mKeyRT = (Image)target;
                    break;
                case 31:
                    this.mKeyLB = (Image)target;
                    break;
                case 32:
                    this.mKeyLT = (Image)target;
                    break;
                case 33:
                    this.mKeyM5 = (Image)target;
                    break;
                case 34:
                    this.mKeyM6 = (Image)target;
                    break;
                case 35:
                    this.mBubble = (Grid)target;
                    break;
                case 36:
                    this.mLabelCurrent = (Label)target;
                    break;
                case 37:
                    this.mImageCurrent = (Image)target;
                    break;
                case 38:
                    this.mLabelTarget = (Label)target;
                    break;
                case 39:
                    this.mImageTarget = (Image)target;
                    break;
                case 40:
                    this.mDisplayInside = (Grid)target;
                    break;
                case 41:
                    this.mLayoutFrontLeft = (Grid)target;
                    break;
                case 42:
                    this.mImgFrontLeft = (Image)target;
                    break;
                case 43:
                    this.mInsideKeyUP = (Image)target;
                    break;
                case 44:
                    this.mInsideKeyRight = (Image)target;
                    break;
                case 45:
                    this.mInsideKeyLeft = (Image)target;
                    break;
                case 46:
                    this.mInsideKeyDown = (Image)target;
                    break;
                case 47:
                    this.mInsideKeySelect = (Image)target;
                    break;
                case 48:
                    this.mInsideKeyL3 = (Image)target;
                    break;
                case 49:
                    this.mLayoutFrontRight = (Grid)target;
                    break;
                case 50:
                    this.mImgFrontRight = (Image)target;
                    break;
                case 51:
                    this.mInsideKeyA = (Image)target;
                    break;
                case 52:
                    this.mInsideKeyB = (Image)target;
                    break;
                case 53:
                    this.mInsideKeyX = (Image)target;
                    break;
                case 54:
                    this.mInsideKeyY = (Image)target;
                    break;
                case 55:
                    this.mInsideKeyC = (Image)target;
                    break;
                case 56:
                    this.mInsideKeyZ = (Image)target;
                    break;
                case 57:
                    this.mInsideKeyStart = (Image)target;
                    break;
                case 58:
                    this.mInsideKeyR3 = (Image)target;
                    break;
                case 59:
                    this.mLayoutAfterLeft = (Grid)target;
                    break;
                case 60:
                    this.mImgAfterLeft = (Image)target;
                    break;
                case 61:
                    this.mInsideKeyM1 = (Image)target;
                    break;
                case 62:
                    this.mInsideKeyM3 = (Image)target;
                    break;
                case 63:
                    this.mLayoutAfterRight = (Grid)target;
                    break;
                case 64:
                    this.mImgAfterRight = (Image)target;
                    break;
                case 65:
                    this.mInsideKeyM2 = (Image)target;
                    break;
                case 66:
                    this.mInsideKeyM4 = (Image)target;
                    break;
                case 67:
                    this.mLayoutTopLeft = (Grid)target;
                    break;
                case 68:
                    this.mImgTopLeft = (Image)target;
                    break;
                case 69:
                    this.mInsideKeyRB = (Image)target;
                    break;
                case 70:
                    this.mInsideKeyRT = (Image)target;
                    break;
                case 71:
                    this.mInsideKeyM5 = (Image)target;
                    break;
                case 72:
                    this.mLayoutTopRight = (Grid)target;
                    break;
                case 73:
                    this.mImgTopRight = (Image)target;
                    break;
                case 74:
                    this.mInsideKeyLB = (Image)target;
                    break;
                case 75:
                    this.mInsideKeyLT = (Image)target;
                    break;
                case 76:
                    this.mInsideKeyM6 = (Image)target;
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }
    }
}
