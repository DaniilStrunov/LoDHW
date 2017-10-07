using System.Collections.Generic;
using System;

namespace CarRent
{
    public class Car
    {
        public Car(string model, string color)
        {
            Model = model;
            Colour = color;
            reservations = new List<Reservation>();
            RentCount = 0;
        }
        public void SentToRent(DateTime _startDate, DateTime _endDate)
        {
            
            bool value = true;
            if (reservations.Count != 0)
            {
                foreach (Reservation r in reservations)
                {
                    value = value & (_endDate < r.StartReservation | _startDate > r.EndReservation);
                }
            }
            int result = DateTime.Compare(_startDate, _endDate);
            int compareResult = DateTime.Compare(_startDate.AddMonths(2), _endDate);
            if (value )
            {
                if(result < 0)
                {
                    if (compareResult > 0)
                    {
                        RentCount++;
                        reservations.Add(new Reservation(_startDate, _endDate));
                    }
                }
            }    
            if (RentCount == 10)
            {
                RentCount = 0;
                reservations.Add(new Reservation(_endDate.AddDays(1), _endDate.AddDays(8)));
            }

        }
        public bool IsCarFreeAt(DateTime _Time)
        {
            bool value = true;
            foreach (Reservation r in reservations)
            {
                value = value & (_Time<r.StartReservation | _Time > r.EndReservation);
            }
            return value;
        }
        public string Model { get; set; }
        public string Colour { get; set; }
        public int RentCount { get; set; }
        public List<Reservation> reservations { get; set; }
    }
}
