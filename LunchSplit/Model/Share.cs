using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchSplit.Model
{
    public class Share
    {

        public String Name;
        public decimal Amount;
        

        public Share(String Name, decimal Amount)
        {
            this.Name = Name;
            this.Amount = Amount;

        }



    }
}
