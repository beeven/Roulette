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
        public QuizPage()
        {
            InitializeComponent();
            _quizcontext = App.Current.Properties["QuizContext"] as QuizContext;
             model = new QuizViewModel();
            DataContext = model;
            rand = new Random();
        }

        private async void BtnShowAnswer_Click(object sender, RoutedEventArgs e)
        {
            if(model.ShowAnswer)
            {
                model.ShowAnswer = false;
                lvSelections.SelectedIndex = -1;
            }
            else
            {
                model.ShowAnswer = true;
                lvSelections.SelectedIndex = model.CurrentQuiz.Answer;
                model.CurrentQuiz.HasChosen = true;
                await _quizcontext.SaveChangesAsync();
            }

        }

        private async void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            var count = await _quizcontext.Quizzes.CountAsync(x => x.HasChosen == false);
            var index = rand.Next(count);
            var quiz = await _quizcontext.Quizzes.FirstAsync(x => x.Key == index);
            model.ShowAnswer = false;
            lvSelections.SelectedIndex = -1;
            model.CurrentQuiz = quiz;
        }
    }
}
