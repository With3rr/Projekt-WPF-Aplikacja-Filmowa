using DataBaseCommunication.DbModel;

using GlobalClassesAndInterfaces.Classes;
using GlobalClassesAndInterfaces.DtoClasses;
using GlobalClassesAndInterfaces.Enumerates;
using GlobalClassesAndInterfaces.Interfaces;
using GlobalClassesAndInterfaces.NotGenericCollections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilnikAplikacji.DbAcesss
{

    public class DownloadingData : IdownloadingData
    {

        private CinemaDbModel usedDataBase;

 
      
        public Account CurrentUser { get; set; }
        public Account CurrentConversationUser { get; set; }      
        public MovieWithDetailsDTO CurrentMovie { get; set; }          
        public Actor CurrentActor { get; set; }
        public News CurrentNews { get; set; }


        public UpgradedObservableCollection<Account> CurrentWatchedUsers { get; set; }


        public UpgradedObservableCollection<News> NewsList { get; set; }       
        public UpgradedObservableCollection<MovieWithDetailsDTO> MoviesWithDetailsDTO { get; set; }
        public UpgradedObservableCollection<Account> Friends { get; set; }      
        public UpgradedObservableCollection<Comment> CurrentMovieComments { get; set; }
        public UpgradedObservableCollection<Actor> CurrentListOfActors { get; set; }
        public UpgradedObservableCollection<MessageDTO> CurrentConversation { get; set; }
        public UpgradedObservableCollection<Account> UsersToAdd { get; set; }


        public bool DeleteFriend(Account account)
        {
            bool currentOrNot;
            if (CurrentConversationUser==null)
                currentOrNot = false;
            else
                currentOrNot = account.Id == CurrentConversationUser.Id ? true : false;



            Friends.Remove(account);
            usedDataBase.Friends.Remove(usedDataBase.Friends.FirstOrDefault(n => (n.Account1.Id == CurrentUser.Id && n.Account2.Id == account.Id) || (n.Account2.Id == CurrentUser.Id && n.Account1.Id == account.Id)));
            usedDataBase.SaveChanges();
            CurrentConversationUser = null;

            return currentOrNot;


        }


        public void SettPersonalData(string name,string hobby,string biography)
        {
            CurrentUser.Biography = biography;
            CurrentUser.Interests = hobby;
            CurrentUser.Name = name;
            usedDataBase.SaveChanges();

        }


        public bool SettNewPassword(string oldPassword,string newPassword)
        {
            if (CurrentUser.Password == oldPassword)
            {
                CurrentUser.Password = newPassword;
                usedDataBase.SaveChanges();
                return true;
            }
            else
                return false;
        }



        

       
        public void ChangePersonalCode()
        {
            Random random = new Random();
            CurrentUser.PersonalCode = CurrentUser.Login + random.Next(100000).ToString();
            usedDataBase.SaveChanges();
        }
        

        public DownloadingData()
        {
            usedDataBase = new CinemaDbModel();

            CurrentWatchedUsers = new UpgradedObservableCollection<Account>();
            CurrentMovieComments = new UpgradedObservableCollection<Comment>();          
            Friends = new UpgradedObservableCollection<Account>();
            UsersToAdd = new UpgradedObservableCollection<Account>();
            CurrentConversation = new UpgradedObservableCollection<MessageDTO>();
            CurrentListOfActors = new UpgradedObservableCollection<Actor>();
            MoviesWithDetailsDTO = new UpgradedObservableCollection<MovieWithDetailsDTO>();
            NewsList = new UpgradedObservableCollection<News>();

        }


        public List<MovieWithFriendReview> ReturnUsersReviewsForMovie()
        {
          

            return usedDataBase.MovieReviews.Where(n => n.Movie.Id == CurrentMovie.Movie.Id && n.Account.Id != CurrentUser.Id).ToList().Select((n) =>
            {
                return new MovieWithFriendReview() { MovieReview = n, Account = n.Account };
            }).ToList();
        }

            
        

        public List<MovieWithDetailsDTO> ReturnSearchedMovies(string text)
        {
            return usedDataBase.Movies.ToList().Select((n) =>
            {
                if (n.Name.Contains(text))
                    return new MovieWithDetailsDTO() { Movie = n, MovieReview = n.MovieReviews.FirstOrDefault(s => s.Account.Id == CurrentUser.Id), Rate = n.MovieReviews.Count() > 0 ? n.MovieReviews.Select((s) => s.Rate).Sum() / n.MovieReviews.Count() : 0, Reviewers = n.MovieReviews.Count() };
                else return null;
            }).Where(n => n != null).ToList().Select((a, c) =>
            {
                return c >= 0 && c < 5 ? a : null;
            }).Where(n => n != null).ToList();
        }

        public void DeleteUser(Account account)
        {
           
            usedDataBase.MovieReviews.RemoveRange(usedDataBase.MovieReviews.Where(n => n.Account.Id == account.Id));
            usedDataBase.WantToSeeMovies.RemoveRange(usedDataBase.WantToSeeMovies.Where(n => n.Account.Id == account.Id));
            usedDataBase.MovieInLibraries.RemoveRange(usedDataBase.MovieInLibraries.Where(n => n.Account.Id == account.Id));
            usedDataBase.Accounts.Remove(usedDataBase.Accounts.FirstOrDefault(n => n.Id == account.Id));
            usedDataBase.SaveChanges();
            

        }

        public void DeleteBan(Account account)
        {
            
            account.BanReason = null;
            account.BanType = null;
            usedDataBase.SaveChanges();
        }

        public void SendNotification(string title,string description)
        {
            usedDataBase.Notifications.Add(new Notification() { Account = CurrentUser, Date = DateTime.Now, Description = description, Title = title });
            usedDataBase.SaveChanges();

        }

        public void SettBan(string banType,DateTime ? dateTime ,string banReason,Account account)
        {
            if(banType=="permBan")
            {
                account.BanType = "Permanent";
                account.BanReason = banReason;
            }
            else
            {
                account.BanType = "Time";
                account.BanReason = banReason;
                account.BanTime = (DateTime)dateTime ;

            }
            usedDataBase.SaveChanges();

        }

        public bool SettCurrentWatchedUsers(int from,int count,string what,bool textsearch)
        {
            List<Account> users = new List<Account>();

            users.AddRange(usedDataBase.Accounts.Where(s =>s.Login.Contains(what) && s.AccounttType=="User").ToList().Select((a, c) =>
            {
                return c >= from && c < from + count ? a : null;
            }).Where(n => n != null).ToList());
            if (users.Count >= 1)
            {
                CurrentWatchedUsers.Clear();
                CurrentWatchedUsers.AddRange(users);
                return true;
            }
            else
            {
                if(textsearch==true)
                    CurrentWatchedUsers.Clear();
                return false;

            }
                



            
        }
        



        public bool SettNews(int from, int count)
        {
            List<News> news = new List<News>();

            news.AddRange(usedDataBase.News.ToList().OrderByDescending(s => s.Date).Select((a, c) =>
            {
                return c >= from && c < from + count ? a : null;
            }).Where(n => n != null).ToList());
            if (news.Count >= 1)
            {
                NewsList.Clear();
                NewsList.AddRange(news);
                return true;
            }
            else
                return false;

        }

               
        public bool SettListOfActors(int from,int count, DisplayingActorList displayingActorList,string actorname, bool textsearch)
        {
            List<Actor> actors = new List<Actor>();

            switch(displayingActorList)
            {
                case DisplayingActorList.Normal:
                    actors.AddRange(usedDataBase.Actors.Where(n=>n.Name.Contains(actorname)).ToList().Select((a, c) =>
                    {
                        return c >= from && c < from + count ? a : null;
                    }).Where(n => n != null).ToList());
                    break;
                case DisplayingActorList.MovieS:
                    actors.AddRange(usedDataBase.ActorInMovies.Where(n => n.Movie.Id == CurrentMovie.Movie.Id).ToList().Select((n) => n.Actor).Where(s=>s.Name.Contains(actorname)).Select((a, c) =>
                    {
                        return c >= from && c < from + count ? a : null;
                    }).Where(n => n != null).ToList());


                    break;

            }

            

            if (actors.Count >= 1)
            {
                CurrentListOfActors.Clear();
                CurrentListOfActors.AddRange(actors);
                return true;
            }
            else
            {
                if (textsearch == true)
                    CurrentListOfActors.Clear();
                return false;
            }
                


        }
        public void MovieListClear()
        {
            MoviesWithDetailsDTO.Clear();
        }

        public bool SettListOfActorMoviesDTO(int from,int count)
        {
            List<MovieWithDetailsDTO> movieWithDetailsDTOs = new List<MovieWithDetailsDTO>();

            
            if (movieWithDetailsDTOs.Count >= 1)
            {
                MoviesWithDetailsDTO.Clear();
                MoviesWithDetailsDTO.AddRange(movieWithDetailsDTOs);
                return true;
            }
            else
                return false;
        }

        public bool SettListOfMoviesDTO(int from, int count, DisplayingMovieLists  displayingMovieLists, WhoseMovieList  whoseMovieList)
        {
            
            Account account = whoseMovieList==WhoseMovieList.User? CurrentUser:CurrentConversationUser;                           
            List<MovieWithDetailsDTO> movieWithDetailsDTOs = new List<MovieWithDetailsDTO>();          
            switch(displayingMovieLists)
            {
                case DisplayingMovieLists.Favourite:
                    {
                        movieWithDetailsDTOs.AddRange(usedDataBase.MovieInLibraries.Where(n => n.Account.Id == account.Id).ToList().Select((a, c) =>
                        {
                            return c >= from && c < from + count ? a : null;
                        }).Where(n => n != null).ToList().Select((n) =>
                        {
                            
                            return new MovieWithDetailsDTO() { Movie = n.Movie, MovieReview = n.Movie.MovieReviews.ToList().FirstOrDefault(pp => pp.Account.Id == account.Id), Rate = n.Movie.MovieReviews.Select((k) => k.Rate).Sum() > 0 ? n.Movie.MovieReviews.Select((k) => k.Rate).Sum() / n.Movie.MovieReviews.Count() : 0, Reviewers = n.Movie.MovieReviews.Count() };
                        }));
                    }                   
                    break;
                case DisplayingMovieLists.WantToSee:
                    {
                        movieWithDetailsDTOs.AddRange(usedDataBase.WantToSeeMovies.Where(n => n.Account.Id == account.Id).ToList().Select((a, c) =>
                        {
                            return c >= from && c < from + count ? a : null;
                        }).Where(n => n != null).ToList().Select((n) =>
                        {
                            return new MovieWithDetailsDTO() {  Movie = n.Movie, MovieReview = n.Movie.MovieReviews.ToList().FirstOrDefault(pp => pp.Account.Id == account.Id), Rate = n.Movie.MovieReviews.Select((k) => k.Rate).Sum()>0? n.Movie.MovieReviews.Select((k) => k.Rate).Sum()/ n.Movie.MovieReviews.Count():0, Reviewers = n.Movie.MovieReviews.Count() };
                        }));

                    }
                    break;
                case DisplayingMovieLists.Rated:
                    movieWithDetailsDTOs.AddRange( usedDataBase.MovieReviews.Where(n=>n.Account.Id==account.Id).ToList().Select((a, c) =>
                    {
                        return c >= from && c < from + count ? a : null;
                    }).Where(n => n != null).ToList().Select((n) =>
                    {
                        return new MovieWithDetailsDTO() {  Movie = n.Movie, MovieReview = n, Rate = n.Movie.MovieReviews.Select((s) => s.Rate).Sum()>0? n.Movie.MovieReviews.Select((s) => s.Rate).Sum()/ n.Movie.MovieReviews.Select((s) => s.Rate).Count():0, Reviewers = n.Movie.MovieReviews.Select((s) => s.Rate).Count() };
                    }));

                    break;
                    
                case DisplayingMovieLists.Top:
                    movieWithDetailsDTOs.AddRange(usedDataBase.Movies.ToList().Select((a, c) =>
                    {
                        return c >= from && c<20&& c < from + count ? a : null;
                    }).Where(n => n != null).ToList().Select((n) =>
                    {
                        return new MovieWithDetailsDTO() {  Movie = n, MovieReview = n.MovieReviews.FirstOrDefault(s => s.Account.Id == account.Id), Rate = n.MovieReviews.Count()>0? n.MovieReviews.Select((s) => s.Rate).Sum()/ n.MovieReviews.Count():0,Reviewers = n.MovieReviews.Count() };


                    }).OrderByDescending(s=>s.Rate));
                    break;
                case DisplayingMovieLists.Upcoming:
                    movieWithDetailsDTOs.AddRange(usedDataBase.Movies.Where(s=>s.PremiereDate>DateTime.Today).ToList().Select((a, c) =>
                    {
                        return c >= from && c < from + count ? a : null;
                    }).Where(n => n != null).ToList().Select((n) =>
                    {
                        return new MovieWithDetailsDTO() {Movie = n };


                    }));
                    break;
                case DisplayingMovieLists.ActorS:                                   
                    movieWithDetailsDTOs.AddRange(usedDataBase.ActorInMovies.Where(n => n.Actor.Id == CurrentActor.Id).ToList().Select((a, c) =>
                    {
                        return c >= from && c < from + count ? a : null;
                    }).Where(n => n != null).ToList().Select((n) =>
                    {

                        return new MovieWithDetailsDTO() { Movie = n.Movie, MovieReview = n.Movie.MovieReviews.ToList().FirstOrDefault(pp => pp.Account.Id == CurrentUser.Id), Rate = n.Movie.MovieReviews.Select((k) => k.Rate).Sum() > 0 ? n.Movie.MovieReviews.Select((k) => k.Rate).Sum() / n.Movie.MovieReviews.Count() : 0, Reviewers = n.Movie.MovieReviews.Count() };
                    }));
                    break;

                default:
                    break;
                

            }  

            
            if (movieWithDetailsDTOs.Count >= 1)
            {
                MoviesWithDetailsDTO.Clear();
                MoviesWithDetailsDTO.AddRange(movieWithDetailsDTOs);
                return true;
            }
            else
                return false;
        }
       

        public void SettFriends()
        {
            Friends.Clear();

            Friends.AddRange(usedDataBase.Friends.ToList().Select((n) =>
            {
                if (n.Account1.Id == CurrentUser.Id && n.IsActive == true)
                    return n.Account2;
                else if (n.Account2.Id == CurrentUser.Id && n.IsActive == true)
                    return n.Account1;
                else
                    return null;
            }).ToList().Where(n => n != null).ToList());
        }

        public List<Account> ReturnUsersToAdd(string search)
        {
            return usedDataBase.Accounts.Where(n => n.Id != CurrentUser.Id && n.Login.Contains(search)).ToList().Select((a) =>
            {
                 if (usedDataBase.Friends.FirstOrDefault(n => (n.Account1.Id == a.Id && n.Account2.Id == CurrentUser.Id) || (n.Account2.Id == a.Id && n.Account1.Id == CurrentUser.Id)) == null)
                     return a;
                 else
                     return null;
            }).Where(n => n != null).Select((a, c) =>
                {
                 return c < 8 ? a : null;
                }).Where(n => n != null).ToList();



        }
        public bool AddFriend(Account account)
        {
            try
            {
                usedDataBase.Friends.Add(new Friend() { Account1 = CurrentUser, Account2 = account, IsActive = false });
                usedDataBase.SaveChanges();
                SettFriends();
                AddRankPoint(3);
                return true;

            }
            catch (Exception) { }           
            return false;

           
        }
        public bool AddUserByCode(string code)
        {
            
            Account temporary = usedDataBase.Accounts.First(n => n.PersonalCode==code);
            if(temporary == null)
                return false;
            else
            {
                Friend friend;
                if ((friend = usedDataBase.Friends.FirstOrDefault(n => (n.Account1.Id == CurrentUser.Id && n.Account2.Id == temporary.Id) || (n.Account1.Id == temporary.Id && n.Account2.Id == CurrentUser.Id)))!=null)
                {
                    if (friend.IsActive == false)
                        friend.IsActive = true;
                }

                else
                    usedDataBase.Friends.Add(new Friend() { Account1 = CurrentUser, Account2 = temporary, IsActive = true });
                usedDataBase.SaveChanges();
                Friends.Clear();
                SettFriends();
                return true;

            }

            
        }
       
        public void AcceptDriendInvitation(Account account)
        {
            usedDataBase.Friends.FirstOrDefault(n => (n.Account2.Id == CurrentUser.Id && account.Id == n.Account1.Id) || (n.Account2.Id == account.Id && CurrentUser.Id == n.Account1.Id)).IsActive = true;
            AddRankPoint(1);
            usedDataBase.SaveChanges();
            SettFriends();

        }
        public void RejectFriendInvitation(Account account)
        {

            usedDataBase.Friends.Remove(usedDataBase.Friends.FirstOrDefault(n => (n.Account2.Id == CurrentUser.Id && account.Id == n.Account1.Id) || (n.Account2.Id == account.Id && CurrentUser.Id == n.Account1.Id)));
            usedDataBase.SaveChanges();

        }
        public List<Account> SendFriendInvitations()
        {
            return usedDataBase.Friends.ToList().Select((n) =>
            {
                if (n.Account2.Id == CurrentUser.Id && n.IsActive == false)
                    return n.Account1;    
                else
                    return null;
            }).ToList().Where(n => n != null).ToList();
        }
        public void SetCurrentlyWatcheActor(Actor actor)
        {
            CurrentActor = actor;

        }

        public void SendMessage(string content)
        {
            AddRankPoint(1);
            usedDataBase.Messages.Add(new Message() { Account1_Sender = CurrentUser, Account2_Receiver = CurrentConversationUser, DateTime = DateTime.Now, Content = content });
            
            usedDataBase.SaveChanges();
            SettCurrentConversation();


        }
        public void SettCurrentConversation()
        {
            CurrentConversation.Clear();
            CurrentConversation.AddRange(usedDataBase.Messages.Where(n => (n.Account1_Sender.Id == CurrentUser.Id && n.Account2_Receiver.Id == CurrentConversationUser.Id) || (n.Account1_Sender.Id == CurrentConversationUser.Id && n.Account2_Receiver.Id == CurrentUser.Id)).OrderBy(n => n.DateTime).ToList().Select((n)=> 
            {
                return new MessageDTO() { Content = n.Content, Sender = n.Account1_Sender.Login, DateTime = n.DateTime, PicturePath=n.Account1_Sender.Picture };

    
            }));
        }

      


        public void AddMovieOpinion(int rate,string opinion)
        {
            AddRankPoint(2);
            if (CurrentMovie.MovieReview==null)
                usedDataBase.MovieReviews.Add(new MovieReview() { Movie = CurrentMovie.Movie, Account = CurrentUser, Opinion = opinion, Rate = rate });

            else
            {
                CurrentMovie.MovieReview.Opinion = opinion;
                CurrentMovie.MovieReview.Rate = rate;
            }
            usedDataBase.SaveChanges();
           
        }
        
        public void SetCurrentlyWatchedMovie(MovieWithDetailsDTO movie)
        {
            CurrentMovie = movie;
        }

        public void AddRankPoint(int points)
        {
            CurrentUser.Rank += points;
            usedDataBase.SaveChanges();

        }



        public Account ReturnCurrentUser()
        {
            return CurrentUser;
        }
        public void SetUserAvatar(string rankPicturePath)
        {
            if(CurrentUser.Picture != rankPicturePath)
                CurrentUser.Picture = rankPicturePath;
            usedDataBase.SaveChanges();
        }
        public Account IsUserInDataBase(string login, string password)
        {
            CurrentUser = null;

            return CurrentUser=usedDataBase.Accounts.FirstOrDefault(n => n.Login == login && n.Password == password);           
        }
      
        public void AddComment(string content,CommentsFor forwho)
        {
            AddRankPoint(3);
            switch (forwho)
            {
                case CommentsFor.Account:
                    usedDataBase.MoviesComments.Add(new Comment() { Content = content, date = DateTime.Now, Friend = CurrentUser, Account = CurrentUser });
                    break;

                case CommentsFor.Friend:
                    usedDataBase.MoviesComments.Add(new Comment() { Content = content, date = DateTime.Now, Friend = CurrentConversationUser, Account = CurrentUser });

                    break;
                case CommentsFor.Movie:
                    usedDataBase.MoviesComments.Add(new Comment() { Content = content, date = DateTime.Now, Movie = CurrentMovie.Movie, Account = CurrentUser });
                    break;
                case CommentsFor.Actor:
                    usedDataBase.MoviesComments.Add(new Comment() { Content = content, date = DateTime.Now, Actor = CurrentActor, Account = CurrentUser });
                    break;            
            }
             
            CurrentMovieComments.Clear();
            usedDataBase.SaveChanges();
            SetCurentMovieComments(0, 5,forwho);
        }
        public void WantToSeeAdd()
        {
            AddRankPoint(2);
            if (usedDataBase.WantToSeeMovies.FirstOrDefault(n => n.Movie.Id == CurrentMovie.Movie.Id && n.Account.Id == CurrentUser.Id)== null)
            {
                usedDataBase.WantToSeeMovies.Add(new WantToSeeMovie() { Movie = CurrentMovie.Movie, Account = CurrentUser });
                usedDataBase.SaveChanges();
            }
        }

        public void DeleteFavourites()
        {
            MovieInLibrary movieInLibrary; 
            if ((movieInLibrary=usedDataBase.MovieInLibraries.FirstOrDefault(n => n.Movie.Id == CurrentMovie.Movie.Id && n.Account.Id == CurrentUser.Id)) != null)
            {
                usedDataBase.MovieInLibraries.Remove(movieInLibrary);
                usedDataBase.SaveChanges();
            }
        }
        public void DeleteWantToSee()
        {
            WantToSeeMovie wantToSeeMovie;
            if ((wantToSeeMovie= usedDataBase.WantToSeeMovies.FirstOrDefault(n => n.Movie.Id == CurrentMovie.Movie.Id && n.Account.Id == CurrentUser.Id)) != null)
            {
                usedDataBase.WantToSeeMovies.Remove(wantToSeeMovie);
                usedDataBase.SaveChanges();
            }
        }
        public void AddToFavouriteMovie()
        {
           
            AddRankPoint(2);
            if (usedDataBase.MovieInLibraries.FirstOrDefault(n => n.Movie.Id == CurrentMovie.Movie.Id && n.Account.Id == CurrentUser.Id)==null)
            {
                usedDataBase.MovieInLibraries.Add(new MovieInLibrary() { Movie = CurrentMovie.Movie, Account = CurrentUser });
                usedDataBase.SaveChanges();
            }              
        }
        public bool UserRejestration(string login, string email,string haslo)
        {
            if (usedDataBase.Accounts.FirstOrDefault(s => s.Login == login || s.Email == email) != null)
                return false;
            else
            {
                Random random = new Random();
                usedDataBase.Accounts.Add(new Account { Login =login,BanTime=DateTime.Now, Picture="pack://application:,,,/Data/Avatars/1-rankAvatar.jpg", AccounttType= "User",  Password = haslo, Email = email, Rank = 10, PersonalCode= login+random.Next(100000).ToString()});
                usedDataBase.SaveChanges();               
                return true;
            }            
        }
        public Account ForgottenPassword(string email)
        {
            return usedDataBase.Accounts.FirstOrDefault(n => n.Email == email);
        }     
        public void CommentsReset()
        {
            CurrentMovieComments.Clear();
        }
        public bool SetCurentMovieComments(int from,int count,CommentsFor forwho)
        {

           
            List<Comment> zast = new List<Comment>();

            switch(forwho)
            {
                case CommentsFor.Friend:
                    zast.AddRange((usedDataBase.MoviesComments.Where(n => n.Friend.Id == CurrentConversationUser.Id).OrderByDescending(n => n.date).ToList()).Select((a, c) =>
                    {
                        return c >= from && c < from + count ? a : null;
                    }).Where(n => n != null).ToList());
                    break;
                case CommentsFor.Movie:
                    zast.AddRange((usedDataBase.MoviesComments.Where(n => n.Movie.Id == CurrentMovie.Movie.Id).OrderByDescending(n => n.date).ToList()).Select((a, c) =>
                    {
                        return c >= from && c < from + count ? a : null;
                    }).Where(n => n != null).ToList());
                    break;
                case CommentsFor.Actor:
                    zast.AddRange((usedDataBase.MoviesComments.Where(n => n.Actor.Id == CurrentActor.Id).OrderByDescending(n => n.date).ToList()).Select((a, c) =>
                    {
                        return c >= from && c < from + count ? a : null;
                    }).Where(n => n != null).ToList());
                    break;
                case CommentsFor.Account:
                    zast.AddRange((usedDataBase.MoviesComments.Where(n => n.Friend.Id == CurrentUser.Id).OrderByDescending(n => n.date).ToList()).Select((a, c) =>
                    {
                        return c >= from && c < from + count ? a : null;
                    }).Where(n => n != null).ToList());
                    break;
            }

            if (zast.Count>=1)
            {                
                CurrentMovieComments.AddRange(zast);
                return true;
            }
            else
                return false;

        }
       
        public bool SetWatchedMovies(int from, int count, string type, string voiceType, int minYear, int maxYear,string name,bool szukajAll)
        {
            bool nameof = name == string.Empty ? true : false;
            bool language = voiceType == "Wszystkie" ? true : false;
            bool movieType = type == "Wszystkie" ? true : false;          
            List<MovieWithDetailsDTO> zastepcze = usedDataBase.Movies.Where(n => (n.PremiereDate.Year >= minYear && n.PremiereDate.Year <= maxYear) && (language || n.Language.Contains(voiceType)) && (movieType || n.Type.Contains(type)) && (n.Name.Contains(name) || nameof)).ToList().Select((a, c) =>
            {
                  return c >= from && c < from + count ? a : null;
            }).Where(n => n != null).ToList().Select((n)=> 
             {
                  return new MovieWithDetailsDTO() {  Movie = n, MovieReview = n.MovieReviews.ToList().FirstOrDefault(pp => pp.Account.Id == CurrentUser.Id), Rate = n.MovieReviews.Select((k) => k.Rate).Sum() > 0 ? n.MovieReviews.Select((k) => k.Rate).Sum() / n.MovieReviews.Count() : 0, Reviewers = n.MovieReviews.Count() };
             }).ToList();

            if (zastepcze.Count >= 1 ||szukajAll)
            {
                MoviesWithDetailsDTO.Clear();
                MoviesWithDetailsDTO.AddRange(zastepcze);
                return true;
            }
            else
                return false;
        }

        

       
    }
    
}
