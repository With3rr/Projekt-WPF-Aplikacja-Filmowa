using GlobalClassesAndInterfaces.Classes;
using SilnikAplikacji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrontEndApplication.FriendsWindowWithData
{

    
    public partial class FriendsWindow : Window
    {
        CollectionView widokFriends;
        public delegate void FunkcjaZwrotna(string name);
        public FunkcjaZwrotna funkcjaZwrotnaGrid;
        public ChatWindow ChatWindow { get; set; }




        private EngineProgram databaseConnection;
        public FriendsWindow(EngineProgram engineProgram)
        {
            InitializeComponent();
            databaseConnection = engineProgram;
            Initialization();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            IsFriendsChatWindowActive.friendsWindowActive = false;
            FriendOptionsPP.IsOpen = false;
        }

        private void Initialization()
        {
            IsFriendsChatWindowActive.friendsWindowActive = true;
            WFname.Text = databaseConnection.connectionClass.CurrentUser.Login;
            friendAvatarImage.DataContext = databaseConnection.connectionClass.CurrentUser.Picture;
            friendsListDG.ItemsSource = databaseConnection.connectionClass.Friends;
            widokFriends = (CollectionView)CollectionViewSource.GetDefaultView(friendsListDG.ItemsSource);
            widokFriends.Filter = FiltrFriends;

            try
            {
               
                WF.Icon = new BitmapImage(new Uri("pack://application:,,,/Data/Images/logo.png"));
                addFriendImage.Source= new BitmapImage(new Uri("pack://application:,,,/Data/Images/add-friend.png"));                
            }
            catch (Exception)
            {              
            }
            
        }
        private bool FiltrFriends(object item)
        {

            if (String.IsNullOrEmpty(MovieSearchTB.Text))
                return true;
            else
                return ((item as Account).Login.Contains(MovieSearchTB.Text));


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            funkcjaZwrotnaGrid("addFriend");

        }

       

        private void friendsListDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FriendOptionsPP.IsOpen = false;
            if (friendsListDG.SelectedItem!=null)
            {

                
                FriendOptionsPP.Placement = PlacementMode.MousePoint;
                FriendOptionsPP.IsOpen = true;
               

            }
           
        }

        private void WF_LocationChanged(object sender, EventArgs e)
        {
            FriendOptionsPP.IsOpen = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            databaseConnection.connectionClass.CurrentConversationUser = friendsListDG.SelectedItem as Account;
            FriendOptionsPP.IsOpen = false;
            if(ChatWindow!=null)
                ChatWindow.Close();
            ChatWindow = new ChatWindow(databaseConnection);
            ChatWindow.Show();
            databaseConnection.connectionClass.SettCurrentConversation();





        }
       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            databaseConnection.connectionClass.CurrentConversationUser = friendsListDG.SelectedItem as Account;
            FriendOptionsPP.IsOpen = false;
            funkcjaZwrotnaGrid("friendProfile");
         


        }
        public void CloseChat()
        {
            ChatWindow.Close();
        }

        private void MovieSearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            widokFriends.Refresh();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           
            if (databaseConnection.connectionClass.DeleteFriend(friendsListDG.SelectedItem as Account))
            {
                if (ChatWindow != null)
                    ChatWindow.Close();
                funkcjaZwrotnaGrid("closeFriendProfile");
                FriendOptionsPP.IsOpen = false;
            }
            
        }
    }
}
