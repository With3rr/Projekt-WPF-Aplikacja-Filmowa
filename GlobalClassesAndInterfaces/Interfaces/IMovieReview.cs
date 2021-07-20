using GlobalClassesAndInterfaces.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Interfaces
{
    public interface IMovieReview
    {

        int Id { get; set; }

        int Rate { get; set; }

        string Opinion { get; set; }

        Movie Movie { get; set; }
    }
}
