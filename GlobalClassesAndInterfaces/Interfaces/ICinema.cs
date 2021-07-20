using GlobalClassesAndInterfaces.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Interfaces
{
    public interface ICinema
    {
        int Id { get; set; }
        string Name { get; set; }
        string Producer { get; set; }

        int AgeCategory { get; set; }

        int Budget { get; set; }

        string Country { get; set; }

        double Rating { get; set; }

        string Type { get; set; }

        string MainActor { get; set; }

        string SecondPlanActor { get; set; }

        string Plot { get; set; }
       

        string Picture { get; set; }
        string Link1 { get; set; }
        string Link2 { get; set; }

        string Trailer { get; set; }


        DateTime PremiereDate { get; set; }


        string Language { get; set; }

      













    }
}
