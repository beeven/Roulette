using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPic.Model
{
    public class QuizViewModel : INotifyPropertyChanged
    {

        private Quiz _currentQuiz;
        public Quiz CurrentQuiz
        {
            get { return _currentQuiz; }
            set
            {
                if (_currentQuiz != value)
                {
                    _currentQuiz = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentQuiz)));
                }
            }
        }

        private bool _showAnswer;
        public bool ShowAnswer
        {
            get { return _showAnswer; }
            set
            {
                if (_showAnswer != value)
                {
                    _showAnswer = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowAnswer)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
