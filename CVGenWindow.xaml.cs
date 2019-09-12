using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
using System.Diagnostics;
using System.IO;

namespace CVGen
{
    /// <summary>
    /// Логика взаимодействия для CVGenWindow.xaml
    /// </summary>
    public partial class CVGenWindow : Window
    {
        private string Image64;
        private string template = "";

        public CVGenWindow()
        {
            InitializeComponent();
            GenerateCVButton.IsEnabled = false;
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png"
            };
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                byte[] imageArray = System.IO.File.ReadAllBytes(op.FileName);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                Image64 = base64ImageRepresentation;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var data = new
            {
                FullName = FullName.Text,
                JobTitle = JobTitle.Text,
                Experience = Experience.Text,
                Education = Education.Text,
                Hobbies = Hobbies.Text,
                Image64,
            };

            CvGenerator cvGenerator = new CvGenerator();
            cvGenerator.ConvertJSONToCV(data, this.template);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Select a template",
                Filter = "HTML only|*.html5;*.htm;*.html"
            };
            if (op.ShowDialog() == true)
            {
                string text = File.ReadAllText(op.FileName);
                this.template = text;
            }
            GenerateCVButton.IsEnabled = true;
        }
    }
}
