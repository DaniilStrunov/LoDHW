using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
     public static class  Client
    {
        public static Car[] ShowAllFreeCars(DateTime _time, Cars _cars)
        {
            return _cars.ReturnFreeCarsArray(_time);
        }
        public static void RentCar(DateTime _rentStart,DateTime _rentEnd, string _model, string _color, Cars _cars )
        {
            _cars.FindCar(_model, _color).SentToRent(_rentStart, _rentEnd);
        }

    }
}
