using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Stopwatch preciseTimer;
        private DispatcherTimer timer;
        private TimeSpan currentTime;
        private bool needReset = false;
        private bool needChangeColor = false;

        public TimerMode[] timerModes;
        public int timerModeIndex = 0;
        public TimerMode currentTimerMode;
        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += MainWindow_KeyDown;
            this.preciseTimer = new Stopwatch();

            timer = new DispatcherTimer(DispatcherPriority.Render)
            {
                Interval = TimeSpan.FromMilliseconds(10)
            };
            timer.Tick += Timer_Tick;


            timerModes = TimerMode.ReadFromConfig();
            if (timerModes.Length == 0)
            {
                timerModes = new TimerMode[]
                {
                    new TimerMode
                    {
                        Name = "Default",
                        Description = "Default Mode",
                        Duration = TimeSpan.FromSeconds(10),
                        IsCountDown = true,
                        TimeToAlert = TimeSpan.Zero,
                        AlertSoundFilePath = ""
                    }
                };
            }

            timerModeIndex = -1;
            SwitchTimerMode(false);
        }

        public void SwitchTimerMode(bool previous = false)
        {
            StopCountDown();

            if (previous)
            {
                timerModeIndex--;
                if (timerModeIndex < 0)
                {
                    timerModeIndex = timerModes.Length - 1;
                }
            }
            else
            {
                timerModeIndex++;
                if (timerModeIndex >= timerModes.Length)
                {
                    timerModeIndex = 0;
                }
            }
            currentTimerMode = timerModes[timerModeIndex];
            ResetTimer();
            this.descText.Content = currentTimerMode.Description;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentTimerMode.IsCountDown)
            {
                currentTime = currentTimerMode.Duration - TimeSpan.FromMilliseconds(this.preciseTimer.ElapsedMilliseconds);
                if (currentTime <= currentTimerMode.TimeToAlert && needChangeColor)
                {
                    needChangeColor = false;
                    this.timerText.Foreground = this.FindResource("AlertTimerBrush") as Brush;
                }
                if(currentTime <= TimeSpan.Zero)
                {
                    StopCountDown();
                    currentTime = TimeSpan.Zero;
                    needReset = true;
                }
            }
            else
            {
                currentTime = TimeSpan.FromMilliseconds(this.preciseTimer.ElapsedMilliseconds);
                if (currentTime >= currentTimerMode.TimeToAlert && needChangeColor)
                {
                    needChangeColor = false;
                    this.timerText.Foreground = this.FindResource("AlertTimerBrush") as Brush;
                }
                if(currentTime >= currentTimerMode.Duration)
                {
                    StopCountDown();
                    currentTime = currentTimerMode.Duration;
                    needReset = true;
                }
            }

            SetTime();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    if (timer.IsEnabled)
                    {
                        StopCountDown();
                    }
                    else if (needReset)
                    {
                        ResetTimer();
                    }
                    else
                    {
                        StartCountDown();
                    }
                    break;
                case Key.Back:
                    ResetTimer();
                    break;
                case Key.F2:
                    SwitchTimerMode(true);
                    break;
                case Key.F3:
                    SwitchTimerMode(false);
                    break;
            }
        }


        public void StartCountDown()
        {
            timer.Start();
            preciseTimer.Start();
        }

        public void StopCountDown()
        {
            timer.Stop();
            preciseTimer.Stop();
        }

        public void ResetTimer()
        {
            if (currentTimerMode.IsCountDown)
            {
                currentTime = currentTimerMode.Duration;
            }
            else
            {
                currentTime = TimeSpan.Zero;
            }
            preciseTimer.Reset();
            this.needChangeColor = true;
            this.needReset = false;
            
            this.timerText.Foreground = this.FindResource("NormalTimerBrush") as Brush;
            SetTime();
        }

        private void SetTime()
        {
            this.timerText.Text = currentTime.ToString(@"mm\:ss\.ff");
        }
    }
}
