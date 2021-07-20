using GlobalClassesAndInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Classes
{
    public class MovieReview : IMovieReview
    {
        public MovieReview()
        {

        }
        public MovieReview(int rate,string opinion,Movie movie,Account account)
        {
            Rate = rate;

            Opinion = opinion;

            Movie= movie;

            Account=account;
        }
        public int Id { get ; set ; }
        public int Rate { get ; set ; }
        public string Opinion { get ; set ; }
        public virtual Movie Movie { get; set ; }

        public virtual Account Account { get; set; }
    }
}
