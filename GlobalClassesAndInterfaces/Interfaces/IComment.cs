using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalClassesAndInterfaces.Classes;

namespace GlobalClassesAndInterfaces.Interfaces
{
    public interface IComment
    {
        int Id { get; set; }
 
        DateTime date { get; set; }

        string Content { get; set; }

        Movie Movie { get; set; }






    }
}
