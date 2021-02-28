using CarRentalDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class RentedCar : IDataModel
    {
        //[Key]
        [Key]
        public int ID { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public DateTime? CarReturnDate { get; set; }

        public int UserID { get; set; }
    }
}
