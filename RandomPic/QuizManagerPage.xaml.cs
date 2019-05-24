using RandomPic.Data;
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
using RandomPic.Model;

namespace RandomPic
{
    /// <summary>
    /// Interaction logic for QuizManagerPage.xaml
    /// </summary>
    public partial class QuizManagerPage : Page
    {
        private readonly QuizContext _quizcontext;
        public QuizManagerPage()
        {
            InitializeComponent();
            _quizcontext = App.Current.Properties["QuizContext"] as QuizContext;
        }

        private async void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Generating...";
            btnGenerate.IsEnabled = false;
            
                Random rand = new Random();

                await _quizcontext.Database.ExecuteSqlCommandAsync("delete from Quizzes");
                for (int i = 0; i < 10; i++)
                {
                    var quiz = new Quiz()
                    {
                        Question = $"Loerm {i}",
                        Answer = rand.Next(4),
                        Key = i + 1,
                        Selections = new List<string>()
                    {
                        "Answer 1",
                        "Answer 2",
                        rand.NextDouble()>0.5? "Answer 3":null,
                        rand.NextDouble()>0.5? "Answer 4":null,
                    }
                    };
                    _quizcontext.Attach(quiz);
                }
                await _quizcontext.SaveChangesAsync();
               
                    tbOutput.Text = "Quiz generated.";
                    btnGenerate.IsEnabled = true;

           
            
           
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Loading...";
        }
    }
}
