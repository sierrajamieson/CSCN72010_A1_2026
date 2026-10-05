using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchSplit.Model
{
    public class Share
    {

        public string Name;
        public decimal Amount;
        

        public Share(string Name, decimal Amount)
        {
            this.Name = Name;
            this.Amount = Amount;

        }



    }
}
