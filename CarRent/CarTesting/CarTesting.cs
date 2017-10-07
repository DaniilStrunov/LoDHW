using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRent;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;


namespace CarTesting
{
    [TestClass]
    public class CarTesting
    {
        [TestMethod]
        public void RentCount_ShouldIncreaseByOne()
        {
            DateTime Start = new DateTime(2017, 10, 7);
            DateTime End = new DateTime(2017, 10, 10);

            string Path = @"";
            Cars CarsList = new Cars(Path);
            CarsList.AddCar("model1", "color1");
            CarsList.FindCar("model1", "color1").SentToRent(Start, End);
            CarsList.Save();
            int r = CarsList.FindCar("model1", "color1").RentCount;
            Assert.AreEqual(r, 1);
        }
        [TestMethod]
        public void RentCount_ShouldNotIncreaseByOneBecauseStartTimeIsMoreThenEndTime()
        {
            DateTime Start = new DateTime(2017, 10, 10);
            DateTime End = new DateTime(2016, 1, 10);
            string Path = @"";
            Cars CarsList1 = new Cars(Path);
            CarsList1.AddCar("model1", "color1");
            CarsList1.FindCar("model1", "color1").SentToRent(Start, End);
            CarsList1.Save();
            int r = CarsList1.FindCar("model1", "color1").RentCount;
            Assert.AreEqual(r, 0);
        }
        [TestMethod]
        public void RentCount_ShouldNotIncreaseByOneBecauseTheDifferenceBetweenIsMoreThenTwoMonth()
        {
            DateTime Start = new DateTime(2017, 10, 10);
            DateTime End = new DateTime(2018, 1, 10);
            string Path = @"";
            Cars CarsList = new Cars(Path);
            CarsList.AddCar("model1", "color1");
            CarsList.FindCar("model1", "color1").SentToRent(Start, End);
            CarsList.Save();
            int r = CarsList.FindCar("model1", "color1").RentCount;
            Assert.AreEqual(r, 0);
        }
        [TestMethod]
        public void TheCar_SouldBeFree()
        {
            DateTime Start = new DateTime(2017, 10, 7);
            DateTime End = new DateTime(2017, 10, 10);
            string Path = @"";
            Cars CarsList = new Cars(Path);
            CarsList.AddCar("model1", "color1");
            CarsList.FindCar("model1", "color1").SentToRent(Start, End);
            CarsList.Save();
            Assert.IsTrue(CarsList.FindCar("model1", "color1").IsCarFreeAt(new DateTime(2017, 10, 11)));
        }
        [TestMethod]
        public void TheCar_SouldNotBeFree()
        {
            DateTime Start = new DateTime(2017, 10, 7);
            DateTime End = new DateTime(2017, 10, 10);
            string Path = @"";
            Cars CarsList = new Cars(Path);
            CarsList.AddCar("model1", "color1");
            CarsList.FindCar("model1", "color1").SentToRent(Start, End);
            CarsList.Save();
            Assert.IsFalse(CarsList.FindCar("model1", "color1").IsCarFreeAt(new DateTime(2017, 10, 8)));
        }
        [TestMethod]
        public void TheCar_SouldBeAdded()
        {
            DateTime Start = new DateTime(2017, 10, 7);
            DateTime End = new DateTime(2017, 10, 10);
            string Path = @"";
            Cars CarsList = new Cars(Path);
            Administrator.AddNewCar("model1", "color1",CarsList );
            Car car = CarsList.FindCar("model1", "color1");
            Assert.AreEqual(car.Model, "model1");
            Assert.AreEqual(car.Colour, "color1");
        }
        [TestMethod]
        public void TheCar_SouldBeRented()
        {
            DateTime Start = new DateTime(2017, 10, 7);
            DateTime End = new DateTime(2017, 10, 10);
            string Path = @"";
            Cars CarsList = new Cars(Path);
            Administrator.AddNewCar("model1", "color1", CarsList);
            Client.RentCar(Start, End, "model1", "color1", CarsList);
            int actualNumber = CarsList.FindCar("model1", "color1").RentCount;
            Assert.AreEqual(1, actualNumber);
        }
        [TestMethod]
        public void AllCars_SholdBeInList()
        {
            string Path = @"";
            Cars CarsList = new Cars(Path);
            Administrator.AddNewCar("model1", "color1", CarsList);
            Administrator.AddNewCar("model2", "color2", CarsList);
            Administrator.AddNewCar("model3", "color3", CarsList);
            CarsList.Save();
            Car[] Cars = Administrator.ShowAllCars(CarsList);
            Car[] ActualArray = new Car[3];
            ActualArray[0] = new Car("model1", "color1");
            ActualArray[1] = new Car("model2", "color2");
            ActualArray[2] = new Car("model3", "color3");

            Assert.AreEqual(ActualArray[0].Model, Cars[0].Model);
            Assert.AreEqual(ActualArray[0].Colour, Cars[0].Colour);
            Assert.AreEqual(ActualArray[0].RentCount, Cars[0].RentCount);
            CollectionAssert.AreEqual(ActualArray[0].reservations, Cars[0].reservations);
            Assert.AreEqual(ActualArray[1].Model, Cars[1].Model);
            Assert.AreEqual(ActualArray[1].Colour, Cars[1].Colour);
            Assert.AreEqual(ActualArray[1].RentCount, Cars[1].RentCount);
            CollectionAssert.AreEqual(ActualArray[1].reservations, Cars[1].reservations);
            Assert.AreEqual(ActualArray[2].Model, Cars[2].Model);
            Assert.AreEqual(ActualArray[2].Colour, Cars[2].Colour);
            Assert.AreEqual(ActualArray[2].RentCount, Cars[2].RentCount);
            CollectionAssert.AreEqual(ActualArray[2].reservations, Cars[2].reservations);
        }
        [TestMethod]
        public void AllFreeCars_SholdBeInList()
        {
            DateTime Start = new DateTime(2017, 10, 7);
            DateTime End = new DateTime(2017, 10, 10);
            DateTime Time = new DateTime(2017, 10, 9);
            string Path = @"";
            Cars CarsList = new Cars(Path);
            Administrator.AddNewCar("model1", "color1", CarsList);
            Administrator.AddNewCar("model2", "color2", CarsList);
            Administrator.AddNewCar("model3", "color3", CarsList);
            Client.RentCar(Start, End, "model1", "color1", CarsList);
            Car[] FreeCars=  Client.ShowAllFreeCars(Time, CarsList);

            Car[] ActualArray = new Car[2];
            ActualArray[0] = new Car("model2", "color2");
            ActualArray[1] = new Car("model3", "color3");

            Assert.AreEqual(ActualArray[0].Model, FreeCars[0].Model);
            Assert.AreEqual(ActualArray[0].Colour, FreeCars[0].Colour);
            Assert.AreEqual(ActualArray[0].RentCount, FreeCars[0].RentCount);
            CollectionAssert.AreEqual(ActualArray[0].reservations, FreeCars[0].reservations);
            Assert.AreEqual(ActualArray[1].Model, FreeCars[1].Model);
            Assert.AreEqual(ActualArray[1].Colour, FreeCars[1].Colour);
            Assert.AreEqual(ActualArray[1].RentCount, FreeCars[1].RentCount);
            CollectionAssert.AreEqual(ActualArray[1].reservations, FreeCars[1].reservations);
        }
        [TestMethod]
        public void File_ShoulSaveAllCars()
        {
            string Path = @"";
            Cars CarsList = new Cars(Path);
            Administrator.AddNewCar("model1", "color1", CarsList);
            Administrator.AddNewCar("model2", "color2", CarsList);
            Administrator.AddNewCar("model3", "color3", CarsList);
            CarsList.Save();

            List<Car> ActualArray = new List<Car>();
            Car car1 = new Car("model1", "color1");
            Car car2 = new Car("model2", "color2");
            Car car3 = new Car("model3", "color3");
            ActualArray.Add(car1);
            ActualArray.Add(car2);
            ActualArray.Add(car3);

            string ActualFile = JsonConvert.SerializeObject(ActualArray);
            string FileString = File.ReadAllText(@"D:\Projects\Don'tForgetToDelet1\Json8.json");

            Assert.AreEqual(ActualFile, FileString);
        }
    }
}
