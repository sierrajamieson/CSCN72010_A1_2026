using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LunchSplit.Model
{
    public class Bill
    {
        public decimal Subtotal;
        public decimal Tax;
        public TipMode tipMode;
        public decimal TipInput;


        public Bill(decimal Subtotal, decimal Tax, TipMode tipMode, decimal TipInput)
        {
            this.Subtotal = Subtotal;
            this.Tax = Tax;
            this.TipInput = TipInput;
            this.tipMode = tipMode;

        }
    }
    
    public class TipMode
    {
        
        public string mode;

        public TipMode(string mode)
        {
            this.mode = mode;
        }

        public static readonly TipMode None = new TipMode("None");
        public static readonly TipMode Percent = new TipMode("Percent");
        public static readonly TipMode Fixed = new TipMode("Fixed");

        


    }
}
