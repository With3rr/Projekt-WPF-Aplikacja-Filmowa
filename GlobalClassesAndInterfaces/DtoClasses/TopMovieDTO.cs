using GlobalClassesAndInterfaces.Classes;
using GlobalClassesAndInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.DtoClasses
{
    public class TopMovieDTO 
    {
        public int Id { get; set; }     

        public string Name { get; set; }
           
        public string Picture { get; set; }

        public int Nr { get; set; }

        public double FinalScore { get; set; }

       
    }
}
