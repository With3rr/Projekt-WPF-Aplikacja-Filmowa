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
using System.Windows.Shapes;

namespace FrontEndApplication.FriendsWindowWithData
{
    /// <summary>
    /// Logika interakcji dla klasy ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private EngineProgram databaseConnection;
        public ChatWindow(EngineProgram engineProgram)
        {
            InitializeComponent();
            databaseConnection = engineProgram;
            Initialization();

        }
        public void Initialization()
        {
            MessagesWindow.Title = databaseConnection.connectionClass.CurrentConversationUser.Login;
            chatDG.ItemsSource = databaseConnection.connectionClass.CurrentConversation;
            IsFriendsChatWindowActive.chatWindowActive = true;
            MessagesWindow.Icon = new BitmapImage(new Uri("pack://application:,,,/Data/Images/logo.png"));
        }

        private void MessagesWindow_Closed(object sender, EventArgs e)
        {
            IsFriendsChatWindowActive.chatWindowActive = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(MessageTB.Text.Count()>0 && MessageTB.Text.Count()<=50)
            {
                databaseConnection.connectionClass.SendMessage(MessageTB.Text);
                MessageTB.Text = String.Empty;
            }            
        }

        

        private void MessageTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                SendMessageButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        }
    }
}
