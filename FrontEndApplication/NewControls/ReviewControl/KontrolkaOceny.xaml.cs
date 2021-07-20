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

namespace FrontEndApplication
{
    /// <summary>
    /// Logika interakcji dla klasy KontrolkaOceny.xaml
    /// </summary>
    public partial class KontrolkaOceny
    {

        
        public EngineProgram UsedDataBase { get; set; }

        public KontrolkaOceny()
        {
            InitializeComponent();
            AddOpinionButton.IsEnabled = false;          
        }
        public void SettReview()
        {
            reviewedScore.Text = string.Empty;
            txtOpinia.Text = string.Empty;
            AddOpinionButton.IsEnabled = false;
            if(UsedDataBase.connectionClass.CurrentMovie.MovieReview !=null)
            {
                reviewedScore.Text = UsedDataBase.connectionClass.CurrentMovie.MovieReview.Rate.ToString();
                txtOpinia.Text = UsedDataBase.connectionClass.CurrentMovie.MovieReview.Opinion;
            }            
        }
        private void Starbt1_Click(object sender, RoutedEventArgs e)
        {
            reviewedScore.Text = (sender as Button).Content.ToString();
            
            AddOpinionButton.IsEnabled = true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtOpinia.Text.Length<50)
                if(reviewedScore.Text!=String.Empty)
                    UsedDataBase.connectionClass.AddMovieOpinion(Int32.Parse(reviewedScore.Text), txtOpinia.Text);
        }

        private void txtOpinia_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddOpinionButton.IsEnabled = true;
        }
    }
}
