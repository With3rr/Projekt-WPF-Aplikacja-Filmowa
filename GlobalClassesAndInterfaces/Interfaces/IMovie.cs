using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalClassesAndInterfaces.Classes;
namespace GlobalClassesAndInterfaces.Interfaces
{
    public interface IMovie:ICinema
    {
        

        int Length { get; set; }

        string Director { get; set; }

        int BoxOffice { get; set; }

        string Scenario { get; set; }



       


    }
}
