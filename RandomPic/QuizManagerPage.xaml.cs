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
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using Microsoft.Win32;

namespace RandomPic
{
    /// <summary>
    /// Interaction logic for QuizManagerPage.xaml
    /// </summary>
    public partial class QuizManagerPage : Page
    {
        private readonly QuizContext _quizcontext;
        public QuizManagerPage(QuizContext quizContext)
        {
            InitializeComponent();
            _quizcontext = quizContext;
            this.Loaded += QuizManagerPage_Loaded;
        }

        private async void QuizManagerPage_Loaded(object sender, RoutedEventArgs e)
        {
            var total = await _quizcontext.Quizzes.CountAsync();
            var count = await _quizcontext.Quizzes.CountAsync(x => x.HasChosen == false);
            tbOutput.Text = $"Total {total} quizzes in database. {count} is available.";
        }

        private async void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Generating...";
            btnGenerate.IsEnabled = false;

            Random rand = new Random();

            _quizcontext.Quizzes.RemoveRange(_quizcontext.Quizzes);
            await _quizcontext.SaveChangesAsync();
            for (int i = 0; i < 10; i++)
            {
                var quiz = new Quiz()
                {
                    Question = $"Loerm {i}",
                    Answer = ((char)rand.Next(0x41,0x44)).ToString(),
                    Key = i + 1,
                    Selections = new List<string>()
                    {
                        "Answer 1",
                        "Answer 2",
                        rand.NextDouble()>0.5? "Answer 3":null,
                        rand.NextDouble()>0.5? "Answer 4":null,
                    }
                };
                await _quizcontext.Quizzes.AddAsync(quiz);
            }
            await _quizcontext.SaveChangesAsync();

            tbOutput.Text = "Quiz generated.";
            btnGenerate.IsEnabled = true;
        }

        private async void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Loading...";
            btnLoad.IsEnabled = false;
            btnGenerate.IsEnabled = false;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.DefaultExt = "xlsx";
            openFileDialog.AddExtension = true;
            
            openFileDialog.Filter = "Excel 2007 Workbook|*.xlsx";
            if (openFileDialog.ShowDialog() == true)
                await ReadFromFile(openFileDialog.FileName);



            var count = await _quizcontext.Quizzes.CountAsync();
            tbOutput.Text = $"{count} quizzes are loaded.";
            btnLoad.IsEnabled = true;
            btnGenerate.IsEnabled = true;
            ((Button)sender).Focus();
        }

        private async Task ReadFromFile(string fileName)
        {
            _quizcontext.Quizzes.RemoveRange(_quizcontext.Quizzes);
            await _quizcontext.SaveChangesAsync();

            var workbook = new XSSFWorkbook(fileName);
            var sheet = workbook.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) == null)
                {
                    break;
                }
                var quiz = new Quiz()
                {
                    Key = (int)row.GetCell(0).NumericCellValue,
                    Question = row.GetCell(1).StringCellValue,
                    Selections = new List<string>(),
                    HasChosen = false
                };
                var answerCell = row.GetCell(6);
                if (answerCell.CellType == CellType.Numeric) // 判断题
                {
                    quiz.Answer = ((char)(answerCell.NumericCellValue + 0x40)).ToString();
                    quiz.Selections.Add("A. 对");
                    quiz.Selections.Add("B. 错");
                }
                else
                {
                    
                    quiz.Answer = answerCell.StringCellValue.ToUpper();
                    for (int j = 2; j <= 5; j++)
                    {
                        quiz.Selections.Add(row.GetCell(j, MissingCellPolicy.RETURN_BLANK_AS_NULL)?.StringCellValue);
                    }

                }
                await _quizcontext.Quizzes.AddAsync(quiz);
            }
            await _quizcontext.SaveChangesAsync();
            workbook.Close();
        }
    }
}
