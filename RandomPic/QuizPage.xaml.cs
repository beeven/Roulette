using RandomPic.Data;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.Windows.Threading;

namespace RandomPic
{
    /// <summary>
    /// Interaction logic for QuizPage.xaml
    /// </summary>
    public partial class QuizPage : Page
    {
        private readonly QuizContext _quizcontext;
        private QuizViewModel model;
        private Random rand;

        private DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(50) };

        private List<Quiz> quizzes;

        public QuizPage(QuizContext quizContext)
        {
            _quizcontext = quizContext;
            model = new QuizViewModel();
            DataContext = model;
            rand = new Random();

            InitializeComponent();

            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GetNextQuiz();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            model.TotalQuizzesCount = await _quizcontext.Quizzes.CountAsync();
            
            quizzes = await _quizcontext.Quizzes.Where(x => x.HasChosen == false).ToListAsync();
            model.RemainingQuizzesCount = quizzes.Count;
            //GetNextQuiz();

            //lbAnswered.Content = $"已答数：{model.TotalQuizzesCount - model.RemainingQuizzesCount}";
            //lbTotal.Content = $"总题数：{model.TotalQuizzesCount}";
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            quizzes.Clear();
            model.CurrentQuiz = null;
        }



        private async void BtnShowAnswer_Click(object sender, RoutedEventArgs e)
        {
            if(model.ShowAnswer)
            {
                model.ShowAnswer = false;
                lvSelections.SelectedIndex = -1;
                model.CurrentQuiz.HasChosen = false;
                quizzes.Add(model.CurrentQuiz);
            }
            else
            {
                model.ShowAnswer = true;
                
                model.CurrentQuiz.HasChosen = true;
                quizzes.Remove(model.CurrentQuiz);
                
                foreach(char a in model.CurrentQuiz.Answer)
                {
                    int i = a - 0x41;
                    lvSelections.SelectedItems.Add(model.CurrentQuiz.Selections[i]);
                }
            }
            model.RemainingQuizzesCount = quizzes.Count;
            //lbAnswered.Content = $"已答数：{model.TotalQuizzesCount - model.RemainingQuizzesCount}";
            await _quizcontext.SaveChangesAsync();
        }

        private async void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            btnNext.IsEnabled = false;
            btnShowAnswer.IsEnabled = false;
            lvSelections.SelectedIndex = -1;
            lvSelections.Visibility = Visibility.Hidden;
            //tbQuestion.Effect = new System.Windows.Media.Effects.BlurEffect() { Radius = 10 };
            timer.Start();

            await Task.Delay(1000);

            timer.Stop();
            btnNext.IsEnabled = true;
            btnShowAnswer.IsEnabled = true;
            lvSelections.Visibility = Visibility.Visible;
            //tbQuestion.Effect = null;

        }

        private void GetNextQuiz()
        {
            if (quizzes.Count == 0) return;
            var index = rand.Next(quizzes.Count);
            model.CurrentQuiz = quizzes[index];
            model.RemainingQuizzesCount = quizzes.Count;
            model.ShowAnswer = false;
        }



        private void LvSelections_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }


    }
}
