using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPic.Model
{
    public class Quiz
    {
        [Key]
        public int Key { get; set; }
        public string Question { get; set; }
        
        public string Answer { get; set; }

        public bool HasChosen { get; set; }

        public List<string> Selections { get; set; }
    }
}
