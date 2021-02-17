using CarRentalDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CarRentalDB.Models
{
    public partial class CarCategory : IDataModel
    {
        [Key]
        public int ID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public bool Automatic { get; set; }
        public DateTime ProductionDate { get; set; }
        public int DailyCost { get; set; }
        public int OverdueDailyCost { get; set; }
    }
}
