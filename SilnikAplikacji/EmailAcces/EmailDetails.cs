using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilnikAplikacji.EmailAcces
{
    public static class EmailDetails
    {
        public static EmailServersAndPorst WhatServerPort(string name)
        {
            if (name.Contains("@o2.pl") || name.Contains("@go2.pl") || name.Contains("@tlen.pl") || name.Contains("@prokonto.pl"))
            {
                return new EmailServersAndPorst() { Port = 587, Server = "poczta.o2.pl" };
               
            }
            else if (name.Contains("@gmail.com"))
            {
                return new EmailServersAndPorst() { Port = 465, Server = "smtp.gmail.com" };
            }
            else if (name.Contains("@onet.pl"))
            {
                
                return new EmailServersAndPorst() { Port = 465, Server = "smtp.poczta.onet.pl" };
            }
            else
            {
                return null;
            }
        }



    }
}
