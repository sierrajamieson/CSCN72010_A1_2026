using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LunchSplit.Model
{
    public class Attendee
    {
        public String Name;
        public int weight;
        public bool Included;

        public Attendee(String Name, int weight, bool Included)
        {
            this.Name = Name;
            this.weight = weight;
            this.Included = Included;

        }

    }
}
