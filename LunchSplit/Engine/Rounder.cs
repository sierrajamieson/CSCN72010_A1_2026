using LunchSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchSplit.Engine
{
    public class Rounder
    {

        public List<Share> RoundShares(List<Share> rawShares, RoundingMode mode)
        {
            //create an empty list
            List<Share> result = new List<Share>();

            //checks if the mode is none
            if (mode == RoundingMode.None)
            {
                //loops through every raw share
                foreach (Share share in rawShares)
                {
                    //copy the share into the result list
                    result.Add(new Share(share.Name, share.Amount));
                }

                return result;
            }
            //checks if there are no shares
            if (rawShares.Count == 0)
            {
                return result;
            }

            //creates a counter for the raw shares 
            decimal rawTotal = 0m;
            //loop through each raw share
            foreach (Share share in rawShares)
            {
                //add this shares amount to the total
                rawTotal += share.Amount;
            }
            //round the total to two decimal places
            decimal target = Math.Round(rawTotal, 2, MidpointRounding.AwayFromZero);

            //loop through every raw share to round them
            foreach (Share share in rawShares)
            {
                decimal rounded;

                //checks if rounding mode is in bankers mode
                if (mode == RoundingMode.Bankers)
                {
                    //round up to two decimal places
                    rounded = Math.Round(share.Amount, 2, MidpointRounding.ToEven);
                }
                //otherwise check if the mode is ceiling
                else if (mode == RoundingMode.Ceiling)
                {
                    rounded = Math.Ceiling(share.Amount * 100m) / 100m;
                }
                else
                {
                    rounded = Math.Floor(share.Amount * 100m) / 100m;
                }

                //create new share with the rounded value and add it to the amount
                result.Add(new Share(share.Name, rounded));
            }

            if (mode == RoundingMode.Bankers)
            {
                //create a counter of the total amount of shares
                decimal roundedTotal = 0m;
                //loop through each share in the result list
                foreach (Share share in result)
                {
                    roundedTotal += share.Amount;
                }

                int pennies = (int)Math.Round((target - roundedTotal) * 100m);
                //decides which direction to asjust +0.01 if cents are missing and -0.01 if there are too many
                decimal adjustment = pennies > 0 ? 0.01m : -0.01m;

                for (int i = 0; i < Math.Abs(pennies); i++)
                {
                    //goes back to the first person if there are more pennies than people
                    result[i % result.Count].Amount += adjustment;
                }
            }

            return result;
        }

    }

    public class RoundingMode
    {
        public string mode;

        public RoundingMode(string mode)
        {
            this.mode = mode;
        }
        public static readonly RoundingMode None = new RoundingMode("None");
        public static readonly RoundingMode Bankers = new RoundingMode("Bankers");
        public static readonly RoundingMode Ceiling = new RoundingMode("Ceiling");
        public static readonly RoundingMode Floor = new RoundingMode("Floor");

    }
}
