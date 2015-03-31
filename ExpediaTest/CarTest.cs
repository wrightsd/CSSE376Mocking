using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestgetCarLocationReturnsRightLocation()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String carLocation = "Atlantis";
            Expect.Call(mockDB.getCarLocation(42)).Return(carLocation);


            mocks.ReplayAll();
            Car target = ObjectMother.BMW();
            target.Database = mockDB;
            String result = target.getCarLocation(42);
            Assert.AreEqual(result, carLocation);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestThatCarHasProperMilage()
        {
            IDatabase mockdb = mocks.StrictMock<IDatabase>();
            int Milage = 424242;
            Expect.Call(mockdb.Miles).PropertyBehavior();
            mockdb.Miles = Milage;
            mocks.ReplayAll();
            var target = new Car(5);
            target.Database = mockdb;
            Assert.AreEqual(target.Mileage, Milage);
            mocks.VerifyAll();

        }
	}
}
