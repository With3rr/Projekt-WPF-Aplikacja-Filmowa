using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalClassesAndInterfaces;
using GlobalClassesAndInterfaces.Classes;

namespace GlobalClassesAndInterfaces.Interfaces
{
    public interface IAccount
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        int Rank { get; set; }
        string Interests { get; set; }


        ICollection<MovieInLibrary> MovieInLibraries { get; set; }

        ICollection<MovieReview> MovieReviews { get; set; }






    }
}
