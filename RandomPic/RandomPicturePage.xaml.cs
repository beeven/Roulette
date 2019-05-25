using RandomPic.Model;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RandomPic
{
    /// <summary>
    /// Interaction logic for RandomPicturePage.xaml
    /// </summary>
    public partial class RandomPicturePage : Page
    {
        public RandomPictureViewModel model = RandomPictureViewModel.LoadFromFolder();

        private DispatcherTimer randPicTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(50) };
        private DispatcherTimer countDownTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(3) };
        private Random rand = new Random();
        public RandomPicturePage()
        {
            InitializeComponent();
            DataContext = model;

            randPicTimer.Tick += RandPicTimer_Tick;
            countDownTimer.Tick += CountDownTimer_Tick;

        }

        private void CountDownTimer_Tick(object sender, EventArgs e)
        {
            randPicTimer.Stop();
            countDownTimer.Stop();
            btnChoose.IsEnabled = true;
        }

        private void RandPicTimer_Tick(object sender, EventArgs e)
        {
            var index = rand.Next(model.Pictures.Count);
            model.SelectedPicture = model.Pictures[index];
        }

        private void ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            dpPicture.Visibility = Visibility.Hidden;
            lvPictures.Visibility = Visibility.Visible;
            countDownTimer.Start();
            randPicTimer.Start();
            btnChoose.IsEnabled = false;
        }


        private void DpPicture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dpPicture.Visibility = Visibility.Hidden;
            lvPictures.Visibility = Visibility.Visible;
        }


        private void ListViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (((ListViewItem)sender).IsSelected)
            {
                dpPicture.Visibility = Visibility.Visible;
                lvPictures.Visibility = Visibility.Hidden;
                var storyboard = (Storyboard)this.FindResource("ScalePicture");
                storyboard.Begin();
            }
        }
    }
}
