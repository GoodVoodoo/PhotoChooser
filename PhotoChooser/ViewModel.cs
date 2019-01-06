using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PhotoChooser
{
    class ViewModel : INotifyPropertyChanged
    {
        public string StartDirectoryName { get; set; }
        public string OutputDirectoryName { get; set; }

        string initialPhotoDirectoryName;
        public string InitialPhotoDirectoryName
        {
            get { return initialPhotoDirectoryName; }
            set {
                initialPhotoDirectoryName = value;
                photoManager.StartDirectoryPath = initialPhotoDirectoryName;
                FillInitilaPhotosListFromModel();
            }
        }
        public BitmapImage CurrentPhoto { get; set; }
        public string CurrentPhotoName { get; set; }
        public ObservableCollection<string> InintilalPhotosList { get; set; } = new ObservableCollection<string>();
        public ICommand OpenInitialPhotoDirectoryCommand { get; set; }
        public ICommand OpenNextPhotoCommand { get; set; }
        public ICommand OpenPreviousPhotoCommand { get; set; }

        readonly PhotoManager photoManager = new PhotoManager();

        public ViewModel()
        {
            OpenInitialPhotoDirectoryCommand = new RelayCommand(p => true, a => OpenInitialPhotoDirectory());
            OpenNextPhotoCommand = new RelayCommand(p => true, a => ShowNextPhoto());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OpenInitialPhotoDirectory()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    InitialPhotoDirectoryName = folderBrowserDialog.SelectedPath;
                    OnPropertyChanged(nameof(InitialPhotoDirectoryName));
                }
            }
        }

        void FillInitilaPhotosListFromModel()
        {
            foreach (var photo in photoManager.ChoosenPhotoFileNames)
                InintilalPhotosList.Add(photo);

            CurrentPhotoName = InintilalPhotosList.First();
            OnPropertyChanged(CurrentPhotoName);
            CurrentPhoto = photoManager.GetPhoto(CurrentPhotoName);
            OnPropertyChanged(nameof(CurrentPhoto));
        }

        void ShowNextPhoto()
        {
            string nextPhotoName = InintilalPhotosList[InintilalPhotosList.IndexOf(CurrentPhotoName) + 1];
            CurrentPhotoName = nextPhotoName;
            OnPropertyChanged(CurrentPhotoName);
            CurrentPhoto = photoManager.GetPhoto(CurrentPhotoName);
            OnPropertyChanged(nameof(CurrentPhoto));
        }

        void ShowPreviousPhoto()
        {

        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
