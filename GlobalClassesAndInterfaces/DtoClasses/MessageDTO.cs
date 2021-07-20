using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalClassesAndInterfaces.DtoClasses
{
    public class MessageDTO
    {
        public DateTime DateTime { get; set; }

        public string Content { get; set; }

        public string Sender { get; set; }

        public string PicturePath { get; set; }

    }
}
