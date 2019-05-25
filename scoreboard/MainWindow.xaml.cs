using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace scoreboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ConsoleWindow consoleWindow;
        public MainWindow()
        {
            InitializeComponent();
            consoleWindow = new ConsoleWindow();
            consoleWindow.CandidateInfoUpdated += ConsoleWindow_CandidateInfoUpdated;
            consoleWindow.Show();
            consoleWindow.Closed += ConsoleWindow_Closed;
            this.DataContextChanged += MainWindow_DataContextChanged;

            DataContext = new CandidateInfo() { Number = "1", Name = "Leon Kennedy", Company = "广州海关", Score = "93.6", Portrait = "portraits/1.jpg" };
        }

        private void ConsoleWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConsoleWindow_CandidateInfoUpdated(object sender, CandidateInfo e)
        {
            DataContext = e;
        }

        private void MainWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var candidate = (CandidateInfo)DataContext;
            if(!string.IsNullOrWhiteSpace(candidate.Score))
            {
                tbScoreTitle.Visibility = Visibility.Visible;
                tbScore.Visibility = Visibility.Visible;
            }
            else
            {
                tbScoreTitle.Visibility = Visibility.Collapsed;
                tbScore.Visibility = Visibility.Collapsed;
            }
            var storyboard = (Storyboard)this.FindResource("infoPanelFlyIn");
            storyboard.Begin();
        }


        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
