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
using System.Windows.Shapes;

namespace scoreboard
{
    /// <summary>
    /// Interaction logic for ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        private MainWindow mainWindow;
        public ConsoleWindow()
        {
            InitializeComponent();
            mainWindow = (MainWindow)this.Owner;
        }

        public event EventHandler<CandidateInfo> CandidateInfoUpdated;

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {

            var candidate = new CandidateInfo()
            {
                Name = tbName.Text,
                Number = tbNumber.Text,
                Company = tbCompany.Text,
                Score = tbScore.Text,
                Portrait = $"portraits/{tbNumber.Text}.jpg"
            };
            CandidateInfoUpdated?.Invoke(this,candidate);
        }
    }

}
