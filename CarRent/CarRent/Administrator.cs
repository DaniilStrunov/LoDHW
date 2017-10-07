using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public static class Administrator
    {
        public static Car[] ShowAllCars(Cars _cars)
        {
            return _cars.ReturnCarsArray();
        }
        public static void AddNewCar(string _model, string _color, Cars _cars)
        {
            _cars.AddCar(_model, _color);
        }
    }
}
