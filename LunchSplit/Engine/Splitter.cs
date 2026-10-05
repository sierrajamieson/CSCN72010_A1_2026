using LunchSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace LunchSplit.Engine
{
    public class Splitter
    {
        //ComputeTip (decimal subtotal, TipMode mode, decimal tipInput)
        public decimal ComputeTip(decimal subtotal, TipMode mode, decimal tipInput)
        {
            if (subtotal < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(subtotal));
            }
            if (mode == TipMode.Fixed && tipInput < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tipInput));
            }

            if (mode == TipMode.None)
            {
                return 0.00m;
            }

            if (mode == TipMode.Percent)
            {
                return subtotal * tipInput / 100m;
            }

            return 0.00m;
        }

        public List<Share> CalculateShares(Bill bill, List<Attendee> attendees, RoundingMode roundingMode)
        {
            return new List<Share>();
        }

    }
        
}
