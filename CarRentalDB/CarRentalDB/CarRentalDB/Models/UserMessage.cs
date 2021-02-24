using CarRentalDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class UserMessage : IDataModel
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Processed { get; set; }
    }
}
