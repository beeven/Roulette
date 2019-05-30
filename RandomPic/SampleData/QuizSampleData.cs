using RandomPic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPic.SampleData
{
    public class QuizSampleData : QuizViewModel
    {
        public QuizSampleData()
        {
            this.CurrentQuiz = new Quiz
            {
                Question = "关于技术创新与企业的关系，以下表述不正确的是：（  ）（单项选择题）",
                Selections = new List<string>
                            {
                                "A.企业的经营战略是推动企业发展的总体战略，技术创新战略是总体经营战略中的一个重要组成部分；",
                                "B.在现代市场经济条件下，技术创新战略的地位日益变得不重要；",
                                "C.技术创新战略有利于企业掌握产品和技术发展的方向；",
                                "D.制定技术创新战略有助于提高企业的技术素质、技术能力和技术管理水平。"
                            },
                Answer = "A",
                Key = 1
            };
            this.ShowAnswer = true;
            
        }
    }
}
