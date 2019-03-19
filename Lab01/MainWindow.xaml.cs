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
        public async Task WriteWord()
        {
                ResultBlock.Text = "huhuhu";
                while (true)
                {
                    Random rnd = new Random();
                    int ind = rnd.Next(1, 100);
                    ResultBlock.Text = words[ind];
                    await Task.Delay(2000);
                }
        }

        //protected void UpdateProgressBlock(string text, TextBlock textBlock)
        //{
        //    try
        //    {
        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            textBlock.Text = text;
        //        });
        //    }
        //    catch { } 
        //}
        
        //class WaitingAnimation
        //{
        //    private int maxNumberOfDots;
        //    private int currentDots;
        //    private MainWindow sender;
            

        //    public WaitingAnimation(int maxNumberOfDots, MainWindow sender)
        //    {
        //        this.maxNumberOfDots = maxNumberOfDots;
        //        this.sender = sender;
        //        currentDots = 0;
        //    }

            //public void CheckStatus(Object stateInfo)
            //{
            //    sender.UpdateProgressBlock(
            //        "Processing" + 
            //        new Func<string>(() => {
            //            StringBuilder strBuilder = new StringBuilder(string.Empty);
            //            for (int i = 0; i < currentDots; i++)
            //                strBuilder.Append(".");
            //            return strBuilder.ToString();
            //        })(), sender.progressTextBlock
            //    );
            //    if (currentDots == maxNumberOfDots)
            //        currentDots = 0;
            //    else
            //        currentDots++;
            //}
        //}

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

        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            people.Add(new Person { Age = int.Parse(ageTextBox.Text), Name = nameTextBox.Text, Filename = filename});
        }

        private void readDataButton_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                String data;
                data = client.DownloadString("https://www.ebeactive.pl/");
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