using LunchSplit.Engine;
using LunchSplit.Helper;
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
            //name: ComputeTip_FixedTipMode_ReturnsFixedAmount

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
            //id: M1-4
            //name: ComputeTip_NegativeSubtotal_ThrowsException

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
            //id: M1-5
            //name: ComputeTip_NegativeFixedTip_ThrowsException

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
            //id: M2-6
            //name: RoundShares_NoRounding_PreservesRawDecimals

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
            //id: M2-7
            //name: RoundShares_BankersRounding_RoundsToNearestEven

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
            //id: M2-8
            //name: RoundShares_RoundUp_AppliesCeilingToCents

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
            //id: M2-9
            //name: RoundShares_RoundDown_AppliesFloorToCents

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
            //id: M2-10
            //name: RoundShares_UnevenSplit_ReconcilesRemainder

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

    [TestClass]
    public class Test11
    {
        [TestMethod]
        [Description("M2-11, Edge")]
        public void RoundShares_EmptyShareCollection_ReturnsEmpty()
        {
            //id: M2-11
            //name: RoundShares_EmptyShareCollection_ReturnsEmpty

            //arrange
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>();

            //act
            List<Share> result = rounder.RoundShares(rawShares, RoundingMode.Bankers);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }

    [TestClass]
    public class Test12
    {
        [TestMethod]
        [Description("M2-12, Typical")]
        public void Validate_CompleteBillDetails_ReturnsOk()
        {
            //id: M2-12
            //name: Validate_CompleteBillDetails_ReturnsOk

            //arrange
            Rounder rounder = new Rounder();
            List<Share> rawShares = new List<Share>();

            //act
            List<Share> result = rounder.RoundShares(rawShares, RoundingMode.Bankers);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }

    [TestClass]
    public class Test13
    {
        [TestMethod]
        [Description("M3-13, Negative")]
        public void Validate_EmptyAttendeeCollection_ReturnsFail()
        {
            //id: M3-13
            //name: Validate_EmptyAttendeeCollection_ReturnsFail

            //arrange
            BillValidator validator = new BillValidator();
            Bill bill = new Bill(100.00m, 13.00m, TipMode.None, 0m);
            List<Attendee> attendees = new List<Attendee>();

            //act
            ValidationResult result = validator.Validate(bill, attendees);

            //assert
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }

    [TestClass]
    public class Test14
    {
        [TestMethod]
        [Description("M3-14, Negative")]
        public void Validate_NegativeSubtotal_ReturnsFail()
        {
            //id: M3-14
            //name: Validate_NegativeSubtotal_ReturnsFai

            //range
            BillValidator validator = new BillValidator();
            Bill bill = new Bill(-50.00m, 6.50m, TipMode.None, 0m);
            List<Attendee> attendees = new List<Attendee>
            {
            new Attendee("Alice", 1, true)
            };

            //act
            ValidationResult result = validator.Validate(bill, attendees);

            //assert
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }

    [TestClass]
    public class Test15
    {
        [TestMethod]
        [Description("M3-15, Negative")]
        public void Validate_NegativeTax_ReturnsFail()
        {
            //id: M3-15
            //name: Validate_NegativeTax_ReturnsFail

            //arrange
            BillValidator validator = new BillValidator();
            Bill bill = new Bill(100.00m, -5.00m, TipMode.None, 0m);
            List<Attendee> attendees = new List<Attendee>
            {
            new Attendee("Alice", 1, true)
            };

            //act
            ValidationResult result = validator.Validate(bill, attendees);

            //assert
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }

    [TestClass]
    public class Test16
    {
        [TestMethod]
        [Description("M3-16, Negative")]
        public void Validate_ZeroAttendees_ReturnsFail()
        {

            //id: M3-16
            //name: Validate_ZeroAttendees_ReturnsFail

            //arrange
            BillValidator validator = new BillValidator();
            Bill bill = new Bill(100.00m, 13.00m, TipMode.None, 0m);
            List<Attendee> attendees = new List<Attendee>();

            //act
            ValidationResult result = validator.Validate(bill, attendees);

            //assert
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }

    [TestClass]
    public class Test17
    {
        [TestMethod]
        [Description("M4-17, Typical")]
        public void CalculateShares_EqualSplit_ApportionsEvenly()
        {
            //id: M4-17
            //name: CalculateShares_EqualSplit_ApportionsEvenly

            //arrange
            Splitter splitter = new Splitter();
            Bill bill = new Bill(90.00m, 0m, TipMode.None, 0m);
            List<Attendee> attendees = new List<Attendee>
            {
            new Attendee("Alice", 1, true),
            new Attendee("Bob", 1, true),
            new Attendee("Cara", 1, true)
            };

            //act
            List<Share> result = splitter.CalculateShares(bill, attendees, RoundingMode.Bankers);

            //assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(30.00m, result[0].Amount);
            Assert.AreEqual(30.00m, result[1].Amount);
            Assert.AreEqual(30.00m, result[2].Amount);
        }
    }

    [TestClass]
    public class Test18
    {
        //id: M4-18
        //name: CalculateShares_ProportionalSplit_ApportionsWeighted

        [TestMethod]
        [Description("M4-18, Typical")]
        public void CalculateShares_ProportionalSplit_ApportionsWeighted()
        {
            //id: M4-18
            //name: CalculateShares_EqualSplit_ApportionsEvenly

            //arrange
            Splitter splitter = new Splitter();
            Bill bill = new Bill(90.00m, 0m, TipMode.None, 0m);
            List<Attendee> attendees = new List<Attendee>
            {
                new Attendee("Alice", 2, true),
                new Attendee("Bob", 1, true)
            };

            //act
            List<Share> result = splitter.CalculateShares(bill, attendees, RoundingMode.Bankers);

            //assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(60.00m, result[0].Amount);
            Assert.AreEqual(30.00m, result[1].Amount);
        }
    }

































}


