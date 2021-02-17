using CarRentalDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class Location : IDataModel
    {
        [Key]
        public int ID { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }
}
