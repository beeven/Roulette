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

namespace RandomPic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigationWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.F1:
                    Navigate(new Uri("WelcomePage.xaml", UriKind.Relative));
                    break;
                case Key.F2:
                    Navigate(new Uri("RandomPicturePage.xaml", UriKind.Relative));
                    break;
                case Key.F3:
                    Navigate(new Uri("QuizPage.xaml", UriKind.Relative));
                    break;
                case Key.F6:
                    Navigate(new Uri("QuizManagerPage.xaml", UriKind.Relative));
                    break;

            }
        }
    }
}
