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
        public string Name;
        public int Weight;
        public bool Included;

        public Attendee(string Name, int Weight, bool Included)
        {
            this.Name = Name;
            this.Weight = Weight;
            this.Included = Included;

        }

    }
}
