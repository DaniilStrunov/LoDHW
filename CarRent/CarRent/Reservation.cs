using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public  class Reservation
    {
       public DateTime StartReservation { get; set; }
        public DateTime EndReservation { get; set; }

        public Reservation(DateTime _start, DateTime _end)
        {
            StartReservation = _start;
            EndReservation = _end;
        }

    }
   
}
