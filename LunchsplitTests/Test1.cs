using LunchSplit.Engine;
using LunchSplit.Model;

namespace LunchsplitTests
{
    [TestClass]
    public class Test1
    {

        [TestMethod]
        [Description("M1-1, Typical")]


        public void ComputeTip_NoTipMode_ReturnsZero()
        {
            //id: M1-1
            //name: ComputeTip_NoTipMode_ReturnsZero

            //arrange
            Splitter splitter = new Splitter();
            decimal subtotal = 100.00m;

            //act
            decimal tip = splitter.ComputeTip(subtotal, TipMode.None, 50.00m);

            //assert
            Assert.AreEqual(0.00m, tip);
        }
    }

    [TestClass]
    public class Test2
    {

        [TestMethod]
        [Description("M1-2, Typical")]


        public void ComputeTip_PercentTipMode_ReturnsCorrectPercent()
        {
            //id: M1-2
            //name: ComputeTip_PercentTipMode_ReturnsCorrectPercent

            //arrange
            Splitter splitter = new Splitter();
            decimal subtotal = 120.00m;
            decimal tipInput = 15.00m;

            //act
            decimal tip = splitter.ComputeTip(subtotal, TipMode.Percent, tipInput);

            //assert
            Assert.AreEqual(18.00m, tip);
        }

       
        }
    }

}
