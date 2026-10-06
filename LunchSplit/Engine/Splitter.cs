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
            List<Attendee> included = new List<Attendee>();
            int totalWeight = 0;

            foreach (Attendee attendee in attendees)
            {
                if (attendee.Included)
                {
                    included.Add(attendee);
                    totalWeight += attendee.Weight;
                }
            }

            decimal tip = ComputeTip(bill.Subtotal, bill.tipMode, bill.TipInput);
            decimal grandTotal = bill.Subtotal + bill.Tax + tip;

            List<Share> rawShares = new List<Share>();
            foreach (Attendee attendee in included)
            {
                decimal amount = grandTotal * attendee.Weight / totalWeight;
                rawShares.Add(new Share(attendee.Name, amount));
            }
            Rounder rounder = new Rounder();
            List<Share> rounded = rounder.RoundShares(rawShares, roundingMode);

            List<Share> result = new List<Share>();
            int roundedIndex = 0;

            foreach (Attendee attendee in attendees)
            {
                if (attendee.Included)
                {
                    result.Add(rounded[roundedIndex]);
                    roundedIndex++;
                }
                else
                {
                    result.Add(new Share(attendee.Name, 0.00m));
                }
            }

            return result;
        }

    }
        
}
