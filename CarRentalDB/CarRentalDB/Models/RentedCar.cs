using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class RentedCar
    {
        // TODO: this model should not have an ID field - 
        // how to define Car as ID? is it ok to make this a 'keyless entity type'?
        [Key]
        public int CarID { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public DateTime CarReturnDate { get; set; }
        public User User { get; set; }
    }
}
