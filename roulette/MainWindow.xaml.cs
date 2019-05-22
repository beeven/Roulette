using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace roulette
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawTextBlocks();
        }

        private void DrawTextBlocks(String[] names = null)
        {
            can_roulette.Children.RemoveRange(1, can_roulette.Children.Count - 1);
            if (names == null)
            {
                int length = 80;
                for (int i = 0; i < length; i++)
                {
                    TextBlock tb = new TextBlock
                    {
                        Text = "Name " + i.ToString(),
                        FontSize = 32,
                        RenderTransform = new RotateTransform(360.0 / length * i, -450, 0)
                    };
                    Canvas.SetLeft(tb, 1050);
                    Canvas.SetTop(tb, 600);
                    can_roulette.Children.Add(tb);
                }
            }
            else
            {
                int length = names.Length;
                for (int i = 0; i < length; i++)
                {
                    TextBlock tb = new TextBlock
                    {
                        Text = names[i],
                        FontSize = 22,
                        RenderTransform = new RotateTransform(360.0 / length * i, -500, 0)
                    };
                    Canvas.SetLeft(tb, 1100);
                    Canvas.SetTop(tb, 600);
                    can_roulette.Children.Add(tb);
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            double roll = rnd.NextDouble() * 5 * 360 + 3 * 360; // At least spin 3 circles;
            lb_Info.Content = roll / 360;

            double angle = tr_roulette.Angle;

            DoubleAnimationUsingKeyFrames rotate = new DoubleAnimationUsingKeyFrames();
            //rotate.KeyFrames.Add(new EasingDoubleKeyFrame(angle + 360, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), new CubicEase() { EasingMode = EasingMode.EaseIn }));
            //rotate.KeyFrames.Add(new LinearDoubleKeyFrame(angle + 360 + roll, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(roll / 360 / 2 + 1))));
            rotate.KeyFrames.Add(new EasingDoubleKeyFrame(angle + 360 * 2 + roll, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(roll / 360 + 2 + 3)), new CubicEase() { EasingMode = EasingMode.EaseOut }));
            tr_roulette.BeginAnimation(RotateTransform.AngleProperty, rotate);

        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button_Click(this, new RoutedEventArgs());
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MenuItem_Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Normal;
            else
                this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MenuItem_LoadNames_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "names";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(filename, true);
                List<String> lines = new List<string>();
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
                this.DrawTextBlocks(lines.ToArray());
            }
        }
    }
}
