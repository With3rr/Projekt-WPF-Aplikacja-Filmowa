
using GlobalClassesAndInterfaces.DtoClasses;
using GlobalClassesAndInterfaces.Enumerates;
using SilnikAplikacji;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FrontEndApplication.NewControls.MoviesList
{
   
    public partial class MoviesList : UserControl
    {

        CollectionView widokFilmow;

        public delegate void FunkcjaZwrotna(MovieWithDetailsDTO movieWithDetailsDTO);

        public FunkcjaZwrotna funkcjaZwrotna;
        private WhoseMovieList UserOrFriend { get; set; }
        public EngineProgram EngineProgram { get; set; }
        public int DisplayMovieCount { get; set; }
        public DisplayingMovieLists WhatMovies { get; set; }
        public int MovieCounter { get; set; }

        public MoviesList()
        {
            InitializeComponent();
            leftarrowMovie.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/left-arrow.png"));
            rightarrowMovie.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/right-arrow.png"));
            
        }

       
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (MovieCounter != 1)
            {
                DisplayMovieCount -= 6;              
                EngineProgram.connectionClass.SettListOfMoviesDTO(DisplayMovieCount, 6, WhatMovies, UserOrFriend);             
                MovieCounter -= 1;
                MovieSide.Content = MovieCounter;
            }
        }
        public void SettStart()
        {
            MoviesDG.ItemsSource = EngineProgram.connectionClass.MoviesWithDetailsDTO;
            
        }
        public void SettMovies(DisplayingMovieLists what, WhoseMovieList userOrFriend,string title)
        {
            EngineProgram.connectionClass.MoviesWithDetailsDTO.Clear();
            WhatMovies = what;
            UserOrFriend = userOrFriend;
            MovieSide.DataContext = MovieCounter = 1;
            DisplayMovieCount = 0;
            movieListTitle.Text = title;           
            EngineProgram.connectionClass.SettListOfMoviesDTO(DisplayMovieCount, 6,WhatMovies,UserOrFriend);

            UstalanieWidokow();


        }


        public void UstalanieWidokow()
        {
            widokFilmow = null;
            GatunekCB.SelectedIndex = 0;
            RokCB.SelectedIndex = 0;
            PopularityCB.SelectedIndex = 0;
            KrajCB.SelectedIndex = 0;
            widokFilmow = (CollectionView)CollectionViewSource.GetDefaultView(MoviesDG.ItemsSource);
            widokFilmow.Filter += MainFiltr;



        }

        public bool MainFiltr(object item)
        {
            return ((FiltrRok(item) && FiltrGatunek(item) && FiltrKraj(item)) ? true : false);
        }

       
        private bool FiltrRok(object item)
        {
            if (RokCB.SelectedIndex == 0)
                return true;
            else if(RokCB.SelectedIndex==2)
            {
                try
                {                    
                    return ((item as MovieWithDetailsDTO).Movie.PremiereDate.Year == Int32.Parse(((RokCB.SelectedItem as ComboBoxItem).Content as TextBox).Text));
                }
                catch (Exception)
                {
                    return false;
                }
            }                              
            else
                return ((item as MovieWithDetailsDTO).Movie.PremiereDate.Year == Int32.Parse((RokCB.SelectedItem as ComboBoxItem).Content.ToString()));

        }
        private bool FiltrKraj(object item)
        {
            if (KrajCB.SelectedIndex == 0)
                return true;
            else
                return ((item as MovieWithDetailsDTO).Movie.Country==(KrajCB.SelectedItem as ComboBoxItem).Content.ToString());


        }

        private bool FiltrPopularność(object item)
        {
            if (GatunekCB.SelectedIndex == 0)
                return true;
            else
                return ((item as MovieWithDetailsDTO).Movie.Type.Contains((GatunekCB.SelectedItem as ComboBoxItem).Content.ToString()));


        }

        private bool FiltrGatunek(object item)
        {
            if(GatunekCB.SelectedIndex==0)
                return true;
            else
                return ((item as MovieWithDetailsDTO).Movie.Type.Contains((GatunekCB.SelectedItem as ComboBoxItem).Content.ToString()));


        }
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            DisplayMovieCount += 6;
            if (EngineProgram.connectionClass.SettListOfMoviesDTO(DisplayMovieCount, 6, WhatMovies, UserOrFriend) == true)
            {
                MovieCounter += 1;
                MovieSide.Content = MovieCounter;
            }
            else
                DisplayMovieCount -= 6;
        }

        private void MoviesDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            EngineProgram.connectionClass.SetCurrentlyWatchedMovie((MoviesDG.SelectedItem as MovieWithDetailsDTO));
            funkcjaZwrotna((MoviesDG.SelectedItem as MovieWithDetailsDTO));      
        }

        
        private void DataGridRow_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void GatunekCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GatunekCB.SelectedIndex == 1)
                GatunekCB.SelectedIndex = 0;

            if(widokFilmow!=null)
                widokFilmow.Refresh();


        }

        private void PopularityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PopularityCB.SelectedIndex == 1)
            {
                PopularityCB.SelectedIndex = 0;
                widokFilmow.SortDescriptions.Clear();
                widokFilmow.Refresh();

            }
                

            if(widokFilmow!=null)
            {
                widokFilmow.SortDescriptions.Clear();
                if (PopularityCB.SelectedIndex == 2)
                    widokFilmow.SortDescriptions.Add(new SortDescription("Reviewers", ListSortDirection.Descending));
                else
                    widokFilmow.SortDescriptions.Add(new SortDescription("Reviewers", ListSortDirection.Ascending));
            }
            

           
                

        }

        private void KrajCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KrajCB.SelectedIndex == 1)
                KrajCB.SelectedIndex = 0;

            if (widokFilmow != null)
                widokFilmow.Refresh();
        }

        private void RokCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RokCB.SelectedIndex == 1)
                RokCB.SelectedIndex = 0;

            if (widokFilmow != null)
                widokFilmow.Refresh();

        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                widokFilmow.Refresh();

        }
    }
}
