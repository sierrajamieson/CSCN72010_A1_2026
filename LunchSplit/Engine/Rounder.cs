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
            List<Share> result = new List<Share>();

            if (mode == RoundingMode.None)
            {
                foreach (Share share in rawShares)
                {
                    result.Add(new Share(share.Name, share.Amount));
                }

                return result;
            }
            if (rawShares.Count == 0)
            {
                return result;
            }
            decimal rawTotal = 0m;
            foreach (Share share in rawShares)
            {
                rawTotal += share.Amount;
            }
            decimal target = Math.Round(rawTotal, 2, MidpointRounding.AwayFromZero);

            foreach (Share share in rawShares)
            {
                decimal rounded;

                if (mode == RoundingMode.Bankers)
                {
                    rounded = Math.Round(share.Amount, 2, MidpointRounding.ToEven);
                }
                else if (mode == RoundingMode.Ceiling)
                {
                    rounded = Math.Ceiling(share.Amount * 100m) / 100m;
                }
                else
                {
                    rounded = Math.Floor(share.Amount * 100m) / 100m;
                }


                result.Add(new Share(share.Name, rounded));
            }

            if (mode == RoundingMode.Bankers)
            {
                decimal roundedTotal = 0m;
                foreach (Share share in result)
                {
                    roundedTotal += share.Amount;
                }

                int pennies = (int)Math.Round((target - roundedTotal) * 100m);
                decimal adjustment = pennies > 0 ? 0.01m : -0.01m;

                for (int i = 0; i < Math.Abs(pennies); i++)
                {
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
