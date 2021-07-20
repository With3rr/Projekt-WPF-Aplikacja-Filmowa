using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.Classes
{
    public class Message
    {
        public int Id { get; set; }
        public Account Account1_Sender { get; set; }

        public Account Account2_Receiver { get; set; }

        public DateTime DateTime { get; set; }

        public string Content { get; set; }

    }
}
