using GlobalClassesAndInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Classes
{
    public class Movie : IMovie
    {

       
        public int Id { get; set; }
        public int Length { get ; set ; }
        public string Director { get ; set; }
        public int BoxOffice { get ; set ; }
      
        public string Name { get ; set ; }
        public string Producer { get ; set ; }
        public int AgeCategory { get ; set ; }
        public int Budget { get ; set ; }
        public string Country { get ; set ; }
        public double Rating { get ; set ; }
        public string Type { get ; set ; }
        public string MainActor { get ; set ; }
        public string SecondPlanActor { get ; set ; }
        public string Plot { get ; set ; }
        public string Picture { get ; set ; }

       
        public string Link1 { get ; set ; }
        public string Link2 { get ; set ; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<MovieInLibrary> MovieInLibraries { get; set; }


        public virtual ICollection<WantToSeeMovie>  WantToSeeMovies { get; set; }

        public virtual ICollection<ActorInMovie> ActorInMovies { get; set; }

        public virtual ICollection<MovieReview> MovieReviews { get; set; }
       
        public string Language { get ; set ; }
        public DateTime PremiereDate { get ; set ; }
        public string Trailer { get ; set ; }

        public string Scenario { get; set; }
    }
}
