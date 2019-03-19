using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace lab02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String filename;
        String words;
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
        public static void ThreadProc()
        {
            for (int i = 0; i < float.MaxValue; i++)
            {
                Console.WriteLine("wejscie do thread");
                using (WebClient client = new WebClient())
                {
                    client.DownloadStringAsync(new Uri("https://pl.wikipedia.org/wiki/Hunter"));
                }
                Console.ReadLine();
                Thread.Sleep(2000);
            }
        }

        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            people.Add(new Person { Age = int.Parse(ageTextBox.Text), Name = nameTextBox.Text, Filename = filename });
            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
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
