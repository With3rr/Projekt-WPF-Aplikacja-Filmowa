using GlobalClassesAndInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Classes
{
    public class Account : IAccount
    {
       
        public Account()
        {
                  
        }
        public int Id { get ; set ; }
        public string Name { get ; set ; }

        public string LastName { get; set; }
        public string Email { get ; set ; }
        public string Login { get ; set ; }
        public string Password { get ; set ; }
        public int Rank { get ; set ; }
        public string Interests { get ; set ; }

        private string rankName;

        public string BanType { get; set; }



        public DateTime BanTime { get; set; }

        public string BanReason { get; set; }

        public string AccounttType { get; set; }

        public string RankName
        {
            get
            {
                if (Rank < 50)
                    return "Początkujący";
                else if (Rank >= 50 && Rank < 100)
                    return "Żołnierz";
                else if (Rank >= 100 && Rank < 200)
                    return "Plutonowy";
                else if (Rank >= 200 && Rank < 400)
                    return "Kapral";
                else
                    return "Lider";

            }
            set
            {
                rankName = value;

            }
        }

        public string Biography { get; set; }

        public string Picture { get; set; }

        public string PersonalCode { get; set; }


        public ICollection<MovieInLibrary> MovieInLibraries { get; set; }

        public virtual ICollection<WantToSeeMovie> WantToSeeMovies { get; set; }

        public virtual ICollection<MovieReview> MovieReviews { get; set; }

        public virtual ICollection<Notification> UserNotifications { get; set; }

        










    }
}
