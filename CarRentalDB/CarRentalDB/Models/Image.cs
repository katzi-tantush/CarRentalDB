using CarRentalDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class Image: IDataModel
    {
        [Key]
        public int ID { get; set; }
        public byte[] File { get; set; }
    }
}
