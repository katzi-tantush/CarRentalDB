using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Validators
{
    // validates that the incoming property is of the specified digit count
    public class DigitCount : ValidationAttribute
    {
        int digitCount { get; }

        public DigitCount(int count, string propertyField)
        {
            digitCount = count;
            ErrorMessage = $"{propertyField} must be exactly {digitCount} digits long";
        }

        public override bool IsValid(object value)
        {
            int idLength = value.ToString().Length;

            return idLength == digitCount;
        }
    }
}
