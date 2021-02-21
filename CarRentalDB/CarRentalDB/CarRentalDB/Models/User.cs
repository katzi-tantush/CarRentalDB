using CarRentalDB.Helpers;
using CarRentalDB.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class User : IDataModel
    {
        [Key][DigitCount(9, "User ID")]
        public int ID { get; set; }
        public string Role { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }

        public int ImageID { get; set; }
    }
}
