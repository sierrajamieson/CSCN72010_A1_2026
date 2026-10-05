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

    [TestClass]
    public class Test3
    {
        [TestMethod]
        [Description("M1-3, Typical")]

        public void ComputeTip_FixedTipMode_ReturnsFixedAmount()
        {
            //id: M1-3
            //name:  ComputeTip_FixedTipMode_ReturnsFixedAmount

            //arrange
            Splitter splitter = new Splitter();
            decimal subtotal = 120.00m;
            decimal tipInput = 15.00m;

            //act&assert
            decimal tip = splitter.ComputeTip(subtotal, TipMode.Fixed, tipInput);

            
            
        }
    }

    [TestClass]
    public class Test4
    {
        [TestMethod]
        [Description("M1-4, Negative")]
        public void ComputeTip_NegativeSubtotal_ThrowsException()

        {
            //arrange
            Splitter splitter = new Splitter();
            decimal subtotal = -10.00m;

            //act&assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => splitter.ComputeTip(subtotal, TipMode.Percent, 15.00m));

        }
    }

    [TestClass]
    public class Test5
    {
        [TestMethod]
        [Description("M1-5, Negative")]

        public void ComputeTip_NegativeFixedTip_ThrowsException()
        {
            //arrange
            Splitter splitter = new Splitter();
            decimal subtotal = 100.00m;
            decimal tipInput = -5.00m;

            //act&assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => splitter.ComputeTip(subtotal, TipMode.Fixed, tipInput));
        }

    }

    [TestClass]
    public class Test6
    {
        [TestMethod]
        [Description("M2-6, Typical")]
        public void RoundShares_NoRounding_PreservesRawDecimals()
        {
            //arrange
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>
            {
            new Share("Alice", 3.3333m),
            new Share("Bob", 3.3333m),
            new Share("Cara", 3.3334m)
            };

            //act
            List<Share> result = rounder.RoundShares(rawShares, RoundingMode.None);

            //assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(3.3333m, result[0].Amount);
            Assert.AreEqual(3.3333m, result[1].Amount);
            Assert.AreEqual(3.3334m, result[2].Amount);
        }
    }

    [TestClass]
    public class Test7
    {
        [TestMethod]
        [Description("M2-7, Edge")]
        public void RoundShares_BankersRounding_RoundsToNearestEven()
        {
            //arrange
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>
            {
            new Share("Alice", 10.325m),
            new Share("Bob", 10.335m)
            };

            //act
            List<Share> result = rounder.RoundShares(rawShares, RoundingMode.Bankers);

            //assert
            Assert.AreEqual(10.32m, result[0].Amount);
            Assert.AreEqual(10.34m, result[1].Amount);
        }
    }

    [TestClass]
    public class Test8
    {
        [TestMethod]
        [Description("M2-8, Typical")]
        public void RoundShares_RoundUp_AppliesCeilingToCents()
        {
            //arrange
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>
            {
            new Share("Alice", 10.331m)
            };

            //act
            List<Share> result = rounder.RoundShares(rawShares, RoundingMode.Ceiling);

            //assert
            Assert.AreEqual(10.34m, result[0].Amount);
        }
    }

    [TestClass]
    public class Test9
    {
        [TestMethod]
        [Description("M2-9, Typical")]
        public void RoundShares_RoundDown_AppliesFloorToCents()
        {
            //arragnge
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>
            {
            new Share("Alice", 10.339m)
            };

            //act
            List<Share> result = rounder.RoundShares(rawShares, RoundingMode.Floor);

            //assert
            Assert.AreEqual(10.33m, result[0].Amount);
        }
    }


    [TestClass]
    public class Test10
    {
        [TestMethod]
        [Description("M2-10, Edge")]
        public void RoundShares_UnevenSplit_ReconcilesRemainder()
        {
            //arrange
            Rounder rounder = new Rounder();
            decimal rawAmount = 10.00m / 3m;
            List<Share> rawShares = new List<Share>
            {
            new Share("Alice", rawAmount),
            new Share("Bob", rawAmount),
            new Share("Charlie", rawAmount)
            };

            //act
            List<Share> result = rounder.RoundShares(rawShares, RoundingMode.Bankers);

            //assert
            Assert.AreEqual(3.34m, result[0].Amount);
            Assert.AreEqual(3.33m, result[1].Amount);
            Assert.AreEqual(3.33m, result[2].Amount);
            Assert.AreEqual(10.00m, result[0].Amount + result[1].Amount + result[2].Amount);
        }
    }




































}


