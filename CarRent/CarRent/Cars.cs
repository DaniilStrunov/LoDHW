using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

namespace CarRent
{
    public class Cars 
    {
        List<Car> carArray = new List<Car>();
        private string ThePathToTheJsonFile;
        public Cars(string _path)
        {
            ThePathToTheJsonFile = _path;
            if (File.Exists(ThePathToTheJsonFile))
            {
                string FileString = File.ReadAllText(ThePathToTheJsonFile);
                if (FileString != "")
                {
                    carArray = JsonConvert.DeserializeObject<List<Car>>(FileString);
                }
            }
            else
            {
                using(File.Create(ThePathToTheJsonFile))
                { }
            }
        }
        public Car FindCar( string _model, string _color)
        { 
            return carArray.Find(x => x.Model == _model && x.Colour == _color);
        }
        public void Save()
        {
            File.WriteAllText(ThePathToTheJsonFile, JsonConvert.SerializeObject(carArray));
        }
        public void AddCar(string model, string colour)
        {
            carArray.Add(new Car(model, colour));
            File.WriteAllText(ThePathToTheJsonFile, JsonConvert.SerializeObject(carArray));
        }
        public Car[] ReturnFreeCarsArray(DateTime _time)
        {
            List<Car> ListCar = new List<Car>();
            foreach( Car car in carArray)
            {
                if( car.IsCarFreeAt(_time))
                {
                    ListCar.Add(car); 
                }
            }
            Car[] List = new Car[ListCar.Count];
            List = ListCar.ToArray();
            return List;
        }
        public Car[] ReturnCarsArray()
        {
            Car[] List = new Car[carArray.Count];
            List = carArray.ToArray();
            return List;
        }
    }
}
