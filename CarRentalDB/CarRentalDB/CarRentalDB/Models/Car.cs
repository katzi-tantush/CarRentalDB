using CarRentalDB.Helpers;
using CarRentalDB.Utilities.Validators;
using CarRentalDB.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class Car : IDataModel
    {
        [Key][DigitCount(8, "Car ID")]
        public int ID { get; set; }
        [Min(0)]
        public int KillometerCount { get; set; }
        public bool AvailableForRent { get; set; }

        public int CarCategoryID { get; set; }
        public int ImageID { get; set; }
        public int BranchID { get; set; }
    }
}
