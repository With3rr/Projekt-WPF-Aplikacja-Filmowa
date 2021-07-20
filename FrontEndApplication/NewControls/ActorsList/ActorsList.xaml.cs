using GlobalClassesAndInterfaces.Classes;
using GlobalClassesAndInterfaces.Enumerates;
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

namespace FrontEndApplication.NewControls.ActorsList
{
    /// <summary>
    /// Logika interakcji dla klasy ActorsList.xaml
    /// </summary>
    public partial class ActorsList : UserControl
    {
        public delegate void FunkcjaZwrotna(Actor movieWithDetailsDTO);

        public FunkcjaZwrotna funkcjaZwrotna;
        public EngineProgram EngineProgram { get; set; }
        public int DisplayActorCount { get; set; }
        public DisplayingActorList DisplayingActorList { get; set; }     
        public int ActorCounter { get; set; }
        public ActorsList()
        {
            InitializeComponent();

            leftarrowMovie.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/left-arrow.png"));
            rightarrowMovie.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/right-arrow.png"));
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (ActorCounter != 1)
            {
                DisplayActorCount -= 6;              
                EngineProgram.connectionClass.SettListOfActors(DisplayActorCount, 6, DisplayingActorList, "", false);
                ActorCounter -= 1;
                ActorSide.Content = ActorCounter;
            }
        }
        public void SettStart()
        {
            ActorsDG.ItemsSource = EngineProgram.connectionClass.CurrentListOfActors;
            actorListTitle.Text = "Aktorzy";

        }
        public void SettActors(DisplayingActorList what)
        {
            ActorSearchbyNameTB.Text = string.Empty;
            DisplayingActorList = what;
            EngineProgram.connectionClass.CurrentListOfActors.Clear();
            ActorSide.Content = ActorCounter = 1;
            DisplayActorCount = 0;
            EngineProgram.connectionClass.SettListOfActors(DisplayActorCount, 6,DisplayingActorList, "", false);           
        }
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            DisplayActorCount += 6;
            if (EngineProgram.connectionClass.SettListOfActors(DisplayActorCount, 6, DisplayingActorList,"",false) == true)
            {
                ActorCounter += 1;
                ActorSide.Content = ActorCounter;
            }
            else
                DisplayActorCount -= 6;






        }
        private void DataGridRow_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void ActorsDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DisplayActorCount = 0;
            ActorCounter = 1;
            EngineProgram.connectionClass.SetCurrentlyWatcheActor(ActorsDG.SelectedItem as Actor);
            funkcjaZwrotna(ActorsDG.SelectedItem as Actor);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DisplayActorCount = 0;
            ActorCounter = 1;
            EngineProgram.connectionClass.SettListOfActors(DisplayActorCount, 6, DisplayingActorList,ActorSearchbyNameTB.Text,true);
           

        }
    }
}
