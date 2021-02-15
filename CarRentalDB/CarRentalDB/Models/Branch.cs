using CarRentalDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class Branch : IDataModel
    {
        [Key]
        public int ID { get; set; }
        public string Address { get; set; }
        // TODO: add location field
        public string Name { get; set; }
    }
}
