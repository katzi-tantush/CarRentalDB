using CarRentalDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class Car : IDataModel
    {
        [Key]
        public int ID { get; set; }
        public int KillometerCount { get; set; }
        public bool RentReady { get; set; }
        public bool AvailableForRent { get; set; }

        public int CarCategoryID { get; set; }
        public int ImageID { get; set; }
        public int BranchID { get; set; }
    }
}
