using CarRentalDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class Branch : IDataModel
    {
        [Key]
        public int ID { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public int LocationID { get; set; }
    }
}
