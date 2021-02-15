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
        public CarCategory CarCategory { get; set; }
        public int KillometerCount { get; set; }
        public Image Image { get; set; }
        public bool RentReady { get; set; }
        public bool AvailableForRent { get; set; }
        public int LicensePlateNumber { get; set; }
        public Branch Branch { get; set; }
    }
}
