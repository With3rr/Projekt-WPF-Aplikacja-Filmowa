using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalClassesAndInterfaces.Classes;

namespace GlobalClassesAndInterfaces.Interfaces
{
    public interface IMovieInLibrary
    {

        int Id { get; set; }

        Account Account { get; set; }
        Movie Movie { get; set; }
    }
}
