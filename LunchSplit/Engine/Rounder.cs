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

            return rawShares;
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

    }
}
