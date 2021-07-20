using GlobalClassesAndInterfaces.Classes;
using GlobalClassesAndInterfaces.NotGenericCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.DtoClasses
{
    public class MovieWithDetailsDTO
    {
        public MovieWithDetailsDTO()
        {

        }
        public MovieWithDetailsDTO(Movie movie)
        {
            Movie = movie;
        }
        public int Id { get; set; }
        public Movie Movie { get; set; }

        public MovieReview MovieReview { get;set;}

       

       public string ShortContent
        {
            get
            {
                return Movie.Plot.Substring(0, 100)+"....";

            }
            
        }
                   
        public double Rate { get; set; }

        public int  Reviewers { get; set; }

       
    }
}
