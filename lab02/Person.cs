using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows;

namespace lab02
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Filename { get; set; }
        public string Random_words { get; set; }
        public Person() { }

        //public Person(String name, int age, String filename, object sender, RoutedEventArgs e)
        //{
        //    Name = name;
        //    Age = age;
        //    Filename = filename;
        //    BitmapImage bitmap = new BitmapImage();
        //    bitmap.BeginInit();
        //    bitmap.UriSource = new Uri(filename);
        //    bitmap.EndInit();
        //    myImage.Source = bitmap;
        //}
    }
}