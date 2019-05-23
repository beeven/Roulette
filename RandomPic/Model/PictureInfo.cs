using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPic.Model
{
    public class PictureInfo
    {
        public Uri FilePath { get; set; }
        public bool IsSelected { get; set; }
        public bool HasBeenChosen { get; set; }
    }
}
