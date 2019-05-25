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
using Microsoft.Extensions.DependencyInjection;

namespace RandomPic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        private readonly IServiceProvider _services;
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _services = serviceProvider;
            var welcomePage = _services.GetRequiredService<WelcomePage>();
            Navigate(welcomePage);
        }

        private void NavigationWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.F1:
                    
                    Navigate(_services.GetRequiredService<WelcomePage>());
                    break;
                case Key.F2:
                    Navigate(_services.GetRequiredService<RandomPicturePage>());
                    break;
                case Key.F3:
                    Navigate(_services.GetRequiredService<QuizPage>());
                    break;
                case Key.F6:
                    Navigate(_services.GetRequiredService<QuizManagerPage>());
                    break;

            }
        }
    }
}
