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

namespace Scoreboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ConsoleWindow consoleWindow;
        public MainWindow(ConsoleWindow consoleWindow)
        {
            InitializeComponent();
            this.consoleWindow = consoleWindow;
            this.consoleWindow.CandidateInfoUpdated += ConsoleWindow_CandidateInfoUpdated;
            this.consoleWindow.Closed += ConsoleWindow_Closed;
            this.consoleWindow.ToggleElements += ConsoleWindow_ToggleElements;
            this.DataContextChanged += MainWindow_DataContextChanged;
            this.consoleWindow.Show();
            DataContext = new CandidateInfo()
            {
                Number = "1",
                Name = "米热班姑·买买提明",
                Company = "广州海关",
                Score = "92.6",
                Score1 = "95.6",
                Score2 = "3"
            };
        }

        private bool bIsElementsHidden = false;
        private void ConsoleWindow_ToggleElements(object sender, RoutedEventArgs e)
        {
            if(!bIsElementsHidden)
            {
                foreach(UIElement elem in this.gridContent.Children)
                {
                    elem.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                foreach (UIElement elem in this.gridContent.Children)
                {
                    elem.Visibility = Visibility.Visible;
                }
            }

            bIsElementsHidden = !bIsElementsHidden;
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
            if (!string.IsNullOrWhiteSpace(candidate.Score) 
                && !string.IsNullOrWhiteSpace(candidate.Score1)
                && !string.IsNullOrWhiteSpace(candidate.Score2))
            {
                gridScore.Visibility = Visibility.Visible;
            }
            else
            {
                gridScore.Visibility = Visibility.Collapsed;
            }
            var storyboard = (Storyboard)this.FindResource("infoPanelFlyIn");
            storyboard.Begin();
        }


        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
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
