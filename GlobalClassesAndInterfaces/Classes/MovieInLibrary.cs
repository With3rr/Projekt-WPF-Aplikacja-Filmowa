using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalClassesAndInterfaces.Interfaces;

namespace GlobalClassesAndInterfaces.Classes
{
    public class MovieInLibrary : IMovieInLibrary
    {
        public int Id { get ; set ; }

        public virtual Account Account { get; set; }
        public virtual Movie Movie { get; set; }

       

      
    }
}
