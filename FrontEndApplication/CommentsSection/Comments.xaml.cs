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

namespace FrontEndApplication.CommentsSection
{
    /// <summary>
    /// Logika interakcji dla klasy Comments.xaml
    /// </summary>
    public partial class Comments : UserControl, INotifyPropertyChanged
    {
        public CommentsFor CommentsFor { get; set; }
        public int DisplayCommentCount { get; set; }
        private int _CommentCounter;
        public event PropertyChangedEventHandler PropertyChanged;
        public int CommentCounter
        {
            get
            {
                return _CommentCounter;
            }
            set
            {
                _CommentCounter = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CommentCounter"));
            }

        }
        public EngineProgram engineProgram { get; set; }



        public Comments()
        {
            InitializeComponent();
            leftarrowcomment.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/left-arrow.png"));
            rightarrowcomment.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/right-arrow.png"));



        }

        public void SettStart()
        {
            CommentsDG.ItemsSource = engineProgram.connectionClass.CurrentMovieComments;
            CommentSide.DataContext = CommentCounter;
            CommentCounter = 1;
            DisplayCommentCount = 0;

        }
        public void SettComments()
        {
            CommentCounter = 1;
            DisplayCommentCount = 0;
            engineProgram.connectionClass.CommentsReset();
            engineProgram.connectionClass.SetCurentMovieComments(DisplayCommentCount, 5, CommentsFor);
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            if(tbCommentContent.Text.Count()>0 && tbCommentContent.Text.Count() <60)
            {
                engineProgram.connectionClass.AddComment(tbCommentContent.Text, CommentsFor);
                CommentCounter = 1;
                DisplayCommentCount = 0;
                tbCommentContent.Text =String.Empty;
               
                CommentSide.Content = CommentCounter;

            }
            
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (CommentCounter != 1)
            {
                DisplayCommentCount -= 5;
                engineProgram.connectionClass.SetCurentMovieComments(DisplayCommentCount, 5, CommentsFor);
                CommentCounter -= 1;
                CommentSide.Content = CommentCounter;
            }
        }
        
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            DisplayCommentCount += 5;
            if (engineProgram.connectionClass.SetCurentMovieComments(DisplayCommentCount, 5, CommentsFor) == true)
            {
                CommentCounter += 1;
                CommentSide.Content = CommentCounter;
            }
                

            else
                DisplayCommentCount -= 5;
        }
    }
}
