using FrontEndApplication.FriendsWindowWithData;
using GlobalClassesAndInterfaces.Classes;
using GlobalClassesAndInterfaces.DtoClasses;
using GlobalClassesAndInterfaces.Enumerates;
using SilnikAplikacji;
using SilnikAplikacji.EmailAcces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FrontEndApplication
{

    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FriendsWindow FriendsWindow { get; set; }
        private List<Grid> mainGrids;
        private List<Grid> accountGrids;
        private EngineProgram engineProgram;
        private ObservableCollection<RadioButton> toggleButtonsGatunek;     
        private AppSettings appSettings;
        public MainWindow()
        {
            InitializeComponent();                   
            SettingInitialElements();          
        }

        /// <summary>
        /// Ustawianie podstawowych wartości elementów interfejsu graficznego(obrazy,zdjęcia).
        /// </summary>
        public void SettingInitialElements()
        {
            mainGrids = new List<Grid>();
            engineProgram = new EngineProgram();
            toggleButtonsGatunek = new ObservableCollection<RadioButton>();
            appSettings = new AppSettings();
            accountGrids = new List<Grid>();
            try
            {

                MainImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Data/Images/MainPict.png"));
                mainPict2.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/MainTitle.png"));
                MW.Icon = new BitmapImage(new Uri("pack://application:,,,/Data/Images/logo.png"));
                fbIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/facebook.png"));
                TwitterIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/twitter.png"));
                YtIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/youtube.png"));
                TikTokIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/tiktok.png"));
                TitleImage.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/MainTitle.png"));               
                friendsImage.Source= new BitmapImage(new Uri("pack://application:,,,/Data/Images/friend.png"));
                rightArrowImage.Source= new BitmapImage(new Uri("pack://application:,,,/Data/Images/right-arrow.png"));             
                leftArrowImage.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/left-arrow.png"));
                PlayTrailerImage.Source= new BitmapImage(new Uri("pack://application:,,,/Data/Images/play.png"));

                rightarrowUsers.Source= new BitmapImage(new Uri("pack://application:,,,/Data/Images/right-arrow.png"));
                leftarrowUsers.Source = new BitmapImage(new Uri("pack://application:,,,/Data/Images/left-arrow.png"));

                toggleButtonsGatunek.Add(tb1);
                toggleButtonsGatunek.Add(tb2);
                toggleButtonsGatunek.Add(tb3);
                toggleButtonsGatunek.Add(tb4);
                toggleButtonsGatunek.Add(tb5);
                toggleButtonsGatunek.Add(tb6);
                toggleButtonsGatunek.Add(tb7);
                toggleButtonsGatunek.Add(tb8);
                toggleButtonsGatunek.Add(tb9);
                toggleButtonsGatunek.Add(tb10);
                toggleButtonsGatunek.Add(tb11);
                toggleButtonsGatunek.Add(tb12);
                toggleButtonsGatunek.Add(tb13);
                toggleButtonsGatunek.Add(tb14);
                toggleButtonsGatunek.Add(tb15);
                toggleButtonsGatunek.Add(tb16);
                toggleButtonsGatunek.Add(tb17);
                toggleButtonsGatunek.Add(tb18);
                toggleButtonsGatunek.Add(tb19);
                toggleButtonsGatunek.Add(tb20);
                toggleButtonsGatunek.Add(tb21);

              

                mainGrids.Add(MoviesSeriesGrid);
                mainGrids.Add(FullMovieSeriesScreen);
                mainGrids.Add(NewsListGrid);       
                mainGrids.Add(ListOfSelectedMovies);
                mainGrids.Add(ListOfSelectedActors);                
                mainGrids.Add(FullActorScreen);
                mainGrids.Add(ContactGrid);
                mainGrids.Add(FullNewsScreen);          
                mainGrids.Add(SettingsGrid);
                mainGrids.Add(UserProfile);
   
                accountGrids.Add(FriendAddGrid);
                accountGrids.Add(InvitationGrid);
                accountGrids.Add(AccountGrid);
                accountGrids.Add(AvatarGrid);
                accountGrids.Add(PasswordGrid);
                

            }
            catch (Exception) { }
           
        }



        public void SettingsWindowActivation(string name)
        {
            if(name=="friendProfile")
            {
                ChangingGridsVisibility("UserProfile", "main");
                UserProfile.DataContext = engineProgram.connectionClass.CurrentConversationUser;
                MyCommentsUC.CommentsFor = CommentsFor.Friend;
                FriendFavouriteMenuWp.Visibility = Visibility.Visible;
                MyCommentsUC.SettComments();
            }    
                
            else if(name=="addFriend")
            {
                AddfriendError.Visibility = Visibility.Collapsed;
                ChangingGridsVisibility("SettingsGrid", "main");
                ChangingGridsVisibility("FriendAddGrid", "else");
            }              
            else if (name=="closeFriendProfile")
            {
                if(UserProfile.Visibility==Visibility.Visible && (UserProfile.DataContext as Account).Id!=engineProgram.connectionClass.CurrentUser.Id)
                    NewsMI.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));

            }
        }

        public void SettingCurrentNews (News news)
        {
            NewsFullScreenG.DataContext = news;            
            ChangingGridsVisibility("FullNewsScreen", "main");
            NewsListUC.SettNews();

        }
       
       
        public void SettingCurrentActor(Actor actor)
        {
            FullActorScreen.DataContext = actor;
            ChangingGridsVisibility("FullActorScreen", "main");
            ActorCommentsUC.SettComments();
        }

        public void SettingCurrentMovie(MovieWithDetailsDTO movieWithDetailsDTO)
        {
            FullMovieSeriesScreen.DataContext = movieWithDetailsDTO;
            ChangingGridsVisibility("FullMovieSeriesScreen", "main");
            MovieCommentsUC.SettComments();
            KontrolkaOcenyFilmu.SettReview();



        }
        


        public void SetAllData()
        {
            licznikokien.DataContext = appSettings;
            KontrolkaOcenyFilmu.UsedDataBase = engineProgram;
            MyListOfMovies.funkcjaZwrotna = SettingCurrentMovie;
            MyActorsList.funkcjaZwrotna = SettingCurrentActor;
            NewsListUC.funkcjaZwrotna = SettingCurrentNews;
            DGMoviesSeriers.ItemsSource = engineProgram.connectionClass.MoviesWithDetailsDTO;                    
            nicknameButton.Header = engineProgram.connectionClass.CurrentUser.Login;
            avatarImage.DataContext = engineProgram.connectionClass.CurrentUser.Picture;
            engineProgram.connectionClass.SetUserAvatar(UstawienieAvataru(engineProgram.connectionClass.CurrentUser.Rank));
            engineProgram.connectionClass.SettFriends();

            MyCommentsUC.engineProgram = engineProgram;
            MyCommentsUC.CommentsFor = CommentsFor.Account;
            MyCommentsUC.SettStart();


            MovieCommentsUC.engineProgram = engineProgram;
            MovieCommentsUC.CommentsFor = CommentsFor.Movie;
            MovieCommentsUC.SettStart();
            ActorCommentsUC.engineProgram = engineProgram;
            ActorCommentsUC.CommentsFor = CommentsFor.Actor;
            ActorCommentsUC.SettStart();
            MyListOfMovies.EngineProgram = engineProgram;
            MyListOfMovies.SettStart();
            MyActorsList.EngineProgram = engineProgram;
            MyActorsList.SettStart();
            NewsListUC.EngineProgram = engineProgram;
            NewsListUC.SettStart();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button).Name=="Ibt1")
                System.Diagnostics.Process.Start("https://twitter.com/cdprojektred");
            else if((sender as Button).Name == "Ibt2")
                System.Diagnostics.Process.Start("https://www.tiktok.com/pl/");
            else if((sender as Button).Name == "Ibt3")
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=Pqxpft2QUZw");
            else if((sender as Button).Name == "Ibt4")
                System.Diagnostics.Process.Start("https://www.facebook.com/netflixpolska/");
        }

        public bool DataLength(string nazwa)
        {
            return nazwa.Length < 8 ? false : true;          
        }
        private void RejestracjaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataLength(loginRejestracja.Text) && DataLength(hasloRejestracja.Password) && DataLength(emailRejestracja.Text))
            {
                LoginRejestracjaError.Visibility = Visibility.Collapsed;
                HasloRejestracjaError.Visibility = Visibility.Collapsed;
                EmailRejestracjaError.Visibility = Visibility.Collapsed;
                if (engineProgram.connectionClass.UserRejestration(loginRejestracja.Text, emailRejestracja.Text, hasloRejestracja.Password))
                {
                    UserNotInDatabase.Visibility = Visibility.Collapsed;
                    loginRejestracja.Text = string.Empty;
                    hasloRejestracja.Password = string.Empty;
                    emailRejestracja.Text = string.Empty;
                    Rejestracja.Visibility = Visibility.Collapsed;
                    Logowanie.Visibility = Visibility.Visible;
                }                   
                else
                    UserInDatabase.Visibility = Visibility.Visible;

            }
            else
            {
                UserInDatabase.Visibility = Visibility.Collapsed;
                if (DataLength(loginRejestracja.Text)==false)
                    LoginRejestracjaError.Visibility = Visibility.Visible;
                else
                    LoginRejestracjaError.Visibility = Visibility.Collapsed;
                if (DataLength(hasloRejestracja.Password) == false)
                    HasloRejestracjaError.Visibility = Visibility.Visible;
                else
                    HasloRejestracjaError.Visibility = Visibility.Collapsed;
                if (DataLength(emailRejestracja.Text) == false)
                    EmailRejestracjaError.Visibility = Visibility.Visible;
                else
                    EmailRejestracjaError.Visibility = Visibility.Collapsed;
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (DataLength(emailusernameLogin.Text) && DataLength(passwordLogin.Password))
            {
                nazwaError.Visibility = Visibility.Collapsed;
                passwordError.Visibility = Visibility.Collapsed;
                Account account = engineProgram.connectionClass.IsUserInDataBase(emailusernameLogin.Text, passwordLogin.Password);
                if (account!=null)
                {
                    if(account.BanType==null || (account.BanType=="TimeBan" && DateTime.Now.CompareTo(account.BanTime)>0))
                    {

                        Subb1.Visibility = Visibility.Collapsed;
                        if (account.AccounttType=="User")
                        {
                            SetAllData();
                            NewsMI.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
                            emailusernameLogin.Text = string.Empty;
                            passwordLogin.Password = string.Empty;
                            Subb2.Visibility = Visibility.Visible;
                            UserNotInDatabase.Visibility = Visibility.Collapsed;
                           
                        }
                        else if(account.AccounttType == "Admin")
                        {
                            Subb3.Visibility = Visibility.Visible;
                            UsersDG.ItemsSource = engineProgram.connectionClass.CurrentWatchedUsers;
                            engineProgram.connectionClass.SettCurrentWatchedUsers(0, 17, "",false);

                        }
                       

                        
                    }
                    else
                    {
                        if(account.BanType=="TimeBan")
                        {
                            BanSP.Visibility = Visibility.Visible;
                            BanNameTB.Text = "Konto zawieszone do:" + account.BanTime.ToString("dddd, dd MMMM yyyy");
                            BanReasonTB.Text = account.BanReason;

                        }
                        else if(account.BanType == "PermBan")
                        {
                            BanSP.Visibility = Visibility.Visible;
                            BanNameTB.Text = "Konto permanentnie zawieszone";
                            BanReasonTB.Text = account.BanReason;

                        }
                       

                    }

                    
                }
                else
                    UserNotInDatabase.Visibility = Visibility.Visible;
            }
            else
            {
                UserNotInDatabase.Visibility = Visibility.Collapsed;
                if (DataLength(emailusernameLogin.Text)==false)
                    nazwaError.Visibility = Visibility.Visible;
                else
                    nazwaError.Visibility = Visibility.Collapsed;
                if (DataLength(passwordLogin.Password) == false)
                    passwordError.Visibility = Visibility.Visible;
                else
                    passwordError.Visibility = Visibility.Collapsed;
            }
        }
        public string UstawienieAvataru(int pkt)
        {
            if(pkt<50)
                return "pack://application:,,,/Data/Avatars/1-rankAvatar.jpg";
            else if(pkt>=50 &&pkt<100)
                return "pack://application:,,,/Data/Avatars/2-rankAvatar.jpg";
            else if (pkt >= 100 && pkt < 200)
                return "pack://application:,,,/Data/Avatars/3-rankAvatar.jpg";
            else if (pkt >= 200 && pkt < 400)
                return "pack://application:,,,/Data/Avatars/4-rankAvatar.jpg";
            else
                return "pack://application:,,,/Data/Avatars/5-rankAvatar.jpg";

        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            Rejestracja.Visibility = Visibility.Collapsed;
            Logowanie.Visibility = Visibility.Visible;

        }

        private void ClickToRegister_Click(object sender, RoutedEventArgs e)
        {
            Rejestracja.Visibility = Visibility.Visible;
            Logowanie.Visibility = Visibility.Collapsed;
        }

        private void PrzypomnijHaslo_Click(object sender, RoutedEventArgs e)
        {
            PrzypomnienieHasla.Visibility = Visibility.Visible;
            Logowanie.Visibility = Visibility.Collapsed;
  
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Logowanie.Visibility = Visibility.Visible;
            PrzypomnienieHasla.Visibility = Visibility.Collapsed;


        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (TbEmail.Text.Length < 7 || TbEmail.Text.Contains(" "))
                mailPrzypomnienie.Visibility = Visibility.Visible;
            else
            {
                mailPrzypomnienie.Visibility = Visibility.Collapsed;
                if (engineProgram.connectionClass.ForgottenPassword(TbEmail.Text) != null)
                {                    
                    try
                    {
                        SendingEmail("youtube1925@o2.pl", TbEmail.Text, "Witaj," + engineProgram.connectionClass.ForgottenPassword(TbEmail.Text).Name, "Oto twoje hasło do platformy Gwatch :" + engineProgram.connectionClass.ForgottenPassword(TbEmail.Text).Password);
                        EmailError.Visibility = Visibility.Collapsed;
                        PotwierdzenieHasla.Visibility = Visibility.Visible;
                        PrzypomnienieHasla.Visibility = Visibility.Collapsed;
                        TbEmail.Text = string.Empty;
                    }
                    catch (Exception) { EmailError.Visibility = Visibility.Visible; }                                    
                }
                else
                    EmailError.Visibility = Visibility.Visible;
            }   
        }
        private void SendingEmail(string from,string to,string title,string body)
        {
            try
            {                                
                    MailMessage mail = new MailMessage(from,to, title,body);
                    SmtpClient client = new SmtpClient(EmailDetails.WhatServerPort(to).Server, EmailDetails.WhatServerPort(to).Port);
                    client.Credentials = new System.Net.NetworkCredential("youtube1925@o2.pl", "With3rrPL");
                    client.EnableSsl = true;
                    client.Send(mail);                                                   
            }
            catch (Exception) { throw new Exception(); }          
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            PotwierdzenieHasla.Visibility = Visibility.Collapsed;
            Logowanie.Visibility = Visibility.Visible;
        }

        private void SzukajFilm_Click(object sender, RoutedEventArgs e)
        {
            appSettings.DisplayCount = 0;
            appSettings.MovieCounter = 1;
            engineProgram.connectionClass.SetWatchedMovies(0, 8, (toggleButtonsGatunek.FirstOrDefault(n => n.IsChecked == true).Content).ToString(), (toggleButtonsGatunek.LastOrDefault(n => n.IsChecked == true).Content.ToString()), int.Parse(rokmin.Text), int.Parse(rokmax.Text),tbSearchbyName.Text,true);
        }
       

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ChangingGridsVisibility("MoviesSeriesGrid", "main");
                    
            toggleButtonsGatunek.FirstOrDefault(n => n.IsChecked == true).IsChecked = false;
            toggleButtonsGatunek.LastOrDefault(n => n.IsChecked == true).IsChecked = false;
            toggleButtonsGatunek[0].IsChecked = true;
            toggleButtonsGatunek[20].IsChecked = true;

            engineProgram.connectionClass.SetWatchedMovies(0, 8, "Wszystkie", "Wszystkie", int.Parse(rokmin.Text), int.Parse(rokmax.Text),string.Empty,true);

        }

 
        private void rokmin_TextChanged(object sender, TextChangedEventArgs e)
        {           
            int i;
            if(Int32.TryParse(rokmin.Text,out i)==false)
                rokmin.Text = "1900";
        }

        private void rokmax_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i;
            if (Int32.TryParse(rokmax.Text, out i) == false)
                rokmax.Text = "2077";
        }
        

        private void rightArrow_Click(object sender, RoutedEventArgs e)
        {
            appSettings.DisplayCount += 8;
            if(engineProgram.connectionClass.SetWatchedMovies(appSettings.DisplayCount, 8, (toggleButtonsGatunek.FirstOrDefault(n => n.IsChecked == true).Content).ToString(), (toggleButtonsGatunek.LastOrDefault(n => n.IsChecked == true).Content.ToString()), int.Parse(rokmin.Text), int.Parse(rokmax.Text), tbSearchbyName.Text, false) == true)
                appSettings.MovieCounter += 1;
            else
                appSettings.DisplayCount -= 8;
        }

        private void leftArrow_Click(object sender, RoutedEventArgs e)
        {
            if(appSettings.MovieCounter != 1)
            {
                appSettings.DisplayCount -= 8;
                engineProgram.connectionClass.SetWatchedMovies(appSettings.DisplayCount, 8, (toggleButtonsGatunek.FirstOrDefault(n => n.IsChecked == true).Content).ToString(), (toggleButtonsGatunek.LastOrDefault(n => n.IsChecked == true).Content.ToString()), int.Parse(rokmin.Text), int.Parse(rokmax.Text), tbSearchbyName.Text, false);
                appSettings.MovieCounter -= 1;               
            }          
        } 
        

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            try
            {             
               System.Diagnostics.Process.Start((FullMovieSeriesScreen.DataContext as MovieWithDetailsDTO).Movie.Trailer);
            }
            catch (Exception)
            {           
            }
        }   
        

       

       

       
       
        

      
        private void Actbt_Click(object sender, RoutedEventArgs e)
        {
            ChangingGridsVisibility("ListOfSelectedActors", "main");
            MyActorsList.SettActors(DisplayingActorList.Normal);
            
            


        }

        

        

       

        public void ChangingGridsVisibility(string name,string whatGrids)
        {
            if(whatGrids=="main")
            {
                foreach (var item in mainGrids)
                {
                    if (item.Name != name)
                        (item as Grid).Visibility = Visibility.Collapsed;
                    else
                        (item as Grid).Visibility = Visibility.Visible;
                }

            }
            else
            {
                foreach (var item in accountGrids)
                {
                    if (item.Name != name)
                        (item as Grid).Visibility = Visibility.Collapsed;
                    else
                        (item as Grid).Visibility = Visibility.Visible;
                }

            }
            

            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            errorEmailData.Visibility = Visibility.Collapsed;
            if (emailBody.Text.Length>=10 && TitleHelpMail.Text.Length>=5)
            {
                TitleHelpMail.Text = string.Empty;
                emailBody.Text = string.Empty;
                engineProgram.connectionClass.SendNotification(TitleHelpMail.Text, emailBody.Text);
            }               
            else
                errorEmailData.Visibility = Visibility.Visible;





        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            errorEmailData.Visibility = Visibility.Collapsed;
            ChangingGridsVisibility("ContactGrid", "main");         
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ChangingGridsVisibility("TopChoicesGrid", "main");                        
        }            
        private void DGMoviesSeriers_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
               
            if (DGMoviesSeriers.SelectedItem!=null)
            {
                engineProgram.connectionClass.SetCurrentlyWatchedMovie(DGMoviesSeriers.SelectedItem as MovieWithDetailsDTO);
                FullMovieSeriesScreen.DataContext = DGMoviesSeriers.SelectedItem as MovieWithDetailsDTO;                
                ChangingGridsVisibility("FullMovieSeriesScreen", "main");                  
                MovieCommentsUC.SettComments();
                KontrolkaOcenyFilmu.SettReview();
            }         
        }

     

        

      
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void settingsbt_Click(object sender, RoutedEventArgs e)
        {
            if(IsFriendsChatWindowActive.friendsWindowActive==false)
            {
                
                FriendsWindow= new FriendsWindow(engineProgram);
                FriendsWindow.Show();              
                FriendsWindow.funkcjaZwrotnaGrid += SettingsWindowActivation;
               
            }
        }

       

        private void MW_LocationChanged(object sender, EventArgs e)
        {
            ResetPopUp();


        }
        private void ResetPopUp()
        {
            var offset = FriendsPopup.HorizontalOffset;
            FriendsPopup.HorizontalOffset = offset + 1;
            FriendsPopup.HorizontalOffset = offset;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if(engineProgram.connectionClass.AddFriend(GDFriendsToAdd.SelectedItem as Account))
                GDFriendsToAdd.ItemsSource=engineProgram.connectionClass.ReturnUsersToAdd(FriendSearchTB.Text);
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            AddfriendError.Visibility = Visibility.Collapsed;
            FriendAddCodeTB.Text = engineProgram.connectionClass.CurrentUser.PersonalCode;
            ChangingGridsVisibility("FriendAddGrid", "accGrids");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            FriendInvitationsDG.ItemsSource = engineProgram.connectionClass.SendFriendInvitations();
            ChangingGridsVisibility("InvitationGrid", "accGrids");
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            AccountGrid.DataContext= engineProgram.connectionClass.CurrentUser;
            ChangingGridsVisibility("AccountGrid", "accGrids");

        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            engineProgram.connectionClass.AcceptDriendInvitation(FriendInvitationsDG.SelectedItem as Account);
            FriendInvitationsDG.ItemsSource = engineProgram.connectionClass.SendFriendInvitations();
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            engineProgram.connectionClass.RejectFriendInvitation(FriendInvitationsDG.SelectedItem as Account);
            FriendInvitationsDG.ItemsSource = engineProgram.connectionClass.SendFriendInvitations();

        }     
       

        private void MovieListChangedButton_Click(object sender, RoutedEventArgs e)
        {
           
            if((sender as MenuItem).Header.ToString()=="Ulubione")
                MyListOfMovies.SettMovies(DisplayingMovieLists.Favourite, WhoseMovieList.User, "Ulubione");
            else if ((sender as MenuItem).Header.ToString() == "Chcę zobaczyć")
                MyListOfMovies.SettMovies(DisplayingMovieLists.WantToSee, WhoseMovieList.User, "Chcę zobaczy");
            else if ((sender as MenuItem).Header.ToString() == "Ocenione")
                MyListOfMovies.SettMovies(DisplayingMovieLists.Rated, WhoseMovieList.User, "Ocenione");
            else if ((sender as MenuItem).Header.ToString() == "Nadchodzące premiery")
                MyListOfMovies.SettMovies(DisplayingMovieLists.Upcoming, WhoseMovieList.User, "Nadchodzące");
            else if ((sender as MenuItem).Header.ToString() == "Najlepsze produkcje")
                MyListOfMovies.SettMovies(DisplayingMovieLists.Top, WhoseMovieList.User, "Najlepsze");
            ChangingGridsVisibility("ListOfSelectedMovies", "main");

        }

        private void FriendMovieListChangedButton_Click(object sender, RoutedEventArgs e)
        {
            
            if ((sender as Button).Content.ToString() == "Ulubione")
                MyListOfMovies.SettMovies(DisplayingMovieLists.Favourite, WhoseMovieList.Friend, "Ulubione");
            else if ((sender as Button).Content.ToString() == "Chcę zobaczyć")
                MyListOfMovies.SettMovies(DisplayingMovieLists.WantToSee, WhoseMovieList.Friend, "Chcę zobaczy");
            else if ((sender as Button).Content.ToString() == "Ocenione")
                MyListOfMovies.SettMovies(DisplayingMovieLists.Rated, WhoseMovieList.Friend, "Ocenione");

            ChangingGridsVisibility("ListOfSelectedMovies", "main");

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            engineProgram.connectionClass.AddToFavouriteMovie();

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            engineProgram.connectionClass.WantToSeeAdd();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            
            ChangingGridsVisibility("ListOfSelectedActors", "main");
            MyActorsList.SettActors(DisplayingActorList.MovieS);
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            
            ChangingGridsVisibility("ListOfSelectedMovies", "main");
            MyListOfMovies.SettMovies(DisplayingMovieLists.ActorS,WhoseMovieList.User,"Filmy");
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            ChangingGridsVisibility("NewsListGrid", "main");
            NewsListUC.SettNews();
        }

        private void loupebt_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MovieSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MovieSearch.Text != string.Empty)
                TitlesDG.ItemsSource = engineProgram.connectionClass.ReturnSearchedMovies(MovieSearch.Text);
            else
                TitlesDG.ItemsSource = null;





        }

        private void TitlesDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            engineProgram.connectionClass.CurrentMovie = (TitlesDG.SelectedItem as MovieWithDetailsDTO);

            MovieSearch.Text = string.Empty;
            FullMovieSeriesScreen.DataContext = engineProgram.connectionClass.CurrentMovie;
            ChangingGridsVisibility("FullMovieSeriesScreen", "main");
            MovieCommentsUC.SettComments();
            KontrolkaOcenyFilmu.SettReview();





        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if (newPasswordTB.Text != newPasswordTBTwo.Text || newPasswordTB.Text.Length < 8 || engineProgram.connectionClass.SettNewPassword(oldPasswordTB.Text, newPasswordTB.Text) == false)
                bladPasswordChange.Visibility = Visibility.Visible;
            else
                bladPasswordChange.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            AvatarGrid.DataContext = engineProgram.connectionClass.CurrentUser;
            ChangingGridsVisibility("AvatarGrid", "accGrids");
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            ChangingGridsVisibility("PasswordGrid", "accGrids");
            
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            AvatarGrid.DataContext = engineProgram.connectionClass.CurrentUser;
            ChangingGridsVisibility("AvatarGrid", "accGrids");
            ChangingGridsVisibility("SettingsGrid", "main");
            
        }

        private void PasswordGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            oldPasswordTB.Text = string.Empty;
            newPasswordTB.Text = string.Empty;
            newPasswordTBTwo.Text = string.Empty;
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            engineProgram.connectionClass.SettPersonalData(NameSettings.Text, HobbySettings.Text, PersonallSettings.Text);
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            if(FriendsReviewsSW.Visibility==Visibility.Collapsed)
            {
                FriendsReviewsSW.Visibility = Visibility.Visible;
                FriendsReviewsDG.ItemsSource = engineProgram.connectionClass.ReturnUsersReviewsForMovie();

            }
            else
            {
                FriendsReviewsSW.Visibility = Visibility.Collapsed;

            }
                
           


        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            if (engineProgram.connectionClass.AddUserByCode(FriendCodeTB.Text) == false)
                AddfriendError.Visibility = Visibility.Visible;
            else
                AddfriendError.Visibility = Visibility.Collapsed;



        }

        private void FriendSearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Account> usersToAdd = engineProgram.connectionClass.ReturnUsersToAdd(FriendSearchTB.Text);
            if (FriendSearchTB.Text != string.Empty && usersToAdd.Count != 0)
            {
                GDFriendsToAdd.ItemsSource = usersToAdd;
                FriendsPopup.IsOpen = true;
            }
            else
                FriendsPopup.IsOpen = false;

        }

        private void MW_Closed(object sender, EventArgs e)
        {
            if (FriendsWindow != null)
            {
                if(FriendsWindow.ChatWindow != null)
                    FriendsWindow.CloseChat();
                FriendsWindow.Close();
            }               
        }
        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            ChangingGridsVisibility("UserProfile", "main");
            UserProfile.DataContext = engineProgram.connectionClass.CurrentUser;
            MyCommentsUC.CommentsFor = CommentsFor.Account;
            FriendFavouriteMenuWp.Visibility = Visibility.Collapsed;
            MyCommentsUC.SettComments();

        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            engineProgram.connectionClass.DeleteFavourites();

        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            engineProgram.connectionClass.DeleteWantToSee();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            engineProgram.connectionClass.ChangePersonalCode();
            FriendAddCodeTB.Text = engineProgram.connectionClass.CurrentUser.PersonalCode;
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            BanSP.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            UsersDG.ItemsSource = engineProgram.connectionClass.CurrentWatchedUsers;

            appSettings.UsersCounter = 1;
            appSettings.DisplayUsersCount = 0;
            engineProgram.connectionClass.SettCurrentWatchedUsers(0, 17,"", false);
           
        }

        private void UsersDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            UsersOptionsPP.IsOpen = false;
            if (UsersDG.SelectedItem!=null)
            {
                UsersOptionsPP.Placement = PlacementMode.MousePoint;
                UsersOptionsPP.IsOpen = true;
            }
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            UsersOptionsPP.IsOpen = false;
        }

        private void UsersLoginSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            UsersSide.Content = appSettings.UsersCounter = 1;
            appSettings.DisplayUsersCount = 0;
            engineProgram.connectionClass.SettCurrentWatchedUsers(appSettings.DisplayUsersCount, 17, UsersLoginSearch.Text,true);
           

        }

        private void Button_Click_26(object sender, RoutedEventArgs e)
        {
            appSettings.DisplayUsersCount += 17;
            
            if (engineProgram.connectionClass.SettCurrentWatchedUsers(appSettings.DisplayUsersCount, 17, UsersLoginSearch.Text,false) == true)
            {
                appSettings.UsersCounter += 1;
                UsersSide.Content = appSettings.UsersCounter;
                UsersOptionsPP.IsOpen = false;
            }
            else
                appSettings.DisplayUsersCount -= 17;


            

        }

        private void Button_Click_27(object sender, RoutedEventArgs e)
        {
            
            if (appSettings.UsersCounter != 1)
            {
                UsersOptionsPP.IsOpen = false;
                appSettings.DisplayUsersCount -= 17;
                engineProgram.connectionClass.SettCurrentWatchedUsers(appSettings.DisplayUsersCount, 17, UsersLoginSearch.Text,false);
                appSettings.UsersCounter -= 1;
                UsersSide.Content = appSettings.UsersCounter;
            }
        }

        private void Button_Click_28(object sender, RoutedEventArgs e)
        {
            if(UsersDG.SelectedItem != null)
            {
                engineProgram.connectionClass.DeleteUser(UsersDG.SelectedItem as Account);

                appSettings.UsersCounter = 1;
                appSettings.DisplayUsersCount = 0;
                engineProgram.connectionClass.SettCurrentWatchedUsers(0, 17, UsersLoginSearch.Text, false);





            }
            UsersOptionsPP.IsOpen = false;

        }

        private void Button_Click_29(object sender, RoutedEventArgs e)
        {
            UsersOptionsPP.IsOpen = false;
            if (UsersDG.SelectedItem != null)
                engineProgram.connectionClass.DeleteBan(UsersDG.SelectedItem as Account);




        }

        private void Button_Click_30(object sender, RoutedEventArgs e)
        {
            UsersOptionsPP.IsOpen = false;
            if (UsersDG.SelectedItem != null)
            {
                BanPanel.Visibility = Visibility.Visible;
                UsersDG.IsEnabled = false;

            }
               

        }

        private void Button_Click_31(object sender, RoutedEventArgs e)
        {
            BanPanel.Visibility = Visibility.Collapsed;
            if (TimeRB.IsChecked==true)
                engineProgram.connectionClass.SettBan("Time", TimeCalendar.SelectedDate, BanReasonTBox.Text, UsersDG.SelectedItem as Account);          
            else if(PermRB.IsChecked == true)
                engineProgram.connectionClass.SettBan("permBan", null, BanReasonTBox.Text, UsersDG.SelectedItem as Account);
           
            TimeRB.IsChecked = false;
            PermRB.IsChecked = false;
            BanReasonTBox.Text = string.Empty;
            UsersDG.IsEnabled = true;

        }

        private void Button_Click_32(object sender, RoutedEventArgs e)
        {
            BanPanel.Visibility = Visibility.Collapsed;
        }

        
    }
}
