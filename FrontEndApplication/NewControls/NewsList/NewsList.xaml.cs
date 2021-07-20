using GlobalClassesAndInterfaces.Classes;
using SilnikAplikacji;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontEndApplication.NewControls.NewsList
{
    /// <summary>
    /// Logika interakcji dla klasy NewsList.xaml
    /// </summary>
    /// 
   
    public partial class NewsList : UserControl
    {
        public delegate void FunkcjaZwrotna(News news);

        public FunkcjaZwrotna funkcjaZwrotna;
      
        public EngineProgram EngineProgram { get; set; }
        public int DisplayNewsCount { get; set; }

        public int NewsCounter { get; set; }

        public NewsList()
        {
            InitializeComponent();
            leftarrowMovie.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/left-arrow.png"));
            rightarrowMovie.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/right-arrow.png"));
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (NewsCounter != 1)
            {
                DisplayNewsCount -= 9;
                EngineProgram.connectionClass.SettNews(DisplayNewsCount, 9);
                NewsCounter -= 1;
                MovieSide.Content = NewsCounter;
            }
        }
        public void SettStart()
        {
            NewsDG.ItemsSource = EngineProgram.connectionClass.NewsList;


        }
        public void SettNews()
        {
            EngineProgram.connectionClass.MoviesWithDetailsDTO.Clear();
           
            MovieSide.DataContext = NewsCounter = 1;
            DisplayNewsCount = 0;
            

            EngineProgram.connectionClass.SettNews(DisplayNewsCount,9);


        }
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            DisplayNewsCount += 9;
            if (EngineProgram.connectionClass.SettNews(DisplayNewsCount, 9) == true)
            {
                NewsCounter += 1;
                MovieSide.Content = NewsCounter;
            }
            else
                DisplayNewsCount -= 9;
        }

        private void MoviesDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            EngineProgram.connectionClass.CurrentNews=((NewsDG.SelectedItem as News));

            funkcjaZwrotna((NewsDG.SelectedItem as News));




        }


        private void DataGridRow_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

       
    }
}
