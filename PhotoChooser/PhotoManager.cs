using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoChooser
{
    class PhotoManager
    {
        public List<string> ChoosenPhotoFileNames { get; set; } = new List<string>();

        string startDirectoryPath;
        public string StartDirectoryPath
        {
            get { return startDirectoryPath; }
            set
            {
                startDirectoryPath = value;
                FillChoosenPhotosList();
            }
        }
        public string OutputDirectoryPath { get; set; }

        void FillChoosenPhotosList()
        {
            ChoosenPhotoFileNames.Clear();

            if (string.IsNullOrEmpty(StartDirectoryPath))
                return;

            var photos = Directory.EnumerateFiles(startDirectoryPath, "*.jpg*");

            foreach (var photo in photos)
                ChoosenPhotoFileNames.Add(Path.GetFileName(photo));
        }

        public BitmapImage GetPhoto(string photoName)
        {
            BitmapImage photo = new BitmapImage();
            photo.BeginInit();
            var pathToPhotoFile = Path.Combine(StartDirectoryPath, photoName);
            photo.UriSource = new Uri(pathToPhotoFile);
            photo.EndInit();
            return photo;
        }
    }
}
