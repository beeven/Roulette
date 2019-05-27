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
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.Win32;

namespace Scoreboard
{
    /// <summary>
    /// Interaction logic for ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        //private MainWindow mainWindow;
        private readonly CandidateDbContext candidateDbContext;
        public ConsoleWindow(CandidateDbContext dbContext)
        {
            InitializeComponent();
            // mainWindow = (MainWindow)this.Owner;
            candidateDbContext = dbContext;
        }

        public event EventHandler<CandidateInfo> CandidateInfoUpdated;

        public event EventHandler<RoutedEventArgs> ToggleElements;

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {

            var candidate = new CandidateInfo()
            {
                Name = tbName.Text,
                Number = tbNumber.Text,
                Company = tbCompany.Text,
                Score = tbScore.Text,
                Score1 = tbScore1.Text,
                Score2 = tbScore2.Text
            };
            CandidateInfoUpdated?.Invoke(this,candidate);
        }

        private async void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            var number = tbNumber.Text;
            if (string.IsNullOrWhiteSpace(number)) return;
            var candidate = await candidateDbContext.Candidates.FirstOrDefaultAsync(x => x.Number == number);
            tbName.Text = candidate?.Name;
            tbCompany.Text = candidate?.Company;
            tbScore.Text = "";
            tbScore1.Text = "";
            tbScore2.Text = "";
        }

        private void ToggleElementsButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleElements?.Invoke(this, e);
        }

        private async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            btnImport.IsEnabled = false;
            OpenFileDialog ofd = new OpenFileDialog
            {
                DefaultExt = "xlsx",
                Filter = "Excel 2007 workbook | *.xlsx",
                CheckFileExists = true
            };
            if (ofd.ShowDialog() == true)
            {
                candidateDbContext.RemoveRange(candidateDbContext.Candidates);
                await candidateDbContext.SaveChangesAsync();
                await ReadFromFile(ofd.FileName);
                await UpdateCount();
            }
            btnImport.IsEnabled = true;
        }

        private async Task ReadFromFile(string filename)
        {
            var workbook = new XSSFWorkbook(filename);
            var sheet = workbook.GetSheetAt(0);
            for(int i =0;i<=sheet.LastRowNum;i++)
            {
                var row = sheet.GetRow(i);
                string number;
                if(row.GetCell(0).CellType == CellType.Numeric)
                {
                    number = row.GetCell(0).NumericCellValue.ToString();
                }
                else
                {
                    number = row.GetCell(0).StringCellValue;
                }

                string name = row.GetCell(1).StringCellValue;
                string company = row.GetCell(2).StringCellValue;
                await candidateDbContext.Candidates.AddAsync(new CandidateInfo()
                {
                    Number = number,
                    Name = name,
                    Company = company
                });
            }
            workbook.Close();
            await candidateDbContext.SaveChangesAsync();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            await UpdateCount();
        }

        private async Task UpdateCount()
        {
            var count = await candidateDbContext.Candidates.CountAsync();
            tbCandidatesCount.Text = $"总记录数量: {count}";
        }
    }

}
