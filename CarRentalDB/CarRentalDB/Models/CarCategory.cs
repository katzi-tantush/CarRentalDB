using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CarRentalDB.Models
{
    public partial class CarCategory
    {
        [Key]
        public int ID { get; set; }
        public string Model { get; set; }
        public bool Automatic { get; set; }
    }
}
