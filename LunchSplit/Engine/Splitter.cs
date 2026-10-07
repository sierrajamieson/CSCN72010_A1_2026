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
            //check if the tip mode is Fixed and the dollar amount entered is below zero
            if (mode == TipMode.Fixed && tipInput < 0)
            { // return error message
                throw new ArgumentOutOfRangeException(nameof(tipInput));
            }
            //checks if user chose tipmode none
            if (mode == TipMode.None)
            {
                return 0.00m;
            }
            //checks if user chose tipmode percent
            if (mode == TipMode.Percent)
            {
                return subtotal * tipInput / 100m;
            }
            //checks if user chose tipmode fixed
            if (mode == TipMode.Fixed)
            {
                return tipInput;
            }

            return 0.00m;
        }


        public List<Share> CalculateShares(Bill bill, List<Attendee> attendees, RoundingMode roundingMode)
        {
            //create an empty list that will hold only the attendees who are part of the split
            List<Attendee> included = new List<Attendee>();
            int totalWeight = 0;

            //loop through every attendee
            foreach (Attendee attendee in attendees)
            {
                //checks if attendee is included
                if (attendee.Included)
                {
                    included.Add(attendee);
                    //add attendees weight to total
                    totalWeight += attendee.Weight;
                }
            }

            //if no attendee is included or theres no weight
            if (included.Count == 0 || totalWeight <= 0)
            {
                //return error message
                throw new InvalidOperationException("At least one included attendee is required.");
            }

            decimal tip = ComputeTip(bill.Subtotal, bill.tipMode, bill.TipInput);
            //adding all the money together
            decimal grandTotal = bill.Subtotal + bill.Tax + tip;

            //create an empty list that will hold each attendees unrounded share
            List<Share> rawShares = new List<Share>();
            foreach (Attendee attendee in included)
            {
                decimal amount = grandTotal * attendee.Weight / totalWeight;
                rawShares.Add(new Share(attendee.Name, amount));
            }
            //create a rounder object to use its method
            Rounder rounder = new Rounder();
            //create a share with an attendees name and amount, and add it to the list
            List<Share> rounded = rounder.RoundShares(rawShares, roundingMode);

            //create a new empty list that will have only one entry for each attendee
            List<Share> result = new List<Share>();
            int roundedIndex = 0;

            //loop through the included attendees
            foreach (Attendee attendee in attendees)
            {
                //check if attendee is part of split
                if (attendee.Included)
                {
                    //take the next rounded share from this list and add it to the final list
                    result.Add(rounded[roundedIndex]);
                    //move to the next rounded share
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
