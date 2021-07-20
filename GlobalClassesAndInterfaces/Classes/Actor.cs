using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Classes
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PicturePath { get; set; }

        public string Age { get; set; }

        public int Growth { get; set; }

        public string Biography { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public double Rating { get; set; }

        public string Gender { get; set; }



        public virtual ICollection<ActorInMovie> ActorInMovies { get; set; }

    }
}
