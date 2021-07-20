using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndApplication
{
    public  class AppSettings:INotifyPropertyChanged
    {
        public AppSettings()
        {
            DisplayCount = 0;
            MovieCounter = 1;

            UsersCounter = 1;
            DisplayUsersCount = 0;



        }
        private int _MovieCounter;
        public int MovieCounter
        {
            get { return _MovieCounter; }
            set
            {
                _MovieCounter = value;
                if(PropertyChanged!=null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MovieCounter"));
            }
        }
        public int DisplayCount { get; set; }


        public int UsersCounter { get; set; }

        public int DisplayUsersCount { get; set; }
       
        public event PropertyChangedEventHandler PropertyChanged;

        
    }
}
