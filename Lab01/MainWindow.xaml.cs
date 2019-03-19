using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String filename;
        String[] words;
        String link = "https://www.google.com/search?biw=667&bih=608&tbm=isch&sa=1&ei=g2WRXPDzPOWHk74P26KI-Ag&q=zwierzeta&oq=zwierzeta&gs_l=img.3..0l10.7038.8881..9517...0.0..0.116.721.8j1......1....1..gws-wiz-img.......35i39j0i67.qK0N6GGVttQ";
        List<string> urls = new List<string>();
        public async Task WriteWord()
        {
                while (true)
                {
                    Random rnd = new Random();
                    int ind = rnd.Next(1, 100);
                    ResultBlock.Text = words[ind];
                    await Task.Delay(2000);
                }
        }
        public async Task WriteImage()
        {
            while (true)
            {
                string html = GetHtmlCode();
                List<string> urls = GetUrls(html);
                var rnd = new Random();
                int randomUrl = rnd.Next(0, urls.Count - 1);
                string luckyUrl = urls[randomUrl];
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(luckyUrl);
                bitmap.EndInit();
                ResultImage.Source = bitmap;
                await Task.Delay(2000);
            }
        }

        ObservableCollection<Person> people = new ObservableCollection<Person>
        { };

        public ObservableCollection<Person> Items
        {
            get => people;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private string GetHtmlCode()
        {
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(link);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;
        }
        private List<string> GetUrls(string html)
        {
            var urls = new List<string>();

            int ndx = html.IndexOf("\"ou\"", StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
                ndx++;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);
                ndx = html.IndexOf("\"ou\"", ndx2, StringComparison.Ordinal);
            }
            return urls;
        }
        private byte[] GetImage(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return null;
                using (var sr = new BinaryReader(dataStream))
                {
                    byte[] bytes = sr.ReadBytes(100000000);

                    return bytes;
                }
            }
        }
        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            people.Add(new Person { Age = int.Parse(ageTextBox.Text), Name = nameTextBox.Text, Filename = filename});
        }

        private void readDataButton_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                String data;
                data = client.DownloadString(link);
                File.WriteAllText("C:\\Users\\Weronika\\Desktop\\studia\\SEMESTR 6\\.NET i Java\\lab2\\Lab02\\Lab01\\data.txt", data);
            }
            using (StreamReader sr = new StreamReader("data.txt"))
            {
                String line = sr.ReadToEnd();
                words = line.Split(new[] { ' ' });
            }
            try
            {
                WriteWord();
                WriteImage();
            }
            catch (Exception ex)
            {
                this.ResultBlock.Text = "Error! " + ex.Message;
            }
        }     

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filename);
            bitmap.EndInit();
            myImage.Source = bitmap;
        }

        private void AddNewImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\Users\\Weronika\\Desktop\\studia\\SEMESTR 6\\.NET i Java\\lab1\\Lab01\\Lab01";
            fileDialog.Filter = "Image files (*.png)|*.png|All Files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == true)
                filename = fileDialog.FileName;
        }
    }
}