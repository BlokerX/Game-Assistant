using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GameAssistant
{
    public class WidgetWindow : Window
    {
        // Varibles
        public bool IsAllowDrag { get; set; } = false;

        public WidgetWindow()
        {
            this.Topmost = true;
            this.ShowInTaskbar = false;
            this.WindowStyle = WindowStyle.None;
            LoadAnimationTimer();
        }

        #region Animations Region

        /// <summary>
        /// Animation Timer
        /// </summary>
        private readonly System.Windows.Threading.DispatcherTimer Animation = new System.Windows.Threading.DispatcherTimer();
        private void Dispatcher_Tick(object sender, EventArgs e)
        {
            switch (ChosedAnimation)
            {
                case Animations.ColorRainbowRGB:
                    Animation_ColorRainbowRGB_GoOn();
                    break;

                case Animations.ColorRainbowRGB2:
                    Animation_ColorRainbowRGB2_GoOn();
                    break;

                case Animations.NULL:
                default:
                    break;
            }
        }

        #region Dispatcher Animation Go On

        protected virtual void Animation_ColorRainbowRGB_GoOn()
        {
            Background = this.Animation_ColorRainbowRGB(Background);
        }

        protected virtual void Animation_ColorRainbowRGB2_GoOn()
        {
            Background = this.Animation_ColorRainbowRGB2(Background);
        }

        #endregion

        private void LoadAnimationTimer()
        {
            Animation.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Animation.Tick += Dispatcher_Tick;
        }

        public Animations ChosedAnimation { get; set; } = Animations.NULL;
        public byte AnimationSpeed { get; set; } = 3;
        private int AnimationModem = 0;
        private Color AnimationColor;

        #region Animation's Methods

        #region Start

        /// <summary>
        /// Start Animation if Animation is disabled
        /// </summary>
        public void AnimationStart()
        {
            if (!Animation.IsEnabled)
            {
                Animation.Start();
            }
        }

        /// <summary>
        /// Start Animation (if is disabled) with reset Animation's properties
        /// </summary>
        public void AnimationStartAndReset()
        {
            if (!Animation.IsEnabled)
            {
                AnimationModem = 0;
                AnimationColor = new Color();
                Animation.Start();
            }
        }

        #endregion

        #region Stop

        /// <summary>
        /// Stop Animation if Animation is enabled
        /// </summary>
        public void AnimationStop()
        {
            if (Animation.IsEnabled) Animation.Stop();
        }

        /// <summary>
        /// Stop Animation (if is enabled) and reset Animation's properties
        /// </summary>
        public void AnimationStopAndReset()
        {
            if (Animation.IsEnabled) Animation.Stop();
            AnimationModem = 0;
            AnimationColor = new Color();

        }

        #endregion

        #endregion

        #region Animations

        protected SolidColorBrush Animation_ColorRainbowRGB(Brush animationBrush)
        {
            if (animationBrush is SolidColorBrush colorBrush)
            {
                AnimationColor = colorBrush.Color;
            }
            //...

            switch (AnimationModem)
            {
                case 0:
                    AnimationColor.R = 255;
                    AnimationColor.G = 0;
                    AnimationColor.B = 0;
                    AnimationModem = 1;
                    break;
                case 1:
                    if (AnimationColor.G < 255)
                    {
                        AnimationColor.G += AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 2;
                    }
                    break;

                case 2:
                    if (AnimationColor.R > 0)
                    {
                        AnimationColor.R -= AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 3;
                    }
                    break;

                case 3:
                    if (AnimationColor.B < 255)
                    {
                        AnimationColor.B += AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 4;
                    }
                    break;

                case 4:
                    if (AnimationColor.G > 0)
                    {
                        AnimationColor.G -= AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 5;
                    }
                    break;

                case 5:
                    if (AnimationColor.R < 255)
                    {
                        AnimationColor.R += AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 6;
                    }
                    break;

                case 6:
                    if (AnimationColor.B > 0)
                    {
                        AnimationColor.B -= AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 0;
                    }
                    break;
            }

            return new SolidColorBrush(AnimationColor);
        }

        protected SolidColorBrush Animation_ColorRainbowRGB2(Brush animationBrush)
        {
            if (animationBrush is SolidColorBrush colorBrush)
            {
                AnimationColor = colorBrush.Color;
            }
            //...

            switch (AnimationModem)
            {
                case 0:
                    AnimationColor.R = 255;
                    AnimationColor.G = 0;
                    AnimationColor.B = 0;
                    AnimationModem = 1;
                    break;

                case 1:
                    if (AnimationColor.B < 255)
                    {
                        AnimationColor.B += AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 2;
                    }
                    break;

                case 2:
                    if (AnimationColor.R > 0)
                    {
                        AnimationColor.R -= AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 3;
                    }
                    break;

                case 3:
                    if (AnimationColor.G < 255)
                    {
                        AnimationColor.G += AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 4;
                    }
                    break;

                case 4:
                    if (AnimationColor.B > 0)
                    {
                        AnimationColor.B -= AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 5;
                    }
                    break;

                case 5:
                    if (AnimationColor.R < 255)
                    {
                        AnimationColor.R += AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 6;
                    }
                    break;

                case 6:
                    if (AnimationColor.G > 0)
                    {
                        AnimationColor.G -= AnimationSpeed;
                    }
                    else
                    {
                        AnimationModem = 0;
                    }
                    break;

            }

            return new SolidColorBrush(AnimationColor);
        }

        #endregion

        public enum Animations
        {
            NULL = 0,
            ColorRainbowRGB,
            ColorRainbowRGB2
        }

        public void SelectAnimation(WidgetInformation WidgetInf)
        {
            switch (WidgetInf.Animation)
            {
                case Animations.ColorRainbowRGB:
                    this.ChosedAnimation = Animations.ColorRainbowRGB;
                    this.AnimationStartAndReset();
                    break;
                case Animations.ColorRainbowRGB2:
                    this.ChosedAnimation = Animations.ColorRainbowRGB2;
                    this.AnimationStartAndReset();
                    break;
                case Animations.NULL:
                default:
                    this.ChosedAnimation = Animations.NULL;
                    AnimationStopAndReset();
                    break;
            }
        }

        #endregion

        // Methods
        public static dynamic DownloadWidgetInformationOfFile() { return 0; }

        public static WidgetInformation GetWidgetInformationOfFile(WidgetInformation widgetInformation, StreamReader sr)
        {

            if (true)
            {
                string a = sr.ReadLine();
                if (bool.TryParse(a, out bool aBool))
                {
                    widgetInformation.IsChosed = aBool;
                }
                else
                {
                    sr.Close();
                    return null;
                }
            }

            if (true)
            {
                string a = sr.ReadLine();
                if (int.TryParse(a, out int aInt))
                {
                    widgetInformation.PositionX = aInt;
                }
                else
                {
                    sr.Close();
                    return null;
                }
            }

            if (true)
            {
                string a = sr.ReadLine();
                if (int.TryParse(a, out int aInt))
                {
                    widgetInformation.PositionY = aInt;
                }
                else
                {
                    sr.Close();
                    return null;
                }
            }

            if (true)
            {
                string a = sr.ReadLine();
                if (int.TryParse(a, out int aInt))
                {
                    widgetInformation.Width = aInt;
                }
                else
                {
                    sr.Close();
                    return null;
                }
            }

            if (true)
            {
                string a = sr.ReadLine();
                if (int.TryParse(a, out int aInt))
                {
                    widgetInformation.Heigth = aInt;
                }
                else
                {
                    sr.Close();
                    return null;
                }
            }

            if (true)
            {
                string a = sr.ReadLine();
                if (Animations.TryParse(a, out Animations aAnimations))
                {
                    widgetInformation.Animation = aAnimations;
                }
                else
                {
                    sr.Close();
                    return null;
                }
            }

            return widgetInformation;
        }

        public static void UpdateWidgetInformationOfFileSaveObject(WidgetWindow argWW, StreamWriter sw)
        {
            sw.WriteLine(true.ToString());

            sw.WriteLine(argWW.Left.ToString());
            sw.WriteLine(argWW.Top.ToString());

            sw.WriteLine(argWW.Width.ToString());
            sw.WriteLine(argWW.Height.ToString());

            sw.WriteLine(argWW.ChosedAnimation.ToString());
        }

        public static void UpdateWidgetInformationOfFileSaveWidgetInformation(WidgetInformation widgetInf, StreamWriter sw)
        {
            sw.WriteLine(false);

            sw.WriteLine(widgetInf.PositionX);
            sw.WriteLine(widgetInf.PositionY);

            sw.WriteLine(widgetInf.Width);
            sw.WriteLine(widgetInf.Heigth);

            sw.WriteLine(widgetInf.Animation);
        }

        public static dynamic CreateWidget() { return 0; }
        /*/ TODO public static void UpdateWidgetInformationOfFile(w) { } (Pomyśleć o interfejsach) /*/

        protected void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (IsAllowDrag && e.LeftButton == MouseButtonState.Pressed)
                this?.DragMove();
        }

    }
}
