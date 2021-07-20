using GlobalClassesAndInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Classes
{
    public class Comment:IComment
    {
        public DateTime date { get; set; }

        public string Content { get; set; }

        public int Id { get; set; }

        public Movie Movie { get; set; }

        public Actor Actor { get; set; }

        public Account Friend { get; set; }

        public Account Account { get; set; }

       
    }
}
