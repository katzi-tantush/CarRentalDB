using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Utilities.Validators
{
    // validates the property as higher than the specified minimum
    public class Min:ValidationAttribute
    {
        public int MinimumValue { get; }

        public Min(int min)
        {
            MinimumValue = min;
        }
        public override bool IsValid(object value)
        {
            return (int)value > MinimumValue;
        }
    }
}
