using GlobalClassesAndInterfaces.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.DtoClasses
{
    public class MovieWithFriendReview
    {
        
        public Account Account { get; set; }
        public MovieReview MovieReview { get; set; }

    }
}
