using LunchSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchSplit.Helper
{
    public class BillValidator
    {
        public ValidationResult Validate(Bill bill, List<Attendee> attendees)
        {
            //Check for null bill
            if (bill == null)
                return ValidationResult.Fail("Bill cannot be null.");

            //Block negative subtotals
            if (bill.Subtotal < 0)
                return ValidationResult.Fail("Subtotal cannot be negative.");

            //Block negative tax
            if (bill.Tax < 0)
                return ValidationResult.Fail("Tax cannot be negative.");

            //Block null or empty attendees list
            if (attendees == null || attendees.Count == 0)
                return ValidationResult.Fail("Attendees list cannot be null or empty.");

            //Block negative weights
            if (attendees.Any(a => a.Weight < 0))
                return ValidationResult.Fail("Attendee weights cannot be negative.");

            return ValidationResult.Success();

        }

    }


    public class ValidationResult
    {
        public bool IsValid {get;}
        public string ErrorMessage {get;}

        public ValidationResult(bool isValid, string errorMessage = "")
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        // Convenience helpers
        public static ValidationResult Success()
        {
            return new ValidationResult(true);
        }

        public static ValidationResult Fail(string message)
        {
            return new ValidationResult(false, message);
        }

    }
}