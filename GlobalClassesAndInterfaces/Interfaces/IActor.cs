using GlobalClassesAndInterfaces.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Interfaces
{
    public interface IActor
    {
        int Id { get; set; }
        string PicturePath { get; set; }

        string Name { get; set; }
        string Age { get; set; }

        string Biography { get; set; }

        DateTime DateOfBirth { get; set; }

        string PlaceOfBirth { get; set; }

        double Rating { get; set; }


       ICollection<ActorInMovie> ActorInMovies { get; set; }
    }
}
