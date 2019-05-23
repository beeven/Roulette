using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPic.Model
{
    public class RandomPictureViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PictureInfo> Pictures { get; set; }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)Pictures).PropertyChanged += value;
                SelectedPictureChanged += value;
            }

            remove
            {
                ((INotifyPropertyChanged)Pictures).PropertyChanged -= value;
                SelectedPictureChanged -= value;
            }
        }

        private PictureInfo _selectedPicture;
        public PictureInfo SelectedPicture
        {
            get { return _selectedPicture; }
            set
            {
                if (_selectedPicture != value)
                {
                    _selectedPicture = value;
                    SelectedPictureChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedPicture)));
                }
            }
        }

        public event PropertyChangedEventHandler SelectedPictureChanged;



        public static RandomPictureViewModel LoadFromFolder(string path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = System.IO.Path.Combine(AppContext.BaseDirectory, "images");
            }
            var pictures = new ObservableCollection<PictureInfo>(System.IO.Directory.GetFiles(path).Select(x => new PictureInfo
            {
                FilePath = new Uri(x),
                IsSelected = false,
                HasBeenChosen = false
            }));

            return new RandomPictureViewModel()
            {
                Pictures = pictures,
                SelectedPicture = null
            };
        }
    }
}
