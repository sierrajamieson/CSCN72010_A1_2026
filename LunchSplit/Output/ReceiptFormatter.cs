using LunchSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchSplit.Output
{
    public class ReceiptFormatter
    {
        public string Format(Bill bill, List<Attendee> attendees, List<Share> roundedShares)
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Student: YOUR NAME HERE");
            sb.AppendLine("Date: " + DateTime.Now.ToString("yyyy-MM-dd"));
            sb.AppendLine("Created: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine();

            foreach (Share share in roundedShares)
            {
                sb.AppendLine(share.Name + ": " + share.Amount.ToString("F2"));
            }

            return sb.ToString();
        }

    }
}
