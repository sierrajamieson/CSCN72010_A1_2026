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


    }
}
